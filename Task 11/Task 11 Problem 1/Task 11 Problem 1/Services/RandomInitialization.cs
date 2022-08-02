static class RandomInitialization
{
    #region Methods
    public static void RandomProducts(string listName, int amount)
    {
        Random random = new Random();
        string[] namesProduct = { "Консерви", "Макарони", "Рис", "Цукор", "Сіль", "Кава", "Мед", "Борошно", "Спеції" };
        string[] namesMeat = { "Ковбаски", "Філе", "Фарш", "Шашлик", "Сосіски", "Сардельки", "Рулет", "Стейки" };
        string[] namesDairy = { "Молоко", "Кефір", "Ряжанка", "Йогурт", "Сметана", "Закваска", "Вершки", "Сир" };
        for (int i = 0; i < amount; i++)
        {
            int change = random.Next(3); // from 0 to 2
            switch (change)
            {
                case 0:
                    {
                        Storage.Append(listName, new Product(namesProduct[random.Next(namesProduct.Length)], random.Next(20, 100), random.Next(100, 2001)));
                        break;
                    }
                case 1:
                    {
                        int meatType = random.Next(2); // from 0 to 1
                        MeatType mt = MeatType.First;
                        switch (meatType)
                        {
                            case 0: mt = MeatType.First; break;
                            case 1: mt = MeatType.Second; break;
                        }
                        int meatCategory = random.Next(4); // from 0 to 3
                        Category ct = Category.Mutton;
                        switch (meatCategory)
                        {
                            case 0: ct = Category.Mutton; break;
                            case 1: ct = Category.Veal; break;
                            case 2: ct = Category.Pork; break;
                            case 3: ct = Category.Chicken; break;
                        }
                        Storage.Append(listName, new Meat(namesMeat[random.Next(namesMeat.Length)], random.Next(20, 100), random.Next(100, 2001), mt, ct));
                        break;
                    }
                case 2:
                    {
                        DateTime dt = DateTime.Now.AddDays(random.Next(181)); // from 15 days to 6 month
                        Storage.Append(listName, new Dairy(namesDairy[random.Next(namesDairy.Length)], random.Next(20, 100), random.Next(100, 2001), dt));
                        break;
                    }
            }
        }
    }
    #endregion
}
