﻿using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProficiencyTest.ViewModels
{
    public class ShellViewModel : Screen
    {
        public void Cancel()
        {            
            TryCloseAsync();
        }
    }
}
