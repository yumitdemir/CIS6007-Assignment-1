namespace Task2;

public class Inventory
{
    internal static readonly Random _random = new Random();
    
    static internal List<Item> GenerateInventory(int size)
    {
        List<Item> inventory = new List<Item>();

        for (int i = 0; i < size; i++)
        {
            int type = _random.Next(1, 101);
            string barcode = Guid.NewGuid().ToString();
            string description = $"Type / {type}";
            inventory.Add(new Item(type, barcode, description));
        }

        return inventory;
    }
}