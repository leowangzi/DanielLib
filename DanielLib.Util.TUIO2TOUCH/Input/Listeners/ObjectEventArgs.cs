using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using DanielLib.Util.TUIO2TOUCH.TUIO;

namespace DanielLib.Util.TUIO2TOUCH.Input.Listeners
{
    public class ObjectEventArgs : EventArgs
    {
        public TuioObject tuioObject;

        public ObjectEventArgs(TuioObject _tuioObject)
        {
            this.tuioObject = _tuioObject;
        }
    }
}
