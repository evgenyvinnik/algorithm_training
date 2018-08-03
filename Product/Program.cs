using System;
using Xunit;
using Xunit.Sdk;

public class Solution
{

    public static int[] GetProductsOfAllIntsExceptAtIndex(int[] intArray)
    {
        if (intArray.Length <= 1)
        {
            throw new ArgumentException();
        }

        //var newArray = new int[intArray.Length];
        //for (int i = 0; i < newArray.Length; i++)
        //{
        //    newArray[i] = 1;
        //}

        //for (int i = 0; i < newArray.Length; i++)
        //{
        //    newArray[i] = 1;
        //}

        // Make an array with the products
        int product = 1;
        int zeroCounter = 0;
        int firstZero = -1;

        for (int i = 0; i < intArray.Length; i++)
        {
            int entry = intArray[i];
            if (entry != 0)
            {
                product *= entry;
            }
            else
            {
                zeroCounter++;
                if (firstZero == -1)
                {
                    firstZero = i;
                }
            }
        }

        var newArray = new int[intArray.Length];

        if (zeroCounter == 0)
        {
            for (int i = 0; i < intArray.Length; i++)
            {
                newArray[i] = product / intArray[i];
            }
        }
        else if (zeroCounter == 1)
        {
            newArray[firstZero] = product;
        }

        return newArray;
    }


















    // Tests

    [Fact]
    public void SmallArrayInputTest()
    {
        var expected = new int[] { 6, 3, 2 };
        var actual = GetProductsOfAllIntsExceptAtIndex(new int[] { 1, 2, 3 });
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void LongArrayInputTest()
    {
        var expected = new int[] { 120, 480, 240, 320, 960, 192 };
        var actual = GetProductsOfAllIntsExceptAtIndex(new int[] { 8, 2, 4, 3, 1, 5 });
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void InputHasOneZeroTest()
    {
        var expected = new int[] { 0, 0, 36, 0 };
        var actual = GetProductsOfAllIntsExceptAtIndex(new int[] { 6, 2, 0, 3 });
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void InputHasTwoZerosTest()
    {
        var expected = new int[] { 0, 0, 0, 0, 0 };
        var actual = GetProductsOfAllIntsExceptAtIndex(new int[] { 4, 0, 9, 1, 0 });
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void InputHasOneNegativeNumberTest()
    {
        var expected = new int[] { 32, -12, -24 };
        var actual = GetProductsOfAllIntsExceptAtIndex(new int[] { -3, 8, 4 });
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void AllNegativesInputTest()
    {
        var expected = new int[] { -8, -56, -14, -28 };
        var actual = GetProductsOfAllIntsExceptAtIndex(new int[] { -7, -1, -4, -2 });
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExceptionWithEmptyInputTest()
    {
        Assert.Throws<ArgumentException>(() => GetProductsOfAllIntsExceptAtIndex(new int[] { }));
    }

    [Fact]
    public void ExceptionWithOneNumberInputTest()
    {
        Assert.Throws<ArgumentException>(() => GetProductsOfAllIntsExceptAtIndex(new int[] { 1 }));
    }

    public static void Main(string[] args)
    {
        //TestRunner.RunTests(typeof(Solution));
    }
}