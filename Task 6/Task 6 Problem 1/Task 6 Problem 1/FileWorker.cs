﻿static class FileWorker
{
    public static string? path;
    public static void CreateDataFile(List<Consumer> consumers, int quarter)
    {
        FileWorker.CheckIfFileExists(path + "Data.txt");
        using (StreamWriter streamWriter = new StreamWriter(path + "Data.txt"))
        {
            streamWriter.WriteLine($"Номер кварталу: {quarter} | Кількість квартир: {consumers.Count}");
            streamWriter.WriteLine();
            for (int i = 0; i < consumers.Count; i++)
            {
                streamWriter.WriteLine($"Фамілія: {consumers[i].Surname} | Номер квартири: {consumers[i].ApartmentNumber} |");
                for (int j = 0; j < (consumers[i].GetMeteringsCount()/4); j++)
                {
                    int meteringIndex = (j+(quarter-1)*3);
                    streamWriter.WriteLine($"\tДата знаття показника: {consumers[i].GetMeteringIndicatorDate(meteringIndex).ToString("dd/MM/yyyy")} | Показник: {consumers[i].GetMeteringIndicator(meteringIndex)} |");
                }
                streamWriter.WriteLine();
            }
        }
    }

    public static void GetDataFromFile(List<Consumer> consumers)
    {
        FileWorker.CheckIfFileExists(path + "Data.txt");

        using (StreamReader streamReader = new StreamReader(path + "Data.txt"))
        {
            string surname = "";
            int apartmentNumber = 0, indicator = 0, day = 0, month = 0, year = 0;
            while (!streamReader.EndOfStream)
            {
                string[] line = streamReader.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < line.Length; i++) // need to be redone
                {
                    if (line[i] == "Фамілія:")
                    {
                        surname = line[i+1];
                    }
                    if (line[i] == "квартири:")
                    {
                        apartmentNumber = Convert.ToInt32(line[i+1]);
                        consumers.Add(new Consumer(surname, apartmentNumber));
                    }
                    if (line[i] == "показника:")
                    {
                        string tempDate = line[i+1];
                        string tempDay = Convert.ToString(tempDate[0]);
                        tempDay += tempDate[1];
                        day = Convert.ToInt32(tempDay);

                        string tempMonth = Convert.ToString(tempDate[3]);
                        tempMonth += tempDate[4];
                        month = Convert.ToInt32(tempMonth);

                        string tempYear = Convert.ToString(tempDate[6]);
                        tempYear += tempDate[7];
                        tempYear += tempDate[8];
                        tempYear += tempDate[9];
                        year = Convert.ToInt32(tempYear);
                    }
                    if (line[i] == "Показник:")
                    {
                        indicator = Convert.ToInt32(line[i+1]);
                        DateTime dateTime = new DateTime(year, month, day);
                        consumers[apartmentNumber-1].AddMetering(new Metering(indicator, dateTime));
                    }
                }
            }
        }
    }

    public static void OutputInFileListConsumers(List<Consumer> consumers, int quarter)
    {
        FileWorker.CheckIfFileExists(path + "Output.txt");

        using (StreamWriter streamWriter = new StreamWriter(path + "Output.txt"))
        {
            string[] months = new string[] { "Січень", "Лютий", "Березень", "Квітень", "Травень", "Червень", "Липень", "Серпень", "Вересень", "Жовтень", "Листопад", "Грудень" };
            int index = (quarter-1)*3;
            streamWriter.WriteLine("-----------------------------------------------------------------------");
            streamWriter.WriteLine(String.Format("| {0,-2} | {1,-10} | {2,-36} | {3,-10} |", "#", "Прізвище", "Квартал", "Витрати"));
            streamWriter.WriteLine("-----------------------------------------------------------------------");
            streamWriter.WriteLine(String.Format("| {0,-2} | {1,-10} | {2,-10} | {3,-10} | {4,-10} | {5,-10} |", "", "", months[index], months[++index], months[++index], ""));
            streamWriter.WriteLine("-----------------------------------------------------------------------");
            foreach (Consumer consumer in consumers)
            {
                streamWriter.WriteLine(String.Format("| {0,-2} | {1,-10} | {2,-10} | {3,-10} | {4,-10} | {5,-10:F1} |", consumer.ApartmentNumber, consumer.Surname, consumer.GetMeteringIndicator(0), consumer.GetMeteringIndicator(1), consumer.GetMeteringIndicator(2), Consumer.GetDebt(consumer)));
            }
            streamWriter.WriteLine("-----------------------------------------------------------------------");
        }
    }

    public static void OutputInFileConsumer(Consumer consumer, int quarter)
    {
        FileWorker.CheckIfFileExists(path + "Consumer.txt");

        using (StreamWriter streamWriter = new StreamWriter(path + "Consumer.txt"))
        {
            string[] months = new string[] { "Січень", "Лютий", "Березень", "Квітень", "Травень", "Червень", "Липень", "Серпень", "Вересень", "Жовтень", "Листопад", "Грудень" };
            int index = (quarter-1)*3;
            streamWriter.WriteLine("-----------------------------------------------------------------------");
            streamWriter.WriteLine(String.Format("| {0,-2} | {1,-10} | {2,-36} | {3,-10} |", "#", "Прізвище", "Квартал", "Витрати"));
            streamWriter.WriteLine("-----------------------------------------------------------------------");
            streamWriter.WriteLine(String.Format("| {0,-2} | {1,-10} | {2,-10} | {3,-10} | {4,-10} | {5,-10} |", "", "", months[index], months[++index], months[++index], ""));
            streamWriter.WriteLine("-----------------------------------------------------------------------");
            streamWriter.WriteLine(String.Format("| {0,-2} | {1,-10} | {2,-10} | {3,-10} | {4,-10} | {5,-10:F1} |", consumer.ApartmentNumber, consumer.Surname, consumer.GetMeteringIndicator(0), consumer.GetMeteringIndicator(1), consumer.GetMeteringIndicator(2), Consumer.GetDebt(consumer)));
            streamWriter.WriteLine("-----------------------------------------------------------------------");
        }

    }

    public static void OutputInFileDifferenceInDates(List<Consumer> consumers)
    {
        FileWorker.CheckIfFileExists(path + "Difference in dates.txt");

        using (StreamWriter streamWriter = new StreamWriter(path + "Difference in dates.txt"))
        {
            streamWriter.WriteLine("-----------------------------------------------------------------");
            streamWriter.WriteLine(String.Format("| {0,-2} | {1,-10} | {2,-25} | {3,-15} |", "#", "Прізвище", "Дата знаття показників", "Різниця в днях"));
            streamWriter.WriteLine("-----------------------------------------------------------------");
            foreach (Consumer consumer in consumers)
            {
                TimeSpan subtractDates = DateTime.Now.Subtract(consumer.GetMeteringIndicatorDate(0));
                streamWriter.WriteLine(String.Format("| {0,-2} | {1,-10} | {2,-25} | {3,-15} |", consumer.ApartmentNumber, consumer.Surname, consumer.GetMeteringIndicatorDate(2).ToString("dd/MM/yyyy"), subtractDates.ToString("dd")));
            }
            streamWriter.WriteLine("-----------------------------------------------------------------");
        }
    }

    public static void CheckIfFileExists(string path)
    {
        if (!File.Exists(path))
        {
            throw new Exception("File for this method does not exist!");
        }
    }
}