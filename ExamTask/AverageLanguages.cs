using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamTask
{
    public class AverageLanguages
    {
        //class for Task 7
        public List<string> EuCountries { get; private set; }
        public List<double> EuAverage { get; private set; }
        private YearInformation _year;
        private DataCalculator _dataCalculator;
        private DataPrinter _printer;
        private Checker _checker;
        private string _file = "AverageLanguages.txt";
        public double _differenece { get; private set; }
        public AverageLanguages(YearInformation year)
        {
            _year = year;
            EuCountries = new List<string>();
            EuAverage = new List<double>();
            _dataCalculator = new DataCalculator(year);
            _printer = new DataPrinter();
            _checker = new Checker();
            if (_checker.CheckIfFileExists(_year))
            {
                ReadFile();
            }
        }

        public void ReadFile()
        {
            //reading information from AverageLanguages file
            try
            {
                using (StreamReader reader = new StreamReader(_file))
                {
                    try
                    {
                        while (!reader.EndOfStream)
                        {
                            string line = reader.ReadLine();
                            line = line.Trim();
                            char[] lineChars = line.ToCharArray();
                            string numbers = new string(lineChars, lineChars.Length - 3, 3);
                            string country = new string(lineChars, 0, lineChars.Length - 3);
                            country = country.Trim();
                            GiveListValues(country, numbers);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Can't read from file");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("No file " + _file + " found");
                Console.WriteLine(e.Message);
            }
        }

        public void GiveListValues(string country, string number)
        {
            //Adding values to the lists
            bool isNumberValid = double.TryParse(number.Replace(".", ","), out double parsedNumber);
            if (isNumberValid)
            {
                EuCountries.Add(country);
                EuAverage.Add(parsedNumber);
            }
            else
            {
                Console.WriteLine("Values " + country + " " + number + " are incorrect");
            }
        }

        public void SwitchValues()
        {
            //Switching estonian value from the file to calculated one
            double[] estoniaAverage = _dataCalculator.CalculateAverageNumberOfLanguages();
            int estIndex = EuCountries.IndexOf("Estonia");
            _differenece = EuAverage[estIndex] - estoniaAverage[0];
            EuCountries.Remove("Estonia");
            EuAverage.RemoveAt(estIndex);
            int index = 0;
            foreach (double average in EuAverage)
            {
                if (average < estoniaAverage[0])
                {
                    index = EuAverage.IndexOf(average);
                    EuAverage.Insert(index, Math.Round(estoniaAverage[0], 1));
                    break;
                }
            }
            EuCountries.Insert(index, "Estonia");
        }
        public void FindEuropeAverages()
        {
            //Printing europe average language values
            if (_checker.CheckIfFileExists(_year))
            {
                SwitchValues();
                Console.WriteLine("Europe average number of spoken languages");
                for (int i = 0; i < EuAverage.Count; i++)
                {
                    Console.WriteLine(EuCountries[i] + " " + EuAverage[i]);
                }

                Console.WriteLine("Difference between estonian value from file and calculated value is " +
                                  Math.Round(_differenece, 3));
                _printer.PrintSeparator();
            }
        }
    }
}
