using System.Linq;
//using NUnit.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AddressProcessing.CSV;
using System.IO;

namespace Csv.Tests
{
    [TestClass]
    public class CSVReaderWriterTests
    {
        private const string testInputFile = @"test_data\contacts.csv";
        private const string testOutputFile = @"test_data\output.csv";
        char[] separator = { '\t' };


        [TestMethod]
        public void TestCSVWrite()
        {
            CSVReaderWriter writer = new CSVReaderWriter();

            writer.Open(testOutputFile, CSVReaderWriter.Mode.Write);

            string[] line = File.ReadAllLines(testInputFile);

            int maxLines = 5;

            for (int i = 0; i < maxLines; i++)
            {
                writer.Write(line[i].Split(separator));
            }

            var outputFile = new FileInfo(testOutputFile);
            Assert.IsTrue(outputFile.Exists);
            Assert.IsTrue(outputFile.Length > 0);
            Assert.AreEqual(File.ReadAllLines(outputFile.FullName).Count(), maxLines);

        }
    }
}
