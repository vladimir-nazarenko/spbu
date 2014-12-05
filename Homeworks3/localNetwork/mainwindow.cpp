#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent) :
	QMainWindow(parent),
	ui(new Ui::MainWindow),
	numberOfComputers(5)
{
	ui->setupUi(this);
	initComputers();
}

MainWindow::~MainWindow()
{
	delete ui;
}

void MainWindow::initComputers()
{
	Computer* prev;
	for(int i = 0; i < numberOfComputers; i++)
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
		QLabel *label = new QLabel(computer->boot()->name());
		label->move(50, i * 15 - 200);
		this->layout()->addWidget(label);
		labels.insert(computer, label);
	}
	computers.last()->connectTo(computers.first());
	computers.first()->connectTo(computers.last());
}

void MainWindow::on_pushButton_2_clicked()
{
	computers.first()->boot()->forceInfect(30);
	foreach (Computer *computer, computers) {
		if (computer->boot()->isInfected())
		{
			labels[computer]->setStyleSheet("QLabel { color : red;}");
		}
	}
}

void MainWindow::on_pushButton_clicked()
{
	foreach (Computer *computer, computers) {
		computer->startup();
	}
	foreach (Computer *computer, computers) {
		if (computer->boot()->isInfected())
		{
			labels[computer]->setStyleSheet("QLabel { color : red;}");
		}
	}
}
