using Caliburn.Micro;
using ProficiencyTest.Models;
using System.Collections.Generic;

namespace ProficiencyTest.ViewModels
{
    public class ShellViewModel : Screen
    {
        private BindableCollection<ParentViewModel>? parents = null;
        public BindableCollection<ParentViewModel>? Parents
        {
            get
            {
                return parents;
            }
            private set
            {
                parents = value;
            }
        }

        public ShellViewModel()
        {
            //IList<Test> tests = new List<Test>() 
            //{ 
            //    new Test(){ Major=1, Minor=1 },
            //    new Test(){ Major=1, Minor=2 },
            //    new Test(){ Major=1, Minor=3 },
            //    new Test(){ Major=1, Minor=4 },
            //    new Test(){ Major=2, Minor=1 },
            //    new Test(){ Major=2, Minor=2 },
            //    new Test(){ Major=2, Minor=3 }
            //};

            IList<Test> tests = new List<Test>()
            {
                new Test(){ Major=2, Minor=3 },
                new Test(){ Major=1, Minor=1 },
                new Test(){ Major=1, Minor=4 },
                new Test(){ Major=2, Minor=1 },
                new Test(){ Major=1, Minor=3 },
                new Test(){ Major=2, Minor=2 },
                new Test(){ Major=1, Minor=2 },
            };
            createTree(tests);
        }
        private void createTree(IList<Test> tests)
        {
            Parents = new();
            if (tests != null)
            {
                foreach (var test in tests)
                {
                    ChildViewModel child = new ChildViewModel() { MyTest = test };

                    if (Parents.Count > 0)
                    {
                        // Check for existing Major
                        bool exist = false;
                        foreach (var parent in Parents)
                        {
                            if (child.Major == parent.Major)
                            {
                                parent.Children.Add(child);
                                exist = true;
                                break;
                            }
                        }
                        if (!exist)
                        {
                            // Create new parent
                            ParentViewModel parent = new ParentViewModel();
                            parent.Children.Add(child);
                            Parents.Add(parent);
                        }
                    }
                    else
                    {
                        // Create new parent
                        ParentViewModel parent = new ParentViewModel();
                        parent.Children.Add(child);
                        Parents.Add(parent);
                    }
                }
            }
        }
        public void UpdateStates()
        {
            NotifyOfPropertyChange(() => IsBackEnabled);
            NotifyOfPropertyChange(() => IsStartEnabled);
        }

        private bool anyChildSelected
        {
            get
            {
                if (Parents != null && Parents.Count > 0)
                {
                    foreach (var parent in Parents)
                    {
                        if (isChildSelected(parent))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }
            
        private bool isChildSelected(ParentViewModel parent)
        {
            foreach (var child in parent.Children)
            {
                if (child.IsSelected)
                {
                    return true;
                }
            }
            return false;
        }
        public bool IsBackEnabled => anyChildSelected;
        public void Back()
        {
            if (Parents != null)
            {
                foreach (var parent in Parents)
                {
                    foreach (var child in parent.Children)
                    {
                        child.IsSelected = false;
                    }
                }
                UpdateStates();
            }
        }
        public bool IsStartEnabled => anyChildSelected;
        public void Start()
        {

        }
        public void Cancel()
        {            
            TryCloseAsync();
        }

    }
}
