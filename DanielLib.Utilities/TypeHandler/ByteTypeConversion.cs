using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DanielLib.Utilities.TypeHandler
{
    /// <summary>
    /// 数组、流、文件之间的类型转换;
    /// </summary>
    public static class ByteTypeConversion
    {
        /// <summary>
        /// StreamToBytes
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        /// <summary>
        /// BytesToStream
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        /// <summary>
        /// BytesToStream
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static Stream BytesToStream(byte[] bytes, int count)
        {
            Stream stream = new MemoryStream(bytes, 0, count);
            return stream;
        }

        /// <summary>
        /// BytesToStream
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static Stream BytesToStream(byte[] bytes, int index, int count)
        {
            Stream stream = new MemoryStream(bytes, index, count);
            return stream;
        }

        /// <summary>
        /// StreamToFile
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileName"></param>
        public static void StreamToFile(Stream stream, string fileName)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Seek(0, SeekOrigin.Begin);

            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(bytes);
            bw.Close();
            fs.Close();
        }

        /// <summary>
        /// FileToStream
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static Stream FileToStream(string fileName)
        {
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        #region Stream转换成String
        public static string ReadAllAsString(this Stream stream)
        {
            using (var sr = new StreamReader(stream))
            {
                return sr.ReadToEnd();
            }
        }
        #endregion

        #region Stream转换成Byte[]
        public static byte[] ReadAllAsBytes(this Stream stream)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
        #endregion
    }
}
