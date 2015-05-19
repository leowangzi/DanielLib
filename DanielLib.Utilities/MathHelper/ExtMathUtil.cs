using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DanielLib.Utilities.MathHelper
{
    /// <summary>
    /// 一些简单的数学方法
    /// </summary>
    public static class ExtMathUtil
    {
        /// <summary>
        /// 弧度转换成角度
        /// </summary>
        /// <param name="radians"></param>
        /// <returns></returns>
        public static float RadiansToDegrees(float radians)
        {
            float res = radians * (float)(180 / Math.PI);
            return res;
        }

        /// <summary>
        /// 角度转换成弧度
        /// </summary>
        /// <param name="degrees"></param>
        /// <returns></returns>
        public static float DegreesToRadians(float degrees)
        {
            return degrees * (float)(Math.PI / 180);
        }

        /// <summary>
        /// 勾股定理
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static float GetHypotenuse(float a, float b)
        {
            return (float)Math.Sqrt(a * a + b * b);
        }
    }
}
