using System;
using System.Net;
using System.Net.Mail;

class EmailHandler {
    private SmtpClient smtpClient;


    public EmailHandler() {
        smtpClient = new SmtpClient("smtp.gmail.com", 587) {
            Credentials = new NetworkCredential("jrsmithhennesy@gmail.com", "npal yhye tkoo bgsd"),
            EnableSsl = true 
        };
    }

    public void Send(string userEmail, string subject, string body) {
        try {
            this.smtpClient.Send("jrsmithhennesy@gmail.com", userEmail, subject, body);
        }catch (Exception ex) {
            Console.WriteLine(ex.Message);
        }
    }

}