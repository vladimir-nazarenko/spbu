#!/bin/sh

rm -rf datasets
mkdir datasets
python3 ConvertMNISTIntoARFF.py
mv *.arff datasets/

