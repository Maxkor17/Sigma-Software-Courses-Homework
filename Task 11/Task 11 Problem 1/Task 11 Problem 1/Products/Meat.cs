#region Enumerations
enum MeatType { First, Second };
enum Category { Mutton, Veal, Pork, Chicken };
#endregion

sealed class Meat : Product
{
    #region Variables
    public MeatType meatType;
    public Category meatCategory;
    private const decimal _FirstPercent = 15, _SecondPercent = 5; // Increase the cost of the product depending on its grade (firstPercent for MeatType.First, secondPercent for MeatType.second)
    #endregion

    #region Constructors
    public Meat(string n, decimal p, decimal w, MeatType t, Category c)
    {
        Name = n;
        Price = p;
        Weight = w;
        meatType = t;
        meatCategory = c;
        if (meatType == MeatType.First)
        {
            Price += Price * (_FirstPercent/100);
        }
        else if (meatType == MeatType.Second)
        {
            Price += Price * (_SecondPercent/100);
        }
    }

    public Meat(string n, decimal p, decimal w, string t, string c)
    {
        Name = n;
        Price = p;
        Weight = w;

        switch (t) {
            case "1-й": { meatType = MeatType.First; break; }
            case "2-й": { meatType = MeatType.Second; break; }
        }

        switch (c)
        {
            case "Баранина": { meatCategory = Category.Mutton; break; }
            case "Телятина": { meatCategory = Category.Veal; break; }
            case "Свинина": { meatCategory = Category.Pork; break; }
            case "Курятина": { meatCategory = Category.Chicken; break; }
        }

        if (meatType == MeatType.First)
        {
            Price += Price * (_FirstPercent/100);
        }
        else if (meatType == MeatType.Second)
        {
            Price += Price * (_SecondPercent/100);
        }
    }
    #endregion

    #region Methods
    public void SetMeatType(int i)
    {
        switch (i)
        {
            case 1: meatType = MeatType.First; break;
            case 2: meatType = MeatType.Second; break;
        }
    }
   
    public string GetMeatType()
    {
        return meatType switch
        {
            MeatType.First => "1-й",
            MeatType.Second => "2-й"
        };
    }

    public void SetCategory(int i)
    {
        switch (i)
        {
            case 1: meatCategory = Category.Mutton; break;
            case 2: meatCategory = Category.Veal; break;
            case 3: meatCategory = Category.Pork; break;
            case 4: meatCategory = Category.Chicken; break;
        }
    }

    public string GetCategory()
    {
        return meatCategory switch
        {
            Category.Mutton => "Баранина",
            Category.Veal => "Телятина",
            Category.Pork => "Свинина",
            Category.Chicken => "Курятина"
        };
    }

    public override void ChangeValue(decimal percent)
    {
        Price += Price * (percent/100);
    }
    public override int GetHashCode()
    {
        return Name.GetHashCode()^Price.GetHashCode()^Weight.GetHashCode()^meatType.GetHashCode()^meatCategory.GetHashCode();
    }
    #endregion
}