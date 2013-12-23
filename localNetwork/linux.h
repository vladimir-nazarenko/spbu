#ifndef LINUX_H
#define LINUX_H
#include "os.h"

class Linux : public OS
{
public:
	Linux(RandomGenerator *generator);
	virtual int reliability();
	virtual QString name();
private:

};

#endif // LINUX_H
