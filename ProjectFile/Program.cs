using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;

namespace LSEG_API
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string projectDir = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;  // get the current project directory
            string resourceDir = Path.Combine(projectDir, "Resources"); // combine the resources directory to the directory path where the csv files are
            string[] csvFiles = Directory.GetFiles(resourceDir, "*.csv",SearchOption.AllDirectories); // read all the csv files from the directory and sub-directories
            string outputDir=Path.Combine(projectDir, "Output"); // the output directory where the csv output file will be located.
            try
            {
                Directory.CreateDirectory(outputDir); // create directory if not created
            }
            catch (Exception e) 
            {
                Console.WriteLine("The process failed: {0}", e.Message);
            }

             foreach (string csvFile in csvFiles )
            {
                try
                {
                    secondAPI(firstAPI(csvFile), outputDir, csvFile);
                }
                catch(Exception e) 
                {
                    Console.WriteLine($"Error Processing file {csvFile} : {e.Message}");
                    continue;
                }
            } 

        }

        public static IEnumerable<Stocks> firstAPI(string filePath)
        {
            try
            {
                var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false // Set to true if your CSV file has a header
                };
                var reader = new StreamReader(filePath);
                var csv = new CsvReader(reader, csvConfig);
                var records = csv.GetRecords<Stocks>().ToList();

                // Use random to get a random number. Startindex is getting an value between 0 and counts of the record which was read- 30 . 
                // If the file have 100 entries, the random method generate a number between 0 and 69
                Random random = new Random();
                int startIndex = random.Next(0, records.Count - 30);
                Console.WriteLine(startIndex);

                return records.Skip(startIndex).Take(30);  // return the records by skipping the first index and take the next 30 entries.  Basically is starts from the random number
            }
            catch(Exception e) 
            {
                Console.WriteLine($"Error reading file {filePath} : {e.Message}");
                return Enumerable.Empty<Stocks>();
            }
            
        }

        public static void secondAPI(IEnumerable<Stocks> stocks,string outlier, string inputFile)
        {
            try
            {
                string fileName = Path.GetFileNameWithoutExtension(inputFile);
                string outputfile = Path.Combine(outlier, $"{fileName}_output.csv");
                using (var writer = new StreamWriter(outputfile))
                {
                    var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HasHeaderRecord = false
                    };
                    var csvOut = new CsvWriter(writer, csvConfig);
                    csvOut.WriteRecords(stocks);
                }
            }
            catch (Exception e) 
            {
                Console.WriteLine($"Error writing to file: {e.Message}");
            }
            
            
        }
    }
}
