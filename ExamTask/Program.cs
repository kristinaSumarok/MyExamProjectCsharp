namespace ExamTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Preparing all necessary classes to complete task
            YearInformation year2000 = new YearInformation(2000);
            YearInformation year2001 = new YearInformation(2001);
            YearInformation year2011 = new YearInformation(2011);
            YearInformation year2021 = new YearInformation(2021);

            YearStatistics info2000 = new YearStatistics(year2000);
            YearStatistics info2001 = new YearStatistics(year2001);
            YearStatistics info2011 = new YearStatistics(year2011);
            YearStatistics info2021 = new YearStatistics(year2021);

            AverageLanguages average2000 = new AverageLanguages(year2000);
            AverageLanguages average2021 = new AverageLanguages(year2021);

            List<YearInformation> years = new List<YearInformation>();
            years.Add(year2011);
            years.Add(year2021);
            years.Add(year2000);
            OverYearsStatistics overYears = new OverYearsStatistics(years);

            //-----------------------------------------------------------------//

            //Base part
            // 1 task
            info2001.FindNumberOfForeignSpeakers();
            info2000.FindNumberOfForeignSpeakers();

            // 2 task
            info2011.FindOnlyNativeLanguage();

            // 3 task 
            info2021.FindForeignByNumber(4);
            info2021.FindForeignByNumber(5);

            // 4 task
            info2000.FindAllStatisticsByAge(1);
            info2000.FindAllStatisticsByAge(-1);

            // 5 task
            info2021.FindPercentageFromAgeTotal(3);

            // 6 task
            info2011.FindAverageNumberOfLanguages();
                
            // 7 task
            average2000.FindEuropeAverages();
            average2021.FindEuropeAverages();

            // Advanced part
            // 1 task
            overYears.FindSumOfForeignSpeakersChange();
            overYears.FindAverageNumberOfLanguagesChange();
            overYears.FindPercentageFromAgeChange(5);
            overYears.FindForeignByNumberChange(2);

            // 2 task
            info2011.FindLanguageStatistics(4);
            info2021.FindLanguageStatistics(3);

        }
    }
}