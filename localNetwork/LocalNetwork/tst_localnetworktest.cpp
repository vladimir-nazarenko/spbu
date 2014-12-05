#include <QString>
#include <QtTest>
#include <QList>
#include "../computer.h"
#include "../linux.h"
#include "../windows.h"
#include "../freebsd.h"
#include "../fakegenerator.h"

class LocalNetworkTest : public QObject
{
	Q_OBJECT

public:
	LocalNetworkTest();
private:
	QList<Computer *> computers;
	void makeTurn();
	int countInfected();

private Q_SLOTS:
	void testInfection();
};

LocalNetworkTest::LocalNetworkTest()
{
	Computer* prev;
	for(int i = 0; i < 5; i++)
	{
		Computer* computer;
		if (i % 3 == 0) {
			computer = new Computer(new Linux(&FakeGenerator::instance()));
		} else {
			if (i % 3 == 1) {
				computer = new Computer(new Windows(&FakeGenerator::instance()));
			} else {
				computer = new Computer(new FreeBSD(&FakeGenerator::instance()));
			}
		}
		if (i != 0) {
			prev->connectTo(computer);
			computer->connectTo(prev);
		}
		prev = computer;
		computers.append(computer);
	}
	computers.last()->connectTo(computers.first());
	computers.first()->connectTo(computers.last());
	computers.first()->boot()->forceInfect(30);
}

void LocalNetworkTest::makeTurn()
{
	foreach (Computer *computer, computers) {
		computer->startup();
	}
}

int LocalNetworkTest::countInfected()
{
	int count = 0;
	foreach (Computer *computer, computers) {
		if (computer->boot()->isInfected())
			count++;
	}
	return count;
}

void LocalNetworkTest::testInfection()
{
	QVERIFY2(countInfected() == 1, "error");
	makeTurn();
	QVERIFY2(countInfected() == 1, "error");
	makeTurn();
	QVERIFY2(countInfected() == 3, "error");
	makeTurn();
	QVERIFY2(countInfected() == 5, "error");
}

QTEST_APPLESS_MAIN(LocalNetworkTest)

#include "tst_localnetworktest.moc"
