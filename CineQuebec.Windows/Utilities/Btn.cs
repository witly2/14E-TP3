﻿using System;
using System.Collections.Generic;
using System.Linq;

using System.Windows;
using System.Windows.Controls;

namespace CineQuebec.Windows.Utilities
{
    public class Btn:RadioButton
    {

        static Btn()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Btn), new FrameworkPropertyMetadata(typeof(Btn)));
        }


    }
}
