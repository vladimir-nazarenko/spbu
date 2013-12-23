#include "linux.h"

Linux::Linux(RandomGenerator *generator)
{
	mGenerator = generator;
}

int Linux::reliability()
{
	return mGenerator->getNumber(0, 100);
}

QString Linux::name()
{
	return "Linux";
}
