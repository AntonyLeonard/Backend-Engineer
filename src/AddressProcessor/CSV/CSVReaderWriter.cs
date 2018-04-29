using System;
using System.IO;

namespace AddressProcessing.CSV
{
    /*
        2) Refactor this class into clean, elegant, rock-solid & well performing code, without over-engineering.
           Assume this code is in production and backwards compatibility must be maintained.


        //My Comments:
        *) Added summary for all methods
        *) Also added inline comments for each modification
        
    */

    public class CSVReaderWriter
    {
        //private StreamReader _readerStream = null;
        //private StreamWriter _writerStream = null;

        //Declaring Contant variables as global variables 
        const int FIRST_COLUMN = 0;
        const int SECOND_COLUMN = 1;
        char[] separator = { '\t' };

        private string _fileName = null;
        int i = 0;
        string[] lines;


        [Flags]
        public enum Mode { Read = 1, Write = 2 };


        /// <summary>
        /// Initilizes read or write files
        /// </summary>
        /// <param name="fileName">input/output file</param>
        /// <param name="mode">Read or Write mode</param>
        public void Open(string fileName, Mode mode)
        {
            _fileName = fileName;
            i = 0;

            if (mode == Mode.Read)
            {
                lines = File.ReadAllLines(_fileName);           //Reads all lines instead of keeping stream reader open. 
            }
            else if (mode == Mode.Write)
            {
                File.Create(_fileName).Dispose();               //Creates output file which will be later be used to append line. 
            }
            else
            {
                throw new Exception("Unknown file mode for " + fileName);
            }
        }

        /// <summary>
        /// Writes given columns as a line to output file defined in Open method
        /// </summary>
        /// <param name="columns">array of string to be written as a line</param>
        public void Write(params string[] columns)
        {
            try
            {
                string outPut = string.Join("\t", columns);                         //string.Join replaced the for loop to format a line.

                using (StreamWriter writer = new StreamWriter(_fileName, true))     //Append line of text to output file
                {
                    writer.WriteLine(outPut);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        //Deleted the other Read method, instead the same following method can be used


        /// <summary>
        /// Reads the current line and splits first two columns
        /// </summary>
        /// <param name="column1">First column of current line</param>
        /// <param name="column2">Second column of current line</param>
        /// <returns>true on successful read and split</returns>
        public bool Read(out string column1, out string column2)
        {
            column1 = null;
            column2 = null;

            //Added try catch block and improved readability by removing unnecessary if else block
            try
            {
                if (i < lines.Length)
                {
                    string[] columns = lines[i].Split(separator);
                    i++;

                    if (columns.Length > 0)
                    {
                        column1 = columns[FIRST_COLUMN];
                        column2 = columns[SECOND_COLUMN];
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                column1 = null;
                column2 = null;
            }


            return false;
        }



    }
}
