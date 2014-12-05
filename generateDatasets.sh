#!/bin/sh

rm -rf unnoised-train-datasets
mkdir unnoised-train-datasets
python3 ConvertMNISTIntoARFF.py
mv *.arff unnoised-train-datasets/

