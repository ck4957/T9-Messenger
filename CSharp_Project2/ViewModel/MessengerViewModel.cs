/*
Creation Date: 3/18/2017
Course: Adv Prog C# Skills
Description: MVVM ViewModel Class
Author: Chirag Kular(ck4957)
Version: Initial Version
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharp_Project2.Model;
using System.ComponentModel;
using System.IO;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.Windows.Input;

namespace CSharp_Project2.ViewModel
{
    class MessengerViewModel : INotifyPropertyChanged
    {
        private MessangerModel objMM;

        int count;
        char str;
        string key, space;
        public string _txtContent;
        public string _predictTxtContent;

        public bool _isPredictive;

        DispatcherTimer disptimer;

        /// <summary>
        /// Constructor that creates object of Model, Command, DispatacherTimer, Timespan
        /// </summary>
        public MessengerViewModel()
        {
            count = 0; key = ""; space = " ";
            msgCommand = new MessengerCommand(this);
            objMM = new MessangerModel();
            disptimer = new DispatcherTimer();
            disptimer.Tick += new EventHandler(disptimer_tick);
            disptimer.Interval = new TimeSpan(0, 0, 0, 0, 1500);
        }

        /// <summary>
        /// Property for predictive checkbox
        /// </summary>
        public bool IsPredictive
        {
            get { return _isPredictive; }
            set
            {
                _isPredictive = value;
                OnPropertyChanged("IsPredictive");
            }
        }

        /// <summary>
        /// Property for Black RichText
        /// </summary>
        public string txtContent
        {
            get { return _txtContent; }
            set
            {
                _txtContent = value;
                OnPropertyChanged("txtContent");
            }
        }

        /// <summary>
        /// Property for Gray RichText (Prediction Text)
        /// </summary>
        public string predictTxtContent
        {
            get { return _predictTxtContent; }
            set
            {
                _predictTxtContent = value;
                OnPropertyChanged("predictTxtContent");
            }
        }

        /// <summary>
        /// ICommand for all Button Clicks
        /// </summary>
        private MessengerCommand msgCommand;
        public ICommand BtnClickCommand
        {
            get
            {
                return msgCommand;
            }
        }

        /// <summary>
        /// Function handler the timer and then checks how many clicks
        /// happened and updates the text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void disptimer_tick(object sender, EventArgs e)
        {
            disptimer.Stop();
            //Console.WriteLine("Timer elapsed.");
            if (count == 1)
            {
                //Console.WriteLine("Single-Click detected");
                key = new string(str, 1);
                count = 0;
                txtContent += objMM.getLetter(key);


            }
            else if (count == 2)
            {
                //Console.WriteLine("2-Click detected");
                if (str == '*') //If delete button is pressed
                {
                    if (txtContent.Length != 0)
                        txtContent = txtContent.Remove(txtContent.Length - 1);
                }
                else if (str == '#') //If space button is pressed
                {
                    txtContent += space;
                }
                else
                { // For remaining buttons
                    key = new string(str, 2);
                    count = 0;
                    txtContent += objMM.getLetter(key);
                }
            }
            else if (count == 3)
            {
                //Console.WriteLine("3-Click detected");
                key = new string(str, 3);
                count = 0;
                txtContent += objMM.getLetter(key);
            }
            else if (count == 4)
            {
                Console.WriteLine("4-Click detected");
                key = new string(str, 4);
                count = 0;
                txtContent += objMM.getLetter(key);
            }
            else if ((count == 5) && (str == '7' || str == '9'))
            {
                //Console.WriteLine("4-Click detected");
                key = new string(str, 5);
                count = 0;
                txtContent += objMM.getLetter(key);
            }
            else
            {
                if (str == '7' || str == '9')
                {
                    //To cycle back to first letter for 7 and 9
                    count = count % 5;
                    key = new string(str, count);
                    txtContent += objMM.getLetter(key);
                    count = 0;
                }
                else
                {
                    //To cycle back to first letter for remaining buttons
                    count = count % 4;
                    key = new string(str, count);
                    txtContent += objMM.getLetter(key);
                    count = 0;
                }

            }
            count = 0;
        }

        string lastBtn = "";
        static int index = 0;
        string tmp = "", query_word = "";
        static List<string> tmplist = new List<string>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="button"></param>
        public void btnEventHandler(string button)
        {
            char btn_char = button.ToCharArray()[0];
            /* Non Predictive Part*/
            if (!IsPredictive)
            {

                if (lastBtn == "")
                    lastBtn = button;
                if (lastBtn != button)
                {
                    count = 0;
                    lastBtn = button;
                }
                count++;
                disptimer.Start();
                str = btn_char;
            }
            else // Predictive Part
            {
                // Button to Delete characters 
                if (button == "*")
                {
                    if (query_word.Length > 0)
                    {
                        predictTxtContent = "";
                        query_word = query_word.Remove(query_word.Length - 1);

                        tmplist = objMM.GetWords(query_word);
                        tmplist = (tmplist.Count == 0) ? new List<string> { new String('-', query_word.Length) } : tmplist;
                        index = 0;
                        // If multiple words present in textbox
                        if (txtContent.Contains(" "))
                        {
                            int space_index = txtContent.LastIndexOf(" ");
                            string split_words = txtContent.Substring(0, space_index + 1);
                            txtContent = split_words + tmplist[index];
                        }
                        else
                            txtContent = tmplist[index];
                    }
                    else
                    {
                        txtContent = "";
                    }

                }
                // Button to add space or accept the word
                else if (button == "#")
                {
                    if (predictTxtContent != "")
                    {
                        txtContent = txtContent + predictTxtContent + space;
                        predictTxtContent = "";
                    }

                    query_word = "";
                    index = 0;
                    txtContent += space;
                }
                // Button to get next word
                else if (button == "0")
                {
                    index++;
                    // Resulted list is not empty
                    if (index < tmplist.Count)
                    {
                        string next_word = tmplist[index];

                        // If query word and predicted word are of same length
                        // Next button will get the next valid word from the list
                        if (next_word.Length == query_word.Length)
                        {
                            if (txtContent.Contains(" "))
                            {
                                int space_index = txtContent.LastIndexOf(" ");
                                string split_words = txtContent.Substring(0, space_index + 1);
                                txtContent = split_words + next_word;
                            }
                            else
                                txtContent = next_word;
                        }
                        else
                        { //IF query word length and predicted word are of different length
                            // it try to predict the word and display text in partial gray color
                            if (txtContent.Contains(" "))
                            {
                                int space_index = txtContent.LastIndexOf(" ");
                                string split_words = txtContent.Substring(space_index, query_word.Length - 1);                                              //txtContent = two_words[0] + space + word_found;
                                predictTxtContent = next_word.Substring(split_words.Length, next_word.Length - split_words.Length);
                            }

                            else
                            {
                                txtContent = next_word.Substring(0, query_word.Length);
                                predictTxtContent = next_word.Substring(txtContent.Length, next_word.Length - txtContent.Length);
                            }
                        }
                    }
                }
                else
                {
                    //Functionality For the remaining number buttons
                    index = 0;
                    query_word += button;
                    tmplist = objMM.GetWords(query_word);

                    if (tmplist.Count > 0)
                    {
                        string word_found = tmplist[index];
                        if (word_found.Length == query_word.Length)
                        {
                            predictTxtContent = "";
                            if (txtContent != null)
                            {
                                //If textbox contains more than one word, split the word 
                                // and update the last word under prediction
                                if (txtContent.Contains(" "))
                                {
                                    int space_index = txtContent.LastIndexOf(" ");
                                    string split_words = txtContent.Substring(0, space_index + 1);
                                    //foreach(string word_index in split_words)
                                    txtContent = split_words + word_found;
                                }
                                else
                                    txtContent = word_found;
                            }
                            else
                                txtContent = word_found;
                        }
                        else
                        {

                            if (txtContent.Contains(" "))
                            {
                                int space_index = txtContent.LastIndexOf(" ");
                                //string[] split_words = txtContent.Split(space_index); //.Substring(txtContent.Length, txtContent.Length - word_found.Length);
                                string split_words = txtContent.Substring(space_index, txtContent.Length - 1 - space_index);                                              //txtContent = two_words[0] + space + word_found;
                                predictTxtContent = word_found.Substring(split_words.Length, word_found.Length - split_words.Length);
                            }
                            else
                                predictTxtContent = word_found.Substring(txtContent.Length, word_found.Length - txtContent.Length);
                        }
                    }
                    else
                    {
                        predictTxtContent = "";
                        if (txtContent.Contains(" "))
                        {
                            int space_index = txtContent.LastIndexOf(" ");
                            string split_words = txtContent.Substring(0, space_index + 1);
                            //foreach(string word_index in split_words)
                            txtContent = split_words + new string('-', query_word.Length);
                        }
                        else
                            txtContent = new string('-', query_word.Length);
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// Function is invoked when properties are updated and  that updates the View
        /// </summary>
        /// <param name="propertyName"></param>
        private void OnPropertyChanged(string propertyName)
        {
            if (propertyName != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
