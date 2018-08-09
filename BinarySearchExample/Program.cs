using System;
using System.Collections.Generic;

/*
[1,3,5,7,9], 3 => 1
[1,3,5,7,9], 7 => 3
[1,3,5,7,9], 0 => 0
[1,3,5,7,9], 4 => 2
[1,3,5,7,9], 10 => 5
*/

/*
[7,9,1,3,5], 3 => 3
[3,5,7,9,1], 3 => 0
[1,3,5,7,9], 3 => 1
[3,5,7,9,1], 0 => -1
*/

class Solution
{
    static void Main(string[] args)
    {
        List<int> array = new List<int>() { 1, 3, 5, 7, 9 };

        // Console.WriteLine($"{IndexApproximation(array, 1)}");
        // Console.WriteLine($"{IndexApproximation(array, 2)}");
        /*Console.WriteLine($"{IndexApproximation(array, 3)}");
        
        Console.WriteLine($"{IndexApproximation(array, 7)}");
        Console.WriteLine($"{IndexApproximation(array, 0)}");
        Console.WriteLine($"{IndexApproximation(array, 2)}");
        Console.WriteLine($"{IndexApproximation(array, 8)}");
        Console.WriteLine($"{IndexApproximation(array, 10)}");*/

        List<int> array1 = new List<int>() { 7, 9, 1, 3, 5 };
        Console.WriteLine($"{IndexSearch(array1, 3)}");
    }

    static int IndexSearch(List<int> array, int searchElement)
    {
        /*int shift = 0;
        for(int i = 0; i < array.Count-1; i++) 
        {
            if(array[i+1] < array[i])
            {
                shift = i;
                break;
            }
        }*/

        int pivot = BinarySearchPivot(array, 0 array.Count - 1);

        if (array[pivot] > searchElement)

            return BinarySearch(array, searchElement, 0, pivot);
        else
            return BinarySearch(array, searchElement, pivot, array.Count - 1);     // O (log M) (remainder of the element worst case M == N   
    }

    //O(logN + logM)

    static int BinarySearchPivot(List<int> array, int lowerIndex, int upperIndex) // O(log N)
    {
        int middleElementIndex = (upperIndex + lowerIndex) / 2;

        if (array[middleElementIndex] > array[lowerIndex])
        {
            return BinarySearchPivot(array, middleElementIndex, upperIndex);
        }
        else if (array[middleElementIndex] < array[lowerIndex])
        {
            return BinarySearchPivot(array, lowerIndex, middleElementIndex);
        }
    }

    static int IndexApproximation(List<int> array, int searchElement)
    {
        return BinarySearch(array, searchElement, 0, array.Count - 1);
    }

    static int BinarySearch(List<int> array, int searchElement, int lowerIndex, int upperIndex)
    {



        int middleElementIndex = (upperIndex + lowerIndex) / 2; // constant space

        //Console.WriteLine($"{lowerIndex} {middleElementIndex} {upperIndex}");

        if (lowerIndex == upperIndex)
            return lowerIndex;


        if (lowerIndex == middleElementIndex)
        {
            if (searchElement <= array[lowerIndex])
                return lowerIndex;
            else if (searchElement <= array[upperIndex])
                return upperIndex;
            else
                return upperIndex + 1;
        }


        if (searchElement == array[middleElementIndex])
        {
            return middleElementIndex;
        }
        else if (searchElement < array[middleElementIndex])
        {
            return BinarySearch(array, searchElement, lowerIndex, middleElementIndex); // call stack O(log n) for storing function calls
        }
        else
        {
            return BinarySearch(array, searchElement, middleElementIndex, upperIndex);
        }
    }
}

