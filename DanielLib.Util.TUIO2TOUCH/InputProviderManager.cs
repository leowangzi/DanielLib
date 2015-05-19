using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using DanielLib.Util.TUIO2TOUCH.Input;
using DanielLib.Util.TUIO2TOUCH.Input.Listeners;

namespace DanielLib.Util.TUIO2TOUCH
{
    public class InputProviderManager
    {

        public static void RegisterEvents(Window window, InputProviders provider)
        {
            switch (provider)
            {
                case InputProviders.Tuio:
                    TuioInputProvider.RegisterWindow(window);
                    break;
                case InputProviders.Mouse:
                    MouseInputProvider.RegisterWindow(window);
                    break;
                case InputProviders.Multipoint:
                    break;
                default:
                    break;
            }
        }
    }

    public enum InputProviders
    {
        Tuio,
        Mouse,
        Multipoint,
    }
}
