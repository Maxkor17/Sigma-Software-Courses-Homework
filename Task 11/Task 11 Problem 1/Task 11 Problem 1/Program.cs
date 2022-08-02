using System;
using System.Text;

namespace Task_8_Problem_3
{
    // Порівняти 2 об’єкти класу Склад і визначити наступні результати:
    // 1. Товари є в першому складі і немає в другому. (class Storage, method GetExclusive)
    // 2. Товари, які є спільними в обох складах. common (class Storage, method GetCommon)
    // 3. Спільний список товарів, які є на обох складах, без повторів елементів. (class Storage, method GetMerger)
    public static class Program
    {
        public static void Main()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance); // Ukrainian symbols in input from file
            Console.OutputEncoding = Encoding.UTF8; // Ukrainian symbols in console        
            UserInterface.MainMenu();
        }
    }
}