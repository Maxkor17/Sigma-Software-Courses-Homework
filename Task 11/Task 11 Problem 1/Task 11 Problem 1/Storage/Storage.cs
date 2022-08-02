static class Storage
{
    #region Variables
    private static List<IGoods> listGoods = new();
    private static Dictionary<string, List<IGoods>> dictionaryProducts = new()
    {
        {"Основний", listGoods }
    };
    #endregion

    #region Methods
    public static void Append(string name, Product product)
    {
        dictionaryProducts[name].Add(product);
    }

    public static List<IGoods> GetListProducts(string name)
    {
        return dictionaryProducts[name];
    }

    public static void SetListProducts(string name, List<IGoods> products)
    {
        dictionaryProducts[name] = products;
    }

    public static void AddListProducts(string name)
    {
        List<IGoods> products = new();
        dictionaryProducts.Add(name, products);
    }

    public static int GetAmountLists()
    {
        return dictionaryProducts.Count;
    }

    public static Dictionary<string, List<IGoods>> GetDictionary()
    {
        return dictionaryProducts;
    }

    public static List<IGoods> GetExclusive(string key1, string key2)
    {
        List<IGoods> res = new();
        res.AddRange(dictionaryProducts[key1]);
        foreach (Product product1 in dictionaryProducts[key1])
        {
            foreach (Product product2 in dictionaryProducts[key2])
            {
                if (product1.GetHashCode() == product2.GetHashCode())
                {
                    res.Remove(product1);
                }
            }
        }
        return res;
    }

    public static List<IGoods> GetCommon(string key1, string key2)
    {
        List<IGoods> res = new();
        foreach (Product product1 in dictionaryProducts[key1])
        {
            foreach (Product product2 in dictionaryProducts[key2])
            {
                if (product1.GetHashCode() == product2.GetHashCode())
                {
                    res.Add(product1);
                }
            }
        }
        return res;
    }

    public static List<IGoods> GetMerged(string key1, string key2)
    {
        List<IGoods> res = new();
        foreach (Product product1 in dictionaryProducts[key1])
        {
            foreach (Product product2 in dictionaryProducts[key2])
            {
                bool notDupe = true;
                foreach (Product product3 in res)
                {
                    if (product3.GetHashCode() == product2.GetHashCode())
                    {
                        notDupe = false;
                    }
                }
                if (product1.GetHashCode() != product2.GetHashCode() && notDupe)
                {
                    res.Add(product2);
                }
            }
        }
        return res;
    }
    #endregion
}