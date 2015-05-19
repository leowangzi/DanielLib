using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.ComponentModel;
using System.Windows;

namespace DanielLib.Util.SimpleMail
{
    //event
    public delegate void FinishedHandler(object sender, EventArgs e);

    public delegate void EFinishedHandler(object sender, MailSentEventArgs e);

    public class SentMail : INotifyPropertyChanged
    {
        private MailAddress from;
        private MailMessage mail = null;
        private SmtpClient client = new SmtpClient();
        public MailInfo mailinfo = null;


        //event
        public event FinishedHandler Finished;
        protected virtual void OnFinished(EventArgs e)
        {
            if (Finished != null)
                Finished(this, e);
        }
        void RaiseFinishedEvent()
        {
            OnFinished(new EventArgs());
        }

        //e event
        public event EFinishedHandler EFinished;
        protected virtual void OnEfinished(MailSentEventArgs e)
        {
            if (EFinished != null)
            {
                EFinished(this, e);
            }
        }

        void RaiseEFinishedEvent(SendState s, MailInfo m)
        {
            OnEfinished(new MailSentEventArgs(s, m));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// name
        /// </summary>
        public string ClientFromName { get; set; }

        /// <summary>
        /// username
        /// </summary>
        public string ClientMailAddress { get; set; }

        /// <summary>
        /// password
        /// </summary>
        public string ClientMailPassWord { get; set; }

        public string ClientSMTP { get; set; }

        /// <summary>
        /// send state
        /// </summary>
        private SendState state;
        public SendState State
        {
            get
            {
                return state;
            }
            private set
            {
                if (state != value)
                {
                    state = value;
                    this.OnPropertyChanged("State");
                }
            }
        }
            
       

#region 构造函数
        public SentMail()
        {
            ClientMailAddress = @"leowangzi@163.com";
            ClientMailPassWord = @"198204055017";
            ClientSMTP = "smtp.163.com";

            InitOnce();
        }

        public SentMail(string FromName)
        {
            ClientMailAddress = @"leowangzi@163.com";
            ClientMailPassWord = @"198204055017";
            ClientFromName = FromName;
            ClientSMTP = "smtp.163.com";

            InitOnce();
        }

        public SentMail(string FromName, string FromMailAddress, string FromMailPassWord, string smtp)
        {
            ClientMailAddress = FromMailAddress;
            ClientMailPassWord = FromMailPassWord;
            ClientFromName = FromName;
            ClientSMTP = smtp;

            InitOnce();
        }
#endregion
        public void SetToMailInfo(string MailTile, string MailPlainTextBody, string MailhtmlContentBody, MailInfo mi/*string CustomerName, string ToMailAdress, string AttachmentFileName*/)
        {
            if (mail != null)
            {
                mail = null;  
            }
            //
            if (mailinfo != null)
            {
                mailinfo = null;
            }
            mailinfo = mi;
            //
            mail = new MailMessage();
            mail.Subject = MailTile;
            mail.From = from;
            mail.To.Add(new MailAddress(mailinfo.MailAddress, mailinfo.MailName));
            mail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(MailPlainTextBody, null, "text/plain"));
            AlternateView htmlBody = AlternateView.CreateAlternateViewFromString(MailhtmlContentBody, null, "text/html");
            mail.AlternateViews.Add(htmlBody);
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
            mail.Headers.Add("Disposition-Notification-To", ClientMailAddress);

            if (mailinfo.AttachmentFile != "" && mailinfo.AttachmentFile != null && mailinfo.AttachmentFile != string.Empty)
            {
                string strFilePath = mailinfo.AttachmentFile;
                Attachment at = new Attachment(strFilePath);
                at.Name = System.IO.Path.GetFileName(strFilePath);
                at.NameEncoding = System.Text.Encoding.GetEncoding("gb2312");
                at.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                at.ContentDisposition.Inline = true;
                at.ContentDisposition.DispositionType = System.Net.Mime.DispositionTypeNames.Inline;
                string cid = at.ContentId;
                mail.Attachments.Add(at);
            }  
        }

        void client_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                State = SendState.Canceled;
            }
            else
            {
                if (e.Error == null)
                {
                    State = SendState.Finish;
                }
                else
                {
                    State = SendState.Failed;
                }
            }
            RaiseEFinishedEvent(State, mailinfo);
            //State = SendState.Finish;
            //RaiseFinishedEvent();//临时，不管成功与否，都返回成功事件
            //RaiseEFinishedEvent(State);
            mail = null;
            mailinfo = null;
        }

#region PUBLIC
        public void SetStateNone()
        {
            State = SendState.None;
        }

        public void InitOnce()
        {
            State = SendState.None;

            from = new MailAddress(ClientMailAddress, ClientFromName);

            client.Host = ClientSMTP;
            client.UseDefaultCredentials = true;
            client.Credentials = new System.Net.NetworkCredential(ClientMailAddress, ClientMailPassWord);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.SendCompleted += new SendCompletedEventHandler(client_SendCompleted);
        }

        public void SimpleSentMail()
        {
            try
            {
                if (mail != null)
                {
                    client.SendAsync(mail, null);

                    State = SendState.Running;
                }
            }
            catch
            {
                State = SendState.NoNet;
                RaiseEFinishedEvent(State, mailinfo);
                return;
            }
        }

        public void SimpleCancelMail()
        {
            try
            {
                if (mail != null)
                {
                    client.SendAsyncCancel();

                    //State = SendState.Canceled;
                }
            }
            catch
            {
            	return;
            }
        }
#endregion    
    }
}
