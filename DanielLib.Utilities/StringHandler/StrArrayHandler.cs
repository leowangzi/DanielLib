using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.IO;

namespace DanielLib.Utilities.StringHandler
{
    /// <summary>
    /// 字符串跟数组之间的转换类;
    /// </summary>
    public class StrArrayHandler
    {
        #region 字符串跟列表之前的转换
        /// <summary>
        /// 把字符串按照分隔符转换成列表集合;
        /// </summary>
        /// <param name="sourceStr">源字符串</param>
        /// <param name="speater">分隔符</param>
        /// <param name="toLower">是否转换成小写字母</param>
        /// <returns></returns>
        public static List<String> StrToArray(String sourceStr, Char speater, Boolean toLower)
        {
            List<String> list = new List<String>();
            String[] str = sourceStr.Split(speater);
            foreach(String strItem in str)
            {
                if (!String.IsNullOrEmpty(strItem) && strItem != speater.ToString())
                {
                    String strVal = strItem;
                    if (toLower)
                    {
                        strVal = strItem.ToLower();
                    }
                    list.Add(strVal);
                }
            }
            return list;
        }

        /// <summary>
        /// 把字符串按照分隔符转换成字符串数组
        /// </summary>
        /// <param name="sourceStr">源字符串</param>
        /// <param name="speater">分隔符</param>
        /// <returns></returns>
        public static String[] StrToArray(String sourceStr, Char speater)
        {
            return sourceStr.Split(speater);
        }

        /// <summary>
        /// 把列表按照分隔符转换成字符串;
        /// </summary>
        /// <param name="srcList">源字符串列表</param>
        /// <param name="speater">分隔符</param>
        /// <returns></returns>
        public static String ArrayToStr(List<String> srcList, String speater)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < srcList.Count; i++)
            {
                if (i == srcList.Count - 1)
                {
                    stringBuilder.Append(srcList[i].ToString());
                } 
                else
                {
                    stringBuilder.Append(srcList[i].ToString());
                    stringBuilder.Append(speater);
                }
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 把列表通过分隔符转换成字符串
        /// </summary>
        /// <param name="srcList"></param>
        /// <param name="speater"></param>
        /// <returns></returns>
        public static String ArrayToStr(List<int> srcList, String speater)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < srcList.Count; i++)
            {
                if (i == srcList.Count - 1)
                {
                    stringBuilder.Append(srcList[i].ToString());
                }
                else
                {
                    stringBuilder.Append(srcList[i].ToString());
                    stringBuilder.Append(speater);
                }
            }
            return stringBuilder.ToString();
        }
        #endregion

        #region 判断字符串是否是有效的数字ID
        /// <summary>
        /// 从字符串中获取正确的数字ID，不是正整数，返回0;
        /// </summary>
        /// <param name="srcStr"></param>
        /// <returns></returns>
        public static int StrToInt(String srcStr)
        {
            if (IsNumberId(srcStr))
            {
                return int.Parse(srcStr);
            } 
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 检查一个字符串是否是纯数字构成的，一般用于查询字符串参数的有效性验证。(0除外)
        /// </summary>
        /// <param name="_value">需验证的字符串。。</param>
        /// <returns>是否合法的bool值。</returns>
        public static bool IsNumberId(string _value)
        {
            return QuickValidate("^[1-9]*[0-9]*$", _value);
        }

        /// <summary>
        /// 快速验证一个字符串是否符合指定的正则表达式。
        /// </summary>
        /// <param name="_express">正则表达式的内容。</param>
        /// <param name="_value">需验证的字符串。</param>
        /// <returns>是否合法的bool值。</returns>
        public static bool QuickValidate(String _express, String _value)
        {
            if (_value == null) return false;
            Regex myRegex = new Regex(_express);
            if (_value.Length == 0)
            {
                return false;
            }
            return myRegex.IsMatch(_value);
        }
        #endregion

        #region MD5加密
        /// <summary>
        /// MD5字符串加密
        /// </summary>
        /// <param name="passWord">源字符串</param>
        /// <returns>加密字符串</returns>
        public static String GetStrUseMD5(String passWord)
        {
            MD5CryptoServiceProvider md5SerProvider = new MD5CryptoServiceProvider();
            Byte[] bytes = Encoding.UTF8.GetBytes(passWord);
            bytes = md5SerProvider.ComputeHash(bytes);
            StringBuilder stringBuilder = new StringBuilder();
            foreach(Byte item in bytes)
            {
                stringBuilder.Append(item.ToString("x2").ToUpper());
            }
            return stringBuilder.ToString();
        }


        #endregion

        #region 判断对象是否为空
        /// <summary>
        /// 判断对象是否为空，为空返回true
        /// </summary>
        /// <typeparam name="T">要验证的对象的类型</typeparam>
        /// <param name="data">要验证的对象</param>        
        public static bool IsNullOrEmpty<T>(T data)
        {
            //如果为null
            if (data == null)
            {
                return true;
            }

            //如果为""
            if (data.GetType() == typeof(String))
            {
                if (string.IsNullOrEmpty(data.ToString().Trim()))
                {
                    return true;
                }
            }

            //如果为DBNull
            if (data.GetType() == typeof(DBNull))
            {
                return true;
            }

            //不为空
            return false;
        }

        /// <summary>
        /// 判断对象是否为空，为空返回true
        /// </summary>
        /// <param name="data">要验证的对象</param>
        public static bool IsNullOrEmpty(object data)
        {
            //如果为null
            if (data == null)
            {
                return true;
            }

            //如果为""
            if (data.GetType() == typeof(String))
            {
                if (string.IsNullOrEmpty(data.ToString().Trim()))
                {
                    return true;
                }
            }

            //如果为DBNull
            if (data.GetType() == typeof(DBNull))
            {
                return true;
            }

            //不为空
            return false;
        }
        #endregion

        #region 将byte[]转换成int
        /// <summary>
        /// 将byte[]转换成int
        /// </summary>
        /// <param name="data">需要转换成整数的byte数组</param>
        public static int BytesToInt32(byte[] data)
        {
            //如果传入的字节数组长度小于4,则返回0
            if (data.Length < 4)
            {
                return 0;
            }

            //定义要返回的整数
            int num = 0;

            //如果传入的字节数组长度大于4,需要进行处理
            if (data.Length >= 4)
            {
                //创建一个临时缓冲区
                byte[] tempBuffer = new byte[4];

                //将传入的字节数组的前4个字节复制到临时缓冲区
                Buffer.BlockCopy(data, 0, tempBuffer, 0, 4);

                //将临时缓冲区的值转换成整数，并赋给num
                num = BitConverter.ToInt32(tempBuffer, 0);
            }

            //返回整数
            return num;
        }
        #endregion
    }
}
