using AddressProcessing.Address;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//using NUnit.Framework;

namespace AddressProcessing.Tests
{
    [TestClass]
    public class AddressFileProcessorTests
    {
        private FakeMailShotService _fakeMailShotService;
        private FakeV2MailShotService _fakeV2MailShotService;
        private const string TestInputFile = @"test_data\contacts.csv";

        [TestInitialize]
        public void SetUp()
        {
            _fakeMailShotService = new FakeMailShotService();
            _fakeV2MailShotService = new FakeV2MailShotService();
        }

        [TestMethod]
        public void Should_send_mail_using_mailshot_service()
        {
            var processor = new AddressFileProcessor(_fakeMailShotService);
            processor.Process(TestInputFile);

            Assert.AreEqual(_fakeMailShotService.Counter, 229);
        }

        internal class FakeMailShotService : Address.v1.IMailShot
        {
            internal int Counter { get; private set; }

            public void SendMailShot(string name, string address)
            {
                Counter++;
            }
        }


        [TestMethod]
        public void Test_SendEmail_Using_V2MailShot_Service()
        {
            var processor = new AddressFileProcessor(_fakeV2MailShotService);
            processor.Process(TestInputFile);

            Assert.AreEqual(_fakeV2MailShotService.Counter, 229);
        }

        internal class FakeV2MailShotService : Address.v2.IMailShot
        {
            internal int Counter { get; private set; }

            public void SendEmailMailShot(string name, string email)
            {
                Counter++;
            }

            public void SendPostalMailShot(string name, string address1, string town, string county, string country, string postCode)
            {
                Counter++;
            }

            public void SendSmsMailShot(string name, string number)
            {
                Counter++;
            }
        }
    }
}