using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AmazingWPFControls.AnimatedExpander
{

    /// <summary>
    /// A WPF animated expander it is just a redifinition
    /// of the basic Expander template but we expose it as a custom control for a easier use.
    /// </summary>
    public class AnimatedExpander : Expander
    {
        static AnimatedExpander()
        {
            DefaultStyleKeyProperty
                .OverrideMetadata(typeof(AnimatedExpander), new FrameworkPropertyMetadata(typeof(AnimatedExpander)));
        }
    }
}
