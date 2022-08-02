static class UserInterface
{
    private static string nameWorkingList = "Основний"; // working list name
    
    #region Methods
    public static void MainMenu()
    {
        Console.WriteLine("Робота з списками продуктів.");
        Console.WriteLine("1. Створити новий список продуктів");
        Console.WriteLine("2. Вивести створені списки товарів");
        Console.WriteLine("3. Товари є в першому складі і немає в другому.");
        Console.WriteLine("4. Товари, які є спільними в обох складах.");
        Console.WriteLine("5. Спільний список товарів, які є на обох складах, без повторів елементів.");
        Console.WriteLine("6. Перейти до роботи з списком");
        
        Console.Write("\nВиберіть функцію: ");
        int input = Convert.ToInt32(Console.ReadLine());
        Console.Clear();
        switch (input)
        {
            case 1: AddListProducts(); break;
            case 2: ListsOutput(); break;
            case 3: GetExclusive(); break;
            case 4: GetCommon(); break;
            case 5: GetMerged(); break;
            case 6: WorkWithList(); break;
        }
    }
    
    private static void GetExclusive()
    {
        Console.Write("Введіть назву першого списку: ");
        string name1 = Console.ReadLine();

        Console.Write("Введіть назву другого списку: ");
        string name2 = Console.ReadLine();

        FileWorker fileWorker = new(FileWorker.PathToData + "Exclusive Output.txt");
        fileWorker.WriteDataToFile(Storage.GetExclusive(name1, name2));
        BackToMainMenu();
    }

    private static void GetCommon()
    {
        Console.Write("Введіть назву першого списку: ");
        string name1 = Console.ReadLine();

        Console.Write("Введіть назву другого списку: ");
        string name2 = Console.ReadLine();

        FileWorker fileWorker = new(FileWorker.PathToData + "Common Output.txt");
        fileWorker.WriteDataToFile(Storage.GetCommon(name1, name2));
        BackToMainMenu();
    }
    private static void GetMerged()
    {
        Console.Write("Введіть назву першого списку: ");
        string name1 = Console.ReadLine();

        Console.Write("Введіть назву другого списку: ");
        string name2 = Console.ReadLine();

        FileWorker fileWorker = new(FileWorker.PathToData + "Merged Output.txt");
        fileWorker.WriteDataToFile(Storage.GetMerged(name1, name2));
        BackToMainMenu();
    }
    private static void ListsOutput()
    {
        ConsoleWorker.ListsOutput();
        BackToMainMenu();
    }
    private static void AddListProducts()
    {
        Console.Write("Введіть назву списку: ");
        string name = Console.ReadLine();
        Storage.AddListProducts(name);
        BackToMainMenu();
    }

    private static void WorkWithList() // !
    {
        Console.Write("Введіть назву списку: ");
        nameWorkingList = Console.ReadLine();
        Console.Clear();
        ListMenu();
    }

    private static void ListMenu()
    {
        Console.WriteLine($"Перелік функцій для листу \"{nameWorkingList}\":");
        Console.WriteLine("Робота з файлами.");
        Console.WriteLine("1. Загрузити дані з файлу");
        Console.WriteLine("2. Зберегти дані в основний файл");
        Console.WriteLine("3. Виправити дані в журналі помилок");
        Console.WriteLine($"Робота із завантаженими даними в листі.");
        Console.WriteLine("4. Наповнення інформацією даних у режимі діалогу з користувачем");
        Console.WriteLine("5. Наповнення інформацією даних шляхом ініціалізації");
        Console.WriteLine("6. Виведення повної інформації про всі товари");
        Console.WriteLine("7. Виведення всіх мясних продуктів");
        Console.WriteLine("8. Змінити ціну для всіх товарів на заданий відсоток");
        Console.WriteLine("9. Переглянути або змінити дані товару за індексом");
        Console.WriteLine("10. Повернутися до головного меню");

        Console.Write("\nВиберіть функцію: ");
        int input = Convert.ToInt32(Console.ReadLine());
        Console.Clear();
        switch (input)
        {
            case 1: FillStorageFromFile(); break;
            case 2: SaveStorageToFile(); break;
            case 3: ImproveErrors(); break;
            case 4: GetProductInDialogue(); break;
            case 5: AddRandomProducts(); break;
            case 6: AllProductsOutput(); break;
            case 7: AllMeatOutput(); break;
            case 8: ChangeValueAllProducts(); break;
            case 9: ChangeProductField(); break;
            case 10: MainMenu(); break;
        }
    }
    
    private static void FillStorageFromFile()
    {
        Console.WriteLine($"1. Загрузити дані з основного файлу");
        Console.WriteLine("2. Загрузити дані з свого файлу");
        Console.Write("Виберіть: ");
        int input = Convert.ToInt32(Console.ReadLine());
        Console.Clear();
        switch (input)
        {
            case 1: { FillFromMainFile(); break; }
            case 2: { FillFromCustomFile(); break; }
        }
        Console.WriteLine("Товари успішно додані!");
        BackToMenu();
    }
    private static void FillFromMainFile() // створювати файл, пустий
    {
        FileWorker fileWorker = new(FileWorker.PathToListFolder + nameWorkingList + ".txt");
        fileWorker.ReadDataFromFile(nameWorkingList);
    }
    private static void FillFromCustomFile()
    {
        string path;
        while (true)
        {
            Console.WriteLine("Введіть шлях по прикладу: D:\\Visual Studio Projects\\Task 7\\Task 7\\data\\products.txt");
            path = Console.ReadLine();
            if (File.Exists(path))
            {
                break;
            }
            else
            {
                Console.WriteLine("Файл не існує!");
                Console.WriteLine("1. Попробувати знову");
                Console.WriteLine("2. Повернутися до меню");
                Console.Write("Виберіть: ");
                Console.Clear();
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1: { break; }
                    case 2: { BackToMenu(); break; }
                }
            }
        }
        FileWorker fileWorker = new(path);
        fileWorker.ReadDataFromFile(nameWorkingList);
    }

    private static void SaveStorageToFile()
    {
        FileWorker fileWorker = new(FileWorker.PathToListFolder + nameWorkingList + ".txt");
        fileWorker.WriteDataToFile(Storage.GetListProducts(nameWorkingList));
        Console.WriteLine("Дані успішно збережено!");
        BackToMenu();
    }

    private static void ImproveErrors()
    {
        List<string[]> parametersList = FileWorker.ReadLog();
        ConsoleWorker.LogOutput(parametersList);
        Console.Write("\nВведіть дату, пізніше якої хочете виправити всі дані: ");
        string date = Console.ReadLine();
        DateTime dateInput = DateTime.Parse(date);
        Console.WriteLine(dateInput.ToString());
        Console.Clear();

        for (int i = 0; i < parametersList.Count; i++)
        {
            if (DateTime.Compare(dateInput, DateTime.Parse(parametersList[i][^1])) < 0)
            {
                Console.WriteLine($"Поточна назва: {parametersList[i][0]}");
                Console.WriteLine("Назва не може утримувати цифри.");
                Console.WriteLine($"Кількість символів в назві повинно бути більше {ErrorChecker.MinCharactersInName}, а також менше {ErrorChecker.MaxCharactersInName}.");
                Console.Write("Введіть нову назву: ");
                string name = Console.ReadLine();

                Console.WriteLine($"Поточна ціна: {parametersList[i][1]}");
                Console.WriteLine("Ціна не може утримувати букви.");
                Console.WriteLine($"Ціна не може бути більшою за {ErrorChecker.MaxPrice}, а також меншою за {ErrorChecker.MinPrice}.");
                Console.Write("Введіть нову ціну: ");
                decimal price = Convert.ToDecimal(Console.ReadLine());

                Console.WriteLine($"Поточна вага: {parametersList[i][2]}");
                Console.WriteLine("Вага не може утримувати букви.");
                Console.WriteLine($"Вага не може бути більшою за {ErrorChecker.MaxWeight}, а також меншою за {ErrorChecker.MinWeight}.");
                Console.Write("Введіть нову вагу: ");
                decimal weight = Convert.ToDecimal(Console.ReadLine());

                if (parametersList[i].Length == 6)
                {
                    Console.WriteLine($"Поточний тип мяса: {parametersList[i][3]}");
                    Console.WriteLine("Доступні типи мяса: 1-й, 2-й.");
                    Console.Write("Введіть новий тип: ");
                    string meatType = Console.ReadLine();

                    Console.WriteLine($"Поточний вид мяса: {parametersList[i][4]}");
                    Console.WriteLine("Доступні види мяса: Баранина, Телятина, Свинина, Курятина.");
                    Console.Write("Введіть новий вид: ");
                    string meatCategory = Console.ReadLine();
                    Storage.Append(name, new Meat(name, price, weight, meatType, meatCategory));
                }
                else if (parametersList[i].Length == 5)
                {
                    Console.WriteLine($"Поточний термін придатності: {parametersList[i][3]}");
                    Console.Write("Введіть нову дату: ");
                    string dateD = Console.ReadLine();
                    DateTime dtD = DateTime.Parse(dateD);
                    Storage.Append(name, new Dairy(name, price, weight, dtD));
                }
                else
                {
                    Storage.Append(name, new Product(name, price, weight));
                }
            }
        }
        Console.WriteLine("Товари успішно додані!");
        BackToMenu();
    }

    private static void GetProductInDialogue()
    {
        Console.Write("1 - Product \n2 - Meat \n3 - Dairy \nВиберіть тип продукту: ");
        int input = Convert.ToInt32(Console.ReadLine());
        Console.Write("Введіть назву продукту: ");
        string name = Console.ReadLine();
        Console.Write("Введіть ціну продукту: ");
        int price = Convert.ToInt32(Console.ReadLine());
        Console.Write("Введіть вагу продукту: ");
        int weight = Convert.ToInt32(Console.ReadLine());
        switch (input)
        {
            case 1: Storage.Append(nameWorkingList, new Product(name, price, weight)); break;
            case 2: GetMeatInDialogue(name, price, weight); break;
            case 3: GetDairyInDialogue(name, price, weight); break;
        }
        Console.WriteLine("\nТовар успішно доданий!");
        BackToMenu();
    }
    private static void GetMeatInDialogue(string name, int price, int weight)
    {
        Console.WriteLine("Виберіть сорт мяса: 1 - перший сорт, 2 - другий сорт");
        int inputType = Convert.ToInt32(Console.ReadLine());
        MeatType type = MeatType.First;
        switch (inputType)
        {
            case 1: type = MeatType.First; break;
            case 2: type = MeatType.Second; break;
        }
        Console.WriteLine("Виберіть тип мяса: 1 - баранина, 2 - телятина, 3 - свинина, 4 - курятина");
        int inputCategory = Convert.ToInt32(Console.ReadLine());
        Category category = Category.Mutton;
        switch (inputCategory)
        {
            case 1: category = Category.Mutton; break;
            case 2: category = Category.Veal; break;
            case 3: category = Category.Pork; break;
            case 4: category = Category.Chicken; break;
        }
        Storage.Append(nameWorkingList, new Meat(name, price, weight, type, category));
    }
    private static void GetDairyInDialogue(string name, int price, int weight)
    {
        Console.WriteLine("Введіть термін придатності.");
        string date = Console.ReadLine();
        DateTime dt = DateTime.Parse(date);
        Storage.Append(nameWorkingList, new Dairy(name, price, weight, dt));
    }
    
    private static void AddRandomProducts()
    {
        Console.Write("Введіть кількість товарів, яку хочете додати: ");
        int amount = Convert.ToInt32(Console.ReadLine());
        RandomInitialization.RandomProducts(nameWorkingList, amount);
        Console.Clear();
        Console.WriteLine("Товари в кількості {0} були успішно сгенеровані!", amount);
        BackToMenu();
    }
   
    private static void AllProductsOutput()
    {
        ConsoleWorker.AllProductsOutput(Storage.GetListProducts(nameWorkingList));
        BackToMenu();
    }

    private static void AllMeatOutput()
    {
        ConsoleWorker.AllMeatOutput(Storage.GetListProducts(nameWorkingList));
        BackToMenu();
    }
    
    private static void ChangeValueAllProducts()
    {
        var listProduct = Storage.GetListProducts(nameWorkingList);
        Console.Write("Введіть на який відсоток змінити ціну: ");
        decimal percent = decimal.Parse(Console.ReadLine());
        for (int i = 0; i < listProduct.Count; i++)
        {
            listProduct[i].ChangeValue(percent);
        }
        Storage.SetListProducts(nameWorkingList, listProduct);
        Console.Clear();
        Console.WriteLine("Ціни всіх продуктів успішно змінені на {0}%!", percent);
        BackToMenu();
    }

    private static void ChangeProductField()
    {
        Console.WriteLine("1. Відобразити дані про товар");
        Console.WriteLine("2. Змінити дані про товар");
        Console.Write("Виберіть функцію: ");
        int choose = Convert.ToInt32(Console.ReadLine());
        Console.Write("Введіть індекс товару: ");
        int index = Convert.ToInt32(Console.ReadLine());
        List<IGoods> listProduct = Storage.GetListProducts(nameWorkingList);
        switch (choose)
        {
            case 1:
                {
                    Console.Clear();
                    ConsoleWorker.OutputProduct(listProduct, index);
                    BackToMenu();
                    break;
                }

            case 2:
                {
                    Console.WriteLine("1. Змінити назву товару");
                    Console.WriteLine("2. Змінити ціну товару");
                    Console.WriteLine("3. Змінити вагу товару");
                    if (listProduct[index] is Meat)
                    {
                        Console.WriteLine("4. Змінити сорт мяса");
                        Console.WriteLine("5. Змінити вид мяса");
                    }
                    else if (listProduct[index] is Dairy)
                    {
                        Console.WriteLine("4. Змінити термін придатності");
                    }
                    Console.Write("Виберіть функцію: ");
                    int choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("Введіть нову назву: ");
                            listProduct[index].Name = Console.ReadLine(); break;
                        case 2:
                            Console.WriteLine("Введіть нову ціну: ");
                            listProduct[index].Price = Convert.ToInt32(Console.ReadLine()); break;
                        case 3:
                            Console.WriteLine("Введіть нову вагу: ");
                            listProduct[index].Weight = Convert.ToInt32(Console.ReadLine()); break;
                        case 4:
                            if (listProduct[index] is Meat)
                            {
                                Meat meatT = listProduct[index] as Meat;
                                Console.Write("Виберіть сорт мяса (1 - перший сорт, 2 - другий сорт): ");
                                int choiceMeatType = Convert.ToInt32(Console.ReadLine());
                                meatT.SetMeatType(choiceMeatType);
                                break;
                            }
                            else if (listProduct[index] is Dairy)
                            {
                                Dairy dairy = listProduct[index] as Dairy;
                                Console.WriteLine("Введіть термін придатності.");
                                Console.Write("Рік: ");
                                int year = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Місяць: ");
                                int month = Convert.ToInt32(Console.ReadLine());
                                Console.Write("День: ");
                                int day = Convert.ToInt32(Console.ReadLine());
                                dairy.SetexpDate(year, month, day);
                            }
                            break;
                        case 5:
                            Meat meatC = listProduct[index] as Meat;
                            Console.Write("Виберіть тип мяса (1 - баранина, 2 - телятина, 3 - свинина, 4 - курятина): ");
                            int choiceCategory = Convert.ToInt32(Console.ReadLine());
                            meatC.SetCategory(choiceCategory);
                            break;
                    }
                    Storage.SetListProducts(nameWorkingList, listProduct);
                    break;
                }
        }
        BackToMenu();
    }

    private static void BackToMenu()
    {
        Console.Write("Нажміть будь-яку клавішу, щоб перейти в меню...");
        Console.ReadKey();
        Console.Clear();
        ListMenu();
    }

    private static void BackToMainMenu()
    {
        Console.Write("Нажміть будь-яку клавішу, щоб перейти в меню...");
        Console.ReadKey();
        Console.Clear();
        MainMenu();
    }

    #endregion
}