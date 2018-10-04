using System;
using System.Collections.Generic;
using System.Text;

namespace ScrambledWord {
    class WordMatcher {
        /// <summary>
        /// find a possible matching between the scrambled word and the dictionary
        /// </summary>
        /// <param name="scrambledWords">scrambled word</param>
        /// <param name="wordList">all line of dictionary</param>
        /// <returns>list of matched word</returns>
        public List<MatchedWord> Match(string[] scrambledWords, string[] wordList) {
            List<MatchedWord> myList = new List<MatchedWord>();
            foreach (string scrambledWord in scrambledWords)
                foreach (string word in wordList) {
                    if (scrambledWord.Equals(word, StringComparison.OrdinalIgnoreCase)) {
                        myList.Add(BuildMatchedWord(scrambledWord, word));
                    } else {
                        Array.Sort(scrambledWord.ToCharArray());
                        Array.Sort(word.ToCharArray());
                        string sortedScrambled = new String(scrambledWord.ToCharArray());
                        string sortedWord = new String(word.ToCharArray());
                        if (sortedScrambled.Equals(sortedWord, StringComparison.OrdinalIgnoreCase))
                            myList.Add(BuildMatchedWord(scrambledWord, word));
                    }
                }
            return myList;
        }

        /// <summary>
        /// Allow to insert into scrambled word into the struct
        /// </summary>
        /// <param name="scrambled">scrambled word</param>
        /// <param name="word">relative word</param>
        /// <returns>object MatchedWord with the data</returns>
        private MatchedWord BuildMatchedWord(string scrambled, string word) {
            MatchedWord newMatch = new MatchedWord {
                ScrambledWord = scrambled,
                Word = word
            };
            return newMatch;
        }

    }

    /// <summary>
    /// struct of matched word
    /// </summary>
    struct MatchedWord {
        public string ScrambledWord { get; set; }
        public string Word { get; set; }
    }
}
