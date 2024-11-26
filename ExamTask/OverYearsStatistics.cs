using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamTask
{
    public class OverYearsStatistics
    {
        //Class for advanced part, Task 1
        public List<DataCalculator> YearsCalculators { get; private set; }
        public List<YearInformation> Years { get; private set; }
        private List<DataCalculator[]> _yearsToCompare;
        private List<YearInformation[]> _yearsToPrint;
        private DataPrinter _printer;
        private Checker _checker;
        private OverYearsCalculator _overYearsCalculator;

        public OverYearsStatistics(List<YearInformation> yearsInformation)
        {
            YearsCalculators = new List<DataCalculator>();
            _yearsToPrint = new List<YearInformation[]>();
            _yearsToCompare = new List<DataCalculator[]>();
            _printer = new DataPrinter();
            Years = new List<YearInformation>(yearsInformation);
            _checker = new Checker();
            _overYearsCalculator = new OverYearsCalculator();
            SortList();
            
            foreach (YearInformation years in Years )
            {
                if (_checker.CheckIfFileExists(years)&& _checker.CheckIfListAlreadyHas(years,YearsCalculators))
                {
                    YearsCalculators.Add(new DataCalculator(years));
                }
            }
            CreateYearPairs();
        }

        private void SortList()
        {
            //Sorting list into correct order
            List<int> years = new List<int>();
            foreach (YearInformation year in Years)
            {
                years.Add(year.GetYear());
            }
            years.Sort();

            for (int i = 0; i < years.Count; i++)
            {
                Years[i] = new YearInformation(years[i]);
            }
        }

        private void CreateYearPairs()
        {
            //creating array of Years arrays to use complete pairs to compare
            if (_checker.CheckIfListContainsEnough(YearsCalculators))
            {
                for (int i = 0; i < YearsCalculators.Count - 1; i++)  
                {
                    DataCalculator[] yearsToCompare = new DataCalculator[]
                        { YearsCalculators[i], YearsCalculators[i + 1] };
                    YearInformation[] yearsToPrint = new YearInformation[]
                        {YearsCalculators[i].Year, YearsCalculators[i+1].Year };
                    _yearsToPrint.Add(yearsToPrint);
                    _yearsToCompare.Add(yearsToCompare);
                }
                if (YearsCalculators.Count >= 3)
                {
                    DataCalculator[] firstAndLast = new DataCalculator[]
                        { YearsCalculators[0], YearsCalculators.Last() };
                    _yearsToCompare.Add(firstAndLast);
                    YearInformation[] yearsToPrint = new YearInformation[]
                        { YearsCalculators[0].Year, YearsCalculators.Last().Year };
                    _yearsToPrint.Add(yearsToPrint);
                }
            }
        }

        public void FindForeignByNumberChange(int number)
        {
            //Finding how number of (0,1,2,3,4) language speakers has changed in percentages
            if (_checker.CheckNumberOfLanguages(number)&& _checker.CheckIfListContainsEnough(YearsCalculators))
            {
                double[] percentage;
                for (int i = 0; i < _yearsToCompare.Count; i++)
                {
                    percentage = _overYearsCalculator.CalculateForeignByNumberChange(number, _yearsToCompare[i]);
                    _printer.PrintDataColumnDouble(percentage, "How number of people who know " + number + " foreign languages has changed from " +
                                                              _yearsToPrint[i][0].GetYear() + " to " + _yearsToPrint[i][1].GetYear());
                }
                _printer.PrintSeparator();
            }
        }

        public void FindSumOfForeignSpeakersChange()
        {
            //Finding how sum of (0+1+2+3+4) language speakers has changed in percentages
            if (_checker.CheckIfListContainsEnough(YearsCalculators))
            {
                double[] sum;
                for (int i = 0; i < _yearsToCompare.Count; i++)
                {
                    sum = _overYearsCalculator.CalculateSumOfForeignSpeakersChange(_yearsToCompare[i]);
                    _printer.PrintDataColumnDouble(sum, "How number of people who know at least one foreign language has has changed from " + _yearsToPrint[i][0].GetYear() + " to " + _yearsToPrint[i][1].GetYear());       
                }
                _printer.PrintSeparator();
            }
        }

        public void FindPercentageFromAgeChange(int ageIndex)
        {
            //Finding how the percentages of people from age group who speak 0, 1, 2, 3, and 4 languages has changed
            if (_checker.CheckAgeIndex(ageIndex) && _checker.CheckIfListContainsEnough(YearsCalculators))
            {
                double[] percentages;
                for (int i = 0; i < _yearsToCompare.Count; i++)
                {
                    percentages = _overYearsCalculator.CalculatePercentageFromAgeChange(ageIndex, _yearsToCompare[i]);
                    _printer.PrintDataRowDouble(percentages, "How percentage of languages spoken in " + _printer.Groups[ageIndex] + " age group has changed from " +
                                                               _yearsToPrint[i][0].GetYear() + " to " + _yearsToPrint[i][1].GetYear());
                }
                _printer.PrintSeparator();
            }
        }

        public void FindAverageNumberOfLanguagesChange()
        {
            //Finding how the average number of spoken languages has changed + printing the highest and the lowest increase 
            if ( _checker.CheckIfListContainsEnough(YearsCalculators))
            {
                double[] percentages;
                for (int i = 0; i < _yearsToCompare.Count; i++)
                {
                    percentages = _overYearsCalculator.CalculateAverageNumberOfLanguagesChange(_yearsToCompare[i]);
                    _printer.PrintDataColumnDouble(percentages, "How average number of spoken languages in different age groups has changed from year " +
                                                             _yearsToPrint[i][0].GetYear() + " to " + _yearsToPrint[i][1].GetYear());
                    double[] newArray = new double[5];
                    Array.Copy(percentages, 1, newArray, 0, 5);
                    _printer.PrintMaxAndMinValue(newArray);
                }
                _printer.PrintSeparator();
            }
        }

    }
}
