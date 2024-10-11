using System.Collections.Concurrent;

namespace Task2;

public class ParallelSearch
{
    private static readonly object _inventoryLock = new();

    internal static ConcurrentDictionary<int, ConcurrentBag<string>> SearchInventory(
        List<Item> inventory,
        Dictionary<int, int> targetCounts,
        int maxThreads)
    {
        var foundItems = InitializeFoundItems(targetCounts);

        Parallel.ForEach(inventory, new ParallelOptions { MaxDegreeOfParallelism = maxThreads }, item =>
        {
            if (targetCounts.ContainsKey(item.Type))
            {
                lock (_inventoryLock)
                {
                    if (foundItems[item.Type].Count < targetCounts[item.Type])
                    {
                        foundItems[item.Type].Add(item.Barcode);

                        if (IsSearchComplete(targetCounts, foundItems)) return;
                    }
                }
            }
        });

        return foundItems;
    }

    private static ConcurrentDictionary<int, ConcurrentBag<string>> InitializeFoundItems(
        Dictionary<int, int> targetCounts)
    {
        var foundItems = new ConcurrentDictionary<int, ConcurrentBag<string>>();
        foreach (var target in targetCounts)
        {
            foundItems.TryAdd(target.Key, new ConcurrentBag<string>());
        }

        return foundItems;
    }

    private static bool IsSearchComplete(Dictionary<int, int> targetCounts,
        ConcurrentDictionary<int, ConcurrentBag<string>> foundItems)
    {
        foreach (var target in targetCounts)
        {
            if (foundItems[target.Key].Count < target.Value) return false;
        }

        return true;
    }
}