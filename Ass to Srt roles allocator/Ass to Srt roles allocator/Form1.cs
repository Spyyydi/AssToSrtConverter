using Ass_to_Srt_roles_allocator.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;

namespace Ass_to_Srt_roles_allocator
{
    public partial class Form1 : Form
    {
        string path = "";
        const string EMPTY_ACTOR = "EMPTY ACTOR";
        const string RIGHT_ARROW = "→";
        readonly char[] ACTOR_SEPARATORS = { '/', '&', '|', '\\' };
        const char GENERAL_SEPARATOR = ';';
        List<string> subtitles;

        public Form1()
        {
            InitializeComponent();
            subtitles = new List<string>();
            cmbDubers.Items.Clear();
            cmbDubers.Items.AddRange(Settings.Default.Dubers.Split(';'));
            lblAllocatedActors.ContextMenuStrip = contextMenuStrip;
        }

        #region Additional methods
        private bool ImportSubtitles(string filePath, string fileName)
        {
            try
            {
                string[] subs = File.ReadAllLines(Path.Combine(filePath, fileName));
                if (!(subs.Any(s => s.StartsWith("Dialogue")) &&
                    subs.Any(s => s.Substring(AssFormat.GetSpecificFormatIndex(s, AssFormat.Text)) != "")))
                {
                    return false;
                }

                subtitles.Clear();
                foreach (string line in subs)
                {
                    string lineToCompare = "";
                    bool isDialogue = false;
                    if (line.StartsWith("Dialogue: 0"))
                    {
                        lineToCompare = line;
                        isDialogue = true;
                    }
                    else if (line.StartsWith("Dialogue"))
                    {
                        int startIndex = line.IndexOf(",");
                        lineToCompare = "Dialogue: 0" + line.Substring(startIndex);
                        isDialogue = true;
                    }

                    if (isDialogue && !subtitles.Contains(lineToCompare))
                    {
                        if (!IsVectorDrawing(lineToCompare))
                        {
                            subtitles.Add(lineToCompare);
                        }
                    }
                }
                //sort subtitles by time
                List<string> subtitlesToCompare = new List<string>(subtitles);
                subtitles.Sort(new AssFormat.SubtitleComparer(subtitlesToCompare));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return true;
        }

        private bool IsVectorDrawing(string line)
        {
            line = ExtractDialogue(line, true);
            //simple pattern of vector drawing
            string pattern = @"m\s*-?\d+(\.\d+)?\s+-?\d+(\.\d+)?(\s*[nlb]\s*-?\d+(\.\d+)?\s+-?\d+(\.\d+)?)*";

            return Regex.IsMatch(line, pattern);
        }

        private void ImportSubFile(string subFilePath)
        {
            int fileNameStartIndex = subFilePath.LastIndexOf('\\') + 1;
            string filePath = subFilePath.Substring(0, fileNameStartIndex);
            string fileName = subFilePath.Substring(fileNameStartIndex);
            path = Path.Combine(filePath, fileName.Substring(0, fileName.LastIndexOf(".")) + ".srt");


            if (!ImportSubtitles(filePath, fileName))
            {
                MessageBox.Show("There are no valid subtitles to convert");
                return;
            }

            lblFilePath.Text = "File name: " + fileName;
            toolTipFileName.SetToolTip(lblFilePath, fileName);


            string emptyActors = LoadActors();
            toolTipActorsLoaded.SetToolTip(lblLoadStatus, "Empty actors: " + emptyActors);
            toolTipAllocatedActors.SetToolTip(lblAllocatedActors, "Right click to see not allocated actors\nyou can select any by pressing on it");
            if (cmbActors.Items.Count < 1)
            {
                lblLoadStatus.ForeColor = Color.Red;
            }
            else
            {
                lblLoadStatus.ForeColor = Color.Green;
            }

            //change number of actors that can be allocated
            lblAllocatedActors.Text = lblAllocatedActors.Text.Substring(0, lblAllocatedActors.Text.IndexOf('\n') + 1)
                                    + "0/" + cmbActors.Items.Count.ToString();


            lstToChange.Items.Clear();
            ChangeAllocatedActorsValue();

            cmbDubers.ResetText();
            changeBtnToAlloc();
            changeBtnToAddRmv();

            changeBtnRemoveLog();
            changeBtnExport();

            changeChkSeparateActors();

            lblConvertionStatus.ForeColor = Color.Red;
            btnConvert.Enabled = true;
        }

        private void ChangeAllocatedActorsValue()
        {
            //change number of actors allocated
            string defaultLblTxt = lblAllocatedActors.Text;
            string loadedActorsNum = defaultLblTxt.Substring(defaultLblTxt.IndexOf('/') + 1);

            int allocActorsNumIndex = defaultLblTxt.LastIndexOf('\n') + 1;
            defaultLblTxt = defaultLblTxt.Substring(0, allocActorsNumIndex);

            lblAllocatedActors.Text = defaultLblTxt + lstToChange.Items.Count.ToString() + "/" + loadedActorsNum;


            List<string> allocatedActors = new List<string>(lstToChange.Items.Cast<string>().ToList());
            List<string> tempList;

            //separate actors from dubers if any
            if (allocatedActors.Any(s => s.Contains(RIGHT_ARROW)))
            {
                tempList = new List<string>(allocatedActors);
                allocatedActors.Clear();

                foreach (string item in tempList)
                {
                    string actor = item.Substring(0, item.IndexOf(RIGHT_ARROW)).Trim();

                    if (!allocatedActors.Contains(actor))
                        allocatedActors.Add(actor);
                }
                tempList.Clear();
            }

            //change contextMenuStrip items
            List<string> allActors = new List<string>(cmbActors.Items.Cast<string>().ToList());
            bool isEmptyActor = false;

            tempList = new List<string>(allocatedActors);
            allocatedActors.Clear();

            //add not allocated actors to contextMenuStrip
            foreach (string actor in allActors)
            {
                if (!tempList.Contains(actor))
                {
                    if (actor != EMPTY_ACTOR)
                    {
                        allocatedActors.Add(actor);
                    }
                    else isEmptyActor = true;
                }
            }
            tempList.Clear();


            allocatedActors.Sort();
            if (isEmptyActor) allocatedActors.Add(EMPTY_ACTOR);

            ToolStripItem[] menuItems = new ToolStripItem[allocatedActors.Count];
            for (int i = 0; i < allocatedActors.Count; ++i)
            {
                menuItems[i] = new ToolStripMenuItem(allocatedActors[i]);
                menuItems[i].Click += contextMenuStrip_Click;
            }

            contextMenuStrip.Items.Clear();
            contextMenuStrip.Items.AddRange(menuItems);
        }

        private string LoadActors()
        {
            List<string> actors = new List<string>();
            //in order to properly reset combobox
            int numOfEmptyActors = 0;
            int numOfTotalActors = 0;
            bool atLeastOneEmptyActor = false;

            for (int i = 0; i < subtitles.Count; ++i)
            {
                string line = subtitles[i];

                //extract actor 
                string actor = ExtractActor(line);

                //split actors if multiple for one dialogue
                if (actor.Contains(GENERAL_SEPARATOR) || actor.Any(c => ACTOR_SEPARATORS.Contains(c)))
                {
                    string[] splitedActors = SplitActors(actor);
                    foreach (string splitedActor in splitedActors)
                    {
                        if (!actors.Contains(splitedActor) && splitedActor != "")
                        {
                            actors.Add(splitedActor);
                        }
                    }
                }
                else
                {
                    if (actor == "")
                    {
                        ++numOfEmptyActors;
                        int actorIndex = AssFormat.GetSpecificFormatIndex(line, AssFormat.Actor);
                        int afterActorIndex = AssFormat.GetSpecificFormatIndex(line, AssFormat.MarginL) - 1;

                        string modifiedLine = subtitles[i].Substring(0, actorIndex);
                        modifiedLine += EMPTY_ACTOR;
                        modifiedLine += subtitles[i].Substring(afterActorIndex);
                        subtitles[i] = modifiedLine;

                        atLeastOneEmptyActor = true;
                    }
                    else if (!actors.Contains(actor))
                    {
                        actors.Add(actor);
                    }
                }

                ++numOfTotalActors;
            }

            cmbActors.Items.Clear();
            if (actors.Count != 0)
                cmbActors.Items.AddRange(actors.OrderBy(x => x).ToArray());
            actors.Clear();

            if (atLeastOneEmptyActor)
            {
                cmbActors.Items.Add(EMPTY_ACTOR);
            }

            return numOfEmptyActors.ToString() + "/" + numOfTotalActors.ToString();
        }

        private string[] SplitActors(string actor)
        {
            if (actor.Any(c => ACTOR_SEPARATORS.Contains(c)))
                foreach (char separator in ACTOR_SEPARATORS)
                {
                    if (actor.Contains(separator))
                    {
                        actor = actor.Replace(separator, GENERAL_SEPARATOR);
                    }
                }

            string[] splitedActors = actor.Split(GENERAL_SEPARATOR);
            int splitLen = splitedActors.Length;

            for (int i = 0; i < splitLen; ++i)
            {
                splitedActors[i] = splitedActors[i].Trim();
            }

            return splitedActors;
        }

        private string ExtractActor(string line)
        {
            int actorIndex = AssFormat.GetSpecificFormatIndex(line, AssFormat.Actor);
            int actorLength = line.IndexOf(',', actorIndex) - actorIndex;
            return line.Substring(actorIndex, actorLength).Trim();
        }

        private string ExtractTime(string line)
        {
            //get start time
            int timeStartIndex = AssFormat.GetSpecificFormatIndex(line, AssFormat.Start);
            int timeStartLength = line.IndexOf(",", timeStartIndex) - timeStartIndex;
            string timeStart = line.Substring(timeStartIndex, timeStartLength).Replace('.', ',');

            //get end time
            int timeEndIndex = AssFormat.GetSpecificFormatIndex(line, AssFormat.End);
            int timeEndLength = line.IndexOf(",", timeEndIndex) - timeEndIndex;
            string timeEnd = line.Substring(timeEndIndex, timeEndLength).Replace('.', ',');

            // add hours leading zeros
            if (timeStart.IndexOf(':') == 1)
                timeStart = "0" + timeStart;
            if (timeEnd.IndexOf(':') == 1)
                timeEnd = "0" + timeEnd;

            // add miliseconds leading zeros
            string miliseconds;

            if (timeStart.Length - timeStart.IndexOf(',') - 1 == 2)
            {
                miliseconds = timeStart.Substring(timeStart.IndexOf(',') + 1) + "0";
                timeStart = timeStart.Substring(0, timeStart.Length - 2) + miliseconds;
            }

            if (timeEnd.Length - timeEnd.IndexOf(',') - 1 == 2)
            {
                miliseconds = timeEnd.Substring(timeEnd.IndexOf(',') + 1) + "0";
                timeEnd = timeEnd.Substring(0, timeEnd.Length - 2) + miliseconds;
            }
            return timeStart + " --> " + timeEnd;
        }

        private string ExtractDialogue(string line, bool removeAssFormating)
        {
            //extract dialogue
            string dialogue = line.Substring(AssFormat.GetSpecificFormatIndex(line, AssFormat.Text));

            //remove ass text formating
            if (removeAssFormating)
            {
                dialogue = Regex.Replace(dialogue, "{.*?}", string.Empty);
            }

            //get rid of chars that starts with backslash ex. \N \h
            /* Regular expression explanation
             * (?<!{[^{}]*) : Makes sure that there are no opening curly braces behind chars that starts with backslash
             * \\. : Matches any occurance of character that starts with backslash
             * (?![^{}]*}) : Makes sure that there are no closing curly braces after chars that starts with backslash
             */
            dialogue = Regex.Replace(dialogue, @"(?<!{[^{}]*)\\.(?![^{}]*})", " ");

            //trim multiple spaces
            dialogue = Regex.Replace(dialogue, @"\s+", " ");

            return dialogue.Trim();
        }

        private string FindDuberForActor(string actor)
        {
            int lstLen = lstToChange.Items.Count;
            string duber = "";

            //find duber according to requested actor
            for (int i = 0; i < lstLen; ++i)
            {
                string currentItem = lstToChange.Items[i].ToString();
                if (currentItem.StartsWith(actor, StringComparison.OrdinalIgnoreCase) && currentItem.Contains(RIGHT_ARROW))
                {
                    duber = currentItem.Substring(actor.Length + 3);
                    break;
                }
            }

            return duber;
        }

        private string ConvertToChangeListForExport()
        {
            //convert list box to string separated with colon
            string dubersToExport = "";
            int lstLen = lstToChange.Items.Count;
            int count = 1;
            foreach (string item in lstToChange.Items)
            {
                string actor = item.Substring(0, item.IndexOf(RIGHT_ARROW)).Trim();
                string duber = item.Substring(item.IndexOf(RIGHT_ARROW) + 1).Trim();
                dubersToExport += actor + ":" + duber;

                if (count < lstLen)
                {
                    dubersToExport += "\n";
                }
                count++;
            }

            return dubersToExport;
        }

        private string ConvertImportedFileToList(string filePath, string fileName)
        {
            //try to add actors from file to to change list
            string report = "";
            int importedDubersCount = 0;
            int dubersCount = 0;
            try
            {
                string[] dubers = File.ReadAllLines(Path.Combine(filePath, fileName));
                foreach (string line in dubers)
                {
                    if (line.Count(c => c == ':') != 1) continue;
                    dubersCount++;

                    //separate actor and duber
                    string actor = line.Substring(0, line.IndexOf(":")).Trim();
                    string duber = line.Substring(line.IndexOf(":") + 1).Trim();

                    //find actor in combobox if found add to listbox if not add to report
                    if (cmbActors.Items.Contains(actor))
                    {
                        if (dubersCount == 1)
                        {
                            lstToChange.Items.Clear();
                        }
                        lstToChange.Items.Add(actor + " " + RIGHT_ARROW + " " + duber);

                        importedDubersCount++;
                    }
                    else
                    {
                        report += actor + " " + RIGHT_ARROW + " " + duber + "\n";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (importedDubersCount == 0)
            {
                report = "Error";
            }
            else if (importedDubersCount != dubersCount)
            {
                report = $"{importedDubersCount} out of {dubersCount} imported\n\nNot imported:\n" + report;
            }

            return report;
        }

        private string ConvertAssToSrt(bool removeAssFormating, bool allocateActors, string separateActors)
        {
            string srtSub = "";

            //convert from ass to srt
            int lineNum = 1;
            int subLen = subtitles.Count;
            bool isActorsAllocated = lstToChange.Items.Count > 0;

            if (subLen > 0)
                foreach (string line in subtitles)
                {
                    if (separateActors != "")
                    {
                        string actor = ExtractActor(line);
                        string[] actors = SplitActors(actor);

                        string[] separatedActor = SplitActors(separateActors);

                        if (separatedActor.Intersect(actors).Any())
                        {
                            if (lineNum != 1)
                            {
                                srtSub += "\n\n";
                            }

                            srtSub += lineNum++.ToString() + "\n";
                            srtSub += ExtractTime(line) + "\n";

                            if (allocateActors)
                            {
                                if (actor.Contains(";"))
                                    actor = actor.Replace(";", ",");
                                srtSub += "[" + actor + "] ";
                            }

                            srtSub += ExtractDialogue(line, removeAssFormating);
                        }
                    }
                    else
                    {
                        srtSub += lineNum.ToString() + "\n";
                        srtSub += ExtractTime(line) + "\n";

                        if (allocateActors)
                        {
                            if (isActorsAllocated)
                            {
                                //Extract actor
                                string actor = ExtractActor(line);
                                //if multiple actors for the subtitle line
                                if (actor.Contains(';'))
                                {
                                    //split actors and find duber for each actor (if duber not found then put actor instead)
                                    string[] actors = SplitActors(actor);
                                    string dubers = "";
                                    bool atLeastOneDuberWritten = false;

                                    foreach (string s in actors)
                                    {
                                        string duber = FindDuberForActor(s);
                                        if (duber == "") dubers += s;
                                        else
                                        {
                                            dubers += duber;
                                            atLeastOneDuberWritten = true;
                                        }
                                        dubers += ", ";
                                    }

                                    if (atLeastOneDuberWritten)
                                        srtSub += "(" + dubers.Substring(0, dubers.LastIndexOf(',')) + ") ";
                                    else
                                        srtSub += "[" + dubers.Substring(0, dubers.LastIndexOf(',')) + "] ";
                                }
                                else
                                {
                                    string duber = FindDuberForActor(actor);
                                    if (duber == "")
                                    {
                                        //duber not found
                                        //(remove comment below if you want actors to be placed anyways whether there are alocated dubers or not)
                                        srtSub += "[" + actor + "] ";
                                    }
                                    else if (duber == "-")
                                    {
                                        //special case for Captions
                                        srtSub += duber + " ";
                                    }
                                    else
                                    {
                                        srtSub += "(" + duber + ") ";
                                    }
                                }
                            }
                        }


                        srtSub += ExtractDialogue(line, removeAssFormating);

                        if (lineNum != subLen)
                        {
                            srtSub += "\n\n";
                        }
                        ++lineNum;
                    }
                }

            return srtSub;
        }
        #endregion

        #region Button click events
        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            openFileDialog.Title = "Choose sub file";
            openFileDialog.FileName = "";
            openFileDialog.Filter = "Subtitles|*.ass";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ImportSubFile(openFileDialog.FileName);
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            const bool removeAssFormating = true;
            bool allocateActors = true;
            const string separateActors = "";
            Dictionary<string, string> separateSrtSubs = new Dictionary<string, string>();

            if (chkSeparateActors.Checked)
            {
                if (lstToChange.Items.Cast<string>().Any(s => s.Contains(RIGHT_ARROW)))
                {
                    if (!chkActorsPerLine.Checked)
                    {
                        allocateActors = false;
                    }

                    // identify every actor per duber
                    foreach (string actorNduber in lstToChange.Items)
                    {
                        string actor = actorNduber.Substring(0, actorNduber.IndexOf(RIGHT_ARROW) - 1);
                        string duber = actorNduber.Substring(actorNduber.IndexOf(RIGHT_ARROW) + 2);
                        if (separateSrtSubs.ContainsKey(duber))
                        {
                            separateSrtSubs[duber] = separateSrtSubs[duber] + ";" + actor;
                        }
                        else
                        {
                            separateSrtSubs.Add(duber, actor);
                        }
                    }

                    // get srt per duber
                    List<string> dubers = separateSrtSubs.Keys.ToList();
                    foreach (string duber in dubers)
                    {
                        string actors = separateSrtSubs[duber];
                        separateSrtSubs[duber] = ConvertAssToSrt(removeAssFormating, allocateActors, actors);
                    }
                }
                else
                {
                    allocateActors = false;
                    foreach (string actor in lstToChange.Items)
                    {
                        separateSrtSubs.Add(actor, ConvertAssToSrt(removeAssFormating, allocateActors, actor));
                    }
                }
            }

            // it doesn't matter state of allocateActors if previous "if" is entered
            string srtSub = ConvertAssToSrt(removeAssFormating, allocateActors, separateActors);

            if (srtSub == "")
            {
                MessageBox.Show("There are no subtitles to convert!");
            }
            else
            {
                try
                {
                    //redo path
                    int fileNameStartIndex = path.LastIndexOf('\\');
                    string filePath = path.Substring(0, fileNameStartIndex);

                    int fileNameLen = path.LastIndexOf(".") - fileNameStartIndex - 1;
                    string fileName = path.Substring(fileNameStartIndex + 1, fileNameLen);

                    //if separateActors checked create dir and save srts for actors in dir else normal srt save
                    if (chkSeparateActors.Checked)
                    {
                        if (chkActorsPerLine.Checked)
                        {
                            filePath = Path.Combine(filePath, "[Dubers with Actors] " + fileName);
                        }
                        else if (lstToChange.Items.Cast<string>().Any(s => s.Contains(RIGHT_ARROW)))
                        {
                            filePath = Path.Combine(filePath, "[Dubers] " + fileName);
                        }
                        else
                        {
                            filePath = Path.Combine(filePath, "[Actors] " + fileName);
                        }

                        Directory.CreateDirectory(filePath);

                        foreach (var subForActor in separateSrtSubs)
                        {
                            string newPath = Path.Combine(filePath, subForActor.Key + ".srt");
                            File.WriteAllText(newPath, subForActor.Value);
                        }
                    }
                    else
                    {
                        if (lstToChange.Items.Count < 1)
                        {
                            filePath = path;
                        }
                        else if (lstToChange.Items.Cast<string>().Any(s => s.Contains(RIGHT_ARROW)))
                        {
                            filePath = Path.Combine(filePath, "[Dubers] " + fileName + ".srt");
                        }
                        else
                        {
                            filePath = Path.Combine(filePath, "[Actors] " + fileName + ".srt");
                        }

                        File.WriteAllText(filePath, srtSub);
                    }

                    lblConvertionStatus.ForeColor = Color.Green;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnToAlloc_Click(object sender, EventArgs e)
        {
            //remove only actors if any
            if (lstToChange.Items.Count > 0)
                if (lstToChange.Items.Cast<string>().Any(s => !s.Contains(RIGHT_ARROW)))
                {
                    List<string> items = lstToChange.Items.OfType<string>().ToList();
                    foreach (string toRemove in items)
                    {
                        if (!toRemove.Contains(RIGHT_ARROW))
                            lstToChange.Items.Remove(toRemove);
                    }
                }

            lstToChange.Items.Add(cmbActors.Text + " " + RIGHT_ARROW + " " + cmbDubers.Text.Trim());

            ChangeAllocatedActorsValue();

            changeBtnToAlloc();
            changeBtnExport();
            changeChkSeparateActors();

            changeBtnRemoveLog();
            lblConvertionStatus.ForeColor = Color.Red;
        }

        private void btnAddDuber_Click(object sender, EventArgs e)
        {
            if (cmbDubers.Items.Count == 1)
            {
                if (cmbDubers.Items[0].ToString() == "") cmbDubers.Items.Clear();
            }
            cmbDubers.Items.Add(cmbDubers.Text);
            changeBtnToAddRmv();
            changeBtnSaveDeleteDubers();
        }

        private void btnRemoveDuber_Click(object sender, EventArgs e)
        {
            if (cmbDubers.Items.Count == 1)
            {
                cmbDubers.Items.Clear();
                cmbDubers.Items.Add("");
            }
            else
            {
                cmbDubers.Items.Remove(cmbDubers.Text);
            }

            cmbDubers.ResetText();
            changeBtnToAddRmv();
            changeBtnSaveDeleteDubers();
        }

        private void btnSaveDeleteDubers_Click(object sender, EventArgs e)
        {
            //update settings string with current duberlist
            Settings.Default.Dubers = string.Join(";", cmbDubers.Items.OfType<String>()
                                                                      .Select(item => item.ToString())
                                                                      .Where(s => !string.IsNullOrEmpty(s)));
            Settings.Default.Save();

            changeBtnSaveDeleteDubers();
        }

        private void btnRemoveLog_Click(object sender, EventArgs e)
        {
            if (btnRemoveLog.Text == "Remove")
            {
                List<string> selectedItems = lstToChange.SelectedItems.OfType<string>().ToList();
                foreach (string actor in selectedItems)
                {
                    lstToChange.Items.Remove(actor);
                }
            }
            else if (btnRemoveLog.Text == "Remove All")
            {
                lstToChange.Items.Clear();
            }

            ChangeAllocatedActorsValue();

            changeBtnRemoveLog();
            changeBtnToAlloc();
            changeBtnExport();
            changeChkSeparateActors();
            lblConvertionStatus.ForeColor = Color.Red;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            openFileDialog.Title = "Choose file for import";
            openFileDialog.FileName = "";
            openFileDialog.Filter = "Text file|*.txt";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                int fileNameStartIndex = openFileDialog.FileName.LastIndexOf('\\') + 1;
                string filePath = openFileDialog.FileName.Substring(0, fileNameStartIndex);
                string fileName = openFileDialog.FileName.Substring(fileNameStartIndex);

                string outputMessage = ConvertImportedFileToList(filePath, fileName);
                if (outputMessage == "Error")
                {
                    MessageBox.Show("Failed to import file");
                }
                else if (outputMessage != "")
                {
                    MessageBox.Show(outputMessage);
                }
            }

            ChangeAllocatedActorsValue();

            changeBtnExport();
            changeBtnToAlloc();
            changeBtnRemoveLog();
            changeChkSeparateActors();
            lblConvertionStatus.ForeColor = Color.Red;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            saveFileDialog.Title = "Choose where to export";
            string fileName = toolTipFileName.GetToolTip(lblFilePath);
            fileName = fileName.Substring(0, fileName.Length - 4);
            saveFileDialog.FileName = "(Actors)" + fileName + ".txt";
            saveFileDialog.Filter = "Text file|*.txt";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.WriteAllText(saveFileDialog.FileName, ConvertToChangeListForExport());
                    MessageBox.Show("List successfully exported");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex);
                }
            }
        }

        private void lblLoadStatus_DoubleClick(object sender, EventArgs e)
        {
            if (lblLoadStatus.ForeColor == Color.Green)
            {
                //remove actors -> dubers if any
                if (lstToChange.Items.Count > 0)
                    if (lstToChange.Items.Cast<string>().Any(s => s.Contains(RIGHT_ARROW)))
                    {
                        List<string> items = lstToChange.Items.OfType<string>().ToList();
                        foreach (string toRemove in items)
                        {
                            if (toRemove.Contains(RIGHT_ARROW))
                                lstToChange.Items.Remove(toRemove);
                        }
                    }

                //add all actors
                foreach (var actor in cmbActors.Items)
                {
                    if (!lstToChange.Items.Contains(actor))
                        lstToChange.Items.Add(actor.ToString());
                }

                ChangeAllocatedActorsValue();

                changeBtnToAlloc();
                changeBtnExport();
                changeChkSeparateActors();

                changeBtnRemoveLog();
                lblConvertionStatus.ForeColor = Color.Red;
            }
        }

        private void lblConvertionStatus_DoubleClick(object sender, EventArgs e)
        {
            const bool removeAssFormating = false;
            const bool allocateActors = false;
            const string separateActors = "";

            string srtSub = ConvertAssToSrt(removeAssFormating, allocateActors, separateActors);

            if (srtSub == "")
            {
                MessageBox.Show("There are no subtitles to convert!");
            }
            else
            {
                try
                {
                    int fileNameStartIndex = path.LastIndexOf('\\') + 1;
                    string filePath = path.Substring(0, fileNameStartIndex);
                    string newFileName = "Unformatted_" + path.Substring(fileNameStartIndex);
                    string newPath = Path.Combine(filePath, newFileName);

                    File.WriteAllText(newPath, srtSub);
                    MessageBox.Show("Subtitles converted with ASS formating");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void contextMenuStrip_Click(object sender, EventArgs e)
        {
            //determine which item clicked and put that option in cmbActors
            if (sender == null) return;
            ToolStripMenuItem selectedItem = sender as ToolStripMenuItem;

            if (selectedItem != null)
            {
                cmbActors.SelectedIndex = cmbActors.Items.IndexOf(selectedItem.Text);
            }
        }
        #endregion

        #region Events to change buttons
        private void cmbActors_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeBtnToAlloc();
        }

        private void cmbDubers_TextUpdate(object sender, EventArgs e)
        {
            changeBtnToAddRmv();
            changeBtnToAlloc();
        }

        private void lstToChange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstToChange.SelectedIndex != -1)
            {
                btnRemoveLog.Text = "Remove";
            }
        }

        private void lblLoadStatus_ForeColorChanged(object sender, EventArgs e)
        {
            changeBtnImport();
        }

        private void chkSeparateActors_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkSeparateActors.Checked)
            {
                chkActorsPerLine.Enabled = false;
                chkActorsPerLine.Checked = false;
            }
            else if (lstToChange.Items.Cast<string>().Any(s => s.Contains(RIGHT_ARROW)))
            {
                chkActorsPerLine.Enabled = true;
            }

            lblConvertionStatus.ForeColor = Color.Red;
        }

