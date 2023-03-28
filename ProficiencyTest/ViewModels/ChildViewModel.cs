using ProficiencyTest.Models;

namespace ProficiencyTest.ViewModels
{
    public class ChildViewModel
    {
        private Test myTest = new();
        public Test MyTest 
        { 
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
    }
}
