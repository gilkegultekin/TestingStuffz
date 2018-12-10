using System.Collections.Generic;

namespace Algorithms
{
    /*
     * Given two strings s and t, determine if they are isomorphic.

        Two strings are isomorphic if the characters in s can be replaced to get t.

        All occurrences of a character must be replaced with another character while preserving the order of characters. No two characters may map to the same character but a character may map to itself.

        Example 1:

        Input: s = "egg", t = "add"
        Output: true
        Example 2:

        Input: s = "foo", t = "bar"
        Output: false
        Example 3:

        Input: s = "paper", t = "title"
        Output: true
        Note:
        You may assume both s and t have the same length.
     */
    public class IsomorphicStringsSolution : ISolution<bool, string, string>
    {
        public bool Solve(string s, string t)
        {
            return WithArrays(s, t);
        }

        private bool WithArrays(string s, string t)
        {
            var sArray = new int[256];
            var tArray = new int[256];

            for (int i = 0; i < s.Length; i++)
            {
                var curCharInS = s[i];
                var curCharInT = t[i];

                //if the current chars have been mapped to each other before, the values in their respective arrays will be the same.
                //if there was no previous mapping, the values in both arrays will be zero.
                //if the values in the arrays differ, then there was a previous mapping that violates the isomorphicity(?) condition.
                if (sArray[curCharInS] != tArray[curCharInT])
                {
                    return false;
                }

                //create the mapping if it does not exist
                //the index is the value of the char
                //the value in the array is the index at which the char was first encountered (+1 to avoid using zero)
                sArray[curCharInS] = i + 1;
                tArray[curCharInT] = i + 1;
            }

            return true;
        }

        //single loop over the entire string
        private bool BruteForce(string s, string t)
        {
            var dict = new Dictionary<char, char>();
            var reverseDict = new Dictionary<char, char>();
            for (int i = 0; i < s.Length; i++)
            {
                var curCharInS = s[i];
                var curCharInT = t[i];
                if (!dict.ContainsKey(curCharInS))
                {
                    //a previously encountered char in s was already mapped to the current char in t ==> fail
                    if (reverseDict.ContainsKey(curCharInT))
                    {
                        return false;
                    }

                    //no mappings exist in either direction ==> create new mappings
                    dict[curCharInS] = curCharInT;
                    reverseDict[curCharInT] = curCharInS;
                }
                else
                {
                    //the char in s exists in the dictionary, hence we have to check if the previous mapping and the current char are the same
                    if (dict[curCharInS] != curCharInT)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
