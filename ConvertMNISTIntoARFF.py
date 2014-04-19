# from doctest import _SpoofOut

__author__ = 'vladimir'
import struct
from ImageManipulator import ImageManipulator
from DatasetWrapper import DatasetWrapper
from ArffWriter import ArffWriter
import time
# import threading

from enum import Enum

# Set to -1 if you want to use constant from file
IMAGES_PATH = 'train-images-idx3-ubyte'
LABELS_PATH = 'train-labels-idx1-ubyte'
NUMBER_OF_IMAGES_TO_LOAD = 10000
NUMBER_OF_FILES_TO_GENERATE = 10
START_FROM_NOISE_RATE = 0.0
STEP_OF_CHANGING_NOISE_RATE = 0.05
IMAGE_OUTPUT_FILENAME = "digits.jpg"


# Provides interface to converting MNIST test dataset into arff
class MNISTConverter:
    # Choose either we use train set or test set
    file_types = Enum('file_type', 'train test')

    def __init__(self, images_path, labels_path):
        self.dataset = DatasetWrapper(images_path=images_path, labels_path=labels_path)
        self.manipulator = ImageManipulator()

    def __enter__(self):
        return self

    @staticmethod
    def check_number_of_rows(expected_number, number_of_rows):
        if expected_number != -1:
            if expected_number < number_of_rows:
                return expected_number
            else:
                print('NUMBER_OF_IMAGES_TO_LOAD constant is too big, using images count from file')
                return number_of_rows
        else:
            return number_of_rows

    def write_into_arff_with_constant_noise_rate(self, noise_rate, number_to_load, name='MNIST'):
        """Converts information from images and labels to arff"""
        assert 0 <= noise_rate <= 1.0
        full_name = name + '-Noise-%1.2f' % noise_rate
        writer = ArffWriter(full_name, full_name + '.arff',
                            n_cols=self.dataset.col_count, n_rows=self.dataset.row_count)
        number_of_images = self.dataset.image_count
        number_of_images = self.check_number_of_rows(number_to_load, number_of_images)
        print('Writing %s with %i instances\n' % (full_name, number_of_images))
        try:
            for i in range(number_of_images):
                pixels = self.dataset.read_one_image()
                pixels[:len(pixels) - 1] = self.manipulator.noise_pixels(pixels[:len(pixels) - 1], noise_rate)
                writer.write_next_row(pixels)
            print('OK\n')
        except IndexError:
            print('Error while reading from file\n')
        writer.close()

    def write_into_arff_with_unnoised_trainset(self, noise_rate, number_to_load, name='MNIST'):
        """Converts information from images and labels to arff"""
        assert 0 <= noise_rate <= 1.0
        full_name = name + '-.66Unnoised-Noise-%1.2f' % noise_rate
        writer = ArffWriter(full_name, full_name + '.arff',
                            n_cols=self.dataset.col_count, n_rows=self.dataset.row_count)
        number_of_images = self.dataset.image_count
        number_of_images = 3 * int(self.check_number_of_rows(number_to_load, number_of_images) / 3)
        print('Writing %s with %i instances\n' % (full_name, number_of_images))
        try:
            for i in range(2 * int(number_of_images / 3)):
                pixels = self.dataset.read_one_image()
                writer.write_next_row(pixels)
            for i in range(int(number_of_images / 3)):
                pixels = self.dataset.read_one_image()
                pixels[:len(pixels) - 1] = self.manipulator.noise_pixels(pixels[:len(pixels) - 1], noise_rate)
                writer.write_next_row(pixels)
            print('OK\n')
        except IndexError:
            print('Error while reading from file\n')
        writer.close()

    def __del__(self):
        self.dataset.close()


def main():
    start_time = time.time()
    for i in range(20):
        converter = MNISTConverter(images_path=IMAGES_PATH, labels_path=LABELS_PATH)
        converter.write_into_arff_with_unnoised_trainset(0.05 * i, 5000)
    print("Exceeded %1.2f seconds" % (time.time() - start_time))


if __name__ == '__main__':
    main()



