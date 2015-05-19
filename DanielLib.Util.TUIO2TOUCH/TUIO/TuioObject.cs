using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DanielLib.Util.TUIO2TOUCH.TUIO
{
    public class TuioObject : TuioContainer
    {
        /**
          * The individual symbol ID number that is assigned to each TuioObject.
          */
        protected int symbol_id;
        /**
         * The rotation angle value.
         */
        protected float angle;
        /**
         * The rotation speed value.
         */
        protected float rotation_speed;
        /**
         * The rotation acceleration value.
         */
        protected float rotation_accel;
        /**
         * Defines the ROTATING state.
         */
        public static readonly int TUIO_ROTATING = 5;

        /**
         * This constructor takes a TuioTime argument and assigns it along with the provided
         * Session ID, Symbol ID, X and Y coordinate and angle to the newly created TuioObject.
         *
         * @param	ttime	the TuioTime to assign
         * @param	si	the Session ID to assign
         * @param	sym	the Symbol ID to assign
         * @param	xp	the X coordinate to assign
         * @param	yp	the Y coordinate to assign
         * @param	a	the angle to assign
         */
        public TuioObject(TuioTime ttime, long si, int sym, float xp, float yp, float a)
            : base(ttime, si, xp, yp)
        {
            symbol_id = sym;
            angle = a;
            rotation_speed = 0.0f;
            rotation_accel = 0.0f;
        }

        /**
         * This constructor takes the provided Session ID, Symbol ID, X and Y coordinate
         * and angle, and assigs these values to the newly created TuioObject.
         *
         * @param	si	the Session ID to assign
         * @param	sym	the Symbol ID to assign
         * @param	xp	the X coordinate to assign
         * @param	yp	the Y coordinate to assign
         * @param	a	the angle to assign
         */
        public TuioObject(long si, int sym, float xp, float yp, float a)
            : base(si, xp, yp)
        {
            symbol_id = sym;
            angle = a;
            rotation_speed = 0.0f;
            rotation_accel = 0.0f;
        }

        /**
         * This constructor takes the atttibutes of the provided TuioObject
         * and assigs these values to the newly created TuioObject.
         *
         * @param	tobj	the TuioObject to assign
         */
        public TuioObject(TuioObject tobj)
            : base(tobj)
        {
            symbol_id = tobj.getSymbolID();
            angle = tobj.getAngle();
            rotation_speed = 0.0f;
            rotation_accel = 0.0f;
        }

        /**
         * Takes a TuioTime argument and assigns it along with the provided
         * X and Y coordinate, angle, X and Y velocity, motion acceleration,
         * rotation speed and rotation acceleration to the private TuioObject attributes.
         *
         * @param	ttime	the TuioTime to assign
         * @param	xp	the X coordinate to assign
         * @param	yp	the Y coordinate to assign
         * @param	a	the angle coordinate to assign
         * @param	xs	the X velocity to assign
         * @param	ys	the Y velocity to assign
         * @param	rs	the rotation velocity to assign
         * @param	ma	the motion acceleration to assign
         * @param	ra	the rotation acceleration to assign
         */
        public void update(TuioTime ttime, float xp, float yp, float a, float xs, float ys, float rs, float ma, float ra)
        {
            base.update(ttime, xp, yp, xs, ys, ma);
            angle = a;
            rotation_speed = rs;
            rotation_accel = ra;
            if ((rotation_accel != 0) && (state != TUIO_STOPPED)) state = TUIO_ROTATING;
        }

        /**
         * Assigns the provided X and Y coordinate, angle, X and Y velocity, motion acceleration
         * rotation velocity and rotation acceleration to the private TuioContainer attributes.
         * The TuioTime time stamp remains unchanged.
         *
         * @param	xp	the X coordinate to assign
         * @param	yp	the Y coordinate to assign
         * @param	a	the angle coordinate to assign
         * @param	xs	the X velocity to assign
         * @param	ys	the Y velocity to assign
         * @param	rs	the rotation velocity to assign
         * @param	ma	the motion acceleration to assign
         * @param	ra	the rotation acceleration to assign
         */
        public void update(float xp, float yp, float a, float xs, float ys, float rs, float ma, float ra)
        {
            base.update(xp, yp, xs, ys, ma);
            angle = a;
            rotation_speed = rs;
            rotation_accel = ra;
            if ((rotation_accel != 0) && (state != TUIO_STOPPED)) state = TUIO_ROTATING;
        }

        /**
         * Takes a TuioTime argument and assigns it along with the provided
         * X and Y coordinate and angle to the private TuioObject attributes.
         * The speed and accleration values are calculated accordingly.
         *
         * @param	ttime	the TuioTime to assign
         * @param	xp	the X coordinate to assign
         * @param	yp	the Y coordinate to assign
         * @param	a	the angle coordinate to assign
         */
        public void update(TuioTime ttime, float xp, float yp, float a)
        {
            TuioPoint lastPoint = path[path.Count - 1];
            base.update(ttime, xp, yp);

            TuioTime diffTime = currentTime - lastPoint.getTuioTime();
            float dt = diffTime.getTotalMilliseconds() / 1000.0f;
            float last_angle = angle;
            float last_rotation_speed = rotation_speed;
            angle = a;

            float da = (angle - last_angle) / (2.0f * (float)Math.PI);
            if (da > 0.75f) da -= 1.0f;
            else if (da < -0.75f) da += 1.0f;

            rotation_speed = da / dt;
            rotation_accel = (rotation_speed - last_rotation_speed) / dt;
            if ((rotation_accel != 0) && (state != TUIO_STOPPED)) state = TUIO_ROTATING;
        }

        /**
         * Takes the atttibutes of the provided TuioObject
         * and assigs these values to this TuioObject.
         * The TuioTime time stamp of this TuioContainer remains unchanged.
         *
         * @param	tobj	the TuioContainer to assign
         */
        public void update(TuioObject tobj)
        {
            base.update(tobj);
            angle = tobj.getAngle();
            rotation_speed = tobj.getRotationSpeed();
            rotation_accel = tobj.getRotationAccel();
            if ((rotation_accel != 0) && (state != TUIO_STOPPED)) state = TUIO_ROTATING;
        }

        /**
         * This method is used to calculate the speed and acceleration values of a
         * TuioObject with unchanged position and angle.
         */
        public new void stop(TuioTime ttime)
        {
            update(ttime, this.xpos, this.ypos, this.angle);
        }

        /**
         * Returns the symbol ID of this TuioObject.
         * @return	the symbol ID of this TuioObject
         */
        public int getSymbolID()
        {
            return symbol_id;
        }

        /**
         * Returns the rotation angle of this TuioObject.
         * @return	the rotation angle of this TuioObject
         */
        public float getAngle()
        {
            return angle;
        }

        /**
         * Returns the rotation angle in degrees of this TuioObject.
         * @return	the rotation angle in degrees of this TuioObject
         */
        public float getAngleDegrees()
        {
            return angle / (float)Math.PI * 180.0f;
        }

        /**
         * Returns the rotation speed of this TuioObject.
         * @return	the rotation speed of this TuioObject
         */
        public float getRotationSpeed()
        {
            return rotation_speed;
        }

        /**
         * Returns the rotation acceleration of this TuioObject.
         * @return	the rotation acceleration of this TuioObject
         */
        public float getRotationAccel()
        {
            return rotation_accel;
        }

        /**
         * Returns true of this TuioObject is moving.
         * @return	true of this TuioObject is moving
         */
        public new bool isMoving()
        {
            if ((state == TUIO_ACCELERATING) || (state == TUIO_DECELERATING) || (state == TUIO_ROTATING)) return true;
            else return false;
        }

    }

}
