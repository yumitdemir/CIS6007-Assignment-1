namespace Task2;

public class Item(int type, string barcode, string description)
{
    public string Barcode { get; set; } = barcode;
    public string Description { get; set; } = description;
    public int Type { get; set; } = type;
}