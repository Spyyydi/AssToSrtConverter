using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ass_to_Srt_roles_allocator
{
    class AssFile
    {
        public string FileName { get; private set; }

        private List<string> Subs;
        private List<string> Actors;
        private List<string> Import;

        public AssFile()
        {
            Subs = new List<string>();
            Actors = new List<string>();
            Import = new List<string>();
            FileName = "";
        }

        public bool ImportAss(string assFilePath)
        {
            if(ImportFile(assFilePath))
            {
                FileName = assFilePath.Substring(assFilePath.LastIndexOf('\\') + 1).Trim();
                FileName = FileName.Substring(0, FileName.IndexOf(".ass"));
                return true;
            }

            return false;
        }

        private bool ImportFile(string path)
        {
            if (!File.Exists(path))
            {
                return false;
            }

            try
            {
                string[] subs = File.ReadAllLines(path);
                if (!subs.Any(s => s.StartsWith("Dialogue") && AssFormat.ExtractActor(s) != ""))
                {
                    return false;
                }

                Subs.Clear();
                Subs.AddRange(subs.Where(s => s.StartsWith("Dialogue") && 
                                              !AssFormat.IsVectorDrawing(s) && 
                                              AssFormat.ExtractDialogue(s, true) != "")
                                  .Distinct()
                             );
                
                for(int i = 0; i < Subs.Count; ++i)
                {
                    if (AssFormat.ExtractActor(Subs[i]) == "")
                    {
                        Subs[i] = AssFormat.ReplaceActor(Subs[i], "EMPTY ACTOR");
                    }
                }

                //sort subtitles by time
                Subs.Sort(new AssFormat.SubtitleComparer(new List<string>(Subs)));

                Actors = Subs.SelectMany(s => AssFormat.SplitActors(AssFormat.ExtractActor(s)))
                             .Distinct().ToList();
                
                Actors.Sort();

                Import = Actors.Select(s => $"{s}:").ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return true;
        }
        
        public List<string> GetActors()
        {
            return Actors;
        }

        public int GetSubLineNum()
        {
            return Subs.Count;
        }

        public int GetAllocatedLinesNum()
        {
            return Subs.Where(s => AssFormat.ExtractActor(s) != "EMPTY ACTOR").ToList().Count;
        }

        public List<string> GetAllocatedImport()
        {
            return Import.Where(s => !s.EndsWith(":")).ToList();
        }

        public static bool IsImportFile(IReadOnlyList<string> file)
        {
            if (file.Any(s => s.Count(c => c == ':') != 1 || s.StartsWith(":")))
                return false;

            return true;
        }

        public bool IsSameImportActors(IReadOnlyList<string> importActors)
        {
            return !Import.Select(s => s.Substring(0, s.IndexOf(':'))).ToList()
                          .Except(importActors.Select(s => s.Substring(0, s.IndexOf(':'))).Distinct().ToList())
                          .Any() && 
                   !importActors.Select(s => s.Substring(0, s.IndexOf(':'))).Distinct().ToList()
                                .Except(Import.Select(s => s.Substring(0, s.IndexOf(':'))).ToList())
                                .Any();
        }

        public void UpdateImport(IReadOnlyList<string> importActors, bool isMainActors = false)
        {
            //Method called: 1) to add duber from main actors 2) to import actors from existing import

            foreach(string actor in importActors)
            {
                if(actor.Count(c => c == ':') == 1 && actor.Last() != ':' && !Import.Contains(actor))
                {
                    string cleanActor = actor.Substring(0, actor.IndexOf(':') + 1);
                    List<string> matches = Import.Where(s => s.StartsWith(cleanActor)).ToList();
                    bool emptyActorFound = false;

                    foreach(string match in matches)
                    {
                        int index = Import.IndexOf(match);
                        if(isMainActors)
                        {
                            if(emptyActorFound)
                            {
                                Import[index] = "";
                                continue;
                            }

                            string newActor = Import[index].Substring(0, Import[index].IndexOf(':'));
                            Import[index] = $"{newActor}:{actor.Substring(cleanActor.Length)}";
                            emptyActorFound = true;
                        }
                        else if (Import[index].Last() == ':')
                        {
                            Import[index] += actor.Substring(cleanActor.Length);
                            emptyActorFound = true;
                            break;
                        }
                    }

                    if(!emptyActorFound && matches.Count > 0 && !isMainActors)
                    {
                        Import.Add(actor);
                    }
                }
            }

            Import.RemoveAll(s => s == "");
            Import.Sort();
        }

        public bool SaveImport(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                File.WriteAllLines(Path.Combine(path, $"[Import] {FileName}.txt"), Import);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        public List<string> GetTimings(HashSet<string> exclude)
        {
            List<string> emptyActors = Import.Where(s => s.Last() == ':')
                                             .Select(s => s.Substring(0, s.IndexOf(':')))
                                             .Distinct()
                                             .Where(actor => !exclude.Contains(actor))
                                             .ToList();

            if (emptyActors.Count < 1) return new List<string>();

            const int timingsNum = 3;

            List<string> timings = new List<string>
            {
                $"== {FileName} =="
            };

            foreach (string actor in emptyActors)
            {
                exclude.Add(actor);

                timings.Add($"[{actor}] - {Subs.Count(c => AssFormat.ExtractActor(c).Contains(actor) && AssFormat.ExtractDialogue(c, true) != "")} line(s)");

                string[] dialogues = Subs.Where(s => AssFormat.ExtractActor(s).Contains(actor) &&
                                                     AssFormat.ExtractDialogue(s, true) != "")
                                         .Take(timingsNum)
                                         .ToArray();
                
                foreach(string dialogue in dialogues)
                {
                    string[] time = AssFormat.ExtractAssTime(dialogue);
                    //start time
                    time[0] = time[0].Substring(0, time[0].IndexOf('.'));
                    if (time[0][0] == '0')
                    {
                        time[0] = time[0].Substring(time[0].IndexOf(":") + 1);
                    }
                    //end time
                    time[1] = time[1].Substring(0, time[1].IndexOf('.'));
                    if (time[1][0] == '0')
                    {
                        time[1] = time[1].Substring(time[1].IndexOf(":") + 1);
                    }

                    timings.Add($"({time[0]} - {time[1]}) {AssFormat.ExtractDialogue(dialogue, true)}");
                }

                timings.Add("");
            }

            return timings;
        }
    }
}
