using Ass_to_Srt_roles_allocator.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using System.Threading;
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
        readonly char[] ACTOR_SEPARATORS = { '/', '&', '|', '\\', '\n', ';' };
        const char GENERAL_SEPARATOR = ';';
        List<string> subtitles;

        bool isDragging = false;

        string origAssPath = "";
        List<string> origAss;
        List<string> assWithActors;

        System.Windows.Forms.Timer scrollTimer;
        bool isQuickScrollEnabled;

        public Form1()
        {
            InitializeComponent();
            subtitles = new List<string>();
            cmbDubers.Items.Clear();
            cmbDubers.Items.AddRange(Settings.Default.Dubers.Split(';'));
            lblAllocatedActors.ContextMenuStrip = contextMenuStrip;

            origAss = new List<string>();
            assWithActors = new List<string>();
            grpOrigAss.AllowDrop = true;
            grpAssWithActors.AllowDrop = true;

            scrollTimer = new System.Windows.Forms.Timer();
            scrollTimer.Interval = 100;
            scrollTimer.Tick += ScrollTimer_Tick;

            isQuickScrollEnabled = false;
        }

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

                if (!subs.Any(s => s.StartsWith("Dialogue") && ExtractDialogue(s, false) != ""))
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
                        if (!IsVectorDrawing(lineToCompare) && ExtractDialogue(lineToCompare, false) != "")
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

        private bool ImportAssWithActorsSubtitles(string filePath, string fileName, ref int subLines, ref int notAllocdNum)
        {
            try
            {
                string[] subs = File.ReadAllLines(Path.Combine(filePath, fileName));
                if (!subs.Any(s => s.StartsWith("Dialogue") && ExtractActor(s) != ""))
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
                        if (!IsVectorDrawing(lineToCompare))
                        {
                            if (ExtractActor(lineToCompare) != "")
                            {
                                assWithActors.Add(lineToCompare);
                            }
                            else
                            {
                                ++notAllocdNum;
                            }

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

        private bool IsVectorDrawing(string line)
        {
            line = ExtractDialogue(line, true);
            //simple pattern of vector drawing
            string pattern = @"m\s*-?\d+(\.\d+)?\s+-?\d+(\.\d+)?(\s*[nlb]\s*-?\d+(\.\d+)?\s+-?\d+(\.\d+)?)*";

            return Regex.IsMatch(line, pattern);
        }

        private bool ImportSubFile(string subFilePath, bool isLoad)
        {
            int fileNameStartIndex = subFilePath.LastIndexOf('\\') + 1;
            string filePath = subFilePath.Substring(0, fileNameStartIndex);
            string fileName = subFilePath.Substring(fileNameStartIndex);
            path = Path.Combine(filePath, fileName.Substring(0, fileName.LastIndexOf(".")) + ".srt");


            if (!ImportSubtitles(filePath, fileName, isLoad))
            {
                MessageBox.Show("There are no valid subtitles to convert");
                return false;
            }

            lblFilePath.Text = "File name: " + fileName;
            toolTipFileName.SetToolTip(lblFilePath, fileName);


            string emptyActors = LoadActors();
            toolTipActorsLoaded.SetToolTip(lblLoadStatus, "Empty actors: " + emptyActors);
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

            return true;
        }

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

            int totalSubLines = origAss.Count(s => s.StartsWith("Dialogue") && !IsVectorDrawing(s));
            int actorsAllocdNum = origAss.Count(s => s.StartsWith("Dialogue") && !IsVectorDrawing(s) && ExtractActor(s) != "");

            lblFileNameOrigAss.Text = "File name: " + fileName;
            toolTipFileNameOrigAss.SetToolTip(lblFileNameOrigAss, fileName);

            lblSubLinesOrigAss.Text = "Total sub lines: " + totalSubLines;
            lblAllocActorsNumOrigAss.Text = "Allocated actors: " + actorsAllocdNum;
            lblNotAllocdActorsNumOrigAss.Text = "Not allocated actors: " + (totalSubLines - actorsAllocdNum).ToString();

            changeSyncBtn();
            btnSaveAss.Enabled = false;
            btnLoadAss.Enabled = false;
            btnOpenReport.Enabled = false;
            richSyncReport.Clear();
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
            lblAllocActorsNumAssWithActors.Text = "Allocated actors: " + (totalSubLines - actorsNotAllocdNum).ToString();
            lblNotAllocdActorsNumAssWithActors.Text = "Not allocated actors: " + actorsNotAllocdNum;

            changeSyncBtn();
            btnOpenReport.Enabled = false;
            richSyncReport.Clear();
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

            toolTipAllocatedActors.SetToolTip(lblAllocatedActors,
                                            $"Right click to see/select not allocated actors ({contextMenuStrip.Items.Count})");
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

        private string[] ExtractAssTime(string line)
        {
            int timeStartIndex = AssFormat.GetSpecificFormatIndex(line, AssFormat.Start);
            int timeStartLength = line.IndexOf(',', timeStartIndex) - timeStartIndex;
            string timeStart = line.Substring(timeStartIndex, timeStartLength).Trim();

            int timeEndIndex = AssFormat.GetSpecificFormatIndex(line, AssFormat.End);
            int timeEndLength = line.IndexOf(',', timeEndIndex) - timeEndIndex;
            string timeEnd = line.Substring(timeEndIndex, timeEndLength).Trim();

            return new string[] { timeStart, timeEnd };
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

        private string ExtractDialogue(string line, bool removeAssFormating)
        {
            //extract dialogue
            string dialogue = line.Substring(AssFormat.GetSpecificFormatIndex(line, AssFormat.Text));

            if (string.IsNullOrWhiteSpace(dialogue))
            {
                return "";
            }

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
                        if (importedDubersCount == 0)
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

        private void ImportActorsFile(string actorsNdubersFilePath)
        {
            int fileNameStartIndex = actorsNdubersFilePath.LastIndexOf('\\') + 1;
            string filePath = actorsNdubersFilePath.Substring(0, fileNameStartIndex);
            string fileName = actorsNdubersFilePath.Substring(fileNameStartIndex);

            string outputMessage = ConvertImportedFileToList(filePath, fileName);
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

        private string ConvertAssToSrt(bool removeAssFormating, bool allocateActors, string separateActorsPerFile)
        {
            string srtSub = "";

            //convert from ass to srt
            int lineNum = 1;
            int subLen = subtitles.Count;
            bool isDubbersAllocated = lstToChange.Items.Cast<string>().Any(s => s.Contains(RIGHT_ARROW));

            if (subLen > 0)
                foreach (string line in subtitles)
                {
                    if (separateActorsPerFile != "")
                    {
                        string actor = ExtractActor(line);
                        string[] actors = SplitActors(actor);

                        string[] separatedActor = SplitActors(separateActorsPerFile);

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
                                if (actor.Any(c => ACTOR_SEPARATORS.Contains(c)))
                                    actor = string.Join(", ", actors);
                                srtSub += "[" + actor + "] ";
                            }

                            srtSub += ExtractDialogue(line, removeAssFormating);
                        }
                    }
                    else
                    {
                        srtSub += lineNum.ToString() + "\n";
                        srtSub += ExtractSrtTime(line) + "\n";

                        if (allocateActors)
                        {
                            //Extract actor
                            string actor = ExtractActor(line);
                            //if multiple actors for the subtitle line
                            if (actor.Any(c => ACTOR_SEPARATORS.Contains(c)))
                            {
                                //split actors and find duber for each actor (if duber not found then put actor instead)
                                string[] actors = SplitActors(actor);
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
                                        if (lstToChange.Items.Contains(s))
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
                                    if (lstToChange.Items.Contains(actor))
                                        srtSub += "[" + actor + "] ";
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

        private string GetCurrentTime()
        {
            return "[" + DateTime.Now.ToString("HH:mm:ss.fff") + "]";
        }

        private string ReplaceActor(string lineToReplaceActor, string actor)
        {
            int beforeActor = AssFormat.GetSpecificFormatIndex(lineToReplaceActor, AssFormat.Actor);
            int afterActor = AssFormat.GetSpecificFormatIndex(lineToReplaceActor, AssFormat.MarginL) - 1;

            return lineToReplaceActor.Substring(0, beforeActor) + actor + lineToReplaceActor.Substring(afterActor);
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
                return ReplaceActor(line, ExtractActor(assWithActors[index]));
            }
            else
            {
                index = assWithActors.FindIndex(s => lineStartTimeWithMs < AssFormat.GetTimeKey(s, AssFormat.End, withMs));

                if (index != -1)
                {
                    if (index + 1 < assWithActors.Count)
                    {
                        if (lineEndTimeWithMs <= AssFormat.GetTimeKey(assWithActors[index + 1], AssFormat.Start, withMs))
                        {
                            int lineStartTime = AssFormat.GetTimeKey(line, AssFormat.Start, !withMs);

                            if (lineStartTime >= AssFormat.GetTimeKey(assWithActors[index], AssFormat.Start, !withMs))
                            {
                                return ReplaceActor(line, ExtractActor(assWithActors[index]));
                            }

                            return "";
                        }
                        else
                        {
                            //origAss line may contain multiple sync options
                            //if all options have same actors synchronize
                            string initialActor = ExtractActor(assWithActors[index]);
                            int count = assWithActors.Count;

                            for (int i = index + 1; i < count; ++i)
                            {
                                if (lineEndTimeWithMs > AssFormat.GetTimeKey(assWithActors[i], AssFormat.Start, withMs))
                                {
                                    if (initialActor.ToLower() != ExtractActor(assWithActors[i]).ToLower())
                                    {
                                        initialActor = "";
                                        break;
                                    }
                                }
                                else break;
                            }

                            if (initialActor != "")
                            {
                                return ReplaceActor(line, initialActor);
                            }
                        }
                    }
                    else
                    {
                        return ReplaceActor(line, ExtractActor(assWithActors[index]));
                    }
                }
            }

            return "";
        }
        #endregion

        #region Convert Tab

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

            allocateActors = lstToChange.Items.Count > 0;
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
            openFileDialog.Filter = "Text files|*.txt";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ImportActorsFile(openFileDialog.FileName);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            saveFileDialog.Title = "Choose where to export";
            string fileName = toolTipFileName.GetToolTip(lblFilePath);
            fileName = fileName.Substring(0, fileName.Length - 4);
            saveFileDialog.FileName = "(Actors)" + fileName + ".txt";
            saveFileDialog.Filter = "Text files|*.txt";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.WriteAllText(saveFileDialog.FileName, ConvertToChangeListForExport());
                    MessageBox.Show("List successfully exported");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
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
                ImportActorsFile(fileList[index]);
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
                if (origAss[i].StartsWith("Dialogue") && !IsVectorDrawing(origAss[i]))
                {
                    if (overwriteActors || ExtractActor(origAss[i]) == "")
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
                            origAss[i] = ReplaceActor(origAss[i], "");
                            report = $"Line {i + 1} not synced.";
                        }

                        if (report != "")
                        {
                            string[] startAndEndTime = ExtractAssTime(origAss[i]);
                            UpdateReport(GetCurrentTime() + $" {report} " + "Timing: " + startAndEndTime[0] + " --> " + startAndEndTime[1]);
                        }

                        ++triedToSyncNum;
                    }
                }
            }

            UpdateReport(GetCurrentTime() + $" {syncedActorsNum}/{triedToSyncNum} actors synced successfully");
            
            if(wrongSyncNum > 0)
                UpdateReport(GetCurrentTime() + $" {wrongSyncNum}/{syncedActorsNum} possible wrong sync");


            // Update labels of origAss
            int newAllocedActorsNum = origAss.Count(s => s.StartsWith("Dialogue") && !IsVectorDrawing(s) && ExtractActor(s) != "");
            int newNotAllocedActorsNum = origAss.Count(s => s.StartsWith("Dialogue") && !IsVectorDrawing(s)) - newAllocedActorsNum;

            lblAllocActorsNumOrigAss.Text = "Allocated actors: " + newAllocedActorsNum;
            lblNotAllocdActorsNumOrigAss.Text = "Not allocated actors: " + newNotAllocedActorsNum;


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

        public static int GetTimeKey(string subLine, int type, bool isWithMs)
        {
            if ((type != 0 && type != 1) || subLine == "") return -1;

            int timeStartIndex = GetSpecificFormatIndex(subLine, type);
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
                    if (isWithMs)
                        return (hour * 3600 + minute * 60 + second) * 1000 + millisecond;
                    else
                        return (hour * 3600 + minute * 60 + second);
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
                const bool withMs = true;
                int compareByStartTime = GetTimeKey(subtitle1, Start, withMs).CompareTo(GetTimeKey(subtitle2, Start, withMs));

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
