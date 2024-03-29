﻿using System;
using System.Text;

// 1. Зчитати інформацію про споживачів з текстового файлу. (class FileWorker, method GetDataFromFile)
// 2. Виведення повної інформації про всіх споживачів. (class FileWorker, method OutputInFileListConsumers)
// 3. Виведення інформації тільки по одній квартирі. (class FileWorker, method OutputInFileConsumer)
// 4. Знайти споживача з найбільшою заборгованістю. (class Consumer, method FindLargestDebt)
// 5. Знайти споживача, який не використовував електроенергію. (class Consumer, method FindExempt)
// 6. Видрукувати інформацію про те, скільки днів пройшло з моменту останнього зняття показу лічильника до поточної дати. (class FileWorker, method OutputInFileDifferenceInDates)
class Program
{
    static void Main()
    {
        Console.OutputEncoding = UTF8Encoding.UTF8;

        try
        {
            int number = UserInterface.GetNumberOfConsumers();
            int quarter = UserInterface.GetQuarter();
            FileWorker.path = "..\\..\\..\\data\\";

            List<Consumer> consumers = new();
            RandomInitialization.RandomConsumers(consumers, number);
            for (int i = 0; i < consumers.Count; i++)
            {
                RandomInitialization.RandomMeterings(consumers[i]);
            }
            FileWorker.CreateDataFile(consumers, quarter);
            consumers.Clear(); // clearing the list of consumers after creating the input file

            FileWorker.GetDataFromFile(consumers);
            FileWorker.OutputInFileListConsumers(consumers, quarter);
            FileWorker.OutputInFileConsumer(consumers[0], quarter);
            FileWorker.OutputInFileDifferenceInDates(consumers);

            Console.WriteLine("Програму виконано успішно!");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            Console.ResetColor();
        }
    }
}