        private void chkActorsPerLine_CheckedChanged(object sender, EventArgs e)
        {
            lblConvertionStatus.ForeColor = Color.Red;
        }
        #endregion

        #region Additional methods to change buttons
        private void changeBtnToAlloc()
        {
            if (cmbActors.Text != "" && cmbDubers.Text != "" && !Regex.IsMatch(cmbDubers.Text, @"^\s*$"))
            {
                //check if actor were added before to list of changes
                if (lstToChange.Items.Cast<string>().Any(s => s.Contains(RIGHT_ARROW)))
                {
                    if (!lstToChange.Items.Cast<string>().Any(item => item.Substring(0, item.IndexOf(RIGHT_ARROW) - 1)
                                                                          .Equals(cmbActors.Text, StringComparison.OrdinalIgnoreCase)
                                                                   && item.Substring(item.IndexOf(RIGHT_ARROW) + 2)
                                                                          .Equals(cmbDubers.Text, StringComparison.OrdinalIgnoreCase)))
                    {
                        btnToAlloc.Enabled = true;
                        return;
                    }
                }
                else
                {
                    btnToAlloc.Enabled = true;
                    return;
                }
            }
            btnToAlloc.Enabled = false;
        }

        private void changeBtnToAddRmv()
        {
            if (!cmbDubers.Items.Contains(cmbDubers.Text) && cmbDubers.Text != "" && !Regex.IsMatch(cmbDubers.Text, @"^\s*$"))
            {
                btnAddDuber.Enabled = true;
                btnRemoveDuber.Enabled = false;
            }
            else if (cmbDubers.Items.Contains(cmbDubers.Text) && cmbDubers.Text != "")
            {
                btnAddDuber.Enabled = false;
                btnRemoveDuber.Enabled = true;
            }
            else
            {
                btnAddDuber.Enabled = false;
                btnRemoveDuber.Enabled = false;
            }
        }

