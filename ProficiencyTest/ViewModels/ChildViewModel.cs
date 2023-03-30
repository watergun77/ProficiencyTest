using ProficiencyTest.Models;
using Caliburn.Micro;

namespace ProficiencyTest.ViewModels
{
    public class ChildViewModel : Screen
    {
        private readonly ITest test;
        public ITest Test => test;

        public ChildViewModel(ITest test)
        {
            this.test = test;
        }

        //private Test myTest = new();
        //public Test MyTest
        //{
        //    get { return myTest; }
        //    set 
        //    {
        //        myTest = value; 
        //    } 
        //}
        public int Major 
        { 
            get 
            { 
                return test.Major;
            } 
        }
        public int Minor
        {
            get
            {
                return test.Minor;
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
