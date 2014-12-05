#include "authenticgenerator.h"

AuthenticGenerator::AuthenticGenerator()
{
	QTime time = QTime::currentTime();
	qsrand((uint)time.msec());
}

AuthenticGenerator &AuthenticGenerator::instance()
{
	static AuthenticGenerator instance;
	return instance;
}

int AuthenticGenerator::getNumber(int low, int high)
{
	return qrand() % ((high + 1) - low) + low;
}
