#!/usr/bin/env python

__author__ = 'vladimir'
import numpy as np
import cv2.cv as cv
import cv2
from tk import FilenameDialog
from os import rename, remove
from Tkinter import *


class Main:
    def __init__(self):
        self.cap = cv2.VideoCapture(0)
        cv2.namedWindow('Thresholded')
        cv2.createTrackbar('threshold', 'Thresholded', self._threshold, 255, self.set_threshold)
        # cv2.createTrackbar('max value', 'Thresholded', self._max_value, 255, self.set_maxvalue)
        # self.bgs = cv2.BackgroundSubtractorMOG2(10, 16, False)
        self.write_statistics = False

    _threshold = 30
    _max_value = 255
    output = None

    def capture(self):
        ret, img = self.cap.read()
        return img

    def thresh(self, image):
        gray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
        blur = cv2.GaussianBlur(gray, (5, 5), 0)
        ret, thresh = cv2.threshold(blur, self._threshold, self._max_value, cv2.THRESH_BINARY)
        # thresh = cv2.adaptiveThreshold(blur, self._max_value, cv2.ADAPTIVE_THRESH_GAUSSIAN_C, cv2.THRESH_BINARY_INV, 5, 1)
        return thresh

    @staticmethod
    def contours(image):
        contours, hierarchy = cv2.findContours(image, cv2.RETR_TREE, cv2.CHAIN_APPROX_TC89_KCOS)
        return contours

    @staticmethod
    def largest_contour(contours):
        max_area = 0
        cnt = None
        for i in contours:
            area = cv2.contourArea(i)
            if area > max_area:
                max_area = area
                cnt = i
        return cnt

    def draw_hull(self, img):
        pass

    def set_threshold(self, x):
        self._threshold = x

    def set_maxvalue(self, x):
        self._max_value = x

    def change_logging(self):
        if self.write_statistics:
            root = Tk()
            root.wm_title("Filename dialog")
            root.withdraw()
            save_dialog = FilenameDialog(root)
            root.wait_window(save_dialog.top)
            self.output.close()
            try:
                remove(save_dialog.result)
            except OSError:
                pass
            rename('tmp', save_dialog.result)
        else:
            self.output = open('tmp', 'w')
        self.write_statistics = not self.write_statistics

    @staticmethod
    def get_defects(cnt):
        hull = cv2.convexHull(cnt, returnPoints=False)
        return cv2.convexityDefects(cnt, hull)

    @staticmethod
    def draw_defects(cnt, defects, drawing):
        for i in range(defects.shape[0]):
            s, e, f, d = defects[i, 0]
            start = tuple(cnt[s][0])
            end = tuple(cnt[e][0])
            far = tuple(cnt[f][0])
            cv2.line(drawing, start, end, [0, 255, 0], 2)
            cv2.circle(drawing, far, 5, [0, 0, 255], 2)

    def log_defects(self, rect, cnt, defects):
        # center = ((rect[0] + rect[2]) / 2., (rect[1] + rect[3]) / 2.)
        for i in range(defects.shape[0]):
            s, e, f, d = defects[i, 0]
            far = tuple(cnt[f][0])
            x = (far[0] - rect[0])
            y = (far[1] - rect[1])
            norm_x = x / float(rect[2])
            norm_y = y / float(rect[3])
            self.output.write('{0},{1};'.format(norm_x, norm_y))
        self.output.write('\n')

    def get_result(self, img):
        drawing = np.zeros(img.shape, np.uint8)
        for_proc = self.thresh(img)
        cnt = self.largest_contour(self.contours(for_proc))
        if not cnt is None:
            hull = cv2.convexHull(cnt)
            # center, radius = cv2.minEnclosingCircle(cnt)
            r = cv2.boundingRect(cnt)
            cv2.rectangle(drawing, (r[0], r[1]), (r[0] + r[2], r[1] + r[3]), (255, 0, 255), 2)
            cv2.drawContours(drawing, [cnt], 0, (0, 255, 0), 2)
            cv2.drawContours(drawing, [hull], 0, (0, 0, 255), 2)
            # cv2.circle(drawing, (int(center[0]), int(center[1])), int(radius), [255, 0, 255], 4)
            defects = self.get_defects(cnt)
            self.draw_defects(cnt, defects, drawing)
            if self.write_statistics:
                self.log_defects(r, cnt, defects)
        return drawing

    def bring_the_action(self):
        while self.cap.isOpened():
            img = self.capture()
            # cv2.imshow('Camera input', img)
            cv2.imshow('Thresholded', self.thresh(img))
            cv2.imshow('Result', self.get_result(img))

            k = cv2.waitKey(10)
            if k == 27:
                break
            elif k == 32:
                self.change_logging()
        if not self.output is None and not self.output.closed:
            self.output.close()

performer = Main()
performer.bring_the_action()