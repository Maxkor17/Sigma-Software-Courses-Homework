sealed class Dairy : Product
{
    #region Variables
    public DateTime expDate;
    private const decimal _Expired = 10, _NotExpired = 15; // Increase and decrease the cost of the product depending on its expiration date
    #endregion

    #region Constructors
    public Dairy(string n, decimal p, decimal w, DateTime e)
    {
        Name = n;
        Price = p;
        Weight = w;
        expDate = e;

        if (DateTime.Compare(expDate, DateTime.Now) > 0) // If Dairy product expiration date has not passed
        {
            
            Price += Price * (_NotExpired/100); // it price increases on {notexpired} percent
        }
        else
        {
            Price -= Price * (_Expired/100); // else it decreases on {expired} percent
        }
    }
    #endregion

    #region Methods
    public void SetexpDate(int year, int month, int day)
    {
        expDate = new DateTime(year, month, day);
    }
   
    public override void ChangeValue(decimal percent)
    {
        Price += Price * (percent/100);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode()^Price.GetHashCode()^Weight.GetHashCode()^expDate.GetHashCode();
    }
    #endregion
}