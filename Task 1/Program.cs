using System.Diagnostics;
using Task_1;

int arraySize = 100000;
int[] threadNumbers = new int[] { 2, 3, 4, 6 };
int[] array = BubbleSortParallel.GenerateRandomArray(arraySize);

foreach (var threadCount in threadNumbers)
{
    int[] arr = new int[arraySize];
    Array.Copy(array, arr, arraySize);

    Stopwatch time = new Stopwatch();
    time.Start();
    BubbleSortParallel.ParallelBubbleSort(arr, threadCount);
    time.Stop();

    Console.WriteLine($" {time.Elapsed.TotalMilliseconds}ms - {threadCount} ");
}