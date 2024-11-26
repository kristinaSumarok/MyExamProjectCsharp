using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExamTask
{
    public class DataPrinter
    {
        //Class for printing data in readable format
        private const int _numberOfAgeGroups = 7;
        public string[] Groups { get; private set; }
        public string[] Languages { get; private set; }
        public DataPrinter()
        {
            Groups = new string[] { "All age groups total", "0-14", "15-29", "30-49",
                "50-64", "65 and older", "Age unknown"};
            Languages = new string[]
                { "Estonian", "Russian", "English", "Finnish", "German", "French", "Swedish", "Latvian" };
        }
        public void PrintDataColumn(int[] array,string message)
        {
            //method for printing out information in columns with int values
            Console.WriteLine(message);
            for (int i = 0; i < _numberOfAgeGroups; i++)
            {
                Console.WriteLine(Groups[i]+": " + array[i]);
            }
            PrintSeparator();
        }
        public void PrintDataRowInt(int[] array,string message)
        {
            //method for printing out information in int rows
            Console.WriteLine(message);

            for (int i = 0; i < 5; i++)
            {
                Console.Write($"Speak {i}");
                if (i == 4)
                {
                    Console.Write("+");
                }
                Console.WriteLine($" foreign languages: {array[i]}");
            }
            PrintSeparator();
        }

        public void PrintDataColumnDouble(double[] array, string message)
        {
            // method for printing out information in column with double values
            Console.WriteLine(message);
            for (int i = 0; i <= 5; i++)
            {
                Console.WriteLine(Groups[i] + ": " + Math.Round(array[i], 2)+" %");
            }
            Console.WriteLine();

        }

        public void PrintDataRowDouble(double[] array, string message)
        {
            //method for printing out information in row with double values
            Console.WriteLine(message);

            for (int i = 0; i < 5; i++)
            {
                Console.Write($"Speak {i}");
                if (i == 4)
                {
                    Console.Write("+");
                }
                Console.WriteLine($" foreign languages: {Math.Round(array[i],2)} %");
            }
            Console.WriteLine();
        }

        public void PrintLanguageStatistics(double[] array, string message)
        {
            //Printing statistics on languages (4 most spoken, percentage of the most popular, the least spoken
            Console.WriteLine(message);
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine((i+1)+". "+Languages[Convert.ToInt32(array[i])] );
            }
            Console.WriteLine();
            Console.Write("% of age group which speaks the most common language: ");
            Console.WriteLine(Math.Round(array[4],2)+" %,"+ Languages[Convert.ToInt32(array[0])]);
            Console.WriteLine("The least spoken language: "+ Languages[Convert.ToInt32(array.Last())]);
            PrintSeparator();
        }

        public void PrintMaxAndMinValue(double[] array)
        {
            //finding the minimum value and max value and printing it, for advanced part 1
            double max = array.Max();
            double min = array.Min();
            int indexMin = Array.IndexOf(array, min);
            int indexMax = Array.IndexOf(array, max);
            string formatedAgeGroupMax = Groups[indexMax+1].Replace(":", "");
            string formatedAgeGroupMin = Groups[indexMin+1].Replace(":", "");
            Console.WriteLine($"The age group with the highest growth:{formatedAgeGroupMax}");
            Console.WriteLine($"The age group with the greatest decline: {formatedAgeGroupMin}");
            Console.WriteLine();
        }
    
        public void PrintSeparator()
        {
            //method for separating data from each other
            for (int i = 0; i < 75; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
        }

    }
}
