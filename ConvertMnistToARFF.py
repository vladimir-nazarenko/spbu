__author__ = 'vladimir'
import struct
from wand.image import *
from wand.display import display
from wand.color import *
from wand.drawing import *
import arff
import random


def convert_bytes_to_int32(data):
    return struct.unpack('i', data[::-1])[0]


# ensure you skipped headers of files
def read_one_image(images_file, labels_file):
    label = labels_file.read(1)[0]
    img = [images_file.read(1)[0] for i in range(numberOfRows) for j in range(numberOfColumns)]
    return img + [str(label)]


def visualize_number(number):
    img = Image()
    img = img.blank(len(number), len(number[0]), background=Color('#ffffff'))
    draw = Drawing()
    for r in range(len(number)):
        for c in range(len(number[0])):
            grey_scale = number[r][c]
            draw.fill_color = Color('#' + 3 * hex(grey_scale)[2:])
            draw.line((c, r), (c, r))
    draw(img)
    display(img)


# assumed ratio from 0.0 to 1.0
def spoil_pixels(pixies, ratio):
    positions = list(range(len(pixies)))
    random.shuffle(positions)
    positions = positions[0:int(len(pixies) * ratio)]
    for i in positions:
        pixies[i] = 255
    return pixies


def make_squared(pixels):
    num = []
    for i in range(numberOfRows):
        row = []
        for j in range(numberOfColumns):
            row.append(pixels[i * numberOfColumns + j])
        num.append(row)
    return num

# initialize files
trainImagesPath = 'train-images-idx3-ubyte'
trainLabelsPath = 'train-labels-idx1-ubyte'
trainImages = open(trainImagesPath, 'rb')
trainLabels = open(trainLabelsPath, 'rb')

# read values from images file
magicNumberImages = convert_bytes_to_int32(trainImages.read(4))
numberOfImages = convert_bytes_to_int32(trainImages.read(4))
numberOfRows = convert_bytes_to_int32(trainImages.read(4))
numberOfColumns = convert_bytes_to_int32(trainImages.read(4))

# read values from labels file
magicNumberLabels = convert_bytes_to_int32(trainLabels.read(4))
numberOfLabels = convert_bytes_to_int32(trainLabels.read(4))
assert numberOfImages == numberOfLabels

# init arff file to write into
colNames = ['x=' + str(i) + 'y=' + str(j) for i in range(28) for j in range(28)] + ['class']
arffWriter = arff.Writer('MNISTDB.arff', relation='MNIST_Database', names=colNames)
# set trigger: if we have str in column, interpret it as a class with such values
arffWriter.pytypes[str] = set([str(i) for i in range(10)])

# write features into file
#for i in range(2000):
#    arffWriter.write(read_one_image(trainImages, trainLabels))

pixels = read_one_image(trainImages, trainLabels)
pixels = spoil_pixels(pixels, 0.5)
num = make_squared(pixels)
print(num)
visualize_number(num)

trainLabels.close()
trainImages.close()
arffWriter.close()