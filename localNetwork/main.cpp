#include "mainwindow.h"
#include <QApplication>
#include "windows.h"
#include "fakegenerator.h"

int main(int argc, char *argv[])
{
	QApplication a(argc, argv);
	MainWindow w;
	w.show();
	return a.exec();
}
