using System;
using AddressProcessing.CSV;

namespace AddressProcessing.Address
{
    public class AddressFileProcessor
    {
        private readonly object _mailShot;

        public AddressFileProcessor(object mailShot)
        {
            if (mailShot == null) throw new ArgumentNullException("mailShot");
            _mailShot = mailShot;
        }

        public void Process(string inputFile)
        {
            try
            {
                var reader = new CSVReaderWriter();
                reader.Open(inputFile, CSVReaderWriter.Mode.Read);

                string column1, column2;

                while (reader.Read(out column1, out column2))
                {
                    if (_mailShot is v1.IMailShot)
                    {
                        ((v1.IMailShot)(_mailShot)).SendMailShot(column1, column2);
                    }
                    else if (_mailShot is v2.IMailShot)
                    {
                        ((v2.IMailShot)(_mailShot)).SendEmailMailShot(column1, column2);
                    }

                    //_mailShot.SendMailShot(column1, column2);
                }

                //reader.Close();
            }
            catch (Exception ex)
            {
                //log error to identify and rectify
            }

        }
    }
}
