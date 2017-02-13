using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Xml;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Web;
using System.Net.Mail;
using System.Net.Sockets;


namespace InternInterviewTask
{
    class EmailSender
    {

        static void Main(string[] args)
        {
            IMongoClient client;
            IMongoDatabase db;

            var settings = new MongoClientSettings();
            settings.Server = new MongoServerAddress("localhost", 27017);
            client = new MongoClient(settings);
            db = client.GetDatabase("mongodb");
            var collection1 = db.GetCollection<Merchants>("Merchants");
            var collection2 = db.GetCollection<Partners>("Partners");

            int pId;

            string body = string.Empty;
            using (StreamReader reader = new StreamReader("C:/Users/asma/Downloads/EmailTemplate.html"))
            {
                body = reader.ReadToEnd();

            }
            
            for (int i = 9; i < 10; i++)
            {
                var merchantDetails = collection1.Find(m => m.IdNo == i).ToListAsync().Result;
                
                foreach (var merchants in merchantDetails)
                {
                    body = body.Replace("[MERCHANT_NAME]", merchants.Name);
                    body = body.Replace("[MERCHANT_EMAILADDRESS]", merchants.EmailAddress );
                    body = body.Replace("[MERCHANT_PHONE]", merchants.Phone);
                    
                    pId = merchants.PartnerId;
                    var partnerDetails = collection2.Find(p => p._Id == pId).ToList();
                    
                    foreach (var partners in partnerDetails)
                    {
                        body = body.Replace("[PARTNER_BANNERIMAGEURL]", partners.BannerImageUrl);
                        body = body.Replace("[PARTNER_BUSINESSNAME]", partners.BusinessName);
                        
                    
                    }

                   
                    try
                    {
                        MailMessage mail = new MailMessage("[Username]", merchants.EmailAddress, "Thankyou mail", body);
                        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                        SmtpServer.Port = 587;
                       
                        SmtpServer.UseDefaultCredentials = false;
                        SmtpServer.Credentials = new System.Net.NetworkCredential("[Username]", "[Password]");
                        
                        SmtpServer.EnableSsl = true;
                        mail.IsBodyHtml = true;
                        //turn "less secure apps" option to "off" of your gmail account if required 
                        SmtpServer.Send(mail);
                        Console.Write("Mail sent to " + merchants.EmailAddress);
                        
                    }
                    catch(Exception ex)
                    {
                        Console.Write(ex.ToString());
                    }
                    Console.Read();


                }
            }
           
        }
        
       
        
    }
    public class Merchants
    {
        public ObjectId Id { get; set; }
        public int IdNo { get; set; }
        public string Name { get; set; }
        public int PartnerId { get; set; }
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
    }

    public class Partners
    {
        public ObjectId _id { get; set; }
        public int _Id { get; set; }
        public string BusinessName { get; set; }
        public string BannerImageUrl { get; set; }
        public string Colour { get; set; }
    }


}
