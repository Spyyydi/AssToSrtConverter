using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ass_to_Srt_roles_allocator
{
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

        public static readonly char[] ACTOR_SEPARATORS = { '/', '&', '|', '\\', '\n', ';' };
        public const char GENERAL_SEPARATOR = ';';

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

        public static bool IsVectorDrawing(string line)
        {
            line = ExtractDialogue(line, true);
            //simple pattern of vector drawing
            string pattern = @"m\s*-?\d+(\.\d+)?\s+-?\d+(\.\d+)?(\s*[nlb]\s*-?\d+(\.\d+)?\s+-?\d+(\.\d+)?)*";

            return Regex.IsMatch(line, pattern);
        }

        public static string[] SplitActors(string actor)
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

        public static string ReplaceActor(string lineToReplaceActor, string actor)
        {
            int beforeActor = GetSpecificFormatIndex(lineToReplaceActor, Actor);
            int afterActor = GetSpecificFormatIndex(lineToReplaceActor, MarginL) - 1;

            return lineToReplaceActor.Substring(0, beforeActor) + actor + lineToReplaceActor.Substring(afterActor);
        }

        public static string ExtractActor(string line)
        {
            int actorIndex = GetSpecificFormatIndex(line, Actor);
            int actorLength = line.IndexOf(',', actorIndex) - actorIndex;
            return line.Substring(actorIndex, actorLength).Trim();
        }

        public static string[] ExtractAssTime(string line)
        {
            int timeStartIndex = GetSpecificFormatIndex(line, Start);
            int timeStartLength = line.IndexOf(',', timeStartIndex) - timeStartIndex;
            string timeStart = line.Substring(timeStartIndex, timeStartLength).Trim();

            int timeEndIndex = GetSpecificFormatIndex(line, End);
            int timeEndLength = line.IndexOf(',', timeEndIndex) - timeEndIndex;
            string timeEnd = line.Substring(timeEndIndex, timeEndLength).Trim();

            return new string[] { timeStart, timeEnd };
        }

        public static string ExtractDialogue(string line, bool removeAssFormating, bool keepNewLines = false)
        {
            //extract dialogue
            string dialogue = line.Substring(GetSpecificFormatIndex(line, Text));

            if (string.IsNullOrWhiteSpace(dialogue))
            {
                return "";
            }

            //remove ass text formating
            if (removeAssFormating)
            {
                dialogue = Regex.Replace(dialogue, "{.*?}", string.Empty);
            }

            //get rid of chars that starts with backslash ex. \t \h
            /* Regular expression explanation
             * (?<!{[^{}]*) : Makes sure that there are no opening curly braces behind chars that starts with backslash
             * \\. : Matches any occurance of character that starts with backslash except \N
             * (?![^{}]*}) : Makes sure that there are no closing curly braces after chars that starts with backslash
             */
            dialogue = Regex.Replace(dialogue, @"(?<!{[^{}]*)\\(?!N)(?![^{}]*})", " ");

            if (keepNewLines)
            {
                dialogue = Regex.Replace(dialogue, @"\\N", "\n");
            }
            else
            {
                dialogue = Regex.Replace(dialogue, @"\\N", " ");
            }

            // Replace multiple spaces/tabs but preserve newlines
            dialogue = Regex.Replace(dialogue, @"[^\S\n]+", " ");

            // Trim spaces around newlines
            dialogue = Regex.Replace(dialogue, @" *\n *", "\n");

            return dialogue.Trim();
        }
    }
}
