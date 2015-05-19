using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace DanielLib.Utilities.AudioHandler
{
    public class AudioEventArgs : RoutedEventArgs
    {
        public AudioEventArgs() : base() { }
        public AudioEventArgs(RoutedEvent routedEvent) : base(routedEvent) { }
        public AudioEventArgs(RoutedEvent routedEvent, Object source) : base(routedEvent, source) { }

        public string Audio { get; set; }
        public double XPosition { get; set; }
    }
}
