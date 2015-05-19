using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Touchtech.Surface
{
    internal class TabletUtil
    {
        public static void DisableTabletGestures(IntPtr handle)
        {
            var atom = NativeMethods.GlobalAddAtom(NativeMethods.MICROSOFT_TABLETPENSERVICE_PROPERTY);
            uint flags = 
                NativeMethods.TABLET_DISABLE_FLICKS |
                NativeMethods.TABLET_DISABLE_PENBARRELFEEDBACK |
                NativeMethods.TABLET_DISABLE_PENTAPFEEDBACK |
                NativeMethods.TABLET_DISABLE_PRESSANDHOLD;
            NativeMethods.SetProp(handle, NativeMethods.MICROSOFT_TABLETPENSERVICE_PROPERTY, new IntPtr(flags));
            NativeMethods.GlobalDeleteAtom(atom);
        }
    }
}
