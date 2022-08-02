class Product : IGoods
{
    #region Variables
    private string _name;
    private decimal _price, _weight;
    #endregion

    #region Properties
    public string Name 
    { 
        get => _name;
        set
        {
            if (!char.IsUpper(value[0]))
            {
                char[] charArr = value.ToCharArray();
                charArr[0] = char.ToUpper(charArr[0]);
                string temp = new string(charArr);
                _name = temp;
            }
            else
            {
                _name = value;
            }
        }
    }
    public decimal Price
    { 
        get => _price;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Ціна не може бути меньшою або дорівнювати нулю!");
            }
            _price = value;
        }
    }
    public decimal Weight
    {
        get => _weight;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Вага не може бути меньшою або дорівнювати нулю!");
            }
            _weight = value;
        }
    }
    #endregion

    #region Constructors
    public Product(string n, decimal p, decimal w)
    {
        Name = n;
        Price = p;
        Weight = w;
    }
    protected Product() { }
    #endregion

    #region Methods
    public virtual void ChangeValue(decimal percent)
    {
        Price += Price * (percent/100);
    }

    public virtual int GetHashCode()
    {
        return Name.GetHashCode()^Price.GetHashCode()^Weight.GetHashCode();
    }
    #endregion
}
