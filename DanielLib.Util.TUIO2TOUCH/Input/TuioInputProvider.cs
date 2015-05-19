using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using DanielLib.Util.TUIO2TOUCH.Input.Listeners;
using DanielLib.Util.TUIO2TOUCH.TUIO;


namespace DanielLib.Util.TUIO2TOUCH.Input
{
    public class TuioInputProvider : TouchDevice
    {
        private static readonly Dictionary<int, TuioInputProvider> Contacts = new Dictionary<int, TuioInputProvider>();

        private static TuioClient _client;

        private static double _w, _h;

        //private static readonly TuioInputProviderListener Listener = new TuioInputProviderListener();

        //Object Test
        public static readonly TuioInputProviderListener Listener = new TuioInputProviderListener();
        //Object Test

        private static Window _window;

        protected TuioInputProvider(int deviceId) :
            base(deviceId)
        {
            Position = new Point();
        }

        public Point Position { get; set; }

        public static void RegisterWindow(Window window)
        {
            _window = window;

            _client = new TuioClient(3333);
            _client.addTuioListener(Listener);
            _client.connect();

            Listener.CursorAdded += CursorAdded;
            Listener.CursorUpdated += CursorUpdated;
            Listener.CursorRemoved += CursorRemoved;

            _w = window.Width;
            _h = window.Height;

            window.SizeChanged += delegate
            {
                _w = window.ActualWidth;
                _h = window.ActualHeight;
            };

            _window.Closing += delegate { Dispose(); };
        }

        public static void Dispose()
        {
            _client.disconnect();
        }

        private static void CursorAdded(TuioCursor tCur)
        {
            _window.Dispatcher.Invoke(
                DispatcherPriority.Normal,
                (Action)(() =>
                {
                    if (Contacts.ContainsKey((int)tCur.getSessionID()))
                        return;

                    var device = new TuioInputProvider((int)tCur.getSessionID());
                    device.SetActiveSource(PresentationSource.FromVisual(_window));
                    device.Position = new Point(
                        tCur.getScreenX(Convert.ToInt32(_w)),
                        tCur.getScreenY(Convert.ToInt32(_h)));
                    device.Activate();
                    device.ReportDown();

                    Contacts.Add((int)tCur.getSessionID(), device);
                }));
        }

        private static void CursorUpdated(TuioCursor tCur)
        {
            _window.Dispatcher.Invoke(
                DispatcherPriority.Normal,
                (Action)(() =>
                {
                    var id = (int)tCur.getSessionID();
                    if (!Contacts.ContainsKey(id))
                        CursorAdded(tCur);

                    var device = Contacts[id];

                    if (device == null || !device.IsActive) return;

                    device.Position = new Point(
                        tCur.getScreenX(Convert.ToInt32(_w)),
                        tCur.getScreenY(Convert.ToInt32(_h)));
                    device.ReportMove();
                }));
        }

        private static void CursorRemoved(TuioCursor tCur)
        {
            _window.Dispatcher.Invoke(
                DispatcherPriority.Normal,
                (Action)(() =>
                {
                    var id = (int)tCur.getSessionID();
                    if (!Contacts.ContainsKey(id))
                        CursorAdded(tCur);

                    var device = Contacts[id];

                    if (device == null || !device.IsActive) return;

                    device.Position = new Point(
                        tCur.getScreenX(Convert.ToInt32(_w)),
                        tCur.getScreenY(Convert.ToInt32(_h)));
                    device.ReportUp();
                    device.Deactivate();
                }));
        }

        public override TouchPointCollection GetIntermediateTouchPoints(IInputElement relativeTo)
        {
            return new TouchPointCollection();
        }

        public override TouchPoint GetTouchPoint(IInputElement relativeTo)
        {
            var point = Position;
            if (relativeTo != null && ActiveSource != null)
            {
                point = ActiveSource.RootVisual.TransformToDescendant((Visual)relativeTo).Transform(Position);
            }

            var rect = new Rect(point, new Size(1, 1));

            return new TouchPoint(this, point, rect, TouchAction.Move);
        }
    }
}
