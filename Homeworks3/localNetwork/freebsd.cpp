#include "freebsd.h"


FreeBSD::FreeBSD(RandomGenerator *generator)
{
	mGenerator = generator;
}

int FreeBSD::reliability()
{
	return mGenerator->getNumber(0, 100);
}

QString FreeBSD::name()
{
	return "FreeBSD";
}
