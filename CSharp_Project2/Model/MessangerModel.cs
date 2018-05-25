/*
Creation Date: 3/19/2017
Course: Adv Prog C# Skills
Description: MVVM Model Class
Author: Chirag Kular(ck4957)
Version: Initial Version
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Threading;
using System.IO;

namespace CSharp_Project2.Model
{
    class MessangerModel
    {
        // Dictonary to store number and corresponding letter
        Dictionary<string, string> letter_dict = new Dictionary<string, string>();

        //Dictonary to store english-words.txt
        Dictionary<string, string> word_dict = new Dictionary<string, string>();
        List<string> all_words;
        StreamReader sr;

        /// <summary>
        /// Constructor that create the loads the two dictionary
        /// </summary>
        public MessangerModel()
        {
            all_words = new List<string>();
            createDict(letter_dict);
            LoadWords();
        }

        /// <summary>
        /// Function to read the words from file and store in the dictionary
        /// </summary>
        public void LoadWords()
        {
            sr = new StreamReader(".\\english-words.txt");
            string oneline;
            while ((oneline = sr.ReadLine()) != null)
            {
                string temp = "";
                foreach (char ch in oneline)
                {
                    temp += getKey(ch.ToString());
                }
                //Console.WriteLine(temp);
                word_dict.Add(oneline, temp);
            }
        }

        /// <summary>
        /// Functions gets the matching words that starts with given input parameter
        /// </summary>
        /// <param name="query_word">button presses by user</param>
        /// <returns></returns>
        public List<string> GetWords(string query_word)
        {
            //LINQ to query the words from dictionary
            var query = from words in word_dict
                        where words.Value.StartsWith(query_word)
                        select words.Key;
            List<string> lookup_list = new List<string>();
            lookup_list.AddRange(query);

            //Sort the list first by length then alphabatically
            var sorted = lookup_list.OrderBy(s => s.Length).ThenBy(s => s);
            return sorted.ToList<String>();
        }

        /// <summary>
        /// Dictionary created to match button click with letter
        /// </summary>
        /// <param name="dict">Object of Dictionary</param>
        public void createDict(Dictionary<string, string> dict)
        {
            //1
            this.letter_dict.Add("1", "1");
            //2
            this.letter_dict.Add("2", "a");
            this.letter_dict.Add("22", "b");
            this.letter_dict.Add("222", "c");
            this.letter_dict.Add("2222", "2");
            //3
            this.letter_dict.Add("3", "d");
            this.letter_dict.Add("33", "e");
            this.letter_dict.Add("333", "f");
            this.letter_dict.Add("3333", "3");
            //4
            this.letter_dict.Add("4", "g");
            this.letter_dict.Add("44", "h");
            this.letter_dict.Add("444", "i");
            this.letter_dict.Add("4444", "4");
            //5
            this.letter_dict.Add("5", "j");
            this.letter_dict.Add("55", "k");
            this.letter_dict.Add("555", "l");
            this.letter_dict.Add("5555", "5");
            //6
            this.letter_dict.Add("6", "m");
            this.letter_dict.Add("66", "n");
            this.letter_dict.Add("666", "o");
            this.letter_dict.Add("6666", "6");
            //7
            this.letter_dict.Add("7", "p");
            this.letter_dict.Add("77", "q");
            this.letter_dict.Add("777", "r");
            this.letter_dict.Add("7777", "s");
            this.letter_dict.Add("77777", "7");
            //8
            this.letter_dict.Add("8", "t");
            this.letter_dict.Add("88", "u");
            this.letter_dict.Add("888", "v");
            this.letter_dict.Add("8888", "8");
            //9
            this.letter_dict.Add("9", "w");
            this.letter_dict.Add("99", "x");
            this.letter_dict.Add("999", "y");
            this.letter_dict.Add("9999", "z");
            this.letter_dict.Add("99999", "9");
        }

        /// <summary>
        /// Function to get the value from dictionary
        /// </summary>
        /// <param name="number">key in dictionary</param>
        /// <returns></returns>
        public string getLetter(string number)
        {
            string value = "";
            if (letter_dict.ContainsKey(number))
                value = letter_dict[number];
            return value;
        }

        /// <summary>
        /// Function to get the key based on value
        /// </summary>
        /// <param name="val">value in dictionary</param>
        /// <returns></returns>
        public string getKey(string val)
        {
            string key = "";
            key = letter_dict.FirstOrDefault(x => x.Value != null && x.Value.Contains(val)).Key;
            return key[0].ToString();
        }
    }
}
