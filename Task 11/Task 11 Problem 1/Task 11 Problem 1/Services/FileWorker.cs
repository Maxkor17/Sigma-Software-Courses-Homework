using System.Text;
class FileWorker
{
    #region Variables
    private string _path;
    public const string PathToData = "..\\..\\..\\StorageInformation\\Output\\";
    public const string PathToListFolder = "..\\..\\..\\StorageInformation\\Product lists\\";
    public const string PathToLog = "..\\..\\..\\StorageInformation\\logs.txt";
    #endregion

    #region Constructors
    public FileWorker(string path)
    {
        _path = path;
    }
    #endregion

    #region Methods
    public void ReadDataFromFile(string listName)
    {
        if (!File.Exists(_path))
        {
            var file = File.Create(_path);
            file.Close();
        }

        using (StreamReader streamReader = new StreamReader(_path))
        {
            // Skip first three lines
            streamReader.ReadLine();
            streamReader.ReadLine();
            streamReader.ReadLine();
            
            while (!streamReader.EndOfStream)
            {
                string line = streamReader.ReadLine();
                string[] lineSplit = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                bool isCorrect = true;

                string name = lineSplit[1].Trim();
                ErrorChecker.CheckName(name, ref isCorrect);

                ErrorChecker.CheckPrice(lineSplit[3].Trim(), ref isCorrect);
                ErrorChecker.CheckWeight(lineSplit[5].Trim(), ref isCorrect);
                decimal price = 0, weight = 0;
                if (isCorrect)
                {
                    price = Convert.ToDecimal(lineSplit[3].Trim());
                    weight = Convert.ToDecimal(lineSplit[5].Trim());
                }
                string[] parameters;
                if (!lineSplit[7].Contains('·'))
                {

                    ErrorChecker.CheckMeatType(lineSplit[7].Trim(), ref isCorrect);
                    ErrorChecker.CheckMeatCategory(lineSplit[9].Trim(), ref isCorrect);
                    string meatType = (lineSplit[7].Trim());
                    string meatCategory = (lineSplit[9].Trim());
                    if (isCorrect)
                    {
                        Storage.Append(listName, new Meat(name, price, weight, meatType, meatCategory));
                    }
                    else
                    {
                        parameters = new string[] { lineSplit[1].Trim(), lineSplit[3].Trim(), lineSplit[5].Trim(), lineSplit[7].Trim(), lineSplit[9].Trim() };
                        WriteLog(parameters);
                    }

                }
                else if (!lineSplit[11].Contains('·'))
                {
                    string date = lineSplit[11].Trim();
                    ErrorChecker.CheckDate(date, ref isCorrect);

                    if (isCorrect)
                    {
                        DateTime dt = DateTime.Parse(date);
                        Storage.Append(listName, new Dairy(name, price, weight, dt));
                    }
                    else
                    {
                        parameters = new string[] { lineSplit[1].Trim(), lineSplit[3].Trim(), lineSplit[5].Trim(), lineSplit[11].Trim() };
                        WriteLog(parameters);
                    }

                }
                else if (lineSplit[7].Contains('·') && lineSplit[11].Contains('·'))
                {

                    if (isCorrect)
                    {
                        Storage.Append(listName, new Product(name, price, weight));
                    }
                    else
                    {
                        parameters = new string[] { lineSplit[1].Trim(), lineSplit[3].Trim(), lineSplit[5].Trim() };
                        WriteLog(parameters);
                    }
                }
            }
        }
    }
    public void WriteDataToFile(List<IGoods> listProduct)
    {
        if (!File.Exists(_path))
        {
            var file = File.Create(_path);
            file.Close();
        }

        using (StreamWriter streamWriter = new(_path))
        {
            streamWriter.WriteLine("----------------------------------------------------------------------------------");
            streamWriter.WriteLine(string.Format("| {0,-10} | {1,-5:F1} | {2,-5} | {3,-10} | {4,-15} | {5,-18} |", "Назва", "Ціна", "Вага", "Сорт мяса", "Вид мяса", "Термін придатності"));
            streamWriter.WriteLine("----------------------------------------------------------------------------------");
            foreach (Product product in listProduct)
            {
                if (product is Meat)
                {
                    Meat p = product as Meat;
                    streamWriter.Write(string.Format("| {0,-10} | {1,-5:F1} | {2,-5} | {3,-10} | {4,-15} | {5,-18} |", p.Name, p.Price, p.Weight, p.GetMeatType(), p.GetCategory(), "··················"));
                }
                else if (product is Dairy)
                {
                    Dairy p = product as Dairy;
                    streamWriter.Write(string.Format("| {0,-10} | {1,-5:F1} | {2,-5} | {3,-10} | {4,-15} | {5,-18} |", p.Name, p.Price, p.Weight, "··········", "···············", p.expDate.ToString("dd/MM/yyyy")));
                }
                else
                {
                    streamWriter.Write(string.Format("| {0,-10} | {1,-5:F1} | {2,-5} | {3,-10} | {4,-15} | {5,-18} |", product.Name, product.Price, product.Weight, "··········", "···············", "··················"));
                }
                streamWriter.WriteLine();
            }
        }
    }

