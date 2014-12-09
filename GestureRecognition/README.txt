Main.py contains the main code of the project. Requires open cv to be installed.
Run python Main.py to start the application.
Press Esc to exit, space to start collecting data/stop collecting and write data, 'p' to save thresholded image into file.

training.py contains the code of the decorator for bayes classifier from opencv.

Tk.py contains the code of simple supplementary window.

csvMaker.rb contains supplementary code for converting the data recieved from Main.py to csv file, each row of which contains a feature vector with label. Columns of the feature vector are distances to the center of the 1 of 100 bounding cell in the normal(1 by 1) square if there is a point in this square, 100 otherwise.
Usage: ruby csvMaker.rb [h] n file
n -- label for class
[h] -- if present, script also writes header to the output

processfiles.sh contains an example of the script for generating a dataset from gathered data

trainSet.csv contains of the gestures

gestures.csv contains description of the gestures supported in the trainSet dataset