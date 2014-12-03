#!/usr/bin/env python

__author__ = 'vladimir'
import numpy as np
import cv2.cv as cv
import cv2


class Main:
    def __init__(self):
        self.cap = cv2.VideoCapture(0)


    _threshold = 30
    _max_value = 255

    def capture(self):
        ret, img = self.cap.read()
        return img

    def thresh(self, image):
        gray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
        blur = cv2.GaussianBlur(gray, (5, 5), 10)
        ret, thresh = cv2.threshold(blur, self._threshold, self._max_value, cv2.THRESH_BINARY)
        return thresh

    @staticmethod
    def contours(image):
        contours, hierarchy = cv2.findContours(image, cv2.RETR_TREE, cv2.CHAIN_APPROX_TC89_KCOS)
        return contours

    def largest_contour(self, contours):
        max_area = 0
        cnt = None
        for i in contours:
            area = cv2.contourArea(i)
            if area > max_area:
                max_area = area
                cnt = i
        return cnt



    def set_threshold(self, x):
        self._threshold = x

    def set_maxvalue(self, x):
        self._max_value = x

    def bring_the_action(self):
        while self.cap.isOpened():
            img = self.capture()
            cv2.imshow('Camera input', img)
            cv2.imshow('Thresholded', self.thresh(img))
            drawing = np.zeros(img.shape, np.uint8)
            cnt = self.largest_contour(self.contours(self.thresh(img)))
            if cnt is not None:
                hull = cv2.convexHull(cnt)
                cv2.drawContours(drawing, [cnt], 0, (0, 255, 0), 2)
                cv2.drawContours(drawing, [hull], 0, (0, 0, 255), 2)
                # cv2.drawContours(img, [cnt], 0, (0, 255, 0), 2)
                # cv2.drawContours(img, [hull], 0, (0, 0, 255), 2)
                center, radius = cv2.minEnclosingCircle(cnt)
                cv2.circle(drawing, (int(center[0]), int(center[1])), int(radius), [255, 0, 255], 4)
                try:
                    hull = cv2.convexHull(cnt, returnPoints=False)
                    defects = cv2.convexityDefects(cnt, hull)
                    for i in range(defects.shape[0]):
                        s, e, f, d = defects[i, 0]
                        start = tuple(cnt[s][0])
                        end = tuple(cnt[e][0])
                        far = tuple(cnt[f][0])
                        #dist = cv2.pointPolygonTest(cnt, centr, True)
                        cv2.line(drawing, start, end, [0, 255, 0], 2)
                        cv2.circle(drawing, far, 5, [0, 0, 255], -1)

                except:
                    pass
            # combined = np.hstack((thresh1, drawing))

            #cv2.imshow('contours', thresh)
            cv2.imshow('result', drawing)
            k = cv2.waitKey(10)
            if k == 27:
                break

performer = Main()
performer.bring_the_action()