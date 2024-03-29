﻿class FileWorker
{
    #region Variables
    private string _path;
    public const string PathToPrices = "..\\..\\..\\data\\Prices.txt";
    public const string PathToMenu = "..\\..\\..\\data\\Menu.txt";
    public const string PathToCourse = "..\\..\\..\\data\\Course.txt";
    #endregion

    #region Constructors
    public FileWorker(string path)
    {
        try
        {
            if (!File.Exists(path))
            {
                var file = File.Create(path);
                file.Close();
            }
            _path = path;
        }
        catch
        {
            throw new Exception($"Деякі проблеми з шляхом до файлу! [{path}]");
        }
    }
    #endregion

    #region Methods
    public void LoadPricesForProducts()
    {
        using (StreamReader pricesFile = new StreamReader(_path))
        {
            try
            {
                while (!pricesFile.EndOfStream)
                {
                    string[] line;
                    do
                    {
                        line = pricesFile.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    } while (line.Length == 0);
                    string name = line[0];
                    decimal price = decimal.Parse(line[2]);
                    Storage.productsPrices.Add(name, price);
                }
            }
            catch (FormatException)
            {
                throw new FormatException($"Деякі проблеми зі зчитуванням цін товарів! [{_path}]");
            }
        }
    }
    public void LoadDishesToMenu()
    {
        using (StreamReader dishesFile = new StreamReader(_path))
        {
            try
            {
                while (!dishesFile.EndOfStream)
                {
                    string dishName;
                    do
                    {
                        dishName = dishesFile.ReadLine();
                    } while (string.IsNullOrEmpty(dishName));

                    List<Product> listProducts = new();
                    while (true && !dishesFile.EndOfStream)
                    {
                        var line = dishesFile.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                        if (line.Length == 0) break;
                        listProducts.Add(new Product(line[0].Replace(",", ""), int.Parse(line[1].Replace("г", "").Replace("мл", ""))));
                    }

                    if (listProducts.Count == 0) throw new Exception($"Інгредієнтів для блюда \"{dishName}\" не знайдено!");
                    Storage.menu.Add(new Dish(dishName, listProducts));
                }
            }
            catch (FormatException)
            {
                throw new FormatException($"Деякі проблеми зі зчитуванням блюд! [{_path}]");
            }
        }
    }
    public void LoadCourses()
    {
        using (StreamReader pricesFile = new StreamReader(_path))
        {
            try
            {
                while (!pricesFile.EndOfStream)
                {
                    string[] line;
                    do
                    {
                        line = pricesFile.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    } while (line.Length == 0);
                    string name = line[1];
                    decimal price = decimal.Parse(line[3]);
                    Storage.courses.Add(name, price);
                }
            }
            catch (FormatException)
            {
                throw new FormatException($"Деякі проблеми зі зчитуванням біжучого курсу валют! [{_path}]");
            }
        }
    }

    public void WriteNewProductPrice(string name)
    {
        using (StreamWriter streamWriter = new StreamWriter(_path, true))
        {
            streamWriter.WriteLine($"{name} - {Storage.productsPrices[name]} UAH");
        }
    }
    public void WriteTotalProducts(Dictionary<string, List<Product>> productsTotal, string courseCode) // the calculation of parameters for the TOTAL can be made in the methods of the class Storage
    {
        using (StreamWriter streamWriter = new StreamWriter(_path))
        {
            int totalProductsNumber = 0, totalProductsWeight = 0;
            decimal totalProductsPrice = 0;
            streamWriter.WriteLine("----------------------------------------------------------------");
            streamWriter.WriteLine(String.Format("| {0,-15} | {1,-3} | {2,-10} | {3,-23} |", "Назва продукту", "#", "Вага/Об`єм", "Ціна"));
            streamWriter.WriteLine("----------------------------------------------------------------");
            streamWriter.WriteLine(String.Format("| {0,-15} | {1,-3} | {2,-10} | {3,-10} | {4,-10} |", "", "", "г/мл", "UAH", courseCode));
            streamWriter.WriteLine("----------------------------------------------------------------");
            foreach (KeyValuePair<string, List<Product>> kvp in productsTotal)
            {
                decimal totalPrice = 0;
                int totalWeight = 0;
                foreach (var product in kvp.Value)
                {
                    totalPrice += product.Price;
                    totalWeight += product.Weight;
                }
                streamWriter.WriteLine(String.Format("| {0,-15} | {1,-3} | {2,-10} | {3,-10:F2} | {4,-10:F2} |", kvp.Key, kvp.Value.Count, totalWeight, totalPrice,  totalPrice/Storage.courses[courseCode]));
                totalProductsPrice += totalPrice;
                totalProductsWeight += totalWeight;
                totalProductsNumber += kvp.Value.Count;
            }
            streamWriter.WriteLine("----------------------------------------------------------------");
            streamWriter.WriteLine(String.Format("| {0,-15} | {1,-3} | {2,-10} | {3,-10:F2} | {4,-10:F2} |", "TOTAL", totalProductsNumber, totalProductsWeight, totalProductsPrice,  totalProductsPrice/Storage.courses[courseCode]));
            streamWriter.WriteLine("----------------------------------------------------------------");
        }
    }
    #endregion
}