    private static void WriteLog(string[] parameters)
    {
        using (StreamWriter streamWriter = new(PathToLog, true))
        {
            if (parameters.Length == 3)
            {
                streamWriter.WriteLine(string.Format("| {0,-10} | {1,-5:F1} | {2,-5} | {3,-10} | {4,-15} | {5,-18} | {6,-18} | ", parameters[0], parameters[1], parameters[2], "··········", "···············", "··················", DateTime.Now));
            }
            else if (parameters.Length == 4)
            {
                streamWriter.WriteLine(string.Format("| {0,-10} | {1,-5:F1} | {2,-5} | {3,-10} | {4,-15} | {5,-18} | {6,-18} |", parameters[0], parameters[1], parameters[2], "··········", "···············", parameters[3], DateTime.Now));
            }
            else if (parameters.Length == 5)
            {
                streamWriter.WriteLine(string.Format("| {0,-10} | {1,-5:F1} | {2,-5} | {3,-10} | {4,-15} | {5,-18} | {6,-18} |", parameters[0], parameters[1], parameters[2], parameters[3], parameters[4], "··················", DateTime.Now));
            }
        }
    }
    public static List<string[]> ReadLog()
    {
        List<string[]> parametersList = new List<string[]>();
        using (StreamReader streamReader = new StreamReader(PathToLog))
        {
            // Skip first three lines
            streamReader.ReadLine();
            streamReader.ReadLine();
            streamReader.ReadLine();
            while (!streamReader.EndOfStream)
            {
                string line = streamReader.ReadLine();
                string[] lineSplit = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string[] parameters;
                if (!lineSplit[7].Contains('·'))
                {
                    parameters = new string[] { lineSplit[1].Trim(), lineSplit[3].Trim(), lineSplit[5].Trim(), lineSplit[7].Trim(), lineSplit[9].Trim(), lineSplit[13].Trim() };
                    parametersList.Add(parameters);
                }
                else if (!lineSplit[11].Contains('·'))
                {
                    parameters = new string[] { lineSplit[1].Trim(), lineSplit[3].Trim(), lineSplit[5].Trim(), lineSplit[11].Trim(), lineSplit[13].Trim() };
                    parametersList.Add(parameters);
                }
                else if (lineSplit[7].Contains('·') && lineSplit[11].Contains('·'))
                {
                    parameters = new string[] { lineSplit[1].Trim(), lineSplit[3].Trim(), lineSplit[5].Trim(), lineSplit[13].Trim() };
                    parametersList.Add(parameters);
                }
            }
        }
        return parametersList;
    }
    #endregion
}