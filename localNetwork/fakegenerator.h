#ifndef FAKEGENERATOR_H
#define FAKEGENERATOR_H
#include <QTime>
#include <QQueue>
#include "randomgenerator.h"

class FakeGenerator : public RandomGenerator
{
public:
	static FakeGenerator &instance();
	// 20 30 40 50 20 30 40 50 20 ...
	virtual int getNumber(int low, int high);
private:
	FakeGenerator();
	int nextNumber();
	QQueue<int> numbers;
};

#endif // FAKEGENERATOR_H
