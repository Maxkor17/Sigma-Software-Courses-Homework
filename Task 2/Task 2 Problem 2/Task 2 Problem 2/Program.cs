﻿using System;
using System.Text;
public static class Program
{
    static void Main()
    {
        Console.OutputEncoding = UTF8Encoding.UTF8;
        Console.Write("Введіть кількість стовпців матриці: ");
        int n = Convert.ToInt32(Console.ReadLine());
        Console.Write("Введіть кількість рядків матриці: ");
        int m = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Типи матриць");
        Console.WriteLine("1. Вертикальна змійка");
        Console.WriteLine("2. Спіральна змійка");
        Console.WriteLine("3. Діагональна змійка");
        Console.Write("Виберіть тип матриці: ");
        int type = Convert.ToInt32(Console.ReadLine());
        Console.Clear();

        if (type == 3 || type == 2 && n != m)
        {
            Console.Write("Помилка! Даний тип матриці доступний тільки для квадратної матриці.");
            return;
        }

        Array arrayObj = new Array();
        int[,] array = new int[n, m];

        switch (type)
        {
            case 1: array = arrayObj.arrayVerticalSnake(n, m); break;
            case 2: array = arrayObj.arraySpiralSnake(n, m); break;
            case 3: array = arrayObj.arrayDiagonalSnake(n, m); break;
        }
        arrayObj.arrayOutput(n, m, array);
    }
}