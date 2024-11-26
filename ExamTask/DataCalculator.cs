using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamTask
{
    public class DataCalculator
    {
        public int[][] NumberOfLanguages { get; private set; } = new int[5][];
        private const int NumberOfAgeGroups = 7;
        public YearInformation Year { get; private set; }
        private Checker _checker;

        public DataCalculator(YearInformation year)
        {
            Year = year;
            _checker = new Checker();
            InitializeArray();
        }

        public void InitializeArray()
        {
            int[] OnlyNativeLanguage = CalculateOnlyNativeLanguage();
            NumberOfLanguages = new int[][]
            { OnlyNativeLanguage, Year.OneLanguage, Year.TwoLanguages,
                Year.ThreeLanguages, Year.FourOrMoreLanguages};
        }
        public int[] CalculateSumOfForeignSpeakers()
        {
            //method for calculating sum of foreign language speakers OneLanguage + TwoLanguages + ThreeLanguages + FourLanguages, Task 1
            int[] sum = new int[NumberOfAgeGroups];
            for (int i = 0; i < NumberOfAgeGroups; i++)
            {
                sum[i] = Year.OneLanguage[i] + Year.TwoLanguages[i] + Year.ThreeLanguages[i] +
                         Year.FourOrMoreLanguages[i];
            }
            return sum;
        }
        public int[] CalculateOnlyNativeLanguage()  
        {
            // method for calculating number of only native language speakers All speakers - one, two,three,four, unknown languages, Task 2
            int[] onlyNativeLanguage = new int[7];
            int[] sumOfForeignSpeakers = CalculateSumOfForeignSpeakers();
            for (int i = 0; i < NumberOfAgeGroups; i++)
            {
                onlyNativeLanguage[i] = Year.AllSpeakers[i] - sumOfForeignSpeakers[i] - Year.UnKnown[i];
            }
            return onlyNativeLanguage;
        }
        public int[] CalculateForeignByNumber(int number)
        {
            //searching for the column with all age group statistics who speak this number of languages, Task 3
            int[] languageByNumber = new int[NumberOfAgeGroups];
            if (_checker.CheckNumberOfLanguages(number))
            {
                languageByNumber = NumberOfLanguages[number];
            }
            return languageByNumber;
        }
        public int[] CalculateAllStatisticsByAge(int ageIndex)
        {
            //searching for the row of numbers (0,1,2,3,4 languages) for language group, Task 4
            int[] allStatisticsByAge = new int[6];
            for (int i = 0; i < 5; i++)
            {
                allStatisticsByAge[i] = NumberOfLanguages[i][ageIndex];
            }
            return allStatisticsByAge;
        }
        public double[] CalculateAverageNumberOfLanguages()
        {
            //Calculating average number of spoken languages in estonia, Task 6
            //(onlyNative * 1 + oneLanguage * 2 + twoLanguage * 3 + threeLanguage * 4 + fourLanguages * 5) / (AllSpeakers - unKnown)
            double[] averageNumberOfLanguages = new double[6];
            double totalLanguages = 0;
            for (int j = 0; j <= 5; j++)
            {
                int totalSpeakers = Year.AllSpeakers[j] - Year.UnKnown[j];
                for (int i = 1; i <= 5; i++)
                {
                    totalLanguages += i * NumberOfLanguages[i - 1][j];
                }
                averageNumberOfLanguages[j] = totalLanguages / Convert.ToDouble(totalSpeakers);
                totalLanguages = 0;
            }
            return averageNumberOfLanguages;
        }

        public double[] CalculatePercentageFromAgeTotal(int ageIndex)
        {
            //Calculating percentages of people from age group who speak different number of languages (0,1,2,3,4)
            //(number of people who speak 0 languages * 100) / AllSpeakers, Task 5
            int[] ageGroup = CalculateAllStatisticsByAge(ageIndex);
            double[] percentages = new double[5];
            double sum = Year.AllSpeakers[ageIndex] - Year.UnKnown[ageIndex];

            for (int i = 0; i <= 4; i++)
            {
                percentages[i] = (ageGroup[i] * 100) / sum;
            }
            
            return percentages;
        }

        public double[] CalculateLanguageStatistics(int ageIndex)
        {
            //Creating a list where first 4 elements contain the most spoken languages in decreasing order, 5th element is percentage of people who speak the most spoken language
            // 6th element is the least spoken language, Advanced Task 2
            List<int> languages = new List<int>()
            {
                Year.Estonian[ageIndex], Year.Russian[ageIndex], Year.English[ageIndex], Year.Finnish[ageIndex],
                Year.German[ageIndex], Year.French[ageIndex],
                Year.Swedish[ageIndex], Year.Latvian[ageIndex]
            };
            List<int> languagesNewList = new List<int>(languages);
            languagesNewList.Sort((a, b) => b.CompareTo(a));
            double[] languagesNeeded = new double[6];
            for (int i = 0; i < 4; i++)
            {
                languagesNeeded[i] = languages.IndexOf(languagesNewList[i]);
            }
            languagesNeeded[4] = (languagesNewList[0] * 100)/Convert.ToDouble(Year.AllSpeakers[ageIndex]);
            languagesNeeded[5] = languages.IndexOf(languagesNewList.Last());
            return languagesNeeded;
        }
        
    }
}
