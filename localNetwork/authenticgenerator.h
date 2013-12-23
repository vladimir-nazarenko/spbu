#ifndef AUTHENTICGENERATOR_H
#define AUTHENTICGENERATOR_H
#include <QTime>
#include "randomgenerator.h"

class AuthenticGenerator : public RandomGenerator
{
public:
	static AuthenticGenerator &instance();
	virtual int getNumber(int low, int high);
private:
	AuthenticGenerator();
};

#endif // AUTHENTICGENERATOR_H
