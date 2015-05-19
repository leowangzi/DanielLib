using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DanielLib.Util.TUIO2TOUCH.TUIO;

namespace DanielLib.Util.TUIO2TOUCH.Input.Listeners
{
    //internal class TuioInputProviderListener : TuioListener
    //Object Test
    public class TuioInputProviderListener : TuioListener
    //Object Test
    {
        #region cursor events

        public delegate void CursorAddedHandler(TuioCursor tCur);

        public delegate void CursorRemovedHandler(TuioCursor tCur);

        public delegate void CursorUpdatedHandler(TuioCursor tCur);

        public event CursorAddedHandler CursorAdded;

        protected virtual void OnCursorAdded(TuioCursor tCur)
        {
            if (CursorAdded != null)
                CursorAdded(tCur);
        }

        public event CursorUpdatedHandler CursorUpdated;

        protected virtual void OnCursorUpdated(TuioCursor tCur)
        {
            if (CursorUpdated != null)
                CursorUpdated(tCur);
        }

        public event CursorRemovedHandler CursorRemoved;

        protected virtual void OnCursorRemoved(TuioCursor tCur)
        {
            if (CursorRemoved != null)
                CursorRemoved(tCur);
        }

        //Object Test
        public delegate void ObjectAddedHandler(object sender, ObjectEventArgs e);
        public event ObjectAddedHandler ObjectAdded;
        protected virtual void OnObjectAdded(ObjectEventArgs e)
        {
            if (ObjectAdded != null)
            {
                ObjectAdded(this, e);
            }
        }

        public delegate void ObjectRemovedHandler(object sender, ObjectEventArgs e);
        public event ObjectRemovedHandler ObjectRemoved;
        protected virtual void OnObjectRemoved(ObjectEventArgs e)
        {
            if (ObjectRemoved != null)
            {
                ObjectRemoved(this, e);
            }
        }

        public delegate void ObjectUpdatedHandler(object sender, ObjectEventArgs e);
        public event ObjectUpdatedHandler ObjectUpdated;
        protected virtual void OnObjectUpdated(ObjectEventArgs e)
        {
            if (ObjectUpdated != null)
            {
                ObjectUpdated(this, e);
            }
        }
        //Object Test

        #endregion

        #region TuioListener interface implementation

        public void addTuioObject(TuioObject tobj)
        {
            OnObjectAdded(new ObjectEventArgs(tobj));
        }

        public void updateTuioObject(TuioObject tobj)
        {
            OnObjectUpdated(new ObjectEventArgs(tobj));
        }

        public void removeTuioObject(TuioObject tobj)
        {
            OnObjectRemoved(new ObjectEventArgs(tobj));
        }

        public void addTuioCursor(TuioCursor tcur)
        {
            OnCursorAdded(tcur);
        }

        public void updateTuioCursor(TuioCursor tcur)
        {
            OnCursorUpdated(tcur);
        }

        public void removeTuioCursor(TuioCursor tcur)
        {
            OnCursorRemoved(tcur);
        }

        public void refresh(TuioTime ftime)
        {
        }

        #endregion
    
    }
}