        private void changeBtnExport()
        {
            if (lstToChange.Items.Count > 0)
            {
                if (!lstToChange.Items.Cast<string>().Any(s => !s.Contains(RIGHT_ARROW)))
                    btnExport.Enabled = true;
                else btnExport.Enabled = false;
            }
            else
            {
                btnExport.Enabled = false;
            }
        }

        private void changeBtnImport()
        {
            if (lblLoadStatus.ForeColor == Color.Green)
            {
                btnImport.Enabled = true;
            }
            else
            {
                btnImport.Enabled = false;
            }
        }

        private void changeBtnSaveDeleteDubers()
        {
            //if string in settings and string in cmbDubers are differs enable else disable
            if (Settings.Default.Dubers == string.Join(";", cmbDubers.Items.OfType<String>().Select(item => item.ToString())))
            {
                btnSaveDeleteDubers.Enabled = false;
            }
            else
            {
                btnSaveDeleteDubers.Enabled = true;
            }
        }

        private void changeChkSeparateActors()
        {
            if (lstToChange.Items.Count > 0)
            {
                chkSeparateActors.Enabled = true;

                if (lstToChange.Items.Cast<string>().Any(s => s.Contains(RIGHT_ARROW)) && chkSeparateActors.Checked)
                    chkActorsPerLine.Enabled = true;
                else
                {
                    chkActorsPerLine.Enabled = false;
                    chkActorsPerLine.Checked = false;
                }
            }
            else
            {
                chkSeparateActors.Enabled = false;
                chkSeparateActors.Checked = false;

                chkActorsPerLine.Enabled = false;
                chkActorsPerLine.Checked = false;
            }
        }

