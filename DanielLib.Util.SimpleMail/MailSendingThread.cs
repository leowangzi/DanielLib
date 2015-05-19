using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections.ObjectModel;

namespace DanielLib.Util.SimpleMail
{
    public class MailSendingThread
    {
        public MailSendingThread(string FromName, string FromAddress, string FromPassWord, string FromSMTP, string FromTitle, string FromText, string FromHtml, ObservableCollection<MailInfo> obc)
        {
            title = FromTitle;
            text = FromText;
            html = FromHtml;

            isSending = true;
            mailManager = obc;
            backgroundThr = new Thread(new ThreadStart(DoMoreWork));
            backgroundThr.IsBackground = true;

            sm = new SentMail(FromName, FromAddress, FromPassWord, FromSMTP);
            sm.EFinished += new EFinishedHandler(FinishedHandlerFunction);
        }
        public SentMail sm {get; set; }
        public Thread backgroundThr  { get; set; }
        public ObservableCollection<MailInfo> mailManager { get; set; }
        public bool isSending { get; set; }

        private string title;
        private string text;
        private string html;

        public void StartThread()
        {
            backgroundThr.Start();
        }

        void FinishedHandlerFunction(object sender, MailSentEventArgs e)
        {
            MailInfo i = e.mailInfo;

            if (i != null)
            {
                if (mailManager.Contains(i))
                {
                    mailManager.Remove(i);
                }
                i = null;
            }

            isSending = true;
        }

        void DoMoreWork()
        {
            while (true)
            {
                if (mailManager!= null && mailManager.Count > 0)
                {
                    if (isSending)
                    {
                        sm.SetToMailInfo(title, text, html, mailManager.First());
                        sm.SimpleSentMail();
                        isSending = false;
                    }
                }
                else
                {
                    sm.SetStateNone();
                }

                Thread.Sleep(3000);
            }
        }
    }
}
