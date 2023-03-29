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
            #region test1
            IList<Test> tests = new List<Test>()
            {
                new Test(){ Major=1, Minor=1 },
                new Test(){ Major=1, Minor=2 },
                new Test(){ Major=1, Minor=3 },
                new Test(){ Major=1, Minor=4 },
                new Test(){ Major=2, Minor=1 },
                new Test(){ Major=2, Minor=2 },
                new Test(){ Major=2, Minor=3 }
            };
            #endregion

            #region test2
            //IList<Test> tests = new List<Test>()
            //{
            //    new Test(){ Major=2, Minor=3 },
            //    new Test(){ Major=1, Minor=1 },
            //    new Test(){ Major=1, Minor=4 },
            //    new Test(){ Major=2, Minor=1 },
            //    new Test(){ Major=1, Minor=3 },
            //    new Test(){ Major=2, Minor=2 },
            //    new Test(){ Major=1, Minor=2 },
            //};
            #endregion

            #region test3
            //IList<Test> tests = new List<Test>()
            //{
            //    new Test(){ Major=4, Minor=2 },
            //    new Test(){ Major=4, Minor=7 },
            //    new Test(){ Major=4, Minor=5 },
            //    new Test(){ Major=4, Minor=4 },
            //    new Test(){ Major=4, Minor=3 },
            //    new Test(){ Major=4, Minor=1 },
            //    new Test(){ Major=4, Minor=6 },

            //    new Test(){ Major=2, Minor=3 },
            //    new Test(){ Major=1, Minor=1 },
            //    new Test(){ Major=1, Minor=4 },
            //    new Test(){ Major=2, Minor=1 },
            //    new Test(){ Major=1, Minor=3 },
            //    new Test(){ Major=2, Minor=2 },
            //    new Test(){ Major=1, Minor=2 },

            //    new Test(){ Major=3, Minor=7 },
            //    new Test(){ Major=3, Minor=6 },
            //    new Test(){ Major=3, Minor=5 },
            //    new Test(){ Major=3, Minor=4 },
            //    new Test(){ Major=3, Minor=3 },
            //    new Test(){ Major=3, Minor=2 },
            //    new Test(){ Major=3, Minor=1 },
            //};
            #endregion

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
                        // Check for existing Major.
                        bool exist = false;
                        foreach (var parent in Parents)
                        {
                            if (child.Major == parent.Major)
                            {
                                // Sort in ascending order while inserting new child.
                                int pos = 0;
                                for (int i = 0; i < parent.Children.Count; i++)
                                {
                                    if (parent.Children[i].Minor < child.Minor) // Sorting condition.
                                    {
                                        pos = i + 1; // Capture the right position.
                                    }
                                }
                                parent.Children.Insert(pos, child); // Insert into the correct sorted position.

                                exist = true;
                                break;
                            }
                        }
                        if (!exist) // Parent not yet exist.
                        {
                            // Create new parent
                            createParent(child);
                        }
                    }
                    else // Parent not yet exist.
                    {
                        // Create new parent
                        createParent(child);
                    }
                }
            }
        }
        private void createParent(ChildViewModel child)
        {
            if (Parents != null)
            {
                ParentViewModel parent = new ParentViewModel();
                parent.Children.Add(child);

                // Sort in ascending order while inserting new parent.
                int pos = 0;
                for (int i = 0; i < Parents.Count; i++)
                {
                    if (Parents[i].Major < parent.Major) // Sorting condition.
                    {
                        pos = i + 1; // Capture the right position.
                    }
                }
                Parents.Insert(pos, parent); // Insert into the correct sorted position.
            }
        }

        public void Expand()
        {
            SetExpand(true);
        }
        public void Collapse()
        {
            SetExpand(false);
        }
        private void SetExpand(bool expand)
        {
            if (Parents != null)
            {
                foreach (var parent in Parents)
                {
                    parent.IsNodeExpanded = expand;
                }
            }
        }
        public void UpdateStates(ChildViewModel? child = null)
        {
            NotifyOfPropertyChange(() => IsBackEnabled); // Update Back button state
            NotifyOfPropertyChange(() => IsStartEnabled); // Update Start button state

            if(child != null) // check if this method is invoked by child
            {
                UpdateParentState(child); // Update parent's selection state
            }
        }
        private void UpdateParentState(ChildViewModel child)
        {            
            if (Parents != null)
            {
                foreach (var parent in Parents)
                {
                    if (parent.Children.Contains(child)) // Find parent of child.
                    {
                        int selectCount = 0;
                        foreach (var parentChild in parent.Children)
                        {
                            if (parentChild.IsSelected)
                            {
                                selectCount++;
                            }
                        }

                        // Determine state of the parent.
                        if (selectCount == 0) // All child are unselected.
                        {
                            parent.IsSelected = false;
                        }
                        else if (selectCount < parent.Children.Count) // One or more child is selected.
                        {
                            parent.IsSelected = null;
                        }
                        else // All children are selected.
                        {
                            parent.IsSelected = true;
                        }
                    }
                }
            }
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
                    parent.IsSelected = false;
                }
                UpdateStates();
            }
        }
        public bool IsStartEnabled => anyChildSelected;
        public void Start()
        {
            if (Parents != null)
            {
                ResultViewModel resultVm = new ResultViewModel();
                foreach (var parent in Parents)
                {
                    foreach (var child in parent.Children)
                    {
                        if (child.IsSelected)
                        {
                            resultVm.SelectedTests.Add(child.MyTest);
                        }
                    }
                }

                IWindowManager manager = new WindowManager();
                manager.ShowDialogAsync(resultVm);
            }
        }
        public void Cancel()
        {            
            TryCloseAsync();
        }

    }
}
