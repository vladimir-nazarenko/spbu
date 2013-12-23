#ifndef OS_H
#define OS_H
#include "randomgenerator.h"
#include <QDebug>

class OS
{
public:
	//OS(RandomGenerator* generator);
	virtual ~OS();
	virtual bool getRoot(int powerOfAttack);
	virtual int reliability() = 0;
	virtual QString name() = 0;
	int powerOfVirus();
	bool isInfected();
	void forceInfect(int powerOfAttack);
protected:
	OS();
	RandomGenerator* mGenerator;
	bool mInfected;
	int mPowerOfVirus;
};

#endif // OS_H
