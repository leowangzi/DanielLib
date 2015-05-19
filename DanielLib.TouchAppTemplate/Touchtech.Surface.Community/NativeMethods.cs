using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Touchtech.Surface
{
    internal class NativeMethods
    {
        /// <summary>
        /// The DeleteObject function deletes a logical pen, brush, font, bitmap, region, or palette, freeing all system resources associated with the object. After the object is deleted, the specified handle is no longer valid.
        /// </summary>
        /// <param name="hObject">A handle to a logical pen, brush, font, bitmap, region, or palette.</param>
        /// <returns>If the function succeeds, the return value is nonzero. If the specified handle is not valid or is currently selected into a DC, the return value is zero.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1414:MarkBooleanPInvokeArgumentsWithMarshalAs", Justification = "Adding this attribute causes an error."), DllImport("gdi32.dll")]
        internal static extern bool DeleteObject(IntPtr hObject);

        /// <summary>
        /// The SetProp function adds a new entry or changes an existing entry in the property list of the specified window. The function adds a new entry to the list if the specified character string does not exist already in the list. The new entry contains the string and the handle. Otherwise, the function replaces the string's current handle with the specified handle.
        /// </summary>
        /// <remarks>
        /// <para>Before a window is destroyed (that is, before it returns from processing the WM_NCDESTROY message), an application must remove all entries it has added to the property list. The application must use the RemoveProp function to remove the entries.</para>
        /// <para>Windows 95/98/Me: SetPropW is supported by the Microsoft Layer for Unicode (MSLU). SetPropA is also supported to provide more consistent behavior across all Microsoft Windows operating systems. To use this, you must add certain files to your application, as outlined in Microsoft Layer for Unicode on Windows 95/98/Me Systems.</para>
        /// <para>Windows Vista: SetProp is subject to the restrictions of User Interface Privilege Isolation (UIPI). A process can only call this function on a window belonging to a process of lesser or equal integrity level. When UIPI blocks property changes, GetLastError will return 5.</para>
        /// </remarks>
        /// <param name="hWnd">Handle to the window whose property list receives the new entry.</param>
        /// <param name="lpString">Pointer to a null-terminated string or contains an atom that identifies a string. If this parameter is an atom, it must be a global atom created by a previous call to the GlobalAddAtom function. The atom must be placed in the low-order word of lpString; the high-order word must be zero.</param>
        /// <param name="hData">Handle to the data to be copied to the property list. The data handle can identify any value useful to the application. </param>
        /// <returns>If the data handle and string are added to the property list, the return value is nonzero. If the function fails, the return value is zero. To get extended error information, call GetLastError.</returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetProp(IntPtr hWnd, string lpString, IntPtr hData);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern ushort GlobalAddAtom(string lpString);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        public static extern ushort GlobalDeleteAtom(ushort nAtom);

        public const string MICROSOFT_TABLETPENSERVICE_PROPERTY = "MicrosoftTabletPenServiceProperty";
        public const uint TABLET_DISABLE_PRESSANDHOLD = 0x00000001;
        public const uint TABLET_DISABLE_PENTAPFEEDBACK = 0x00000008;
        public const uint TABLET_DISABLE_PENBARRELFEEDBACK = 0x00000010;
        public const uint TABLET_DISABLE_TOUCHUIFORCEON = 0x00000100;
        public const uint TABLET_DISABLE_TOUCHUIFORCEOFF = 0x00000200;
        public const uint TABLET_DISABLE_TOUCHSWITCH = 0x00008000;
        public const uint TABLET_DISABLE_FLICKS = 0x00010000;
        public const uint TABLET_ENABLE_FLICKSONCONTEXT = 0x00020000;
        public const uint TABLET_ENABLE_FLICKLEARNINGMODE = 0x00040000;
        public const uint TABLET_DISABLE_SMOOTHSCROLLING = 0x00080000;
        public const uint TABLET_DISABLE_FLICKFALLBACKKEYS = 0x00100000;
        public const uint TABLET_ENABLE_MULTITOUCHDATA = 0x01000000;
    }
}
