using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Program which solve the first challenge of ReplySecuty Challenge Oct2018
/// --
/// R-boy is convinced that something’s going on! The website contains a games area: 
/// he's sure that it contains hidden messages. The first game is scrambled words. Help him find the hidden secret code!
/// You have to find the original unscrambled words, which were randomly taken from a dictionary.
/// Note:
/// The SHA-256 hash of the concatenated unscrambled words will be *the content* 
/// of the flag and it needs to be converted to lowercase.
/// To get the complete flag, insert the lowercase string obtained between 
/// “{ FLG:” and “}” without any blank space after the “:” and before the “}”.
/// Consider UTF-8 as the character encoding.Unscrambled words must have the same order
/// of scrambled ones and they must be concatenated without spaces.
/// </summary>
namespace ScrambledWord {
    class Program {
        private static readonly WordMatcher _wordMatcher = new WordMatcher();
        static string pathScrambled = @"scrambled-words.txt";
        static string pathDictionary = @"dictionary.txt";
        static string[] lineScrambled;
        static string[] lineDictionary;

        static void Main(string[] args) {
            //line of scrabled word from txt file
            lineScrambled = File.ReadAllLines(pathScrambled);

            //line of dictionary word from txt file
            lineDictionary = File.ReadAllLines(pathDictionary);

            //run match process
            DisplayUnscrambled(lineScrambled);

            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }


        private static void DisplayUnscrambled(string[] scrambledWords) {
            string[] wordList = lineDictionary;
            List<MatchedWord> matchedWords = _wordMatcher.Match(scrambledWords, wordList);
            StringBuilder sb = new StringBuilder();
            if (matchedWords.Any()) {
                Console.WriteLine("\nFounded:\n");
                foreach (var match in matchedWords) {
                    Console.WriteLine("Match found for {0}:{1}", match.ScrambledWord, match.Word);
                    sb.Append(match.Word);
                }
                Console.WriteLine("\nAppend:\n");
                Console.WriteLine(sb.ToString().ToLower());
                Console.WriteLine("\nDigest:\n");
                Console.WriteLine(GetSha256Digest(sb.ToString().ToLower()));
            } else { Console.WriteLine("No matches were found!"); }
        }

        /// <summary>
        /// generate Sha256 digest
        /// </summary>
        /// <param name="rawData">simple data</param>
        /// <returns>digest</returns>
        static string GetSha256Digest(string rawData) {
            using (SHA256 sha256Hash = SHA256.Create()) {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++) {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }

}
