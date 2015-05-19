using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace AmazingWPFControls.HandWritingToText
{
    /// <summary>
    /// Event args to tell a text has been entered
    /// </summary>
    public class TextEnteredEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// Gets the text entered.
        /// </summary>
        /// <value>The text entered.</value>
        public String TextEntered { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextEnteredEventArgs"/> class.
        /// </summary>
        /// <param name="routedEvent">The routed event.</param>
        /// <param name="text">The text.</param>
        public TextEnteredEventArgs(RoutedEvent routedEvent, String text)
            : base(routedEvent)
        {
            TextEntered = text;
        }
    }
}
