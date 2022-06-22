﻿using System;
using System.Text;

namespace Task_10_Problem_1
{
// Дано текст в текстовому з довільною кількістю пропусків між словами. Дано текстовий файл з парами слів, розділеними тире. Кожне слово має відповідника для трансляції.
// Створити транслятор для перетворення тексту, де кожне слово замінюється відповідником з словника. Отримана трансляція повинна зберігати початкову пунктуацію, кількість пропусків між словами з вихідного тексту, а також дотримуватись аналогічного використання регістрів.
// У випадку, якщо для якогось слова немає відповідника, запропонувати користувачеві доповнити словник та продовжити трансляцію. Кількість спроб для корекції словника має бути обмеженою.
// Початкова реалізація задачі відбулась на практичному занятті.Проєкт є внесений у групу.Можна доповнити і оптимізувати цю версію, або обґрунтовано запропонувати іншу версію.Обґрунтування змін описати в коментарях до основного класу.

    class Program
    {
        static void Main(string[] args)
        {
            TextWorker fileWorker = new("..\\..\\..\\data\\");
            fileWorker.ReadFromFile();
            fileWorker.SplitText();
            fileWorker.WritoToFile();
            fileWorker.FindWords();
        }
    }
}