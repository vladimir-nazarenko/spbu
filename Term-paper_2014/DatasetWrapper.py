__author__ = 'vladimir'
import struct


class DatasetWrapper:
    def __init__(self, images_path, labels_path):
        """Check out http://yann.lecun.com/exdb/mnist/ for files description"""
        # open files and read head values
        self.images = open(images_path, 'rb')
        self.labels = open(labels_path, 'rb')
        # skip magic numbers
        self.images.read(4)
        self.labels.read(4)
        im_count = self.convert_bytes_to_int32(self.images.read(4))
        la_count = self.convert_bytes_to_int32(self.labels.read(4))
        assert im_count == la_count
        self.image_count = im_count
        self.row_count = self.convert_bytes_to_int32(self.images.read(4))
        self.col_count = self.convert_bytes_to_int32(self.images.read(4))

    @staticmethod
    def convert_bytes_to_int32(data):
        """Gets bytes and converts them into integer"""
        return struct.unpack('i', data[::-1])[0]

    def read_one_image(self):
        """Read one image from file. Assumes headers were skipped. Returns list of bytes and byte with label."""
        pixels = []
        for i in range(self.col_count):
            for j in range(self.row_count):
                pixels.append(self.images.read(1)[0])
        label = str(self.labels.read(1)[0])
        pixels.append(label)
        return pixels

    def close(self):
        self.images.close()
        self.labels.close()