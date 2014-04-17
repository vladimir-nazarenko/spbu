# from doctest import _SpoofOut

__author__ = 'vladimir'
import struct
import arff
import random
import time
# import threading
from wand.image import Image
# from wand.display import display
from wand.color import Color
from wand.drawing import Drawing
from enum import Enum

# Set to -1 if you want to use constant from file
NUMBER_OF_IMAGES_TO_LOAD = 10000
NUMBER_OF_FILES_TO_GENERATE = 10
START_FROM_NOISE_RATE = 0.0
STEP_OF_CHANGING_NOISE_RATE = 0.05
IMAGE_OUTPUT_FILENAME = "digits.jpg"


class MNISTConverter:
    file_types = Enum('file_type', 'train test')

    def __init__(self, test_images_path, test_labels_path, train_images_path, train_labels_path):
        self.images = {'train': open(train_images_path, 'rb'),
                       'test': open(test_images_path, 'rb')}
        self.labels = {'train': open(train_labels_path, 'rb'),
                       'test': open(test_labels_path, 'rb')}
        for f in self.images.keys():
            self.images[f] = {'file': self.images[f],
                              'magicNumber': self.convert_bytes_to_int32(self.images[f].read(4)),
                              'numberOfImages': self.convert_bytes_to_int32(self.images[f].read(4)),
                              'rowCount': self.convert_bytes_to_int32(self.images[f].read(4)),
                              'columnCount': self.convert_bytes_to_int32(self.images[f].read(4))}
        for f in self.images.keys():
            self.labels[f] = {'file': self.labels[f],
                              'magicNumber': self.convert_bytes_to_int32(self.labels[f].read(4)),
                              'numberOfLabels': self.convert_bytes_to_int32(self.labels[f].read(4))}
        self.writer = None

    def __enter__(self):
        return self

    def init_arff_writer(self, file_type, name='MNIST'):
        """Set writer to appropriate position"""
        print('Writing %s' % name)
        col_names = []
        for i in range(self.images['train']['columnCount']):
            for j in range(self.images['train']['rowCount']):
                col_names.append('x=%iy=%i' % (j, i))
        col_names.append('class')
        arff_writer = arff.Writer(name + '.arff', relation=name, names=col_names)
        # set trigger: if we have str in column, interpret it as a class with such values
        arff_writer.pytypes[str] = set([str(i) for i in range(10)])
        self.writer = arff_writer

    @staticmethod
    def convert_bytes_to_int32(self, data):
        """Gets bytes and converts them into integer"""
        return struct.unpack('i', data[::-1])[0]

    def read_one_image(self):
        """Read one image from file. Assumes headers were skipped. Returns list of bytes and byte with number type.
            If labels_file is passed, adds label byte at the end"""
        img = []
        for i in range(self.images['train']['columnCount']):
            for j in range(self.images['train']['rowCount']):
                img.append(self.images['train']['file'].read(1)[0])
        return img

    def print_bytes(self, bytes):
    """Show picture of number in grey scale. Gets list of bytes, defining pixels in grey scale"""
    def skip_extra_parts(hex_str):
        """Gets string, representing hex number and skips part 0x"""
        return hex_str[2:]
    img = Image()
    nrows = self.images['train']['rowCount']
    ncols = self.images['train']['columnCount']
    img = img.blank(ncols, number_of_rows, background=Color('#ffffff'))
    draw = Drawing()
    assert number_of_columns * number_of_rows == len(number)
    for r in range(number_of_rows):
        for c in range(number_of_columns):
            grey_scale = number[r * number_of_columns + c]
            draw.fill_color = Color('#' + 3 * skip_extra_parts(hex(grey_scale)))
            draw.line((c, r), (c, r))
    draw(img)
    img.save(filename=IMAGE_OUTPUT_FILENAME)

    def __del__(self):
        for f in self.files.itervalues():
            f.close()








def noise_pixels(pixels, ratio, contains_label=False):
    assert 0.0 <= ratio <= 1.0
    if ratio == 0.0:
        return pixels
    """Gets list of image pixels and for pixels * ratio of them sets random ones to be white"""
    pix_len = len(pixels) if not contains_label else len(pixels) - 1
    positions = list(range(pix_len))
    random.shuffle(positions)
    positions = positions[0:int(pix_len * ratio)]
    for i in positions:
        pixels[i] = 255 * random.randint(0, 1)


