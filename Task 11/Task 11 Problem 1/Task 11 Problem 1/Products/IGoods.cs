interface IGoods
{
    string Name { get; set; }
    decimal Price { get; set; }
    decimal Weight { get; set; }
    public void ChangeValue(decimal percent);
}
