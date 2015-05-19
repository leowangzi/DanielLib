using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace DanielLib.Util.TUIO2TOUCH.Input
{
    public class MouseInputProvider : TouchDevice
    {
        private static MouseInputProvider _contact;

        public Point Position { get; set; }

        public static void RegisterWindow(Window window)
        {
            window.MouseDown += element_MouseDown;
            window.MouseMove += element_MouseMove;
            window.MouseUp += element_MouseUp;
        }

        static void element_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _contact = new MouseInputProvider(e.MouseDevice.GetHashCode()) { Position = e.GetPosition(null) };
            _contact.SetActiveSource(e.MouseDevice.ActiveSource);
            _contact.Activate();
            _contact.ReportDown();
        }

        static void element_MouseMove(object sender, MouseEventArgs e)
        {
            if (_contact == null || !_contact.IsActive)
                return;

            _contact.Position = e.GetPosition(null);
            _contact.ReportMove();
        }

        static void element_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_contact == null || !_contact.IsActive)
                return;

            _contact.Position = e.GetPosition(null);
            _contact.ReportUp();
            _contact.Deactivate();
        }

        public MouseInputProvider(int deviceId)
            : base(deviceId)
        {
            Position = new Point();
        }

        public override TouchPoint GetTouchPoint(IInputElement relativeTo)
        {
            Point position = Position;

            if (ActiveSource != null && relativeTo != null)
                position = ActiveSource.RootVisual.TransformToDescendant((Visual)relativeTo).Transform(position);

            return new TouchPoint(this, position, new Rect(position, new Size(1, 1)), TouchAction.Move);
        }

        public override TouchPointCollection GetIntermediateTouchPoints(IInputElement relativeTo)
        {
            return new TouchPointCollection();
        }
    }
}
