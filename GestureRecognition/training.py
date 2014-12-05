__author__ = 'vladimir'
import numpy as np
import cv2.cv as cv
import cv2
import csv
import numpy as np
from threading import Thread


class Classifier(Thread):
    def __init__(self):
        super(Classifier, self).__init__()
        self.daemon = True
        self.cancelled = False
        self.classifier = cv2.NormalBayesClassifier()
        self.trained = False

    def run(self):
        self.train()

    def train(self):
        samples = []
        responses = []
        with open('trainSet.csv', 'r') as csvfile:
            csvfile.readline()
            reader = csv.reader(csvfile, delimiter=',')
            for row in reader:
                responses.append(int(row[-1]))
                samples.append([float(x) for x in row[:-1]])
            samples = np.array(samples, dtype=np.float32)
        self.classifier.train(np.matrix(samples).astype('float32'), np.asarray(responses))
        self.trained = True

    def cancel(self):
        self.cancelled = True

    def update(self):
        pass

    def predict(self, feature_vector):
        feature_vector = np.array(feature_vector, dtype=np.float32)
        _, results = self.classifier.predict(np.matrix(feature_vector))
        return int(results[0][0])

    def store(self):
        pass
