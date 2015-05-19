using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DanielLib.Util.TUIO2TOUCH.TUIO
{
    public interface TuioListener
    {
        /**
		 * This callback method is invoked by the TuioClient when a new TuioObject is added to the session.
		 *
		 * @param  tobj  the TuioObject reference associated to the addTuioObject event
		 */
        void addTuioObject(TuioObject tobj);

        /**
         * This callback method is invoked by the TuioClient when an existing TuioObject is updated during the session.
         *
         * @param  tobj  the TuioObject reference associated to the updateTuioObject event
         */
        void updateTuioObject(TuioObject tobj);

        /**
         * This callback method is invoked by the TuioClient when an existing TuioObject is removed from the session.
         *
         * @param  tobj  the TuioObject reference associated to the removeTuioObject event
         */
        void removeTuioObject(TuioObject tobj);

        /**
         * This callback method is invoked by the TuioClient when a new TuioCursor is added to the session.
         *
         * @param  tcur  the TuioCursor reference associated to the addTuioCursor event
         */
        void addTuioCursor(TuioCursor tcur);

        /**
         * This callback method is invoked by the TuioClient when an existing TuioCursor is updated during the session.
         *
         * @param  tcur  the TuioCursor reference associated to the updateTuioCursor event
         */
        void updateTuioCursor(TuioCursor tcur);

        /**
         * This callback method is invoked by the TuioClient when an existing TuioCursor is removed from the session.
         *
         * @param  tcur  the TuioCursor reference associated to the removeTuioCursor event
         */
        void removeTuioCursor(TuioCursor tcur);

        /**
         * This callback method is invoked by the TuioClient to mark the end of a received TUIO message bundle.
         *
         * @param  ftime  the TuioTime associated to the current TUIO message bundle
         */
        void refresh(TuioTime ftime);
    }
}

