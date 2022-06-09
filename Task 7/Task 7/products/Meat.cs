﻿enum MeatType { First, Second };
enum Category { Mutton, Veal, Pork, Chicken };

sealed class Meat : Product
{
    public MeatType meatType;
    public Category meatCategory;
    private const decimal _FirstPercent = 15, _SecondPercent = 5; // Increase the cost of the product depending on its grade (firstPercent for MeatType.First, secondPercent for MeatType.second)
   
    public Meat(string n, decimal p, decimal w, MeatType t, Category c)
    {
        Name = n;
        price = p;
        weight = w;
        meatType = t;
        meatCategory = c;
        if (meatType == MeatType.First)
        {
            price += price * (_FirstPercent/100);
        }
        else if (meatType == MeatType.Second)
        {
            price += price * (_SecondPercent/100);
        }
    }

    public Meat(string n, decimal p, decimal w, string t, string c)
    {
        Name = n;
        price = p;
        weight = w;

        switch (t) {
            case "1-й": { meatType = MeatType.First; break; }
            case "2-й": { meatType = MeatType.Second; break; }
            default: { break; } // !
        }

        switch (c)
        {
            case "Баранина": { meatCategory = Category.Mutton; break; }
            case "Телятина": { meatCategory = Category.Veal; break; }
            case "Свинина": { meatCategory = Category.Pork; break; }
            case "Курятина": { meatCategory = Category.Chicken; break; }
            default: { break; } // !
        }

        if (meatType == MeatType.First)
        {
            price += price * (_FirstPercent/100);
        }
        else if (meatType == MeatType.Second)
        {
            price += price * (_SecondPercent/100);
        }
    }
   
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
        price += price * (percent/100);
    }
}