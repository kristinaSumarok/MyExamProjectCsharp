using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamTask
{
    public class OverYearsCalculator
    {
        //method for calculations using several years, advanced part Task 1
        public OverYearsCalculator()
        {

        }
        public double[] CalculateAverageNumberOfLanguagesChange(DataCalculator[] yearsToCompare)
        {
            // arranging AverageNumberOfLanguages for first and second arrays and using formula method
            double[] firstYear = yearsToCompare[0].CalculateAverageNumberOfLanguages();
            double[] secondYear = yearsToCompare[1].CalculateAverageNumberOfLanguages();
            double[] percentage = UseFormulaDouble(firstYear, secondYear);
            return percentage;
        }

        public double[] CalculatePercentageFromAgeChange(int ageIndex, DataCalculator[] yearsToCompare)
        {
            // arranging PercentageFromAgeTotal for first and second arrays and using formula method
            double[] firstYear = yearsToCompare[0].CalculatePercentageFromAgeTotal(ageIndex);
            double[] secondYear = yearsToCompare[1].CalculatePercentageFromAgeTotal(ageIndex);
            double[] percentage = UseFormulaDouble(firstYear, secondYear);
            return percentage;
        }

        public double[] CalculateSumOfForeignSpeakersChange(DataCalculator[] yearsToCompare)
        {
            // arranging SumOfForeignSpeakers for first and second arrays and using formula method
            int[] firstYear = yearsToCompare[0].CalculateSumOfForeignSpeakers();
            int[] secondYear = yearsToCompare[1].CalculateSumOfForeignSpeakers();
            double[] percentage = UseFormulaInt(firstYear, secondYear);
            return percentage;
        }

        public double[] CalculateForeignByNumberChange(int number, DataCalculator[] yearsToCompare)
        {
            // arranging ForeignByNumber for first and second arrays and using formula method
            int[] firstYear = yearsToCompare[0].CalculateForeignByNumber(number);
            int[] secondYear = yearsToCompare[1].CalculateForeignByNumber(number);
            double[] percentage = UseFormulaInt(firstYear, secondYear);
            return percentage;
        }

        private double[] UseFormulaInt(int[] firstYear, int[] secondYear)
        {
            //formula for calculations int[]
            double[] percentage = new double[7];
            for (int i = 0; i < firstYear.Length - 1; i++)
            {
                percentage[i] = ((Convert.ToDouble(secondYear[i]) - Convert.ToDouble(firstYear[i])) / firstYear[i] * 100);
                percentage[i] = Math.Round(percentage[i], 2);

            }
            return percentage;
        }

        private double[] UseFormulaDouble(double[] firstYear, double[] secondYear)
        {
            //formula for calculations double[]
            double[] percentage = new double[6];
            for (int i = 0; i < firstYear.Length; i++)
            {
                percentage[i] = ((Convert.ToDouble(secondYear[i]) - Convert.ToDouble(firstYear[i])) / firstYear[i] * 100);
                percentage[i] = Math.Round(percentage[i], 2);

            }
            return percentage;
        }
    }
}
