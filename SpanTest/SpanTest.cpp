// SpanTest.cpp : This file contains the 'main' function. Program execution begins and ends there.
//
#include "pch.h"

#define _AMD64_ 1

#include <iostream>
#include <nt.h>
#include <vector>
#include <gsl/gsl>

//
// Collects items of binary data into a buffer, then dispatches the buffer to a provided callback
// each time the buffer is full. There is also another callback which is invoked for each item
// added to the buffer.
//
class BatchedBinaryDataDispatcher
{
public:

    typedef void(*singleItemCallback_t)(gsl::span<gsl::byte> buffer);
    typedef void(*batchCallback_t)(gsl::span<gsl::byte> buffer, size_t itemCount);

    BatchedBinaryDataDispatcher(size_t bufferSize, singleItemCallback_t singleItemCallback, batchCallback_t batchCallback)
        : singleItemCallback(singleItemCallback), batchCallback(batchCallback),
        batchedDataBuffer(bufferSize), currentInsertionPoint(std::begin(batchedDataBuffer)) {}

    //
    // Submits an item to the dispatcher. If the item is not larger than the maximum buffer size,
    // the single item callback is invoked, then the item is added to the buffer (flushing the
    // buffer first, if it lacks free space to hold the new item).
    //
    void PostItem(gsl::span<gsl::byte> item)
    {
        if (item.size() <= static_cast<ptrdiff_t>(batchedDataBuffer.size()))
        {
            singleItemCallback(item);

            if (BufferSizeUsed() + static_cast<size_t>(item.size()) > batchedDataBuffer.size())
            {
                Flush();
            }

            currentItemCount += 1;
            currentInsertionPoint = std::copy(
                std::begin(item),
                std::end(item),
                currentInsertionPoint);
        }
    }

    //
    // Flushes the buffer. This calls the batch callback, then clears the batch buffer.
    //
    void Flush()
    {
        batchCallback(gsl::span<gsl::byte>{ batchedDataBuffer.data(), BufferSizeUsed() }, currentItemCount);

        currentInsertionPoint = std::begin(batchedDataBuffer);
        currentItemCount = 0;
    }

private:

    gsl::span<gsl::byte, -1>::index_type BufferSizeUsed()
    {
        return std::distance(std::begin(batchedDataBuffer), currentInsertionPoint);
    }

    std::vector<gsl::byte> batchedDataBuffer;
    std::vector<gsl::byte>::iterator currentInsertionPoint;
    size_t currentItemCount = 0;

    singleItemCallback_t singleItemCallback;
    batchCallback_t batchCallback;
};

int main()
{
    std::cout << "Hello World!\n"; 
}
