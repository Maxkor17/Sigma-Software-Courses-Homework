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
    static void Main(string[] args)
    {
        Console.OutputEncoding = UTF8Encoding.UTF8;

        int number = UserInterface.GetNumberOfConsumers();
        int quarter = UserInterface.GetQuarter();
        string path = "D:\\Sigma-Software-Courses-Homework\\Task 6\\Task 6 Problem 1\\Task 6 Problem 1\\data\\";
        
        List <Consumer> consumers = new List <Consumer> ();
        RandomInitialization.RandomConsumers(consumers, number);
        for (int i = 0; i < consumers.Count; i++)
        {
            RandomInitialization.RandomMeterings(consumers[i]);
        }
        FileWorker.CreateDataFile(consumers, path, quarter);
        consumers.Clear(); // clearing the list of consumers after creating the input file

        FileWorker.GetDataFromFile(path, consumers);
        FileWorker.OutputInFileListConsumers(consumers, path, quarter);
        FileWorker.OutputInFileConsumer(consumers[0], path, quarter);
        FileWorker.OutputInFileDifferenceInDates(consumers, path);

        Console.WriteLine("Програму виконано успішно!");
    }
}