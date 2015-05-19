using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Touchtech.Surface;

namespace DanielLib.TouchAppTemplate
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Suppress Surface Input if running on Surface hardware
            SurfaceEnvironmentHelper.TrySuppressSurfaceInputOnNonSurfaceHardware();

            base.OnStartup(e);
        }
    }
}