using ProductSellingWorkflow.Service.NotificationHandlers;
using ProductSellingWorkflow.Service.Notifications;
using System;
using System.Configuration;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Threading;

namespace ProductSellingWorkflow.NotificationHandlers
{
	public class EmailNotificationHandler : NotificationHandlerBase<EmailNotification>
	{
		public override bool CanHandle(INotification notification)
		{
			return notification is EmailNotification;
		}

		public override void Handle(EmailNotification notification)
		{
			SendAsync(notification.From, notification.From, notification.To, notification.Subject, notification.Message);
		}

		private bool SendAsync(string from, string displayName, string to, string subject, string message)
		{
			var mail = CreateMailMessage(from, displayName, to, subject, message);

			return SendAsync(mail);
		}

		private MailMessage CreateMailMessage(string from, string displayName, string to, string subject, string message)
		{
			var mail = new MailMessage();

			mail.From = new MailAddress(from, displayName);
			mail.To.Add(to);
			mail.Subject = subject;
			mail.IsBodyHtml = true;
			mail.Body = message;

			return mail;
		}

		private bool SendAsync(MailMessage mail)
		{
			var client = CreateSmtpClient(mail);

			try
			{
				ThreadPool.QueueUserWorkItem(x => client.SendAsync(mail, mail));
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}

		private SmtpClient CreateSmtpClient(MailMessage mail)
		{
			if (mail == null)
				throw new ArgumentNullException(nameof(mail));

			var client = new SmtpClient();
			var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

			client.Credentials = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
			client.SendCompleted += (s, e) =>
			{
				client.Dispose();
				mail.Dispose();
			};

			return client;
		}
	}
}