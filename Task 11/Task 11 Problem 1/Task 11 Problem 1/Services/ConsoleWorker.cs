static class ConsoleWorker
{
    #region Methods
    public static void AllProductsOutput(List<IGoods> listProduct)
    {
        OutputHeader();
        foreach (IGoods goods in listProduct)
        {
            if (goods is Meat)
            {
                Meat g = goods as Meat;
                Console.Write(string.Format("| {0,-10} | {1,-5:F1} | {2,-5} | {3,-10} | {4,-15} | {5,-18} |", g.Name, g.Price, g.Weight, g.GetMeatType(), g.GetCategory(), "··················"));
            }
            else if (goods is Dairy)
            {
                Dairy g = goods as Dairy;
                Console.Write(string.Format("| {0,-10} | {1,-5:F1} | {2,-5} | {3,-10} | {4,-15} | {5,-18} |", g.Name, g.Price, g.Weight, "··········", "···············", g.expDate.ToString("dd/MM/yyyy")));
            }
            else
            {
                Console.Write(string.Format("| {0,-10} | {1,-5:F1} | {2,-5} | {3,-10} | {4,-15} | {5,-18} |", goods.Name, goods.Price, goods.Weight, "··········", "···············", "··················"));
            }
            Console.WriteLine();
        }
        Console.WriteLine("----------------------------------------------------------------------------------");
    }

    public static void AllMeatOutput(List<IGoods> listProduct)
    {
        OutputHeader();
        foreach (IGoods goods in listProduct)
        {
            if (goods is Meat)
            {
                Meat g = goods as Meat;
                Console.WriteLine(string.Format("| {0,-10} | {1,-5:F1} | {2,-5} | {3,-10} | {4,-15} | {5,-18} |", g.Name, g.Price, g.Weight, g.GetMeatType(), g.GetCategory(), "··················"));
            }
        }
        Console.WriteLine("----------------------------------------------------------------------------------");
    }

    public static void OutputProduct(List<IGoods> listProduct, int index) // need to optimize
    {
        OutputHeader();
        if (listProduct[index] is Meat)
        {
            Meat p = listProduct[index] as Meat;
            Console.WriteLine(string.Format("| {0,-10} | {1,-5:F1} | {2,-5} | {3,-10} | {4,-15} | {5,-18} |", p.Name, p.Price, p.Weight, p.GetMeatType(), p.GetCategory(), "··················"));
        }
        else if (listProduct[index] is Dairy)
        {
            Dairy p = listProduct[index] as Dairy;
            Console.WriteLine(string.Format("| {0,-10} | {1,-5:F1} | {2,-5} | {3,-10} | {4,-15} | {5,-18} |", p.Name, p.Price, p.Weight, "··········", "···············", p.expDate.ToString("dd/MM/yyyy")));
        }
        else
        {
            Console.WriteLine(string.Format("| {0,-10} | {1,-5:F1} | {2,-5} | {3,-10} | {4,-15} | {5,-18} |", listProduct[index].Name, listProduct[index].Price, listProduct[index].Weight, "··········", "···············", "··················"));
        }
    }

    public static void ListsOutput()
    {
        Console.WriteLine("-------------------------");
        Console.WriteLine(string.Format("| {0,-15} | {1,-3} |", "Назва", "#"));
        Console.WriteLine("-------------------------");
        var dictionary = Storage.GetDictionary();

        foreach(var kvp in dictionary)
        {
            Console.WriteLine(String.Format("| {0,-15} | {1,-3} |", kvp.Key, kvp.Value.Count));
        }
        Console.WriteLine("-------------------------");
    }

    public static void LogOutput(List<string[]> parametersList)
    {
        OutputHeaderLog();
        for (int i = 0; i < parametersList.Count; i++)
        {
            if (parametersList[i].Length == 4)
            {
                Console.WriteLine(string.Format("| {0,-10} | {1,-5:F1} | {2,-5} | {3,-10} | {4,-15} | {5,-18} | {6,-18} | ", parametersList[i][0], parametersList[i][1], parametersList[i][2], "··········", "···············", "··················", parametersList[i][3]));
            }
            else if (parametersList[i].Length == 5)
            {
                Console.WriteLine(string.Format("| {0,-10} | {1,-5:F1} | {2,-5} | {3,-10} | {4,-15} | {5,-18} | {6,-18} |", parametersList[i][0], parametersList[i][1], parametersList[i][2], "··········", "···············", parametersList[i][3], parametersList[i][4]));
            }
            else if (parametersList[i].Length == 6)
            {
                Console.WriteLine(string.Format("| {0,-10} | {1,-5:F1} | {2,-5} | {3,-10} | {4,-15} | {5,-18} | {6,-18} |", parametersList[i][0], parametersList[i][1], parametersList[i][2], parametersList[i][3], parametersList[i][4], "··················", parametersList[i][5]));
            }
        }
    }

    private static void OutputHeader()
    {
        Console.WriteLine("----------------------------------------------------------------------------------");
        Console.WriteLine(string.Format("| {0,-10} | {1,-5:F1} | {2,-5} | {3,-10} | {4,-15} | {5,-18} |", "Назва", "Ціна", "Вага", "Сорт мяса", "Вид мяса", "Термін придатності"));
        Console.WriteLine("----------------------------------------------------------------------------------");
    }

    private static void OutputHeaderLog()
    {
        Console.WriteLine("--------------------------------------------------------------------------------------------------------");
        Console.WriteLine(string.Format("| {0,-10} | {1,-5:F1} | {2,-5} | {3,-10} | {4,-15} | {5,-18} | {6,-18} |", "Назва", "Ціна", "Вага", "Сорт мяса", "Вид мяса", "Термін придатності", "Дата помилки"));
        Console.WriteLine("--------------------------------------------------------------------------------------------------------");
    }
    #endregion
}