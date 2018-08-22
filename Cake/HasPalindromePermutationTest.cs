using System;
using System.Collections.Generic;
using Xunit;

public class HasPalindromePermutationTest
{
    public static bool HasPalindromePermutation(string theString)
    {
        // Check if any permutation of the input is a palindrome
        Dictionary<char, int> count = new Dictionary<char, int>();

        foreach (char c in theString)
        {
            if (count.ContainsKey(c))
            {
                count[c]++;
            }
            else
            {
                count.Add(c, 1);
            }
        }

        int oddCount = 0;
        foreach (var c in count)
        {
            if (c.Value % 2 == 1)
            {
                oddCount++;
            }
        }

        if (oddCount >= 2)
            return false;

        return true;
    }


















    // Tests

    [Fact]
    public void PermutationWithOddNumberOfCharsTest()
    {
        var result = HasPalindromePermutation("aabcbcd");
        Assert.True(result);
    }

    [Fact]
    public void PermutationWithEvenNumberOfCharsTest()
    {
        var result = HasPalindromePermutation("aabccbdd");
        Assert.True(result);
    }

    [Fact]
    public void NoPermutationWithOddNumberOfChars()
    {
        var result = HasPalindromePermutation("aabcd");
        Assert.False(result);
    }

    [Fact]
    public void NoPermutationWithEvenNumberOfCharsTest()
    {
        var result = HasPalindromePermutation("aabbcd");
        Assert.False(result);
    }

    [Fact]
    public void EmptyStringTest()
    {
        var result = HasPalindromePermutation("");
        Assert.True(result);
    }

    [Fact]
    public void OneCharacterStringTest()
    {
        var result = HasPalindromePermutation("a");
        Assert.True(result);
    }

    public static void Main(string[] args)
    {
        //TestRunner.RunTests(typeof(Solution));
    }
}