using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Surface.Presentation.Controls;
using Microsoft.Surface.Presentation;

namespace DanielLib.Surface.Presentation.Common
{
    public static class SurfaceWindowExtension
    {
        public static void ClearAllSurfaceDragCursor(this SurfaceWindow _window)
        {
            foreach (SurfaceDragCursor cursor in SurfaceDragDrop.GetAllCursors(_window))
            {
                SurfaceDragDrop.CancelDragDrop(cursor);
            }
        }
    }
}
