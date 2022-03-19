using System;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace InterfaceTesting
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            GetNgrokAPI();
            //SendTwilioMessage();

            Console.ReadLine();
        }

        private static void SendTwilioMessage()
        {
            string accountSid = "AC6c0c64f5fbd6ca40793345dd631ba2db";
            string authToken = "bf1dd2f72ac5316bd187b36f54071180";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                from: new Twilio.Types.PhoneNumber("whatsapp:+14155238886"),
                body: "Hello, there!",
                to: new Twilio.Types.PhoneNumber("whatsapp:+4917647270701")
            );
        }

        private static void GetNgrokAPI()
        {
        }
    }
}