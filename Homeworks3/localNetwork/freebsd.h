#ifndef FREEBSD_H
#define FREEBSD_H
#include "os.h"

class FreeBSD : public OS
{
public:
	FreeBSD(RandomGenerator *generator);
	virtual int reliability();
	virtual QString name();
private:

};

#endif // FREEBSD_H
