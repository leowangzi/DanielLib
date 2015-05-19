using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Animation;

namespace DanielLib.Surface.Presentation
{
    /// <summary>
    /// Different animation equations
    /// </summary>
    public enum PointEasingFunction
    {
        Quad,        
        Expo
    }

    /// <summary>
    /// Different animation modes
    /// </summary>
    public enum PointEasingMode
    {
        EaseIn,
        EaseOut,
        EaseInOut
    }

    public class WPFPointAnimationHelper
    {


        public WPFPointAnimationHelper()
        {
        }

        /// <summary>
        /// Setup the animation for the point
        /// </summary>
        /// <param name="element">Element to run the animation on</param>
        /// <param name="prop">Property to run the animation on</param>
        /// <param name="function">Type of easing equation</param>
        /// <param name="mode">type of easing mode</param>
        /// <param name="to">destination point</param>
        /// <param name="durationMS">duration of the animation in milliseconds</param>
        /// <param name="callbackFunc">callback function</param>
        /// <returns></returns>
        public static AnimationClock AnimatePointEasingEquation(
            DependencyObject element,
            DependencyProperty prop,
            PointEasingFunction function,
            PointEasingMode mode,
            Point to,
            int durationMS,
            EventHandler callbackFunc)
        {
            return AnimatePointEasingEquation(element, prop, function, mode, null, to, durationMS, callbackFunc);
        }

        /// <summary>
        /// Setup the animation for the point
        /// </summary>
        /// <param name="element">Element to run the animation on</param>
        /// <param name="prop">Property to run the animation on</param>
        /// <param name="function">Type of easing equation</param>
        /// <param name="mode">type of easing mode</param>
        /// <param name="from">starting point</param>
        /// <param name="to">destination point</param>
        /// <param name="durationMS">duration of the animation in milliseconds</param>
        /// <param name="callbackFunc">callback function</param>
        /// <returns></returns>
        public static AnimationClock AnimatePointEasingEquation(
            DependencyObject element,
            DependencyProperty prop,
            PointEasingFunction function,
            PointEasingMode mode,
            Point? from,
            Point to,
            int durationMS,
            EventHandler callbackFunc)
        {
            Point defaultFrom = element.GetValue(prop) == null ?
                                 new Point(0,0) :
                                 (Point)element.GetValue(prop);

            AnimationTimeline anim = GetPointEasingAnimation(function, mode, from.GetValueOrDefault(defaultFrom), to, durationMS);
            return Animate(element, prop, anim, durationMS, null, null, callbackFunc);
        }

        /// <summary>
        /// Setup the animation for the point
        /// </summary>
        /// <param name="element">Element to run the animation on</param>
        /// <param name="prop">Property to run the animation on</param>
        /// <param name="from">starting point</param>
        /// <param name="to">destination point</param>
        /// <param name="acceleration">acceleration rate</param>
        /// <param name="deceleration">deceleration rate</param>
        /// <param name="durationMS">duration of the animation in milliseconds</param>
        /// <param name="callbackFunc">callback function</param>
        public static AnimationClock AnimatePointEasingEquation(
            DependencyObject element,
            DependencyProperty prop,
            Point? from,
            Point to,
            int durationMS,
            double? acceleration,
            double? deceleration,
            EventHandler callbackFunc)
        {
            Point defaultFrom = element.GetValue(prop) == null ?
                                 new Point(0, 0) :
                                 (Point)element.GetValue(prop);

            PointAnimation anim = new PointAnimation();
            anim.From = from.GetValueOrDefault(defaultFrom);
            anim.To = to;

            return Animate(element, prop, anim, durationMS, null, null, callbackFunc);
        }

        /// <summary>
        /// Setup the animation for the point
        /// </summary>
        /// <param name="animatable">Element to run the animation on</param>
        /// <param name="prop">Property to run the animation on</param>
        /// <param name="anim">animation timeline</param>
        /// <param name="duration">animation duration</param>
        /// <param name="acceleration">acceleration rate</param>
        /// <param name="deceleration">deceleration rate</param>
        /// <param name="callbackFunc">callback function</param>
        private static AnimationClock AnimatePointEasingEquation(
            DependencyObject animatable,
            DependencyProperty prop,
            AnimationTimeline anim,
            int duration,
            double? acceleration,
            double? deceleration,
            EventHandler func
            )
        {
            if (acceleration.HasValue)
            {
                anim.AccelerationRatio = acceleration.GetValueOrDefault(0);
            }

            if (deceleration.HasValue)
            {
                anim.DecelerationRatio = deceleration.GetValueOrDefault(0);
            }

            anim.Duration = TimeSpan.FromMilliseconds(duration);
            anim.Freeze();

            AnimationClock animClock = anim.CreateClock();

            // When animation is complete, remove animation and set the animation's "To" 
            // value as the new value of the property.
            EventHandler eh = null;
            eh = delegate(object sender, EventArgs e)
            {
                animatable.SetValue(prop, animatable.GetValue(prop));

                ((IAnimatable)animatable).ApplyAnimationClock(prop, null);

                animClock.Completed -= eh;
            };

            animClock.Completed += eh;

            // assign completed eventHandler, if defined
            if (func != null)
                animClock.Completed += func;

            animClock.Controller.Begin();

            // goferit
            ((IAnimatable)animatable).ApplyAnimationClock(prop, animClock);

            return animClock;

        }

        private static AnimationClock Animate(
            DependencyObject animatable,
            DependencyProperty prop,
            AnimationTimeline anim,
            int duration,
            double? acceleration,
            double? deceleration,
            EventHandler func
            )
        {
            if (acceleration.HasValue)
            {
                anim.AccelerationRatio = acceleration.GetValueOrDefault(0);
            }

            if (acceleration.HasValue)
            {
                anim.DecelerationRatio = deceleration.GetValueOrDefault(0);
            }
            anim.Duration = TimeSpan.FromMilliseconds(duration);
            anim.Freeze();

            AnimationClock animClock = anim.CreateClock();

            // When animation is complete, remove animation and set the animation's "To" 
            // value as the new value of the property.
            EventHandler eh = null;
            eh = delegate(object sender, EventArgs e)
            {
                animatable.SetValue(prop, animatable.GetValue(prop));

                ((IAnimatable)animatable).ApplyAnimationClock(prop, null);

                animClock.Completed -= eh;
            };

            animClock.Completed += eh;

            // assign completed eventHandler, if defined
            if (func != null)
                animClock.Completed += func;

            animClock.Controller.Begin();

            // goferit
            ((IAnimatable)animatable).ApplyAnimationClock(prop, animClock);

            return animClock;

        }

        /// <summary>
        /// Select the appropriate animation via switch
        /// </summary>
        /// <param name="function">function to swich on</param>
        /// <param name="mode">easing mode to use</param>
        /// <param name="from">Starting Point</param>
        /// <param name="to">End point</param>
        /// <param name="durationMS">duration of animation</param>
        /// <returns></returns>
        private static AnimationTimeline GetPointEasingAnimation(PointEasingFunction function, PointEasingMode mode, Point from, Point to, int durationMS)
        {
            AnimationTimeline returnTimeline = null;
            switch (function)
            {
                case PointEasingFunction.Quad:
                    returnTimeline = new QuadPointEasingAnimation(from, to, mode, new Duration(new TimeSpan(0, 0, 0, 0, durationMS)));
                    break;
                case PointEasingFunction.Expo:
                    returnTimeline = new ExpoPointEasingAnimation(from, to, mode, new Duration(new TimeSpan(0, 0, 0, 0, durationMS)));
                    break; 
                default:
                    break;
            }

            return returnTimeline;
        }        

    }
}

