﻿using System;
using System.Text;

// Перевантажити оператори +, - для класу Consumer.
// 1. Оператор + перевантажити, створюючи спільну колекцію, з об’єднаною інформацією з першого та другого операнда. 
// 2. Операцію мінус протрактувати як зміну першого операнда з вилученням з нього інформації про квартири, які є в другому операнді.
// Код в файлі Consumer.cs
class Program
{
    static void Main()
    {
        Console.OutputEncoding = UTF8Encoding.UTF8;

        int quarter = 2;
        FileWorker.path = "..\\..\\..\\data\\";

        ConsumerList<Consumer> consumers1 = new();
        FileWorker.GetDataFromFile(consumers1, "Consumers List #1.txt");

        ConsumerList<Consumer> consumers2 = new();
        FileWorker.GetDataFromFile(consumers2, "Consumers List #2.txt");

        Random rand = new Random();
        for (int i = 0; i < 15; i++)
        {
            consumers2[rand.Next(15)] = consumers1[rand.Next(15)];
        }

        consumers1 += consumers2;
        FileWorker.OutputInFileListConsumers(consumers1, quarter, "Consumers List Merging Result.txt");

        consumers1 -= consumers2;
        FileWorker.OutputInFileListConsumers(consumers1, quarter, "Consumers List Removal Result.txt");
    }
}