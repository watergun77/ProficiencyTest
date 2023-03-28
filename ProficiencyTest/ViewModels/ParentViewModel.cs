using Caliburn.Micro;

namespace ProficiencyTest.ViewModels
{
    public class ParentViewModel
    {
        private BindableCollection<ChildViewModel> children = new();
        public BindableCollection<ChildViewModel> Children
        {
            get { return children; }
            set { children = value; }
        }        
        public int Major
        {
            get 
            { 
                if(Children.Count > 0)
                {
                    return Children[0].Major;
                }
                else
                {
                    return 0;
                }
            }            
        }
    }
}
