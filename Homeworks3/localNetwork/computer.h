#ifndef COMPUTER_H
#define COMPUTER_H
#include "os.h"
#include <QList>
#include <QObject>

class Computer: public QObject
{
public:
	Computer(OS* os);
	~Computer();
	OS *boot();
	void connectTo(Computer *&other);
private:
	QList<Computer *> connected;
	OS *_os;
public slots:
	void startup();
};

#endif // COMPUTER_H
