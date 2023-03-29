﻿using ProficiencyTest.Models;
using Caliburn.Micro;

namespace ProficiencyTest.ViewModels
{
    public class ChildViewModel : Screen
    {
        private Test myTest = new();
        public Test MyTest
        {
            get { return myTest; }
            set 
            {
                myTest = value; 
            } 
        }
        public int Major 
        { 
            get 
            { 
                return myTest.Major;
            } 
        }
        public int Minor
        {
            get
            {
                return myTest.Minor;
            }
        }

        private bool isSelected;
        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set 
            { 
                isSelected = value;
                NotifyOfPropertyChange(() => IsSelected);
            }
        }
        public bool IsNodeExpanded { get; set; } // To remove warning/error : System.Windows.Data Error: 40 : BindingExpression path error.
    }
}
