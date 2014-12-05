#-------------------------------------------------
#
# Project created by QtCreator 2013-12-23T04:13:42
#
#-------------------------------------------------

QT       += testlib

QT       += gui

TARGET = tst_localnetworktest
CONFIG   += console
CONFIG   -= app_bundle

TEMPLATE = app


SOURCES += tst_localnetworktest.cpp
DEFINES += SRCDIR=\\\"$$PWD/\\\"
SOURCES += ../computer.cpp ../linux.cpp ../windows.cpp ../freebsd.cpp ../fakegenerator.cpp ../os.cpp
