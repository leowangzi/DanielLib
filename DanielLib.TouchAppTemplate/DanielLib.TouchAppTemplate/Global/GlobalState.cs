using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DanielLib.TouchAppTemplate
{
    public enum ApplicationState { ScreenSaver, MainView, Other };

    static class GlobalState
    {
        #region Application State Changed

        public delegate void ApplicationStateChangedEventHandler(object sender, ApplicationStateChangedEventArgs e);
        public static event ApplicationStateChangedEventHandler ApplicationStateChanged;

        private static ApplicationState applicationState;
        public static ApplicationState ApplicationState
        {
            get { return applicationState; }
            set
            {
                ApplicationState oldState = applicationState;
                ApplicationState newState = value;
                applicationState = newState;
                if (oldState != newState && ApplicationStateChanged != null)
                    ApplicationStateChanged(null, new ApplicationStateChangedEventArgs(oldState, newState));
            }
        }

        #endregion
    }
}
