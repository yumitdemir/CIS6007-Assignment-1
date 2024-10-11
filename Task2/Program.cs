using System.Diagnostics;
using Task2;


List<Item> inventoryList = Inventory.GenerateInventory(100500);

Dictionary<int, int> searchTargets = new Dictionary<int, int>
{
    { 1, 30 },
    { 7, 15 },
    { 10, 8 },
};

int[] threadNumbers = new int[] { 2, 3, 4, 6 };

foreach (int threadCount in threadNumbers)
{
    ParallelSearch.SearchInventory(inventoryList, searchTargets, threadCount);

    Stopwatch stopwatch = new Stopwatch();
    double totalElapsedMilliseconds = 0;

    for (int iteration = 0; iteration < 5; iteration++)
    {
        stopwatch.Restart();
        ParallelSearch.SearchInventory(inventoryList, searchTargets, threadCount);
        stopwatch.Stop();
        totalElapsedMilliseconds += stopwatch.Elapsed.TotalMilliseconds;
    }

    double averageElapsedTime = totalElapsedMilliseconds / 5;

    Console.WriteLine($"{threadCount} Threads - {averageElapsedTime} ms");
}