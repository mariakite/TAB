using System.Net.Mail;

public class communicate
{
    public static void sentMail(string email, string message, string team, bool isHtml = false)
    {
        sender(email, message, team, isHtml);
    }

    private static void sender(string emailTo, string textMessage, string subjectMessage, bool isHtml = false)
    {
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress(constant.str.email, constant.str.email);
        mail.To.Add(emailTo);

        //set the content
        mail.Subject = subjectMessage;
        mail.Body = textMessage;
        mail.IsBodyHtml = isHtml;

        //send the message
        SmtpClient smtp = new SmtpClient(constant.str.smtp);
        smtp.Port = constant.str.smtp_port;
        smtp.EnableSsl = true;
        smtp.Credentials = new System.Net.NetworkCredential(constant.str.email, constant.str.email_password);
        try
        {
            smtp.Send(mail);
        }
        finally
        {
            mail.Dispose();
            smtp = null;
        }
    }
}
