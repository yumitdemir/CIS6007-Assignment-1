namespace Task_1;

public class BubbleSortParallel
{
      internal static int[] GenerateRandomArray(int n)
    {
        Random rng = new Random();
        return Enumerable.Range(0, n).Select(x => rng.Next(1, 1000)).ToArray();
    }

    internal static int[] ParallelBubbleSort(int[] array, int threadSize)
    {
        int arrSize = array.Length;
        int segmentSize = arrSize / threadSize;

        
        int[][] subArrays = new int[threadSize][];
        for (int i = 0; i < threadSize; i++)
        {
            int startIdx = i * segmentSize;
            int endIdx = (i == threadSize - 1) ? arrSize : startIdx + segmentSize;

            subArrays[i] = new int[endIdx - startIdx];
            Array.Copy(array, startIdx, subArrays[i], 0, endIdx - startIdx);
        }

        
        
        
        Parallel.For(0, threadSize, i =>
        {
            BubbleSort(subArrays[i]);
        });

        
        return MergedArrays(subArrays);
    }

    internal static void BubbleSort(int[] segment)
    {
        int temp;
        for (int i = 0; i < segment.Length - 1; i++)
        {
            for (int j = 0; j < segment.Length - i - 1; j++)
            {
                if (segment[j] > segment[j + 1])
                {
                    temp = segment[j];
                    segment[j] = segment[j + 1];
                    segment[j + 1] = temp;
                }
            }
        }
    }

    internal static int[] MergedArrays(int[][] segments)
    {
        int totalSize = segments.Sum(arr => arr.Length);
        int[] mergedSegment = new int[totalSize];
        int[] pointers = new int[segments.Length];

        for (int i = 0; i < totalSize; i++)
        {
            int minVal = int.MaxValue;
            int minIdx = -1;

            for (int j = 0; j < segments.Length; j++)
            {
                if (pointers[j] < segments[j].Length && segments[j][pointers[j]] < minVal)
                {
                    minVal = segments[j][pointers[j]];
                    minIdx = j;
                }
            }

            mergedSegment[i] = minVal;
            pointers[minIdx]++;
        }

        return mergedSegment;
    }
}