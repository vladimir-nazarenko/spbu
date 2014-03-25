__author__ = 'vladimir'
import struct
import arff
import random
import time
from wand.image import Image
from wand.display import display
from wand.color import Color
from wand.drawing import Drawing


NUMBER_OF_IMAGES_TO_LOAD = 10000
NUMBER_OF_EXPERIMENTS = 3
STEP = 0.1


def convert_bytes_to_int32(data):
    """Gets bytes and converts them into integer"""
    return struct.unpack('i', data[::-1])[0]


def read_one_image(images_file, labels_file, number_of_rows=28, number_of_columns=28):
    """Read one image from file. Assumes headers were skipped. Returns list of bytes and byte with number type"""
    label = str(labels_file.read(1)[0])
    img = []
    for i in range(number_of_rows):
        for j in range(number_of_columns):
            img.append(images_file.read(1)[0])
    img.append(label)
    return img


def visualize_number(number, number_of_rows=28, number_of_columns=28):
    """Show picture of number in grey scale. Gets list of bytes, defining pixels in grey scale"""
    def skip_extra_parts(hex_str):
        """Gets string, representing hex number and skips part 0x"""
        return hex_str[2:]
    img = Image()
    img = img.blank(number_of_rows, number_of_columns, background=Color('#ffffff'))
    draw = Drawing()
    for r in range(number_of_rows):
        for c in range(number_of_columns):
            grey_scale = number[r * number_of_columns + c]
            draw.fill_color = Color('#' + 3 * skip_extra_parts(hex(grey_scale)))
            draw.line((c, r), (c, r))
    draw(img)
    display(img)


def spoil_pixels(pixels, ratio, contains_label=False):
    """Gets list of image pixels and for pixels * ratio of them sets random ones to be white"""
    pix_len = len(pixels) if not contains_label else len(pixels) - 1
    positions = list(range(pix_len))
    random.shuffle(positions)
    positions = positions[0:int(pix_len * ratio)]
    for i in positions:
        pixels[i] = 255


def make_squared(pixels, number_of_rows=28, number_of_columns=28):
    """Get list of pixels of an image and convert them to list of lists, row wise"""
    num = []
    for i in range(number_of_rows):
        row = []
        for j in range(number_of_columns):
            row.append(pixels[i * number_of_columns + j])
        num.append(row)
    return num


def writeintoarff(images, labels, noise_rate, name='MNIST', numberofcolumns=28, numberofrows=28):
    print(noise_rate)
    filename = '%s%iNoiseRate%f.arff' % (name, NUMBER_OF_IMAGES_TO_LOAD, noise_rate)
    col_names = []
    for i in range(numberofcolumns):
        for j in range(numberofrows):
            col_names.append('x=%iy=%i' % (j, i))
    col_names.append('class')
    arff_writer = arff.Writer(filename, relation='MNISTNoise%i' % noise_rate, names=col_names)
    # set trigger: if we have str in column, interpret it as a class with such values
    arff_writer.pytypes[str] = set([str(i) for i in range(10)])
    # write features into file
    for i in range(NUMBER_OF_IMAGES_TO_LOAD):
        pixels = read_one_image(images, labels)
        spoil_pixels(pixels, noise_rate, contains_label=True)
        arff_writer.write(pixels)
    arff_writer.close()


def main():
    train_images_path = 'train-images-idx3-ubyte'
    train_labels_path = 'train-labels-idx1-ubyte'
    with open(train_images_path, 'rb') as train_images, open(train_labels_path, 'rb') as train_labels:
        # Read values from images file
        magicnumberimages = convert_bytes_to_int32(train_images.read(4))
        numberofimages = convert_bytes_to_int32(train_images.read(4))
        numberofrows = convert_bytes_to_int32(train_images.read(4))
        numberofcolumns = convert_bytes_to_int32(train_images.read(4))

        # read values from labels file
        magicnumberlabels = convert_bytes_to_int32(train_labels.read(4))
        numberoflabels = convert_bytes_to_int32(train_labels.read(4))
        assert numberofimages == numberoflabels
        rate = 0.0
        for i in range(NUMBER_OF_EXPERIMENTS):
            writeintoarff(train_images, train_labels, rate)
            rate += STEP


if __name__ == '__main__':
    main()



