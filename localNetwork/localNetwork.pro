#-------------------------------------------------
#
# Project created by QtCreator 2013-12-22T19:37:15
#
#-------------------------------------------------

QT       += core gui

greaterThan(QT_MAJOR_VERSION, 4): QT += widgets

TARGET = localNetwork
TEMPLATE = app


SOURCES += main.cpp\
        mainwindow.cpp \
    computer.cpp \
    os.cpp \
    linux.cpp \
    freebsd.cpp \
    windows.cpp \
    randomgenerator.cpp \
    fakegenerator.cpp \
    authenticgenerator.cpp

HEADERS  += mainwindow.h \
    computer.h \
    os.h \
    linux.h \
    freebsd.h \
    windows.h \
    randomgenerator.h \
    fakegenerator.h \
    authenticgenerator.h

FORMS    += mainwindow.ui
