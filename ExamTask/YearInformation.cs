using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamTask
{
    public class YearInformation
    { 
        private int _arrayAddNumber = 0;
        public bool FileExists { get; private set; }
        private readonly int _year;
        public int[] AllSpeakers { get; private set; } = new int[7];
        public int[] OneLanguage { get; private set; } = new int[7];
        public int[] TwoLanguages { get; private set; } = new int[7];
        public int[] ThreeLanguages { get; private set; } = new int[7];
        public int[] FourOrMoreLanguages { get; private set; } = new int[7];
        public int[] UnKnown { get; private set; } = new int[7];
        public int[] Estonian { get; private set; } = new int[7];
        public int[] Russian { get; private set; } = new int[7];
        public int[] English { get; private set; } = new int[7];
        public int[] Finnish { get; private set; } = new int[7];
        public int[] German { get; private set; } = new int[7];
        public int[] French { get; private set; } = new int[7];
        public int[] Swedish { get; private set; } = new int[7];
        public int[] Latvian { get; private set; } = new int[7];

        public YearInformation(int year)
        {
            _year = year;
            ReadFile();
        }
        //checking if file exists and then reading from it
        public void ReadFile()
        {
            string fileName = "Language_" + _year + ".csv";
            try
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    FileExists = true;
                    reader.ReadLine();
                    try
                    {
                        while (!reader.EndOfStream)
                        {
                            string line = reader.ReadLine();
                            string[] splitLine = line.Split(',');
                            if (!AreNumbersValid(splitLine))
                            {
                                return;
                            }
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
                FileExists = false;
                Console.WriteLine(e.Message);
            }
        }
        public bool AreNumbersValid(string[] splitLine)
        {
            //checking if numbers can me parsed and adding values to the arrays
            bool isAllValid = int.TryParse(splitLine[1], out int all);
            bool isOneValid = int.TryParse(splitLine[2], out int one);
            bool isTwoValid = int.TryParse(splitLine[3], out int two);
            bool isThreeValid = int.TryParse(splitLine[4], out int three);
            bool isFourValid = int.TryParse(splitLine[5], out int four);
            bool isUnKnownValid = int.TryParse(splitLine[6], out int unKnown);
            if (isAllValid & isOneValid & isTwoValid & isThreeValid & isFourValid & isUnKnownValid)
            {
                AllSpeakers[_arrayAddNumber] = all;
                OneLanguage[_arrayAddNumber] = one;
                TwoLanguages[_arrayAddNumber] = two;
                ThreeLanguages[_arrayAddNumber] = three;
                FourOrMoreLanguages[_arrayAddNumber] = four;
                UnKnown[_arrayAddNumber] = unKnown;
                CheckLength(splitLine);
                _arrayAddNumber++;
                return true;
            }
            else
            {
                Console.WriteLine("There are invalid values in line " + (_arrayAddNumber + 1));
                return false;
            }
        }

        private void CheckLength(string[] splitLine)
        {
            //checking if file contains information on languages and if yes adds values to arrays (Estonian, russian, etc.)
            if (splitLine.Length > 7)
            { 
                bool isEstonianValid = int.TryParse(splitLine[7], out int estonian);
                bool isRussianValid = int.TryParse(splitLine[8], out int russian);
                bool isEnglishValid = int.TryParse(splitLine[9], out int english);
                bool isFinnishValid = int.TryParse(splitLine[10], out int finnish);
                bool isGermanValid = int.TryParse(splitLine[11], out int german);
                bool isFrenchValid = int.TryParse(splitLine[12], out int french);
                bool isSwedishValid = int.TryParse(splitLine[13], out int swedish);
                bool isLatvianValid = int.TryParse(splitLine[14], out int latvian);
                if (isEstonianValid && isRussianValid && isEnglishValid && isFinnishValid && isGermanValid &&
                        isFrenchValid && isSwedishValid && isLatvianValid)
                {
                    Estonian[_arrayAddNumber] = estonian;
                    Russian[_arrayAddNumber] = russian;
                    English[_arrayAddNumber] = english;
                    Finnish[_arrayAddNumber] = finnish;
                    German[_arrayAddNumber] = german;
                    French[_arrayAddNumber] = french; 
                    Swedish[_arrayAddNumber] = swedish; 
                    Latvian[_arrayAddNumber] = latvian;
                }
                else
                { 
                    Console.WriteLine("There are invalid values in line " + (_arrayAddNumber + 1));
                }
            }
        }
        public int GetYear()
        {
            return _year;
        }
    }
}

