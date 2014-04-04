__author__ = 'vladimir'
import struct
import arff
import random
import time
import threading
from wand.image import Image
from wand.display import display
from wand.color import Color
from wand.drawing import Drawing

# Set to -1 if you want to use constant from file
NUMBER_OF_IMAGES_TO_LOAD = -1
NUMBER_OF_FILES_TO_GENERATE = 1
START_FROM_NOISE_RATE = 0.0
STEP_OF_CHANGING_NOISE_RATE = 0.1


def convert_bytes_to_int32(data):
    """Gets bytes and converts them into integer"""
    return struct.unpack('i', data[::-1])[0]


def read_one_image(images_file, labels_file=None, number_of_rows=28, number_of_columns=28):
    """Read one image from file. Assumes headers were skipped. Returns list of bytes and byte with number type"""
    img = []
    for i in range(number_of_rows):
        for j in range(number_of_columns):
            img.append(images_file.read(1)[0])
    if labels_file is not None:
        label = str(labels_file.read(1)[0])
        img.append(label)
    return img

# TODO: make usable in thread
def visualize_number(number, number_of_rows=28, number_of_columns=28):
    """Show picture of number in grey scale. Gets list of bytes, defining pixels in grey scale"""
    def skip_extra_parts(hex_str):
        """Gets string, representing hex number and skips part 0x"""
        return hex_str[2:]
    img = Image()
    img = img.blank(number_of_columns, number_of_rows, background=Color('#ffffff'))
    draw = Drawing()
    assert number_of_columns * number_of_rows == len(number)
    for r in range(number_of_rows):
        for c in range(number_of_columns):
            grey_scale = number[r * number_of_columns + c]
            draw.fill_color = Color('#' + 3 * skip_extra_parts(hex(grey_scale)))
            draw.line((c, r), (c, r))
    draw(img)
    display(img)


def spoil_pixels(pixels, ratio, contains_label=False):
    assert 0 <= ratio <= 1.0
    """Gets list of image pixels and for pixels * ratio of them sets random ones to be white"""
    pix_len = len(pixels) if not contains_label else len(pixels) - 1
    positions = list(range(pix_len))
    random.shuffle(positions)
    positions = positions[0:int(pix_len * ratio)]
    for i in positions:
        pixels[i] = 255 * random.randint(0, 1)


def make_squared(pixels, number_of_rows=28, number_of_columns=28):
    """Get list of pixels of an image and convert them to list of lists, row wise"""
    num = []
    for i in range(number_of_rows):
        row = []
        for j in range(number_of_columns):
            row.append(pixels[i * number_of_columns + j])
        num.append(row)
    return num


def writeintoarff(images, labels, noise_rate, numberofimages, name='MNIST', numberofcolumns=28, numberofrows=28):
    assert 0 <= noise_rate <= 1.0
    """Converts information from images and labels to arff"""
    filename = '%s-NoiseRate%1.2f.arff' % (name, noise_rate)
    print('Writing %s' % filename)
    col_names = []
    for i in range(numberofcolumns):
        for j in range(numberofrows):
            col_names.append('x=%iy=%i' % (j, i))
    col_names.append('class')
    arff_writer = arff.Writer(filename, relation='MNISTNoise%i' % noise_rate, names=col_names)
    # set trigger: if we have str in column, interpret it as a class with such values
    arff_writer.pytypes[str] = set([str(i) for i in range(10)])
    # write features into file
    if NUMBER_OF_IMAGES_TO_LOAD != -1:
        if NUMBER_OF_IMAGES_TO_LOAD < numberofimages:
            numberofimages = NUMBER_OF_IMAGES_TO_LOAD
        else:
            print("NUMBER_OF_IMAGES_TO_LOAD constant is too big, using images count from file")
    try:
        for i in range(numberofimages):
            pixels = read_one_image(images, labels)
            spoil_pixels(pixels, noise_rate, contains_label=True)
            arff_writer.write(pixels)
        print('OK\n')
    except IndexError:
        print('Error while reading from file\n')
    arff_writer.close()

# TODO: make function that can visualize first n^2 images of file
def visualize_first_n_images(images_path, n):
    assert n < 50
    with open(images_path, 'rb') as images:
        # Read values from images file
        magicnumberimages = convert_bytes_to_int32(images.read(4))
        numberofimages = convert_bytes_to_int32(images.read(4))
        numberofrows = convert_bytes_to_int32(images.read(4))
        numberofcolumns = convert_bytes_to_int32(images.read(4))
        # print(numberofimages)
        # print(numberofrows)
        # print(numberofcolumns)
        resultimage = []
        for c in range(n):
            read_images = []
            for j in range(n):
                read_images.append(read_one_image(images))
            for i in range(28):
                for k in range(n):
                    resultimage += read_images[k][28 * i: 28 * i + 28]
        visualize_number(number=resultimage, number_of_rows=n * 28, number_of_columns=n * 28)
        # print(len(resultimage))
        # print(len(resultimage))

def process_dataset(images_path, labels_path, spoil_rate=0.0):
    assert 0 <= spoil_rate <= 1.0
    with open(images_path, 'rb') as images, open(labels_path, 'rb') as labels:
        # Read values from images file
        magicnumberimages = convert_bytes_to_int32(images.read(4))
        numberofimages = convert_bytes_to_int32(images.read(4))
        numberofrows = convert_bytes_to_int32(images.read(4))
        numberofcolumns = convert_bytes_to_int32(images.read(4))

        # read values from labels file
        magicnumberlabels = convert_bytes_to_int32(labels.read(4))
        numberoflabels = convert_bytes_to_int32(labels.read(4))
        assert numberofimages == numberoflabels
        for i in range(NUMBER_OF_FILES_TO_GENERATE):
            writeintoarff(images, labels, spoil_rate, numberofimages)
            spoil_rate += STEP_OF_CHANGING_NOISE_RATE


def main():
    train_images_path = 'train-images-idx3-ubyte'
    train_labels_path = 'train-labels-idx1-ubyte'
    test_images_path = 't10k-images-idx3-ubyte'
    test_labels_path = 't10k-labels-idx1-ubyte'
    # process_dataset(images_path=test_images_path, labels_path=test_labels_path, spoil_rate=START_FROM_NOISE_RATE)
    start_time = time.time()
    visualize_first_n_images(test_images_path, 10)
    print("Exceeded %1.2f seconds" % (time.time() - start_time))


if __name__ == '__main__':
    main()



