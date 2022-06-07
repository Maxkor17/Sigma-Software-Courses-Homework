﻿using System;
using System.Text;
class Program
{
    // 1. Змінити метод сортування злиттям, враховуючи обмеження,
    // що елементи для сортування розташовані в файлі і в програмі можна використовувати тільки масиви,
    // кількість елементів яких вдвічі менша за кількість елементів в файлі.
    // P.S. Як я зрозумів завдання: в пам'яті не може бути більше одного масиву з кількістю елементів вдічі мешною, ніж у кінцевому файлі.
    // (region Task 1 in class Vector)

    // 2. Реалізувати в класі Vector метод пірамідального сортування.
    // (region Task 2 in class Vector)
    static void Main()
    {
        Console.Write("Enter the number of matrix elements: ");
        int n = Convert.ToInt32(Console.ReadLine());

        // Task 1
        string path = "..\\..\\..\\arrays\\"; // change path here
        Vector.FilesExistsCheck(path); // check if all files exists for Task 1

        // Random initialization an array and writing to Unsorted Array.txt
        Console.WriteLine("\nTask 1:");
        Vector vectorMerge = new(n); // exception checking is in Properties
        vectorMerge.RandomInitialization(1, n);
        using (StreamWriter unsortedArrayFile = new(path + "Unsorted Array.txt"))
        {
            unsortedArrayFile.Write(vectorMerge.ToString());
        }

        // Sorting an array and writing to Sorted Array.txt
        Vector.MergeSortFromFile(path);
        Console.WriteLine("Sorted Array in file!");

        // Left files for clarity
        // File.Delete(path + "Sorted Array First Part.txt");
        // File.Delete(path + "Sorted Array Second Part.txt");

        // Task 2
        Console.WriteLine("\nTask 2:");
        Vector vectorHeap = new(n);
        vectorHeap.RandomInitialization(1, n);
        Console.WriteLine("Unsorted Array: " + vectorHeap.ToString());
        vectorHeap.HeapSort();
        Console.WriteLine("Sorted Array: " + vectorHeap.ToString());
    }
}