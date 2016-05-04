using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jImaging
{
    public class ExifSort
    {
        private readonly string _inputPath;

        public ExifSort(string inputPath)
        {
            _inputPath = inputPath;
        }

        public void Sort()
        {
            var inputFiles = Directory.GetFiles(_inputPath, "*.jpg");

            foreach (var inputFile in inputFiles)
            {
                if (inputFile == null) continue;

                var reader = new ExifReader(inputFile);
                var picDate = reader[ExifTagName.ExifDTOrig]?.ToString();
                var allData = reader.AllData();
                reader.Dispose();
                DateTime dtDate;
                if (picDate != null && DateTime.TryParseExact(picDate, "yyyy:MM:dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtDate))
                {
                    // Place image in yyyy-mm folder
                    var outPath = Path.Combine(_inputPath, dtDate.ToString("yyyy-MM"));
                    var outFile = Path.Combine(outPath, Path.GetFileName(inputFile));
                    if (Directory.Exists(outPath) == false)
                        Directory.CreateDirectory(outPath);

                    Console.WriteLine($"Sorting file: {inputFile}...");
                    File.Move(inputFile, outFile);
                }
                else
                {
                    // Place image in "unknown" folder
                    var outPath = Path.Combine(_inputPath, "unknown");
                    var outFile = Path.Combine(outPath, Path.GetFileName(inputFile));
                    if (Directory.Exists(outPath) == false)
                        Directory.CreateDirectory(outPath);

                    File.Move(inputFile, outFile);
                }
            }


        }
    }
}
