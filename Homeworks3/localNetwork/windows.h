#ifndef WINDOWS_H
#define WINDOWS_H
#include "os.h"

class Windows : public OS
{
public:
	Windows(RandomGenerator* generator);
	virtual int reliability();
	virtual QString name();
private:

};

#endif // WINDOWS_H
