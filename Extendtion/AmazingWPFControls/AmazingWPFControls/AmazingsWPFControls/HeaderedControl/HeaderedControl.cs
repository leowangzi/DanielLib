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
using System.Windows.Markup;

namespace AmazingWPFControls
{

    /// <summary>
    /// A control displaying an header at the top of its content.
    /// </summary>    
    [TemplatePart(Name = "PART_Header", Type = typeof(Border))]
    [ContentPropertyAttribute("Content")]
    public class HeaderedControl : ContentControl
    {
        static HeaderedControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HeaderedControl),
                new FrameworkPropertyMetadata(typeof(HeaderedControl)));

            //Instanciate the command
            MoveHeaderToTopCommand = new RoutedUICommand("MoveHeaderToTop",
                "MoveHeaderToTop", typeof(HeaderedControl));

            //Create the command binding
            CommandBinding moveHeaderToTopCommandBinding =
                new CommandBinding(MoveHeaderToTopCommand, MoveHeaderToTopCommand_Executed,
                    MoveHeaderToTopCommand_CanExecute);

            CommandManager.
                RegisterClassCommandBinding(typeof(HeaderedControl), moveHeaderToTopCommandBinding);
        }

        #region fields
        Border PART_Header;
        #endregion


        static void MoveHeaderToTopCommand_Executed(
            object sender, ExecutedRoutedEventArgs e)
        {
            HeaderedControl headeredControl = sender as HeaderedControl;
            if (headeredControl != null)
                headeredControl.moveHeaderToTop();
        }

        static void MoveHeaderToTopCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void moveHeaderToTop()
        {
            this.PositionOfTheHeader = HeaderPosition.Top;
        }

        #region commands

        #region MoveHeaderToTopCommand

        private static RoutedUICommand _moveHeaderToTopCommand;
        /// <summary>
        /// Gets or sets the MoveHeaderToTop command : it set the PositionOfTheHeader to "Top"
        /// </summary>
        /// <value>The MoveHeaderToTop command.</value>
        public static RoutedUICommand MoveHeaderToTopCommand
        {
            get { return _moveHeaderToTopCommand; }
            private set { _moveHeaderToTopCommand = value; }
        }

        #endregion
        #endregion

        #region Properties

        #region Header

        /// <summary>
        /// Header Dependency Property
        /// </summary>
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(object), typeof(HeaderedControl),
                new FrameworkPropertyMetadata((object)null));

        /// <summary>
        /// Gets or sets the Header property. This dependency property 
        /// indicates the header to display.
        /// </summary>
        public object Header
        {
            get { return (object)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        #endregion


        #region HeaderPosition

        /// <summary>
        /// HeaderPosition Dependency Property
        /// </summary>
        public static readonly DependencyProperty PositionOfTheHeaderProperty =
            DependencyProperty.Register("PositionOfTheHeader", typeof(HeaderPosition), typeof(HeaderedControl),
                new FrameworkPropertyMetadata(HeaderPosition.Top));

        /// <summary>
        /// Gets or sets the HeaderPosition property. This dependency property 
        /// indicates ....
        /// </summary>
        public HeaderPosition PositionOfTheHeader
        {
            get { return (HeaderPosition)GetValue(PositionOfTheHeaderProperty); }
            set { SetValue(PositionOfTheHeaderProperty, value); }
        }

        #endregion

        #endregion
        #region internal classes

        /// <summary>
        /// Defines where to place the header
        /// </summary>
        public enum HeaderPosition : int
        {
            /// <summary>
            /// The header is positioned on the left.
            /// </summary>
            Left = 0,

            /// <summary>
            /// The header is positioned at the top.
            /// </summary>
            Top = 1,

            /// <summary>
            ///  The header is positioned on the right.
            /// </summary>
            Right = 2,

            /// <summary>
            ///  The header is positioned at the bottom.
            /// </summary>
            Bottom = 3,

        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            PART_Header = this.GetTemplateChild("PART_Header") as Border;
            if (PART_Header == null)
                throw new ArgumentNullException(
                    "Can't find PART_Header in the HeaderedControl template.");
            PART_Header.MouseDown += (a, b) => { RaiseHeaderClickedEvent(); };

        }


        /// <summary>
        /// the event handler delegate
        /// </summary>
        public delegate void HeaderClickedEventHandler(object sender, HeaderClickedEventArgs e);

        /// <summary>
        /// Create a custom routed event by first registering a RoutedEventID
        /// This event uses the bubbling routing strategy
        /// </summary>
        public static readonly RoutedEvent HeaderClickedEvent = EventManager.RegisterRoutedEvent(
            "HeaderClicked", RoutingStrategy.Bubble, typeof(HeaderClickedEventHandler), typeof(HeaderedControl));

        /// <summary>
        /// Occurs when the header is clicked.
        /// </summary>
        public event RoutedEventHandler HeaderClicked
        {
            add { AddHandler(HeaderClickedEvent, value); }
            remove { RemoveHandler(HeaderClickedEvent, value); }
        }

        /// <summary>
        /// Raises the header clicked event.
        /// </summary>
        void RaiseHeaderClickedEvent()
        {
            HeaderClickedEventArgs newEventArgs = new HeaderClickedEventArgs(HeaderedControl.HeaderClickedEvent);
            RaiseEvent(newEventArgs);
        }

        /// <summary>
        /// The header has been clicked event args
        /// </summary>
        public class HeaderClickedEventArgs : RoutedEventArgs
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="HeaderClickedEventArgs"/> class.
            /// </summary>
            /// <param name="routedEvent">The routed event.</param>
            public HeaderClickedEventArgs(RoutedEvent routedEvent) : base(routedEvent) { }
        }
        #endregion
    }


}
