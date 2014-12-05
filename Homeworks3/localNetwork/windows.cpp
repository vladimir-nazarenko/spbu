#include "windows.h"



Windows::Windows(RandomGenerator *generator)
{
	mGenerator = generator;
}

int Windows::reliability()
{
	return mGenerator->getNumber(0, 100);
}

QString Windows::name()
{
	return "Windows";
}
