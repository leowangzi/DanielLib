using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DanielLib.TouchAppTemplate
{
    internal class ApplicationStateChangedEventArgs : EventArgs
    {
        public ApplicationState OldState { get; private set; }
        public ApplicationState NewState { get; private set; }

        public ApplicationStateChangedEventArgs(ApplicationState oldState, ApplicationState newState)
        {
            OldState = oldState;
            NewState = newState;
        }
    }
}
