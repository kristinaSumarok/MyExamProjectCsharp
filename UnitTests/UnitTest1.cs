using ExamTask;
using Microsoft.VisualBasic;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        private YearInformation _2001;
        private YearInformation _2011;
        private YearInformation _2021;
        private YearInformation _2000;
        private DataCalculator _dataCalculator;

        private AverageLanguages _average2021;
        [TestInitialize]
        public void TestInitialize()
        {
            _2001 = new YearInformation(2001);
            _2011 = new YearInformation(2011);
            _2021 = new YearInformation(2021);
            _2000 = new YearInformation(2000);

            _average2021 = new AverageLanguages(_2021);
        }
        //File information
        [TestMethod]
        public void ReadFile_FileExists_2000()
        {
            //Checking if file with year 2000 exists
            Assert.IsTrue(_2000.FileExists);
        }

        [TestMethod]
        public void ReadFile_FileDoesNotExist_2001()
        {
            //Checking if file with year 2001 doesn't exist
            Assert.IsFalse(_2001.FileExists);
        }

        [TestMethod]
        public void ReadFile_FileWithoutLanguages_2021() 
        {
            //Checking if file which doesn't contain information on languages doesn't give language arrays values
            int[] array = new int[7];
            CollectionAssert.AreEqual(array,_2021.English);
            CollectionAssert.AreEqual(array, _2021.German);
            CollectionAssert.AreEqual(array, _2021.Estonian);
        }

        [TestMethod]
        public void ReadFile_FileWithLanguages_2011()
        {
            //Checking if file which contains information on languages gives language arrays values
            Assert.AreEqual(55550, _2011.Estonian[3]);
            Assert.AreEqual(194758, _2011.Russian[3]);
            Assert.AreEqual(179153, _2011.English[3]);
            Assert.AreEqual(82362, _2011.Finnish[3]);
            Assert.AreEqual(40311, _2011.German[3]);
            Assert.AreEqual(5957, _2011.French[3]);
            Assert.AreEqual(6695, _2011.Swedish[3]);
            Assert.AreEqual(876, _2011.Latvian[3]);

        }

        [TestMethod]
        public void AreNumbersValid_True()
        {
            //Checking AreNumbersValid method with invalid year as then values were not set before
            string[] array = new string[] {"values","3", "5", "6567", "7", "9087", "566"};
            Assert.IsTrue(_2001.AreNumbersValid(array));

            Assert.AreEqual(3, _2001.AllSpeakers[0]);
            Assert.AreEqual(5, _2001.OneLanguage[0]);
            Assert.AreEqual(6567, _2001.TwoLanguages[0]);
            Assert.AreEqual(7, _2001.ThreeLanguages[0]);
            Assert.AreEqual(9087, _2001.FourOrMoreLanguages[0]);
            Assert.AreEqual(566, _2001.UnKnown[0]);
        }

        [TestMethod]
        public void AreNumbersValid_False()
        {
            //Checking AreNumbersValid method with invalid year as then values were not set before
            //checking if string int array[1] can't be parsed
            string[] array = new string[] { "values", "year", "5", "6567", "7", "9087", "566" };
            Assert.IsFalse(_2001.AreNumbersValid(array));
        }

        [TestMethod]
        public void GetYear_AreEqual_2021()
        {
            //Checking if year is correct
            Assert.AreEqual(2021,_2021.GetYear());
        }

        //Calculations
        [TestMethod]
        public void DataCalculator_Initialize_2000_AreNotEmpty_NumberOFLanguages()
        {
            //Checking if after dataCalculator initialization array NumberOfLanguages is not empty
            _dataCalculator = new DataCalculator(_2000);
            CollectionAssert.AreNotEqual(new int[5][], _dataCalculator.NumberOfLanguages);
        }

        [TestMethod]
        public void SumOfForeignSpeakers_2021_AreEqual()
        {
            //Checking if calculated sum of foreign language speakers is correct
            _dataCalculator = new DataCalculator(_2021);

            int expectedSum = _2021.OneLanguage[0] + _2021.TwoLanguages[0] + _2021.ThreeLanguages[0] + _2021.FourOrMoreLanguages[0];
            int[] actualSum = _dataCalculator.CalculateSumOfForeignSpeakers();

            Assert.AreEqual(expectedSum, actualSum[0]);
        }

        [TestMethod]
        public void OnlyNativeSpeakers_2011_AreEqual()
        {
            //Checking if calculated number of only native language speakers is correct
            _dataCalculator = new DataCalculator(_2011);

            int expectedNumber = _2011.AllSpeakers[1] - _2011.OneLanguage[1] - _2011.TwoLanguages[1] - _2011.ThreeLanguages[1] - _2011.FourOrMoreLanguages[1]- _2011.UnKnown[1];
            int[] actualNumber = _dataCalculator.CalculateOnlyNativeLanguage();

            Assert.AreEqual(expectedNumber, actualNumber[1]);
        }

        [TestMethod]
        public void ForeignByNumber_1_2000_AreEqual_OneLanguage()
        {
            //Checking if array of one languages speakers was taken correctly
            _dataCalculator = new DataCalculator(_2000);

            int[] actualNumber = _dataCalculator.CalculateForeignByNumber(1);
            int[] expectedNumber = _2000.OneLanguage;

            Assert.AreEqual(expectedNumber[1], actualNumber[1]);
            Assert.AreEqual(expectedNumber[4], actualNumber[4]);
            Assert.AreEqual(expectedNumber[6], actualNumber[6]);
        }
        //Mistake in 2021 file found!!!
        [TestMethod]
        public void ForeignByNumber_4_2021_AreEqual_FourLanguages()
        {
            //Mistake: 4 language speakers sum in file is slightly different from real (calculated) sum
            _dataCalculator = new DataCalculator(_2021);

            int[] actualArray = _dataCalculator.CalculateForeignByNumber(4);
            int actualNumber = actualArray[0];
            int expectedNumber = actualArray[1] + actualArray[2]+ actualArray[3] + actualArray[4] + actualArray[5] + actualArray[6];

            Assert.AreEqual(expectedNumber, actualNumber);
        }


        [TestMethod]
        public void AllStatisticsByAge_15_29_2011_AreEqual_ThreeLanguage_2()
        {
            //Checking if array of age group statistics was taken correctly and value match with initial value 
            _dataCalculator = new DataCalculator(_2011);

            int[] actualNumber = _dataCalculator.CalculateAllStatisticsByAge(2);
            int[] expectedNumber = _2011.ThreeLanguages;

            Assert.AreEqual(expectedNumber[2], actualNumber[3]);
        }

        [TestMethod]
        public void AverageNumberOfLanguages_2000_AreEqual_2_2()
        {
            //checking if average number was calculated correctly
            _dataCalculator = new DataCalculator(_2000);

            double[] actualNumber = _dataCalculator.CalculateAverageNumberOfLanguages();
            double expectedNumber = 2.2;

            Assert.AreEqual(expectedNumber, actualNumber[0], 0.01);
        }

        [TestMethod]
        public void PercentageFromAgeTotal_50_64_2021_All_AreEqual_100()
        {
            //checking if sum of percentages from the 50-64 age group is equal 100%
            _dataCalculator = new DataCalculator(_2021);

            double[] actualNumber = _dataCalculator.CalculatePercentageFromAgeTotal(4);
            double actual = actualNumber.Sum() + (_2021.UnKnown[4] * 100 / _2021.AllSpeakers[4]); 
            double expectedNumber = 100;

            Assert.AreEqual(100, actual, 0.01);
        }

        [TestMethod]
        public void LanguageStatistics_0_14_2011_AreEqual_index()
        {
            //checking if calculated array contains same indexes and calculated percentage of the most spoken language
            _dataCalculator = new DataCalculator(_2011);
            double[] actualArray = _dataCalculator.CalculateLanguageStatistics(1);
            double[] expectedArray = new double[] { 2, 0, 1, 4, 18.32, 7 };

            Assert.AreEqual(expectedArray[4], actualArray[4],0.01);
            Assert.AreEqual(expectedArray[0], actualArray[0]);
            Assert.AreEqual(expectedArray[1], actualArray[1]);
            Assert.AreEqual(expectedArray[2], actualArray[2]);
            Assert.AreEqual(expectedArray[3], actualArray[3]);
            Assert.AreEqual(expectedArray[5], actualArray[5]);
        }

        //Checker class
        [TestMethod]
        public void Checker_CheckIfFileExists_isTrue()
        {
            //checking if file checker works correctly, files 2000, 2011, 2021 exist
            Checker checker = new Checker();
            Assert.IsTrue(checker.CheckIfFileExists(_2011));
            Assert.IsTrue(checker.CheckIfFileExists(_2021));
            Assert.IsTrue(checker.CheckIfFileExists(_2000));
        }

        [TestMethod]
        public void Checker_CheckIfFileExists_2001_isFalse()
        {
            //checking if file checker works correctly, file 2001 does not exist
            Checker checker = new Checker();
            Assert.IsFalse(checker.CheckIfFileExists(_2001));
        }

        [TestMethod]
        public void Checker_CheckNumberOfLanguages_3_isTrue()
        {
            //checking if number of languages checker works correctly, 3 languages exist
            Checker checker = new Checker();
            Assert.IsTrue(checker.CheckNumberOfLanguages(3));
        }
        [TestMethod] 
        public void Checker_CheckNumberOfLanguages_5_isFalse()
        {
            //checking if number of languages checker works correctly, 5 languages does not exist
            Checker checker = new Checker();
            Assert.IsFalse(checker.CheckNumberOfLanguages(5));
        }

        [TestMethod]
        public void Checker_CheckAgeIndex_0_isTrue()
        {
            //checking if age index checker works correctly, 0 index exists
            Checker checker = new Checker();
            Assert.IsTrue(checker.CheckAgeIndex(0));
        }
        [TestMethod]
        public void Checker_CheckAgeIndex_10_isFalse()
        {
            //checking if age index checker works correctly, 10 index does not exist
            Checker checker = new Checker();
            Assert.IsFalse(checker.CheckAgeIndex(10));
        }
        [TestMethod]
        public void Checker_CheckIfContainsLanguages_2011_isTrue()
        {
            //checking if method checks whether file has information on languages or not, 2011 contains
            Checker checker = new Checker();
            Assert.IsTrue(checker.CheckIfContainsLanguagesYear(_2011));
        }
        [TestMethod]
        public void Checker_CheckIfContainsLanguages_2000_isFalse()
        {
            //checking if method checks whether the file has information on languages or not, 2000 and 2021 does not contain
            Checker checker = new Checker();
            Assert.IsFalse(checker.CheckIfContainsLanguagesYear(_2000));
            Assert.IsFalse(checker.CheckIfContainsLanguagesYear(_2021));
        }

        [TestMethod]
        public void Checker_CheckIfListContainsEnough_isTrue()
        {
            //Checking if list with dataCalculators contains enough years, 3 is enough
            Checker checker = new Checker();
            List<DataCalculator> list = new List<DataCalculator>();
            list.Add(new DataCalculator(_2011));
            list.Add(new DataCalculator(_2021));
            list.Add(new DataCalculator(_2000));
            Assert.IsTrue(checker.CheckIfListContainsEnough(list));
        }
        [TestMethod]
        public void Checker_CheckIfListContainsEnough_2021_isFalse()
        {
            //Checking if list with dataCalculators contains enough years, 1 year is not enough
            Checker checker = new Checker();
            List<DataCalculator> list = new List<DataCalculator>();
            list.Add(new DataCalculator(_2021));
            Assert.IsFalse(checker.CheckIfListContainsEnough(list));
        }
        //AverageLanguages class
        [TestMethod]
        public void AverageLanguages_ReadFile_2011_ListNotEmpty()
        {
            //Checking if after reading from file arrays are not empty
            List<string> actualListCountries = _average2021.EuCountries;
            List<double> actualListAverage = _average2021.EuAverage;

            Assert.AreEqual(actualListCountries.Count, 27);
            Assert.AreEqual(actualListAverage.Count, 27);
        }
        [TestMethod]
        public void EuropeAverages_2011_difference_AreEqual_0_45()
        {
            //Checking if difference between calculated average value and value found in file is correct
            _average2021.FindEuropeAverages();
            double expectedValue = 0.438;
            double actualValue = _average2021._differenece;

            Assert.AreEqual(expectedValue, actualValue,0.01);
        }
        //OverYearsStatistics class
        [TestMethod]
        public void OverYearsStatistics_Initialize_Sorted()
        {
            //Checking if after OverYearsStatistics initialization all years from the list were sorted correctly in increasing order
            List<YearInformation> list = new List<YearInformation>();
            list.Add(_2011);
            list.Add(_2021);
            list.Add(_2011);
            list.Add(_2000);
            OverYearsStatistics overYears = new OverYearsStatistics(list);
            int actualValue1 = overYears.YearsCalculators[0].Year.GetYear();
            int actualValue2 = overYears.YearsCalculators[1].Year.GetYear();
            int actualValue3 = overYears.YearsCalculators[2].Year.GetYear();

            Assert.AreEqual(2000,actualValue1);
            Assert.AreEqual(2011, actualValue2);
            Assert.AreEqual(2021, actualValue3);
        }
        [TestMethod]
        public void AverageNumberOfLanguagesChange_2011_2021_AreEqual_percentages()
        {
            //Checking if method AverageNumberOfLanguagesChange calculates changes in average number of spoken languages between 2 years correctly
            OverYearsCalculator calculators = new OverYearsCalculator();

            DataCalculator calculator1 = new DataCalculator(_2011);
            DataCalculator calculator2 = new DataCalculator(_2021);
            DataCalculator[] array = new DataCalculator[] { calculator1, calculator2 };
            double[] actualValue = calculators.CalculateAverageNumberOfLanguagesChange(array);

            Assert.AreEqual(11.57, actualValue[1], 0.01);
            Assert.AreEqual(-0.8, actualValue[3], 0.01);
            Assert.AreEqual(9.48, actualValue[5], 0.01);
        }

        [TestMethod]
        public void SumOfForeignSpeakersChange_2000_2021_AreEqual_percentages()
        {
            //Checking if method SumOfForeignSpeakersChange calculates changes in number of foreign language speakers between 2 years correctly
            OverYearsCalculator calculators = new OverYearsCalculator();

            DataCalculator calculator1 = new DataCalculator(_2000);
            DataCalculator calculator2 = new DataCalculator(_2021);
            DataCalculator[] array = new DataCalculator[] { calculator1, calculator2 };
            double[] actualValue = calculators.CalculateSumOfForeignSpeakersChange(array);

            Assert.AreEqual(16.68, actualValue[1], 0.01);
            Assert.AreEqual(11.71, actualValue[3], 0.01);
            Assert.AreEqual(107.34, actualValue[5], 0.01);
        }
        [TestMethod]
        public void PercentageFromAgeChange_0_14_2000_2011_AreEqual_percentages()
        {
            //Checking if method  PercentageFromAgeChange calculates changes in percentages from age total between 2 years correctly
            OverYearsCalculator calculators = new OverYearsCalculator();

            DataCalculator calculator1 = new DataCalculator(_2000);
            DataCalculator calculator2 = new DataCalculator(_2011);
            DataCalculator[] array = new DataCalculator[] { calculator1, calculator2 };
            double[] actualValue = calculators.CalculatePercentageFromAgeChange(3,array);

            Assert.AreEqual(16.86, actualValue[0], 0.01);
            Assert.AreEqual(8.19, actualValue[2], 0.01);
            Assert.AreEqual(158.1, actualValue[4], 0.01);
        }
        [TestMethod]
        public void ForeignByNumberChange_2_2011_2021_AreEqual_percentages()
        {
            //Checking if method ForeignByNumberChange calculates changes in number of two language speakers between 2 years correctly
            OverYearsCalculator calculators = new OverYearsCalculator();

            DataCalculator calculator1 = new DataCalculator(_2011);
            DataCalculator calculator2 = new DataCalculator(_2021);
            DataCalculator[] array = new DataCalculator[] { calculator1, calculator2 };
            double[] actualValue = calculators.CalculateForeignByNumberChange(2, array);

            Assert.AreEqual(34.03, actualValue[1], 0.01);
            Assert.AreEqual(20.89, actualValue[4], 0.01);
            Assert.AreEqual(59.62, actualValue[5], 0.01);
        }

    }

}