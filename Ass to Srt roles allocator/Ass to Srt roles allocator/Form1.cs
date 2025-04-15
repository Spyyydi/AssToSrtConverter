using Ass_to_Srt_roles_allocator.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;

namespace Ass_to_Srt_roles_allocator
{
    public partial class Form1 : Form
    {
        private const string EMPTY_ACTOR = "EMPTY ACTOR";
        private const string RIGHT_ARROW = "→";
        private const string IMPORT_DIRNAME = "Actors to import";
        private const string MAIN_ACTORS_FILENAME = "Actors main.txt";

        private const int ACTORS_LINE_NUM = 0;
        private const int DUBERS_LINE_NUM = 1;

        string path = "";
        List<string> subtitles;

        bool isDragging = false;

        string origAssPath = "";
        List<string> origAss;
        List<string> assWithActors;

        System.Windows.Forms.Timer scrollTimer;
        bool isQuickScrollEnabled;

        string batchDirPath = "";
        List<AssFile> batchFiles;
        List<string> batchMainActors;
        List<string> lastSavedMainActors;

        private BatchMainActorsSelector mainActorsSelector;

        BindingList<string> lstToChangeItems;

        public Form1()
        {
            InitializeComponent();
            subtitles = new List<string>();
            lblAllocatedActors.ContextMenuStrip = contextMenuStrip;

            origAss = new List<string>();
            assWithActors = new List<string>();
            grpOrigAss.AllowDrop = true;
            grpAssWithActors.AllowDrop = true;

            batchFiles = new List<AssFile>();
            batchMainActors = new List<string>();
            lastSavedMainActors = new List<string>();

            scrollTimer = new System.Windows.Forms.Timer();
            scrollTimer.Interval = 100;
            scrollTimer.Tick += ScrollTimer_Tick;

            isQuickScrollEnabled = false;

            lstToChangeItems = new BindingList<string>();
            lstToChangeItems.ListChanged += lstToChangeItems_ListChanged;
            lstToChange.DataSource = lstToChangeItems;
        }

        #region Convert Tab

        #region Additional methods
        private bool ImportSubtitles(string filePath, string fileName, bool isLoad)
        {
            try
            {
                string[] subs;
                if (isLoad)
                {
                    subs = origAss.ToArray();
                }
                else
                {
                    subs = File.ReadAllLines(Path.Combine(filePath, fileName));
                }

                if (!subs.Any(s => s.StartsWith("Dialogue") && AssFormat.ExtractDialogue(s, false) != ""))
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
                        if (!AssFormat.IsVectorDrawing(lineToCompare) && AssFormat.ExtractDialogue(lineToCompare, true) != "")
                        {
                            subtitles.Add(lineToCompare);
                        }
                    }
                }
                //sort subtitles by time
                subtitles.Sort(new AssFormat.SubtitleComparer(new List<string>(subtitles)));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return true;
        }

        private bool ImportSubFile(string subFilePath, bool isLoad)
        {
            int fileNameStartIndex = subFilePath.LastIndexOf('\\') + 1;
            string filePath = subFilePath.Substring(0, fileNameStartIndex);
            string fileName = subFilePath.Substring(fileNameStartIndex);

            if (isLoad) fileName = "[Synced] " + fileName;
            path = Path.Combine(filePath, fileName.Substring(0, fileName.LastIndexOf(".")) + ".srt");


            if (!ImportSubtitles(filePath, fileName, isLoad))
            {
                MessageBox.Show("There are no valid subtitles to convert");
                return false;
            }

            lblFilePath.Text = "File name: " + fileName;
            toolTipFileName.SetToolTip(lblFilePath, fileName);


            string emptyActors = LoadActorsToCmbBox();
            toolTipActorsLoaded.SetToolTip(lblActorsLoadStatus, "Empty actors: " + emptyActors);
            if (cmbActors.Items.Count < 1)
            {
                lblActorsLoadStatus.ForeColor = Color.Red;
                btnLoadActorsToList.Enabled = false;
            }
            else
            {
                lblActorsLoadStatus.ForeColor = Color.Green;
                btnLoadActorsToList.Enabled = true;
            }

            //change number of actors that can be allocated
            lblAllocatedActors.Text = lblAllocatedActors.Text.Substring(0, lblAllocatedActors.Text.IndexOf('\n') + 1)
                                    + "0/" + cmbActors.Items.Count.ToString();


            lstToChangeItems.Clear();
            ChangeLinesNumLabels(ACTORS_LINE_NUM);
            ChangeAllocatedActorsValue();

            cmbDubers.ResetText();
            ChangeLinesNumLabels(DUBERS_LINE_NUM);
            changeBtnToAlloc();

            changeBtnRemoveLog();
            changeBtnExport();

            changeChkSeparateActors();

            lblConvertionStatus.ForeColor = Color.Red;
            btnConvert.Enabled = true;
            btnConvertWithAssFormatting.Enabled = true;
            chkKeepNewLines.Enabled = true;

            return true;
        }

        private string LoadActorsToCmbBox()
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
                string actor = AssFormat.ExtractActor(line);

