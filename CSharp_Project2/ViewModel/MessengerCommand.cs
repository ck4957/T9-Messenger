/*
Creation Date: 3/23/2017
Course: Adv Prog C# Skills
Description: Button Click Commands using ICommand interface
Author: Chirag Kular(ck4957)
Version: Initial Version
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CSharp_Project2.ViewModel
{
    class MessengerCommand : ICommand
    {
        private MessengerViewModel msgVM;
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_msgVM">View Model Object</param>
        public MessengerCommand(MessengerViewModel _msgVM)
        {
            this.msgVM = _msgVM;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            string btnName = parameter.ToString();
            msgVM.btnEventHandler(btnName);
        }
    }
}
