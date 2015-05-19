using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DanielLib.Util.SimpleMail
{
    public class MailInfo
    {
        public MailInfo()
        {
            MailAddress = string.Empty;
            AttachmentFile = string.Empty;
            MailName = string.Empty;
        }

        public MailInfo(string m, string a, string n)
        {
            MailAddress = m;
            AttachmentFile = a;
            MailName = n;
        }
        public string MailAddress { get; set; }
        public string AttachmentFile { get; set; }
        public string MailName { get; set; }
    }
}
