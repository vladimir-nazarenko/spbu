#include "fakegenerator.h"

FakeGenerator::FakeGenerator()
{
	for (int i = 10; i < 80; i += 10)
		numbers.enqueue(i);
}

FakeGenerator &FakeGenerator::instance()
{
	static FakeGenerator instance;
	return instance;
}

int FakeGenerator::getNumber(int low, int high)
{
	return nextNumber() % ((high + 1) - low) + low;
}

int FakeGenerator::nextNumber()
{
	int next = numbers.dequeue();
	numbers.enqueue(next);
	return next;
}
