using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DanielLib.Util.SimpleMail
{
    public class MailSentEventArgs : EventArgs
    {
        public SendState sendState;

        public MailInfo mailInfo;

        public MailSentEventArgs(SendState s, MailInfo m)
        {
            sendState = s;
            mailInfo = m;
        }
    }
}
