//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using RazorEngine.Templating;
//using REscraper;
//using REscraper.Client;
//using System.Net;
//using System.Net.Mail;
//using System.Net.Mime;
//using REscraper.Models;

//namespace REconsole
//{
//    class Program2
//    {
//        static void Main2(string[] args)
//        {
//            //var refresh = new RefreshDB();
//            //refresh.Scrape();

//            var templateFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EmailTemplates");

//            // Create a model for our email
//            var model = new UserEmail() { Name = "Andrew Hinger", Email = "andrewjhinger@gmail.com" };

//            // Generate the email body from the template file.
//            // 'templateFilePath' should contain the absolute path of your template file.
//            var templateService = new TemplateService();
//            var emailHtmlBody = templateService.Parse(File.ReadAllText(@"C:\Users\andrew\Documents\Visual Studio 2013\Projects\REscraper\REconsole\bin\Debug\EmailTemplates\Email.cshtml"), model, null, null);


//            var client = new SmtpClient("smtp.gmail.com", 587)
//            {
//                Credentials = new NetworkCredential("andrewjhinger@gmail.com", "Tenstar6"),
//                EnableSsl = true
//            };

//            //var email = new MailMessage()
//            //{
//            //    Body = emailHtmlBody,
//            //    IsBodyHtml = true,
//            //    Subject = "Welcome",
//            //    From = "andrewjhinger@gmail.com"
                
//            //};

//            //email.To.Add(new MailAddress(model.Email, model.Name));
//            // The From field will be populated from the app.config value by default

//            //client.Send(email);
//            //string body = "";
//            client.Send("andrewjhinger@gmail.com", "andrewjhinger@gmail.com", "test hello", emailHtmlBody);
//            //Console.WriteLine("Sent");
//            //Console.ReadLine();
//        }

        

//    }
//}
