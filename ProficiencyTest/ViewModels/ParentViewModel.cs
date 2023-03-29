﻿using Caliburn.Micro;

namespace ProficiencyTest.ViewModels
{
    public class ParentViewModel : Screen
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

        private bool? isSelected = false;
        public bool? IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                isSelected = value;
                if (isSelected != null)
                {
                    foreach (var child in Children)
                    {
                        child.IsSelected = isSelected.Value;
                    }
                }
                NotifyOfPropertyChange(() => IsSelected);
            }
        }

        private bool isNodeExpanded = false;
        public bool IsNodeExpanded
        {
            get { return isNodeExpanded; }
            set 
            { 
                isNodeExpanded = value;
                NotifyOfPropertyChange(() => IsNodeExpanded);
            }
        }
    }
}
