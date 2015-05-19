using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DanielLib.Util.SimpleMail
{
    public enum SendState
    {
        None,
        Running,
        Finish,
        Failed,
        Canceled,
        NoNet,
    }
}
