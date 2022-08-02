static class ErrorChecker
{
    #region Variables
    public const int MinCharactersInName = 2, MaxCharactersInName = 10;
    public const int MaxPrice = 10000, MinPrice = 0;
    public const int MaxWeight = 10000, MinWeight = 50;
    #endregion

    #region Methods
    public static void CheckName(string name, ref bool isCorrect)
    {
        if(name.Any(char.IsDigit) || name.Length <= MinCharactersInName || name.Length >= MaxCharactersInName)
        {
            isCorrect = false;
        }
    }

    public static void CheckPrice(string price, ref bool isCorrect)
    {
        if (!decimal.TryParse(price, out _))
        {
            isCorrect = false;
        }
        else
        {
            decimal priceD = Convert.ToDecimal(price);
            if (priceD > MaxPrice || priceD < MinPrice)
            {
                isCorrect = false;
            }
        }
    }

    public static void CheckWeight(string weight, ref bool isCorrect)
    {
        if (!decimal.TryParse(weight, out _))
        {
            isCorrect = false;
        }
        else
        {
            decimal priceD = Convert.ToDecimal(weight);
            if (priceD > MaxWeight || priceD < MinWeight)
            {
                isCorrect = false;
            }
        }
    }

    public static void CheckMeatType(string meatType, ref bool isCorrect)
    {
        if (meatType != "1-й" && meatType != "2-й")
        {
            isCorrect = false;
        }
    }

    public static void CheckMeatCategory(string meatCategory, ref bool isCorrect)
    {
        if (meatCategory != "Баранина" && meatCategory != "Телятина" && meatCategory != "Свинина" && meatCategory != "Курятина")
        {
            isCorrect = false;
        }
    }

    public static void CheckDate(string date, ref bool isCorrect)
    {
        if(!DateTime.TryParse(date, out _))
        {
            isCorrect = false;
        }
    }
    #endregion
}