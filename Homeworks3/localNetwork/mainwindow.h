#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include <QLayout>
#include <QList>
#include <QLabel>
#include "computer.h"
#include "linux.h"
#include "windows.h"
#include "freebsd.h"
#include "authenticgenerator.h"
#include "fakegenerator.h"
#include <QMap>

namespace Ui {
class MainWindow;
}

class MainWindow : public QMainWindow
{
	Q_OBJECT

public:
	explicit MainWindow(QWidget *parent = 0);
	~MainWindow();

private slots:
	void on_pushButton_2_clicked();

	void on_pushButton_clicked();

private:
	void initComputers();
	Ui::MainWindow *ui;
	const int numberOfComputers;
	QList<Computer *> computers;
	QMap<Computer *, QLabel *> labels;
};

#endif // MAINWINDOW_H
