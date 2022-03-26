using System;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Flugplatzfest_Terminal.MVVM.Model.Interfaces
{
    public class WhatsApp
    {
        private readonly string accountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
        private readonly string authToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN");

        public WhatsApp()
        {
            TwilioClient.Init(accountSid, authToken);
            var message = MessageResource.Create(
            from: new Twilio.Types.PhoneNumber("whatsapp:+14155238886"),
            body: "Hello, there!",
            to: new Twilio.Types.PhoneNumber("whatsapp:+4917647270701")
            );
        }
    }
}