                //split actors if multiple for one dialogue
                if (actor.Contains(AssFormat.GENERAL_SEPARATOR) || actor.Any(c => AssFormat.ACTOR_SEPARATORS.Contains(c)))
                {
                    string[] splitedActors = AssFormat.SplitActors(actor);
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
                    else if(actor == EMPTY_ACTOR)
                    {
                        ++numOfEmptyActors;
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

        private List<string> ConvertToChangeListForExport()
        {
            List<string> dubersToExport = new List<string>();
            
            foreach (string item in lstToChangeItems)
            {
                string actor = item.Substring(0, item.IndexOf(RIGHT_ARROW)).Trim();
                string duber = item.Substring(item.IndexOf(RIGHT_ARROW) + 1).Trim();
                dubersToExport.Add(actor + ":" + duber);
            }

            foreach(string actor in cmbActors.Items)
            {
                if(!dubersToExport.Where(s => s.StartsWith(actor)).Any())
                {
                    dubersToExport.Add($"{actor}:");
                }
            }

            dubersToExport.Sort();
            return dubersToExport;
        }

        private void ImportActorsFile(string[] importedActors)
        {
            string outputMessage = ConvertImportedFileToListBox(importedActors);

            if (outputMessage == "Error")
            {
                MessageBox.Show("Failed to import file");
            }
            else if (outputMessage != "")
            {
                // Custom MessageBox.Show()
                new ShowLongMessage(outputMessage).ShowDialog();
            }

            ChangeAllocatedActorsValue();

            changeBtnExport();
            changeBtnToAlloc();
            changeBtnRemoveLog();
            changeChkSeparateActors();
            lblConvertionStatus.ForeColor = Color.Red;
        }

        private string ConvertImportedFileToListBox(string[] importedActors)
        {
            //try to add actors from file to to change list
            string report = "";
            int importedDubersCount = 0;
            int dubersCount = 0;
            try
            {
                foreach (string line in importedActors)
                {
                    if (line.Count(c => c == ':') != 1 || line.Last() == ':') continue;
                    dubersCount++;

                    //separate actor and duber
                    string actor = line.Substring(0, line.IndexOf(":")).Trim();
                    string duber = line.Substring(line.IndexOf(":") + 1).Trim();

                    //find actor in combobox if found add to listbox if not add to report
                    if (cmbActors.Items.Contains(actor))
                    {
                        if (importedDubersCount == 0)
                        {
                            lstToChangeItems.Clear();
                        }
                        lstToChangeItems.Add(actor + " " + RIGHT_ARROW + " " + duber);

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

        private string ConvertAssToSrt(bool removeAssFormating, bool allocateActors, string separateActorsPerFile)
        {
            string srtSub = "";
            bool keepNewLines = chkKeepNewLines.Checked;

            //convert from ass to srt
            int lineNum = 1;
            int subLen = subtitles.Count;
            bool isDubbersAllocated = lstToChangeItems.Any(s => s.Contains(RIGHT_ARROW));

            if (subLen > 0)
                foreach (string line in subtitles)
                {
                    if (separateActorsPerFile != "")
                    {
                        string actor = AssFormat.ExtractActor(line);
                        string[] actors = AssFormat.SplitActors(actor);

                        string[] separatedActor = AssFormat.SplitActors(separateActorsPerFile);

                        if (separatedActor.Intersect(actors).Any())
                        {
                            if (lineNum != 1)
                            {
                                srtSub += "\n\n";
                            }

                            srtSub += lineNum++.ToString() + "\n";
                            srtSub += ExtractSrtTime(line) + "\n";

                            if (allocateActors)
                            {
                                if (actor.Any(c => AssFormat.ACTOR_SEPARATORS.Contains(c)))
                                    actor = string.Join(", ", actors);
                                srtSub += "[" + actor + "] ";
                            }

                            srtSub += AssFormat.ExtractDialogue(line, removeAssFormating, keepNewLines);
                        }
                    }
                    else
                    {
                        srtSub += lineNum.ToString() + "\n";
                        srtSub += ExtractSrtTime(line) + "\n";

                        if (allocateActors)
                        {
                            //Extract actor
                            string actor = AssFormat.ExtractActor(line);
                            //if multiple actors for the subtitle line
                            if (actor.Any(c => AssFormat.ACTOR_SEPARATORS.Contains(c)))
                            {
                                //split actors and find duber for each actor (if duber not found then put actor instead)
                                string[] actors = AssFormat.SplitActors(actor);
                                string dubers = "";
                                bool atLeastOneDuberWritten = false;
                                bool atLeastOneActorWritten = false;

                                foreach (string s in actors)
                                {
                                    if (isDubbersAllocated)
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
                                    else
                                    {
                                        if (lstToChangeItems.Contains(s))
                                        {
                                            dubers += s + ", ";
                                            atLeastOneActorWritten = true;
                                        }
                                    }
                                }

                                if (atLeastOneDuberWritten)
                                    srtSub += "(" + dubers.Substring(0, dubers.LastIndexOf(',')) + ") ";
                                else if (atLeastOneActorWritten)
                                    srtSub += "[" + dubers.Substring(0, dubers.LastIndexOf(',')) + "] ";
                            }
                            else
                            {
                                if (isDubbersAllocated)
                                {
                                    string duber = FindDuberForActor(actor);
                                    if (duber == "")
                                    {
                                        //dubber not found
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
                                else
                                {
                                    if (lstToChangeItems.Contains(actor))
                                        srtSub += "[" + actor + "] ";
                                }
                            }
                        }


                        srtSub += AssFormat.ExtractDialogue(line, removeAssFormating, keepNewLines);

                        if (lineNum != subLen)
                        {
                            srtSub += "\n\n";
                        }
                        ++lineNum;
                    }
                }

            return srtSub;
        }

        private string FindDuberForActor(string actor)
        {
            int lstLen = lstToChangeItems.Count;
            string duber = "";

            //find duber according to requested actor
            for (int i = 0; i < lstLen; ++i)
            {
                string currentItem = lstToChangeItems[i].ToString();
                if (currentItem.StartsWith(actor, StringComparison.OrdinalIgnoreCase) && currentItem.Contains(RIGHT_ARROW))
                {
                    duber = currentItem.Substring(actor.Length + 3);
                    break;
                }
            }

            return duber;
        }

        private string ExtractSrtTime(string line)
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

        private void ChangeAllocatedActorsValue()
        {
            //change number of actors allocated
            string defaultLblTxt = lblAllocatedActors.Text;
            string loadedActorsNum = defaultLblTxt.Substring(defaultLblTxt.IndexOf('/') + 1);

            int allocActorsNumIndex = defaultLblTxt.LastIndexOf('\n') + 1;
            defaultLblTxt = defaultLblTxt.Substring(0, allocActorsNumIndex);

            lblAllocatedActors.Text = defaultLblTxt + lstToChangeItems.Count.ToString() + "/" + loadedActorsNum;


            List<string> allocatedActors = new List<string>(lstToChangeItems);
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

            toolTipAllocatedActors.SetToolTip(lblAllocatedActors,
                                            $"Right click to see/select not allocated actors ({contextMenuStrip.Items.Count})");
        }

        private List<string> GetActorsOfDuberFromListBox(string duber)
        {
            List<string> actors = new List<string>();
            if (lstToChangeItems.Any(s => !s.Contains(RIGHT_ARROW)))
                return actors;

            foreach(string item in lstToChangeItems)
            {
                if(item.Contains(RIGHT_ARROW) && item.EndsWith(duber))
                {
                    actors.Add(item.Substring(0, item.IndexOf(RIGHT_ARROW)).Trim());
                }
            }

            return actors;
        }
        #endregion

        #region Button click events
        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            openFileDialog.Title = "Choose sub file";
            openFileDialog.FileName = "";
            openFileDialog.Filter = "ASS files|*.ass";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ImportSubFile(openFileDialog.FileName, false);
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            const bool removeAssFormating = true;
            bool allocateActors = true;
            const string separateActorsPerFile = "";
            Dictionary<string, string> separateSrtSubs = new Dictionary<string, string>();

            if (chkSeparateActors.Checked)
            {
                if (lstToChangeItems.Any(s => s.Contains(RIGHT_ARROW)))
                {
                    if (!chkActorsPerLine.Checked)
                    {
                        allocateActors = false;
                    }

                    // identify every actor per duber
                    foreach (string actorNduber in lstToChangeItems)
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
                    foreach (string actor in lstToChangeItems)
                    {
                        separateSrtSubs.Add(actor, ConvertAssToSrt(removeAssFormating, allocateActors, actor));
                    }
                }
            }

            allocateActors = lstToChangeItems.Count > 0;
            string srtSub = ConvertAssToSrt(removeAssFormating, allocateActors, separateActorsPerFile);

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
                        else if (lstToChangeItems.Any(s => s.Contains(RIGHT_ARROW)))
                        {
                            filePath = Path.Combine(filePath, "[Dubers] " + fileName);
                        }
                        else
                        {
                            filePath = Path.Combine(filePath, "[Actors] " + fileName);
                        }

                        if(!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);

                        foreach (var subForActor in separateSrtSubs)
                        {
                            string newPath = Path.Combine(filePath, subForActor.Key + ".srt");
                            File.WriteAllText(newPath, subForActor.Value);
                        }
                    }
                    else
                    {
                        if (lstToChangeItems.Count < 1)
                        {
                            filePath = path;
                        }
                        else if (lstToChangeItems.Any(s => s.Contains(RIGHT_ARROW)))
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
            string newDuber = cmbDubers.Text; //needed because after removing actors cmbDubers text resets
            //remove only actors if any
            if (lstToChangeItems.Count > 0)
                if (lstToChangeItems.Any(s => !s.Contains(RIGHT_ARROW)))
                {
                    List<string> items = new List<string>(lstToChangeItems);
                    foreach (string toRemove in items)
                    {
                        if (!toRemove.Contains(RIGHT_ARROW))
                            lstToChangeItems.Remove(toRemove);
                    }
                }
            
            cmbDubers.Text = newDuber;
            lstToChangeItems.Add(cmbActors.Text + " " + RIGHT_ARROW + " " + cmbDubers.Text.Trim());

            ChangeAllocatedActorsValue();

            changeBtnToAlloc();
            changeBtnExport();
            changeChkSeparateActors();

            changeBtnRemoveLog();
            lblConvertionStatus.ForeColor = Color.Red;
        }

        private void btnRemoveLog_Click(object sender, EventArgs e)
        {
            if (btnRemoveLog.Text == "Remove")
            {
                List<string> selectedItems = lstToChange.SelectedItems.OfType<string>().ToList();
                foreach (string actor in selectedItems)
                {
                    lstToChangeItems.Remove(actor);
                }
            }
            else if (btnRemoveLog.Text == "Remove All")
            {
                lstToChangeItems.Clear();
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
            openFileDialog.Filter = "Text files|*.txt";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] importedActors = File.ReadAllLines(openFileDialog.FileName);
                ImportActorsFile(importedActors);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            saveFileDialog.Title = "Choose where to export";
            string fileName = toolTipFileName.GetToolTip(lblFilePath);
            fileName = fileName.Substring(0, fileName.Length - 4);
            saveFileDialog.FileName = "[Import] " + fileName + ".txt";
            saveFileDialog.Filter = "Text files|*.txt";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.WriteAllLines(saveFileDialog.FileName, ConvertToChangeListForExport());
                    MessageBox.Show("List successfully exported");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnConvertWithAssFormatting_Click(object sender, EventArgs e)
        {
            const bool removeAssFormating = false;
            const bool allocateActors = false;
            const string separateActorsPerFile = "";

            string srtSub = ConvertAssToSrt(removeAssFormating, allocateActors, separateActorsPerFile);

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
                    lblConvertionStatus.ForeColor = Color.Green;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnLoadActorsToList_Click(object sender, EventArgs e)
        {
            if (lblActorsLoadStatus.ForeColor == Color.Green)
            {
                //remove actors -> dubers if any
                if (lstToChangeItems.Count > 0)
                    if (lstToChangeItems.Any(s => s.Contains(RIGHT_ARROW)))
                    {
                        List<string> items = new List<string>(lstToChangeItems);
                        foreach (string toRemove in items)
                        {
                            if (toRemove.Contains(RIGHT_ARROW))
                                lstToChangeItems.Remove(toRemove);
                        }
                    }

                //add all actors
                foreach (var actor in cmbActors.Items)
                {
                    if (!lstToChangeItems.Contains(actor))
                        lstToChangeItems.Add(actor.ToString());
                }

                ChangeAllocatedActorsValue();

                changeBtnToAlloc();
                changeBtnExport();
                changeChkSeparateActors();

                changeBtnRemoveLog();
                lblConvertionStatus.ForeColor = Color.Red;
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
            if(cmbActors.SelectedIndex != -1)
            {
                string actor = cmbActors.Text;
                int lineNums = subtitles.Count(c => AssFormat.ExtractActor(c).Contains(actor));

                ChangeLinesNumLabels(ACTORS_LINE_NUM, lineNums);
            }

            changeBtnToAlloc();
        }

        private void cmbDubers_TextUpdate(object sender, EventArgs e)
        {
            UpdateDuberLinesNum();
            changeBtnToAlloc();
        }

        private void lstToChange_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeBtnRemoveLog();
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
            else if (lstToChangeItems.Any(s => s.Contains(RIGHT_ARROW)))
            {
                chkActorsPerLine.Enabled = true;
            }

            lblConvertionStatus.ForeColor = Color.Red;
        }

        private void chkActorsPerLine_CheckedChanged(object sender, EventArgs e)
        {
            lblConvertionStatus.ForeColor = Color.Red;
        }

        private void chkKeepNewLines_CheckedChanged(object sender, EventArgs e)
        {
            lblConvertionStatus.ForeColor = Color.Red;
        }

        private void lstToChangeItems_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (lstToChangeItems.Count < 1 ||
                !lstToChangeItems.Where(s => s.Contains(RIGHT_ARROW)).Any()
                )
            {
                cmbDubers.Items.Clear();
                cmbDubers.ResetText();
                ChangeLinesNumLabels(DUBERS_LINE_NUM);
            }
            else
            {
                cmbDubers.Items.Clear();
                cmbDubers.Items.AddRange(lstToChangeItems.Where(s => s.Contains(RIGHT_ARROW))
                                                         .Select(s => s.Substring(s.IndexOf(RIGHT_ARROW) + 2).Trim())
                                                         .Distinct()
                                                         .ToArray());
                
                if (cmbDubers.Text != "" && !cmbDubers.Items.Contains(cmbDubers.Text))
                {
                    cmbDubers.ResetText();
                    ChangeLinesNumLabels(DUBERS_LINE_NUM);
                }
                else if (cmbDubers.Text != "") UpdateDuberLinesNum();
                else if (cmbDubers.Text == "" && lblDuberLines.Text != "Lines: 0") ChangeLinesNumLabels(DUBERS_LINE_NUM);
            }
        }
        #endregion

        #region Additional methods to change buttons/text boxes
        private void changeBtnToAlloc()
        {
            if (cmbActors.Text != "" && cmbDubers.Text != "" && !Regex.IsMatch(cmbDubers.Text, @"^\s*$"))
            {
                //check if actor were added before to list of changes
                if (lstToChangeItems.Any(s => s.Contains(RIGHT_ARROW)))
                {
                    if (!lstToChangeItems.Any(item => item.Substring(0, item.IndexOf(RIGHT_ARROW) - 1)
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

        private void changeBtnExport()
        {
            if (lstToChangeItems.Count > 0)
            {
                if (!lstToChangeItems.Any(s => !s.Contains(RIGHT_ARROW)))
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
            if (lblActorsLoadStatus.ForeColor == Color.Green)
            {
                btnImport.Enabled = true;
            }
            else
            {
                btnImport.Enabled = false;
            }
        }

        private void changeChkSeparateActors()
        {
            if (lstToChangeItems.Count > 0)
            {
                chkSeparateActors.Enabled = true;

                if (lstToChangeItems.Any(s => s.Contains(RIGHT_ARROW)) && chkSeparateActors.Checked)
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
            if (lstToChangeItems.Count > 0)
            {
                if (lstToChange.SelectedIndex == -1 || lstToChange.SelectedItems.Count == lstToChangeItems.Count)
                    btnRemoveLog.Text = "Remove All";
                else btnRemoveLog.Text = "Remove";
                btnRemoveLog.Enabled = true;
            }
            else
            {
                btnRemoveLog.Text = "Remove";
                btnRemoveLog.Enabled = false;
            }
        }
        
        private void ChangeLinesNumLabels(int txtBox, int lineNum = 0)
        {
            string text = "Lines: ";
            if(txtBox == ACTORS_LINE_NUM)
            {
                lblActorLines.Text = $"{text}{lineNum}";
            }
            else if(txtBox == DUBERS_LINE_NUM)
            {
                lblDuberLines.Text = $"{text}{lineNum}";
            }
        }
        
        private void UpdateDuberLinesNum()
        {
            if (cmbDubers.Items.Cast<string>().Contains(cmbDubers.Text))
            {
                List<string> actors = GetActorsOfDuberFromListBox(cmbDubers.Text);
                int linesCount = subtitles
                                 .Select(s => AssFormat.SplitActors(AssFormat.ExtractActor(s)).ToHashSet())
                                 .Count(actorsPerLine => actors.Any(actor => actorsPerLine.Contains(actor)));

                ChangeLinesNumLabels(DUBERS_LINE_NUM, linesCount);
            }
            else ChangeLinesNumLabels(DUBERS_LINE_NUM);
        }
        #endregion

        #region Drag and Drop

        private void tabConvert_DragDrop(object sender, DragEventArgs e)
        {
            tabConvert.BackColor = SystemColors.Control;
            isDragging = false;

            string[] fileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            int index = Array.FindIndex(fileList, s => s.ToLower().EndsWith(".ass"));

            if (index >= 0)
            {
                ImportSubFile(fileList[index], false);
            }
        }

        private void tabConvert_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
                tabConvert.BackColor = Color.LightBlue;
                isDragging = true;
            }
            else
                e.Effect = DragDropEffects.None;
        }

        private void tabConvert_DragOver(object sender, DragEventArgs e)
        {
            if (isDragging && !lstToChange.ClientRectangle.Contains(lstToChange.PointToClient(new Point(e.X, e.Y))))
            {
                tabConvert.BackColor = Color.LightBlue;
            }
        }

        private void tabConvert_DragLeave(object sender, EventArgs e)
        {
            tabConvert.BackColor = SystemColors.Control;
            isDragging = false;
        }

        private void lstToChange_DragDrop(object sender, DragEventArgs e)
        {
            lstToChange.BackColor = SystemColors.Window;
            isDragging = false;

            string[] fileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            int index = Array.FindIndex(fileList, s => s.ToLower().EndsWith(".txt"));

            if (index >= 0)
            {
                string[] importedActors = File.ReadAllLines(fileList[index]);
                ImportActorsFile(importedActors);
            }
        }

        private void lstToChange_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
                lstToChange.BackColor = Color.LightBlue;
                isDragging = true;
            }
            else
                e.Effect = DragDropEffects.None;
        }

        private void lstToChange_DragOver(object sender, DragEventArgs e)
        {
            if (isDragging && lstToChange.ClientRectangle.Contains(lstToChange.PointToClient(new Point(e.X, e.Y))))
            {
                lstToChange.BackColor = Color.LightBlue;
            }
        }

        private void lstToChange_DragLeave(object sender, EventArgs e)
        {
            lstToChange.BackColor = SystemColors.Window;
            isDragging = false;
        }
        #endregion

        #endregion

        #region Sync Actors Tab

        #region Additional methods

        private void ImportOrigAss(string subFilePath)
        {
            int fileNameStartIndex = subFilePath.LastIndexOf('\\') + 1;
            string filePath = subFilePath.Substring(0, fileNameStartIndex);
            string fileName = subFilePath.Substring(fileNameStartIndex);
            origAssPath = subFilePath;

            if (!ImportOrigAssSubtitles(filePath, fileName))
            {
                MessageBox.Show("There are no valid subtitles to synchronize");
                return;
            }

            int totalSubLines = origAss.Count(s => s.StartsWith("Dialogue") && !AssFormat.IsVectorDrawing(s));
            int actorsAllocdNum = origAss.Count(s => s.StartsWith("Dialogue") && !AssFormat.IsVectorDrawing(s) && AssFormat.ExtractActor(s) != "");

            lblFileNameOrigAss.Text = "File name: " + fileName;
            toolTipFileNameOrigAss.SetToolTip(lblFileNameOrigAss, fileName);

            lblSubLinesOrigAss.Text = "Total sub lines: " + totalSubLines;
            lblAllocActorsNumOrigAss.Text = "Lines with actors: " + actorsAllocdNum;
            lblNotAllocdActorsNumOrigAss.Text = "Lines without actors: " + (totalSubLines - actorsAllocdNum).ToString();

            changeSyncBtn();
            btnSaveAss.Enabled = false;
            btnLoadAss.Enabled = false;
            btnOpenReport.Enabled = false;
            richSyncReport.Clear();
        }

        private bool ImportOrigAssSubtitles(string filePath, string fileName)
        {
            try
            {
                string[] subs = File.ReadAllLines(Path.Combine(filePath, fileName));
                if (!subs.Any(s => s.StartsWith("Dialogue")))
                {
                    return false;
                }

                origAss.Clear();
                origAss.AddRange(subs);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return true;
        }

        private void ImportAssWithActors(string subFilePath)
        {
            int fileNameStartIndex = subFilePath.LastIndexOf('\\') + 1;
            string filePath = subFilePath.Substring(0, fileNameStartIndex);
            string fileName = subFilePath.Substring(fileNameStartIndex);
            int totalSubLines = 0;
            int actorsNotAllocdNum = 0;

            if (!ImportAssWithActorsSubtitles(filePath, fileName, ref totalSubLines, ref actorsNotAllocdNum))
            {
                MessageBox.Show("There are no valid subtitles with allocated actors");
                return;
            }


            lblFileNameAssWithActors.Text = "File name: " + fileName;
            toolTipFileNameAssWithActors.SetToolTip(lblFileNameAssWithActors, fileName);

            lblSubLinesAssWithActors.Text = "Total sub lines: " + totalSubLines;
            lblAllocActorsNumAssWithActors.Text = "Lines with actors: " + (totalSubLines - actorsNotAllocdNum).ToString();
            lblNotAllocdActorsNumAssWithActors.Text = "Lines without actors: " + actorsNotAllocdNum;

            changeSyncBtn();
            btnOpenReport.Enabled = false;
            richSyncReport.Clear();
        }

        private bool ImportAssWithActorsSubtitles(string filePath, string fileName, ref int subLines, ref int notAllocdNum)
        {
            try
            {
                string[] subs = File.ReadAllLines(Path.Combine(filePath, fileName));
                if (!subs.Any(s => s.StartsWith("Dialogue") && AssFormat.ExtractActor(s) != ""))
                {
                    return false;
                }

                assWithActors.Clear();
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

                    if (isDialogue && !assWithActors.Contains(lineToCompare))
                    {
                        if (!AssFormat.IsVectorDrawing(lineToCompare))
                        {
                            if (AssFormat.ExtractActor(lineToCompare) == "")
                            {
                                ++notAllocdNum;
                            }

                            assWithActors.Add(lineToCompare);
                            ++subLines;
                        }
                    }
                }
                //sort subtitles by time
                assWithActors.Sort(new AssFormat.SubtitleComparer(new List<string>(assWithActors)));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return true;
        }

        private string SyncActor(string line, string nextLine)
        {
            const bool withMs = true;

            int lineStartTimeWithMs = AssFormat.GetTimeKey(line, AssFormat.Start, withMs);
            int lineEndTimeWithMs = AssFormat.GetTimeKey(line, AssFormat.End, withMs);

            int index = assWithActors.FindIndex(s => lineStartTimeWithMs == AssFormat.GetTimeKey(s, AssFormat.Start, withMs)
                                                  && lineEndTimeWithMs == AssFormat.GetTimeKey(s, AssFormat.End, withMs));

            if (index != -1)
            {
                if (AssFormat.ExtractActor(assWithActors[index]) != "")
                {
                    return AssFormat.ReplaceActor(line, AssFormat.ExtractActor(assWithActors[index]));
                }
                else
                {
                    return "";
                }
            }
            else
            {
                index = assWithActors.FindIndex(s => lineStartTimeWithMs < AssFormat.GetTimeKey(s, AssFormat.End, withMs));

                if (index != -1)
                {
                    if (AssFormat.ExtractActor(assWithActors[index]) == "")
                    {
                        return "";
                    }

                    if (index + 1 < assWithActors.Count)
                    {
                        if (lineEndTimeWithMs <= AssFormat.GetTimeKey(assWithActors[index + 1], AssFormat.Start, withMs))
                        {
                            int lineStartTime = AssFormat.GetTimeKey(line, AssFormat.Start, !withMs);

                            if (lineStartTime >= AssFormat.GetTimeKey(assWithActors[index], AssFormat.Start, !withMs))
                            {
                                return AssFormat.ReplaceActor(line, AssFormat.ExtractActor(assWithActors[index]));
                            }

                            return "";
                        }
                        else
                        {
                            //origAss line may contain multiple sync options
                            //if all options have same actors synchronize
                            string initialActor = AssFormat.ExtractActor(assWithActors[index]);
                            int count = assWithActors.Count;

                            for (int i = index + 1; i < count; ++i)
                            {
                                if (lineEndTimeWithMs > AssFormat.GetTimeKey(assWithActors[i], AssFormat.Start, withMs))
                                {
                                    if (initialActor.ToLower() != AssFormat.ExtractActor(assWithActors[i]).ToLower())
                                    {
                                        initialActor = "";
                                        break;
                                    }
                                }
                                else break;
                            }

                            if (initialActor != "")
                            {
                                return AssFormat.ReplaceActor(line, initialActor);
                            }
                        }
                    }
                    else
                    {
                        return AssFormat.ReplaceActor(line, AssFormat.ExtractActor(assWithActors[index]));
                    }
                }
            }

            return "";
        }

        private string GetCurrentTime()
        {
            return "[" + DateTime.Now.ToString("HH:mm:ss.fff") + "]";
        }

        #endregion

        #region Button click events
        private void btnSelectOrigAss_Click(object sender, EventArgs e)
        {
            openFileDialog.Title = "Choose ASS file";
            openFileDialog.FileName = "";
            openFileDialog.Filter = "Subtitles|*.ass";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ImportOrigAss(openFileDialog.FileName);
            }
        }

        private void btnSelectAssWithActors_Click(object sender, EventArgs e)
        {
            openFileDialog.Title = "Choose ASS file";
            openFileDialog.FileName = "";
            openFileDialog.Filter = "Subtitles|*.ass";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ImportAssWithActors(openFileDialog.FileName);
            }
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            bool overwriteActors = !chkKeepActors.Checked;
            btnLoadAss.Enabled = false;
            btnSaveAss.Enabled = false;
            btnSync.Enabled = false;
            chkKeepActors.Enabled = false;


            int origAssCount = origAss.Count;
            int syncedActorsNum = 0;
            int wrongSyncNum = 0;
            int triedToSyncNum = 0;

            isQuickScrollEnabled = true;
            for (int i = 0; i < origAssCount; ++i)
            {
                if (origAss[i].StartsWith("Dialogue") && !AssFormat.IsVectorDrawing(origAss[i]))
                {
                    if (overwriteActors || AssFormat.ExtractActor(origAss[i]) == "")
                    {
                        string synced = SyncActor(origAss[i], (i + 1 < origAssCount) ? origAss[i + 1] : "");
                        string report = "";

                        if (synced != "")
                        {
                            if (synced.StartsWith(RIGHT_ARROW))
                            {
                                synced = synced.Substring(RIGHT_ARROW.Length);
                                ++wrongSyncNum;
                                report = $"Line {i + 1} wrong sync.";
                            }

                            origAss[i] = synced;
                            ++syncedActorsNum;
                        }
                        else
                        {
                            origAss[i] = AssFormat.ReplaceActor(origAss[i], "");
                            report = $"Line {i + 1} not synced.";
                        }

                        if (report != "")
                        {
                            string[] startAndEndTime = AssFormat.ExtractAssTime(origAss[i]);
                            UpdateReport(GetCurrentTime() + $" {report} " + "Timing: " + startAndEndTime[0] + " --> " + startAndEndTime[1]);
                        }

                        ++triedToSyncNum;
                    }
                }
            }

            UpdateReport(GetCurrentTime() + $" {syncedActorsNum}/{triedToSyncNum} actors synced successfully");

            if (wrongSyncNum > 0)
                UpdateReport(GetCurrentTime() + $" {wrongSyncNum}/{syncedActorsNum} possible wrong sync");


            // Update labels of origAss
            int newAllocedActorsNum = origAss.Count(s => s.StartsWith("Dialogue") && !AssFormat.IsVectorDrawing(s) && AssFormat.ExtractActor(s) != "");
            int newNotAllocedActorsNum = origAss.Count(s => s.StartsWith("Dialogue") && !AssFormat.IsVectorDrawing(s)) - newAllocedActorsNum;

            lblAllocActorsNumOrigAss.Text = "Lines with actors: " + newAllocedActorsNum;
            lblNotAllocdActorsNumOrigAss.Text = "Lines without actors: " + newNotAllocedActorsNum;


            btnLoadAss.Enabled = true;
            btnSaveAss.Enabled = true;
            btnOpenReport.Enabled = true;
        }

        private void btnSaveAss_Click(object sender, EventArgs e)
        {
            saveFileDialog.Title = "Choose where to save ASS";
            string fileName = toolTipFileNameOrigAss.GetToolTip(lblFileNameOrigAss);
            saveFileDialog.FileName = "[Synced] " + fileName;
            saveFileDialog.Filter = "ASS files|*.ass|All files|*.*";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.WriteAllLines(saveFileDialog.FileName, origAss);
                    UpdateReport(GetCurrentTime() + " ASS file saved");
                    btnOpenReport.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnLoadAss_Click(object sender, EventArgs e)
        {
            if (ImportSubFile(origAssPath, true))
            {
                UpdateReport(GetCurrentTime() + " ASS file loaded to Convert tab");
            }
            else
            {
                UpdateReport(GetCurrentTime() + " ASS file NOT loaded to Convert tab");
            }

            btnOpenReport.Enabled = true;
        }

        private void btnOpenReport_Click(object sender, EventArgs e)
        {
            new ShowLongMessage(richSyncReport.Text).ShowDialog();
        }
        #endregion

        #region Events to change buttons
        private void richSyncReport_TextChanged(object sender, EventArgs e)
        {
            if (isQuickScrollEnabled)
            {
                scrollTimer.Stop();
                scrollTimer.Start();
            }
            else
            {
                ScrollToEnd();
            }
        }

        private void ScrollTimer_Tick(object sender, EventArgs e)
        {
            ScrollToEnd();
            scrollTimer.Stop();
        }
        #endregion

        #region Additional methods to change buttons
        private void changeSyncBtn()
        {
            if (lblFileNameOrigAss.Text.Length > "File name: ".Length &&
               lblFileNameAssWithActors.Text.Length > "File name: ".Length)
            {
                btnSync.Enabled = true;
                chkKeepActors.Enabled = true;
                chkKeepActors.Checked = false;
            }
            else
            {
                btnSync.Enabled = false;
                chkKeepActors.Enabled = false;
            }
        }

        private void UpdateReport(string message)
        {
            // Append new text
            if (richSyncReport.Text != string.Empty)
            {
                richSyncReport.AppendText("\n" + message);
            }
            else
            {
                richSyncReport.AppendText(message);
            }
        }

        private void ScrollToEnd()
        {
            richSyncReport.SelectionStart = richSyncReport.Text.Length;
            richSyncReport.ScrollToCaret();
        }
        #endregion

        #region Drag and Drop
        private void grpOrigAss_DragDrop(object sender, DragEventArgs e)
        {
            grpOrigAss.BackColor = SystemColors.Control;
            isDragging = false;

            string[] fileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            int index = Array.FindIndex(fileList, s => s.ToLower().EndsWith(".ass"));

            if (index >= 0)
            {
                ImportOrigAss(fileList[index]);
            }
        }

        private void grpOrigAss_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
                grpOrigAss.BackColor = Color.LightBlue;
                isDragging = true;
            }
            else
                e.Effect = DragDropEffects.None;
        }

        private void grpOrigAss_DragOver(object sender, DragEventArgs e)
        {
            if (isDragging && grpOrigAss.ClientRectangle.Contains(lstToChange.PointToClient(new Point(e.X, e.Y))))
            {
                grpOrigAss.BackColor = Color.LightBlue;
            }
        }

        private void grpOrigAss_DragLeave(object sender, EventArgs e)
        {
            grpOrigAss.BackColor = SystemColors.Control;
            isDragging = false;
        }

        private void grpAssWithActors_DragDrop(object sender, DragEventArgs e)
        {
            grpAssWithActors.BackColor = SystemColors.Control;
            isDragging = false;

            string[] fileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            int index = Array.FindIndex(fileList, s => s.ToLower().EndsWith(".ass"));

            if (index >= 0)
            {
                ImportAssWithActors(fileList[index]);
            }
        }

        private void grpAssWithActors_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
                grpAssWithActors.BackColor = Color.LightBlue;
                isDragging = true;
            }
            else
                e.Effect = DragDropEffects.None;
        }

        private void grpAssWithActors_DragOver(object sender, DragEventArgs e)
        {
            if (isDragging && grpAssWithActors.ClientRectangle.Contains(lstToChange.PointToClient(new Point(e.X, e.Y))))
            {
                grpAssWithActors.BackColor = Color.LightBlue;
            }
        }

        private void grpAssWithActors_DragLeave(object sender, EventArgs e)
        {
            grpAssWithActors.BackColor = SystemColors.Control;
            isDragging = false;
        }
        #endregion

        #endregion

        #region Batch import creator Tab

        #region Additional methods
        private bool ReadBatchAssFiles(string dirPath)
        {
            string[] assFilePaths = Directory.GetFiles(dirPath, "*.ass");
            bool isAnyAssImported = false;

            if (assFilePaths.Length > 0)
            {
                assFilePaths = assFilePaths.OrderBy(s => s).ToArray();

                foreach (string assFilePath in assFilePaths)
                {
                    AssFile singleAss = new AssFile();
                    if (singleAss.ImportAss(assFilePath))
                    {
                        if (!isAnyAssImported) batchFiles.Clear();

                        batchFiles.Add(singleAss);
                        isAnyAssImported = true;
                    }
                }

                if (isAnyAssImported)
                {
                    SaveMainActorsForBatch(); //need to save before path didn't change

                    batchDirPath = dirPath;
                    lblPathForBatch.Text = $"Path: {batchDirPath}";
                    toolTipBatchPath.SetToolTip(lblPathForBatch, batchDirPath);

                    lstAssFiles.Items.Clear();
                    lstAssFiles.Items.AddRange(batchFiles.Select(s => s.FileName).ToArray());
                }
            }

            return isAnyAssImported;
        }

        private void SaveMainActorsForBatch()
        {
            if (!Directory.Exists(batchDirPath) || batchMainActors.Count(s => s.Count(c => c == ':') == 1 &&
                                                                             !s.EndsWith(":") &&
                                                                             !s.StartsWith(":")) < 1) return;

            string actorsPath = Path.Combine(batchDirPath, IMPORT_DIRNAME);

            try
            {
                if (!Directory.Exists(actorsPath)) Directory.CreateDirectory(actorsPath);
                lastSavedMainActors.Clear();
                lastSavedMainActors.AddRange(batchMainActors.Where(s => s.Count(c => c == ':') == 1 && !s.EndsWith(":") && !s.StartsWith(":"))
                                                     .ToList());
                File.WriteAllLines(
                    Path.Combine(actorsPath, $"{MAIN_ACTORS_FILENAME}"),
                    lastSavedMainActors
                    );

                lblStatusBatch.Text = "Main actors saved to file";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool ReadMainActorsFile(string dirPath)
        {
            if (!Directory.Exists(dirPath)) return false;

            string fileNamePath = Path.Combine(dirPath, $"{MAIN_ACTORS_FILENAME}");
            if (!File.Exists(fileNamePath)) return false;

            try
            {
                string[] actorsFile = File.ReadAllLines(fileNamePath);
                List<string> mainActors = actorsFile.Where(s => s.Count(c => c == ':') == 1 && !s.EndsWith(":") && !s.StartsWith(":")).ToList();

                if (mainActors.Count < 1) return false;

                mainActors.Sort();

                batchMainActors.Clear();
                batchMainActors.AddRange(mainActors);

                lastSavedMainActors.Clear();
                lastSavedMainActors.AddRange(mainActors);

                foreach (AssFile ass in batchFiles)
                {
                    ass.UpdateImport(batchMainActors, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        private void ReadImportFiles(string dirPath)
        {
            if (!Directory.Exists(dirPath)) return;

            string[] importFilePaths = Directory.GetFiles(dirPath, "*.txt");
            try
            {
                if (importFilePaths.Length > 0)
                {
                    importFilePaths = importFilePaths.OrderBy(s => s).ToArray();

                    foreach (string importFilePath in importFilePaths)
                    {
                        if (importFilePath.EndsWith($"{MAIN_ACTORS_FILENAME}")) continue;

                        string[] importFile = File.ReadAllLines(importFilePath);
                        if (AssFile.IsImportFile(importFile.ToList()))
                            foreach (AssFile ass in batchFiles)
                            {
                                if (!ass.IsSameImportActors(importFile)) continue;
                                ass.UpdateImport(importFile);
                                break;
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool ReadDirectory(string dirPath)
        {
            StartReadingFiles();
            bool isDirRead = false;
            // if found ass files
            if (ReadBatchAssFiles(dirPath))
            {
                UpdateLabels();
                isDirRead = true;

                string actorDirPath = Path.Combine(batchDirPath, IMPORT_DIRNAME);
                ReadImportFiles(actorDirPath); //main actors will overwrite this changes (don't change order!)

                if (!ReadMainActorsFile(actorDirPath))
                {
                    batchMainActors.Clear();
                    lastSavedMainActors.Clear();
                }

                if (mainActorsSelector != null && !mainActorsSelector.IsDisposed)
                {
                    mainActorsSelector.SetMainActorsAfterReload(batchMainActors);
                    mainActorsSelector.SetAllocedActors(GetAllSelectedImports());
                }
            }

            EndReadingFiles();
            return isDirRead;
        }

        private void OpenBatchSelector()
        {
            if (mainActorsSelector == null || mainActorsSelector.IsDisposed)
            {
                mainActorsSelector = new BatchMainActorsSelector(
                    GetAllSelectedImports(),
                    batchMainActors, lastSavedMainActors); // Created only if it doesn't exist


                mainActorsSelector.Size = new Size(451, 360);
                //Set position
                var offset = new Point(-this.Width - 30, -18);
                mainActorsSelector.Location = new Point(this.Location.X + offset.X, this.Location.Y + offset.Y);
                
                //Event hookups
                mainActorsSelector.ChangeMainActorsEvent += mainActors =>
                {
                    batchMainActors.Clear();
                    batchMainActors.AddRange(mainActors);
                    batchMainActors.Sort();

                    foreach (AssFile ass in batchFiles)
                    {
                        ass.UpdateImport(batchMainActors, true);
                    }
                };
                mainActorsSelector.SaveMainActorsEvent += () => SaveMainActorsForBatch();
                mainActorsSelector.MainActorsChangedEvent += () => lblStatusBatch.Text = "Main actors saved";
                mainActorsSelector.FormClosed += (s, e) => mainActorsSelector = null; // Reset when closed
            }

            mainActorsSelector.Show();  // Show the form
            mainActorsSelector.BringToFront(); // Bring to front if already open
        }

        private List<string> GetAllSelectedImports()
        {
            if(lstAssFiles.SelectedItems.Count < 1)
                return batchFiles.Where(s => lstAssFiles.Items.Contains(s.FileName))
                              .SelectMany(s => s.GetAllocatedImport())
                              .Distinct()
                              .ToList();

            return batchFiles.Where(s => lstAssFiles.SelectedItems.Contains(s.FileName))
                              .SelectMany(s => s.GetAllocatedImport())
                              .Distinct()
                              .ToList();
        }
        #endregion

        #region Button click events
        private void btnSelectDirForBatch_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.Description = "Choose directory with subtitles";
            folderBrowserDialog.SelectedPath = batchDirPath;

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                if (batchDirPath != folderBrowserDialog.SelectedPath)
                    if(ReadDirectory(folderBrowserDialog.SelectedPath))
                        lblStatusBatch.Text = "Ass files imported";
            }
        }

        private void btnSelectMainActors_Click(object sender, EventArgs e)
        {
            lblStatusBatch.Text = "Main actors selector form opened";
            OpenBatchSelector();
        }

        private void btnCreateImports_Click(object sender, EventArgs e)
        {
            bool isItemsSelected = true;
            if (lstAssFiles.SelectedItems.Count == 0) isItemsSelected = false;

            string importsDirPath = Path.Combine(batchDirPath, IMPORT_DIRNAME);
            if (!Directory.Exists(importsDirPath)) Directory.CreateDirectory(importsDirPath);

            bool isImportsSaved = false;

            foreach (string item in lstAssFiles.Items)
            {
                if (isItemsSelected && !lstAssFiles.SelectedItems.Cast<string>().Contains(item))
                    continue;

                if(batchFiles.FirstOrDefault(f => f.FileName == item).SaveImport(importsDirPath) && !isImportsSaved)
                {
                    isImportsSaved = true;
                }
            }

            if(isImportsSaved)
            {
                lblStatusBatch.Text = "Imports saved";
            }

        }

        private void btnGenerateTimings_Click(object sender, EventArgs e)
        {
            bool isItemsSelected = true;
            if (lstAssFiles.SelectedItems.Count == 0) isItemsSelected = false;

            List<string> timings = new List<string>();
            HashSet<string> actorsToExclude = new HashSet<string>();
            foreach (string item in lstAssFiles.Items)
            {
                if (isItemsSelected && !lstAssFiles.SelectedItems.Cast<string>().Contains(item))
                    continue;

                timings.AddRange(batchFiles.FirstOrDefault(f => f.FileName == item).GetTimings(actorsToExclude));
            }

            if (timings.Count > 0)
            {
                string timingsFileNamePath = Path.Combine(batchDirPath, IMPORT_DIRNAME);
                if (!Directory.Exists(timingsFileNamePath)) Directory.CreateDirectory(timingsFileNamePath);

                string timingsFileName = "";
                if (isItemsSelected && lstAssFiles.SelectedItems.Count == 1)
                    timingsFileName = $"[Timings] {lstAssFiles.SelectedItem}.txt";
                else
                    timingsFileName = "timings.txt";

                timingsFileNamePath = Path.Combine(timingsFileNamePath, timingsFileName);
                File.WriteAllLines(timingsFileNamePath, timings);

                lblStatusBatch.Text = "Timings generated";
            }
            else MessageBox.Show("No timings to generate");
        }

        private void btnReloadFiles_Click(object sender, EventArgs e)
        {
            if(ReadDirectory(batchDirPath))
                lblStatusBatch.Text = "Ass files reloaded";
        }

        private void btnLoadAssNimport_Click(object sender, EventArgs e)
        {
            if (ImportSubFile(Path.Combine(batchDirPath, lstAssFiles.SelectedItem.ToString() + ".ass"), false))
            {
                string status = "Ass file";
                AssFile foundAss = batchFiles.FirstOrDefault(f => f.FileName == lstAssFiles.SelectedItem.ToString());

                if (foundAss != null)
                {
                    List<string> allocatedActors = foundAss.GetAllocatedImport();
                    if (allocatedActors.Count > 0)
                    {
                        ImportActorsFile(allocatedActors.ToArray());
                        status += " and it's import";
                    }
                }
                
                lblStatusBatch.Text = status + " sent to Convert tab";
            }
        }
        #endregion

        #region Events to change buttons
        private void lstAssFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateLabels();
            if (lstAssFiles.SelectedItems.Count == 1) btnLoadAssNimport.Enabled = true;
            else btnLoadAssNimport.Enabled = false;

            if(mainActorsSelector != null && !mainActorsSelector.IsDisposed)
            {
                mainActorsSelector.SetAllocedActors(GetAllSelectedImports());
            }
        }
        #endregion

        #region Additional methods to change buttons/labels
        private void StartReadingFiles()
        {
            btnSelectDirForBatch.Enabled = false;
            btnSelectMainActors.Enabled = false;
            btnCreateImports.Enabled = false;
            btnGenerateTimings.Enabled = false;
            btnReloadFiles.Enabled = false;
            btnLoadAssNimport.Enabled = false;
        }

        private void EndReadingFiles()
        {
            btnSelectDirForBatch.Enabled = true;

            if (lstAssFiles.Items.Count > 0)
            {
                btnSelectMainActors.Enabled = true;
                btnCreateImports.Enabled = true;
                btnGenerateTimings.Enabled = true;
                btnReloadFiles.Enabled = true;
            }
        }

        private void UpdateLabels()
        {
            if (lstAssFiles.SelectedItems.Count < 1)
            {
                lblBatchAssSelected_Change(0, batchFiles.Count);
                lblBatchLinesWithActors_Change(
                    batchFiles.Sum(e => e.GetAllocatedLinesNum()),
                    batchFiles.Sum(e => e.GetSubLineNum())
                    );
                lblBatchTotalActors_Change(
                    batchFiles.SelectMany(e => e.GetActors()).Distinct().Count()
                    );
            }
            else
            {
                int selectedFilesNum = 0;
                int allocedLinesNum = 0;
                int totalLinesNum = 0;
                HashSet<string> totalActors = new HashSet<string>();

                List<string> selectedItems = lstAssFiles.SelectedItems.Cast<string>().ToList();
                foreach (string item in selectedItems)
                {
                    AssFile foundAss = batchFiles.FirstOrDefault(f => f.FileName == item);
                    if (foundAss != null)
                    {
                        ++selectedFilesNum;
                        allocedLinesNum += foundAss.GetAllocatedLinesNum();
                        totalLinesNum += foundAss.GetSubLineNum();
                        totalActors.UnionWith(foundAss.GetActors());
                    }
                }

                lblBatchAssSelected_Change(selectedFilesNum, batchFiles.Count);
                lblBatchLinesWithActors_Change(
                    allocedLinesNum,
                    totalLinesNum
                    );
                lblBatchTotalActors_Change(
                    totalActors.Count
                    );
            }
        }

        //labels change
        private void lblBatchAssSelected_Change(int firstNum, int secondNum)
        {
            lblBatchAssSelected.Text = $"ASS selected\r\n{firstNum}/{secondNum}";
        }

        private void lblBatchLinesWithActors_Change(int firstNum, int secondNum)
        {
            lblBatchLinesWithActors.Text = $"Lines allocated\r\n{firstNum}/{secondNum}";
        }

        private void lblBatchTotalActors_Change(int num)
        {
            lblBatchTotalActors.Text = $"Actors number\r\n{num}";
        }
        #endregion

        #region Drag and Drop
        private void lstAssFiles_DragDrop(object sender, DragEventArgs e)
        {
            lstAssFiles.BackColor = SystemColors.Window;
            isDragging = false;

            string[] fileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            if (Directory.Exists(fileList[0]) && batchDirPath != fileList[0])
                if(ReadDirectory(fileList[0]))
                    lblStatusBatch.Text = "Ass files imported";
        }

        private void lstAssFiles_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
                lstAssFiles.BackColor = Color.LightBlue;
                isDragging = true;
            }
            else
                e.Effect = DragDropEffects.None;
        }

        private void lstAssFiles_DragOver(object sender, DragEventArgs e)
        {
            if (isDragging && lstAssFiles.ClientRectangle.Contains(lstAssFiles.PointToClient(new Point(e.X, e.Y))))
            {
                lstAssFiles.BackColor = Color.LightBlue;
            }
        }

        private void lstAssFiles_DragLeave(object sender, EventArgs e)
        {
            lstAssFiles.BackColor = SystemColors.Window;
            isDragging = false;
        }
        #endregion

        #endregion

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveMainActorsForBatch();
        }

    }
}
