using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DanielLib.Util.TUIO2TOUCH.TUIO
{
    public class TuioCursor : TuioContainer
    {
        /**
         * The individual cursor ID number that is assigned to each TuioCursor.
         */
        protected int cursor_id;

        /**
         * This constructor takes a TuioTime argument and assigns it along with the provided
         * Session ID, Cursor ID, X and Y coordinate to the newly created TuioCursor.
         *
         * @param	ttime	the TuioTime to assign
         * @param	si	the Session ID to assign
         * @param	ci	the Cursor ID to assign
         * @param	xp	the X coordinate to assign
         * @param	yp	the Y coordinate to assign
         */
        public TuioCursor(TuioTime ttime, long si, int ci, float xp, float yp)
            : base(ttime, si, xp, yp)
        {
            cursor_id = ci;
        }

        /**
         * This constructor takes the provided Session ID, Cursor ID, X and Y coordinate
         * and assigs these values to the newly created TuioCursor.
         *
         * @param	si	the Session ID to assign
         * @param	ci	the Cursor ID to assign
         * @param	xp	the X coordinate to assign
         * @param	yp	the Y coordinate to assign
         */
        public TuioCursor(long si, int ci, float xp, float yp)
            : base(si, xp, yp)
        {
            cursor_id = ci;
        }

        /**
         * This constructor takes the atttibutes of the provided TuioCursor
         * and assigs these values to the newly created TuioCursor.
         *
         * @param	tcur	the TuioCursor to assign
         */
        public TuioCursor(TuioCursor tcur)
            : base(tcur)
        {
            cursor_id = tcur.getCursorID();
        }

        /**
         * Returns the Cursor ID of this TuioCursor.
         * @return	the Cursor ID of this TuioCursor
         */
        public int getCursorID()
        {
            return cursor_id;
        }

    }
}

