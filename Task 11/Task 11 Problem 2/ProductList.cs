internal class ProductList<T>
{
    List<T> products;
    public T this[int index]
    {
        get => products[index];
        set => products[index] = value;
    }

    public ProductList()
    {
        products = new List<T>();
    }

    public void Add(T item)
    {
        products.Add(item);
    }

    public void Clear()
    {
        products = new List<T>();
    }

    public bool Contains(T item)
    {
        return products.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        for (int i = arrayIndex, j = 0; i < arrayIndex + products.Count; i++, j++)
        {
            array[i] = products[j];
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        return products.GetEnumerator();
    }

    public int IndexOf(T item)
    {
        return products.IndexOf(item);
    }

    public void Insert(int index, T item)
    {
        products.Insert(index, item);
    }

    public bool Remove(T item)
    {
        return products.Remove(item);
    }

    public void RemoveAt(int index)
    {
        products.RemoveAt(index);
    }
}