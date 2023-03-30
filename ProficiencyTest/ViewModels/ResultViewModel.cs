﻿using Caliburn.Micro;
using ProficiencyTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProficiencyTest.ViewModels
{
    public class ResultViewModel : Screen
    {
        public List<ITest> SelectedTests { get; set; } = new List<ITest>();     


    }
}