        private void changeBtnRemoveLog()
        {
            if (lstToChange.Items.Count > 0)
            {
                if (lstToChange.SelectedIndex == -1)
                    btnRemoveLog.Text = "Remove All";
                btnRemoveLog.Enabled = true;
            }
            else
            {
                btnRemoveLog.Text = "Remove";
                btnRemoveLog.Enabled = false;
            }
        }
        #endregion

        #region Drag and Drop
        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] fileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (fileList[0].Substring(fileList[0].Length - 4).ToLower() == ".ass")
            {
                ImportSubFile(fileList[0]);
            }
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }
        #endregion
    }

    static class AssFormat
    {
        public const int Start = 0;
        public const int End = 1;
        public const int Style = 2;
        public const int Actor = 3;
        public const int MarginL = 4;
        public const int MarginR = 5;
        public const int MarginV = 6;
        public const int Effect = 7;
        public const int Text = 8;

        public static int GetSpecificFormatIndex(string line, int format)
        {
            int index = 0;
            int timeStartIndex = line.IndexOf(',') + 1;
            int timeEndIndex = line.IndexOf(',', timeStartIndex) + 1;
            int styleStartIndex = line.IndexOf(',', timeEndIndex) + 1;
            int actorStartIndex = line.IndexOf(',', styleStartIndex) + 1;
            int marginlStartIndex = line.IndexOf(",", actorStartIndex) + 1;
            int marginrStartIndex = line.IndexOf(",", marginlStartIndex) + 1;
            int marginvStartIndex = line.IndexOf(",", marginrStartIndex) + 1;
            int effectStartIndex = line.IndexOf(",", marginvStartIndex) + 1;
            int textStartIndex = line.IndexOf(",", effectStartIndex) + 1;

            switch (format)
            {
                case Start:
                    index = timeStartIndex;
                    break;
                case End:
                    index = timeEndIndex;
                    break;
                case Style:
                    index = styleStartIndex;
                    break;
                case Actor:
                    index = actorStartIndex;
                    break;
                case MarginL:
                    index = marginlStartIndex;
                    break;
                case MarginR:
                    index = marginrStartIndex;
                    break;
                case MarginV:
                    index = marginvStartIndex;
                    break;
                case Effect:
                    index = effectStartIndex;
                    break;
                case Text:
                    index = textStartIndex;
                    break;
                default:
                    index = -1;
                    break;
            }
            return index;
        }

        public static int GetStartTimeKey(string subLine)
        {
            int timeStartIndex = GetSpecificFormatIndex(subLine, Start);
            int timeStartLength = subLine.IndexOf(",", timeStartIndex) - timeStartIndex;
            string[] timeStart = subLine.Substring(timeStartIndex, timeStartLength).Replace('.', ':').Split(':');

            int hour = -1;
            int minute = -1;
            int second = -1;
            int millisecond = -1;

            try
            {
                if (int.TryParse(timeStart[0], out hour) &&
                   int.TryParse(timeStart[1], out minute) &&
                   int.TryParse(timeStart[2], out second) &&
                   int.TryParse(timeStart[3], out millisecond)
                  )
                {
                    return (hour * 3600 + minute * 60 + second) * 1000 + millisecond;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return -1;
        }

        public class SubtitleComparer : IComparer<string>
        {
            private List<string> subtitles;

            public SubtitleComparer(List<string> subtitles)
            {
                this.subtitles = subtitles;
            }

            public int Compare(string subtitle1, string subtitle2)
            {
                int compareByStartTime = GetStartTimeKey(subtitle1).CompareTo(GetStartTimeKey(subtitle2));

                if (compareByStartTime == 0)
                {
                    int originalOrder1 = subtitles.IndexOf(subtitle1);
                    int originalOrder2 = subtitles.IndexOf(subtitle2);
                    return originalOrder1.CompareTo(originalOrder2);
                }

                return compareByStartTime;
            }
        }
    }
}
