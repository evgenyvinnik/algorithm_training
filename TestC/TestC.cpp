// TestC.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <set>
#include <vector>

using namespace std;
class TwoSum {
public:

    vector<int> numbers;
	set<int> sums;

    /** Initialize your data structure here. */
    TwoSum() {

    }

    /** Add the number to an internal data structure.. */
    void add(int number) {
		for (int n : numbers)
		{
			sums.insert(number + n);
		}

		numbers.push_back(number);
    }

    /** Find if there exists any pair of numbers which sum is equal to the value. */
    bool find(int value) {
		auto it = sums.find(value);
		if (it == sums.end())
			return false;
		return true;
    }
};

int main()
{
    return 0;
}

