#include "computer.h"

Computer::Computer(OS *os):
	_os(os)
{
	connected.clear();
}

Computer::~Computer()
{
}

OS *Computer::boot()
{
	return _os;
}

void Computer::connectTo(Computer *&other)
{
	connected.append(other);
}

void Computer::startup()
{
	if (_os->isInfected())
	{
		if (_os->canInfect()) {
			foreach(Computer *computer, connected)
				computer->boot()->getRoot(_os->powerOfVirus());
		}
		else {
			_os->releaseVirus();
		}
	}
}
