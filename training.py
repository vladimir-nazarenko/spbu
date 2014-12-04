__author__ = 'vladimir'
import numpy as np
import cv2.cv as cv
import cv2
import csv
import numpy as np


class Training:
    def __init__(self):
        self.classifier = cv2.SVM()

    def train(self):
        samples = []
        responses = []
        with open('set.csv', 'r') as csvfile:
            csvfile.readline()
            reader = csv.reader(csvfile, delimiter=',')
            for row in reader:
                responses.append(int(row[-1]))
                samples.append([float(x) for x in row[:-1]])
        self.classifier.train(np.asfarray(samples, dtype=np.float32), np.asarray(responses))

    def store(self):
        self.classifier.save('classifier.xml')

t = Training()
t.train()
t.store()