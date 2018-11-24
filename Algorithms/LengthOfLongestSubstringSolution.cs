using System;
using System.Collections.Generic;

namespace Algorithms
{
    //Given a string, find the length of the longest substring without repeating characters.
    //Example 1:
    //Input: "abcabcbb"
    //Output: 3 
    //Explanation: The answer is "abc", with the length of 3. 
    //Example 2:
    //Input: "bbbbb"
    //Output: 1
    //Explanation: The answer is "b", with the length of 1.
    //Example 3:
    //Input: "pwwkew"
    //Output: 3
    //Explanation: The answer is "wke", with the length of 3. 
    //Note that the answer must be a substring, "pwke" is a subsequence and not a substring.
    public class LengthOfLongestSubstringSolution : ISolution<int, string>
    {
        public int Solve(string s)
        {
            return SlidingWindowOptimizedWithForLoop(s);
        }

        private int SlidingWindowOptimizedWithForLoop(string s)
        {
            var start = 0;
            var map = new Dictionary<char, int>();
            var answer = 0;

            for (var end = 0; end < s.Length; end++)
            {
                if (map.ContainsKey(s[end]))
                    start = Math.Max(map[s[end]] + 1, start);

                map[s[end]] = end;
                answer = Math.Max(answer, end - start + 1);
            }

            return answer;
        }

        private int SlidingWindowOptimized(string s)
        {
            var start = 0;
            var end = 0;
            var map = new Dictionary<char, int>();
            var answer = 0;

            while (end < s.Length)
            {
                if (map.ContainsKey(s[end]))
                    start = Math.Max(map[s[end]] + 1, start);
                
                map[s[end]] = end;
                end++;
                answer = Math.Max(answer, end - start);
            }

            return answer;
        }

        private int SlidingWindow(string s)
        {
            var start = 0;
            var end = 0;
            var set = new HashSet<char>();
            var answer = 0;

            while (end < s.Length)
            {
                if (set.Contains(s[end]))
                    set.Remove(s[start++]);
                else
                {
                    set.Add(s[end++]);
                    answer = Math.Max(answer, end - start);
                }
            }

            return answer;
        }

        private int MySlidingWindowSingleLoop(string s)
        {
            var currentSubstring = string.Empty;
            var longestSubstringLength = 0;
            var currentSubstringCharSet = new HashSet<char>();

            for (var i = 0; i < s.Length; i++)
            {
                var chr = s[i];
                if (currentSubstringCharSet.Contains(chr))
                {
                    //process current substring and update result if necessary
                    if (currentSubstring.Length > longestSubstringLength)
                        longestSubstringLength = currentSubstring.Length;

                    //get the part of the last substring without repeating characters
                    var indexOfRepeatingChar = currentSubstring.IndexOf(chr);
                    if (indexOfRepeatingChar == currentSubstring.Length - 1)
                    {
                        //have to start over in this case
                        //check the length of the rest of the string and if necessary initialize new substring
                        if (s.Length - i <= longestSubstringLength)
                            return longestSubstringLength;

                        currentSubstring = chr.ToString();
                        currentSubstringCharSet = new HashSet<char>(new List<char> { chr });
                    }
                    else
                    {
                        var remainingSubstring = currentSubstring.Substring(indexOfRepeatingChar + 1);
                        currentSubstring = remainingSubstring + chr;

                        //assuming the rest of the string does not have repeating chars, check if the max length can exceed longest substring length until now, if not return
                        if (currentSubstring.Length + s.Length - (i + 1) <= longestSubstringLength)
                            return longestSubstringLength;

                        //reinitialize set of known chars
                        currentSubstringCharSet = new HashSet<char>(currentSubstring);
                    }
                }
                else
                {
                    //advance current substring
                    currentSubstring += chr.ToString();
                    currentSubstringCharSet.Add(chr);
                }
            }

            return currentSubstring.Length > longestSubstringLength ? currentSubstring.Length : longestSubstringLength;
        }
    }
}