def write_next_row(arff_writer, images, labels, noise_rate):
    pixels = read_one_image(images, labels)
    noise_pixels(pixels, noise_rate, contains_label=True)
    arff_writer.write(pixels)





def check_number_of_rows(number_of_rows):
    if NUMBER_OF_IMAGES_TO_LOAD != -1:
        if NUMBER_OF_IMAGES_TO_LOAD < number_of_rows:
            number_of_rows = NUMBER_OF_IMAGES_TO_LOAD
        else:
            print("NUMBER_OF_IMAGES_TO_LOAD constant is too big, using images count from file")
    return number_of_rows


def write_into_arff_with_constant_noise_rate(images, labels, noise_rate, number_of_images, name='MNIST', numberofcolumns=28, numberofrows=28):
    """Converts information from images and labels to arff"""
    assert 0 <= noise_rate <= 1.0
    number_of_images = check_number_of_rows(number_of_images)
    writer = init_arff_writer(noise_rate, name, numberofcolumns, numberofrows)
    try:
        for i in range(number_of_images):
            write_next_row(writer, images, labels, noise_rate)
        print('OK\n')
    except IndexError:
        print('Error while reading from file\n')
    writer.close()


def write_into_arff_with_unnoised_trainset(images, labels, noise_rate, number_of_images, name='MNIST', numberofcolumns=28, numberofrows=28):
    """Converts information from images and labels to arff"""
    assert 0 <= noise_rate <= 1.0
    number_of_images = 3 * int(check_number_of_rows(number_of_images) / 3)
    writer = init_arff_writer(noise_rate, name, numberofcolumns, numberofrows)
    try:
        for i in range(2 * int(number_of_images / 3)):
            write_next_row(writer, images, labels, 0)
        for i in range(int(number_of_images / 3)):
            write_next_row(writer, images, labels, noise_rate)
        print('OK\n')
    except IndexError:
        print('Error while reading from file\n')
    writer.close()


# TODO: return class with read values
def skip_head(images):
    images.read(16)


def visualize_first_n_images(images_path, n, noise_rate):
    assert n < 50
    with open(images_path, 'rb') as images:
        skip_head(images)
        resultimage = []
        for c in range(n):
            read_images = []
            for j in range(n):
                read_images.append(read_one_image(images))
                noise_pixels(read_images[j], noise_rate)
            for i in range(28):
                for k in range(n):
                    resultimage += read_images[k][28 * i: 28 * i + 28]
        print_bytes(number=resultimage, number_of_rows=n * 28, number_of_columns=n * 28)


def process_dataset(images_path, labels_path, noise_rate=0.0, mode="TrainWithNoNoise"):
    assert mode == "TrainWithNoNoise" or mode == "TrainWithNoise"
    assert 0 <= noise_rate <= 1.0
    for i in range(NUMBER_OF_FILES_TO_GENERATE):
        with open(images_path, 'rb') as images, open(labels_path, 'rb') as labels:
            # Read values from images file

            # read values from labels file
            magicnumberlabels = convert_bytes_to_int32(labels.read(4))
            numberoflabels = convert_bytes_to_int32(labels.read(4))
            assert numberofimages == numberoflabels
            if mode == "TrainWithNoNoise":
                write_into_arff_with_constant_noise_rate(images, labels, noise_rate, numberofimages)
            else:
                write_into_arff_with_unnoised_trainset(images, labels, noise_rate, numberofimages)
            noise_rate += STEP_OF_CHANGING_NOISE_RATE


def main():
    start_time = time.time()
    train_images_path = 'train-images-idx3-ubyte'
    train_labels_path = 'train-labels-idx1-ubyte'
    test_images_path = 't10k-images-idx3-ubyte'
    test_labels_path = 't10k-labels-idx1-ubyte'
    process_dataset(images_path=train_images_path, labels_path=train_labels_path, noise_rate=START_FROM_NOISE_RATE,
                    mode='TrainWithNoNoise')
    # visualize_first_n_images(test_images_path, 5, 0.1)
    print("Exceeded %1.2f seconds" % (time.time() - start_time))


if __name__ == '__main__':
    main()



