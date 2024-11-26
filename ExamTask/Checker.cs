using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamTask
{
    public class Checker
    {
        //Class for checking methods
        DataPrinter _printer;
        public Checker()
        {
            _printer = new DataPrinter();
        }
        public bool CheckIfFileExists(YearInformation year)
        {
            //Checking if file with set year exists
            if (year.FileExists)
            {
                return true;
            }
            else
            {
                GiveMessageFormat($"NB! Can't calculate statistics. No file {year.GetYear()} found.");
                return false;
            }
        } 
        public bool CheckNumberOfLanguages(int number)
        {
            //checking if number of spoken languages is valid
            if (number >= 0 && number <= 4)
            {
                return true;
            }
            else
            {
                GiveMessageFormat($"NB! Invalid number of foreign languages ({number})");
            }
            return false;
        }

        public bool CheckAgeIndex(int age)
        {
            //checking  if age index value is correct
            if (age >= 0 && age <= 6)
            {
                return true;
            }
            else
            {
                GiveMessageFormat($"NB! Invalid index for age group ({age})");
            }
            return false;
        }

        public bool CheckIfContainsLanguagesYear(YearInformation year)
        {
            //checking if file has information on languages
            if (year.GetYear() == 2011)
            {
                return true;        
            }
            else
            {
                GiveMessageFormat($"You can use FindLanguageStatistics method only if file contains information on spoken languages, year {year.GetYear()} doesn't contain it");
            }

            return false;
        }

        public bool CheckIfListContainsEnough(List<DataCalculator> calculators)
        {
            //checking if list contains at least 2 values to compare
            if (calculators.Count >=2)
            {
                return true;
            }
            else
            {
                GiveMessageFormat("There is less than 2 years. Nothing to compare.");
            }

            return false;
        }

        public bool CheckIfListAlreadyHas(YearInformation year, List<DataCalculator> calculators)
        {
            //Checking if list already contains the same element as new one
            foreach (DataCalculator calculator in calculators)
            {
                if (calculator.Year.GetYear() == year.GetYear())
                {
                    return false;
                }
            }

            return true;
        }

        public void GiveMessageFormat(string message)
        {
            // error messages format
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
            _printer.PrintSeparator();
        }

    }
}
