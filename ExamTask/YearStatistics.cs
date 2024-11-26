using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamTask
{
    public class YearStatistics
    {
        private YearInformation _yearInfo;
        private DataCalculator _calculator;
        private DataPrinter _printer;
        private Checker _checker;
        public YearStatistics(YearInformation year)
        {
            _yearInfo = year;
            _calculator = new DataCalculator(_yearInfo);
            _printer = new DataPrinter();
            _checker = new Checker();
        }

        public void FindNumberOfForeignSpeakers()
        {
            //method for finding sum of foreign language speakers, task 1
            if (_checker.CheckIfFileExists(_yearInfo))
            {
                int[] sum = _calculator.CalculateSumOfForeignSpeakers();
                _printer.PrintDataColumn(sum, "Finding how many people know foreign languages from year " + _yearInfo.GetYear() +
                    " statistics");
            }
        }
        public void FindOnlyNativeLanguage()
        {
            //method for finding number of only native language speakers, task 2
            if (_checker.CheckIfFileExists(_yearInfo))
            {
                int[] onlyNative = _calculator.CalculateOnlyNativeLanguage();
                _printer.PrintDataColumn(onlyNative, "Finding how many people know only their native language from year " + _yearInfo.GetYear() + " statistics");
            }
        }
        public void FindForeignByNumber(int number)
        {
            //method for finding number of foreign language speakers by the amount of languages, task 3
            if (_checker.CheckIfFileExists(_yearInfo)&&_checker.CheckNumberOfLanguages(number))
            {
                int[] languageByNumber = _calculator.CalculateForeignByNumber(number);
                if (languageByNumber == new int[7])
                { return;}
                _printer.PrintDataColumn(languageByNumber, "Finding how many people from year " + _yearInfo.GetYear() +
                                                           " statistics speak this number of foreign languages: " + number);
            }
        }
        public void FindAllStatisticsByAge(int ageIndex)
        {
            //method for finding all statistics on spoken languages from chosen age group, task 4
            if (_checker.CheckIfFileExists(_yearInfo)&& _checker.CheckAgeIndex(ageIndex))
            {
                int[] allStatisticsByAge = _calculator.CalculateAllStatisticsByAge(ageIndex);
                if(allStatisticsByAge == new int[6]){return;}
                _printer.PrintDataRowInt(allStatisticsByAge, "Finding all information on amount of languages spoken in " + _printer.Groups[ageIndex] +
                                                             " age group  from year " + _yearInfo.GetYear() + " statistics");
            }
        }
        public void FindAverageNumberOfLanguages()
        {
            //method for finding average number of spoken languages , task 6
            if (_checker.CheckIfFileExists(_yearInfo))
            {
                double[] average = _calculator.CalculateAverageNumberOfLanguages();

                Console.WriteLine("Average number of languages spoken in Estonia from " + _yearInfo.GetYear() + " statistics is: " + Math.Round(average[0],2));
                _printer.PrintSeparator();
            }
        }

        public void FindPercentageFromAgeTotal(int ageIndex)
        {
            //method for finding the percentages of people from age group who speak 0, 1, 2, 3, and 4 languages, task 5 (my method)
            if (_checker.CheckIfFileExists(_yearInfo)&&_checker.CheckAgeIndex(ageIndex))
            {
                double[] percentageFromAge = _calculator.CalculatePercentageFromAgeTotal(ageIndex);
                if (percentageFromAge == new double[4]) { return; }
                _printer.PrintDataRowDouble(percentageFromAge,"Finding percentages of languages spoken in " + _printer.Groups[ageIndex] + " age group from year " + _yearInfo.GetYear() + " statistics");
                _printer.PrintSeparator();
            }
        }

        public void FindLanguageStatistics(int ageIndex)
        {
            //Advanced Part method, task 2 (calculating statistics for finding most spoken languages)
            if (_checker.CheckIfContainsLanguagesYear(_yearInfo) && _checker.CheckAgeIndex(ageIndex))
            {
                double[] array = _calculator.CalculateLanguageStatistics(ageIndex);
                
                _printer.PrintLanguageStatistics(array, $"4 most probably spoken languages in {_printer.Groups[ageIndex]} age group from the year {_yearInfo.GetYear()}");
            }
        }

    }
}
