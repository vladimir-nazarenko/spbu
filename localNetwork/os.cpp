#include "os.h"

//OS::OS(RandomGenerator* generator):
//	generator(generator)
//{

//}

OS::~OS()
{
	//delete mGenerator;
}

bool OS::getRoot(int powerOfAttack)
{
	if (powerOfAttack > reliability())
	{
		mInfected = true;
		mPowerOfVirus = powerOfAttack;
	}
	return mInfected;
}

int OS::powerOfVirus()
{
	if (mPowerOfVirus == 0)
		qDebug() << "error in infection";
	return mPowerOfVirus;
}

bool OS::isInfected()
{
	return mInfected;
}

void OS::forceInfect(int powerOfAttack)
{
	mInfected = true;
	mPowerOfVirus = powerOfAttack;
}

OS::OS()
{
	mInfected = false;
	mPowerOfVirus = 0;
}
