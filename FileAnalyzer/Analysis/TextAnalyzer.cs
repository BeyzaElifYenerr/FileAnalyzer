using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace FileAnalyzer.Analysis
{
    public class TextAnalyzer
    {

        private static readonly HashSet<string> StopWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            // Türkçe
            "ve","ile","ama","ancak","fakat","çünkü","veya","ya","ya da","ki","de","da","bu","şu","o",
            "ben","sen","biz","siz","onlar","için","gibi","kadar","daha","çok","az","bir","şey","her",
            "mı","mi","mu","mü","ise","eğer","yani","tam","çok",
            // İngilizce
            "a","an","the","and","or","but","if","then","else","when","at","by","for","with","about",
            "between","into","through","before","after","above","below","to","from","up","down","in",
            "out","on","off","over","under","again","further","once","here","there","all","any","both",
            "each","few","more","most","other","some","such","no","nor","not","only","own","same","so",
            "than","too","very","can","will","just"
        };

        private static readonly Regex WordRegex = new Regex(@"\p{L}+", RegexOptions.Compiled);

        public Dictionary<string, int> ComputeWordFrequencies(string text, string culture = "tr-TR")
        {
            var ci = new CultureInfo(culture);
            var counts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            foreach (Match m in WordRegex.Matches(text))
            {
                var token = m.Value;
                var lowered = token.ToLower(ci);

                if (StopWords.Contains(lowered))
                    continue;

                if (lowered.Length <= 1)
                    continue;

                counts[lowered] = counts.TryGetValue(lowered, out var c) ? c + 1 : 1;
            }

            return counts
                .OrderByDescending(kv => kv.Value)
                .ThenBy(kv => kv.Key, StringComparer.OrdinalIgnoreCase)
                .ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        public Dictionary<char, int> CountPunctuation(string text)
        {
            var dict = new Dictionary<char, int>();

            foreach (var ch in text)
            {
                if (char.IsPunctuation(ch))
                {
                    dict[ch] = dict.TryGetValue(ch, out var c) ? c + 1 : 1;
                }
            }

            return dict.OrderByDescending(kv => kv.Value)
                       .ToDictionary(kv => kv.Key, kv => kv.Value);
        }
    }
}
