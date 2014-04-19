__author__ = 'vladimir'

from wand.image import Image
# from wand.display import display
from wand.color import Color
from wand.drawing import Drawing
from collections import deque
import random


class ImageManipulator:
    @staticmethod
    def noise_pixels(pixels, ratio):
        """Adds random white or black points to image representation"""
        assert 0.0 <= ratio <= 1.0
        if ratio == 0.0:
            return pixels
        """Gets list of image pixels and for pixels * ratio of them sets random ones to be white"""
        pix_len = len(pixels)
        positions = list(range(pix_len))
        random.shuffle(positions)
        positions = positions[0:int(pix_len * ratio)]
        for i in positions:
            pixels[i] = 255 * random.randint(0, 1)
        return pixels

    @staticmethod
    def write_image_into_file(img, label='pixels'):
        def skip_extra_parts(hex_str):
            """Gets string, representing hex number and skips part 0x"""
            return hex_str[2:]
        n_rows = len(img)
        assert n_rows > 0
        n_cols = len(img[0])
        picture = Image()
        picture = picture.blank(n_cols, n_rows, background=Color('#ffffff'))
        draw = Drawing()
        for r in range(n_rows):
            for c in range(n_cols):
                grey_scale = img[r][c]
                draw.fill_color = Color('#' + 3 * skip_extra_parts(hex(grey_scale)))
                draw.line((c, r), (c, r))
        draw(picture)
        picture.save(filename=label + '.jpg')

    @staticmethod
    def join_horizontally(img1, img2):
        assert len(img1) == len(img2)
        big_img = []
        for r in range(len(img1)):
            assert len(img1[r]) == len(img2[r])
            big_img.append(img1[r] + img2[r])
        return big_img

    @staticmethod
    def join_vertically(img1, img2):
        assert len(img1) == len(img2)
        return img1 + img2

    @staticmethod
    def normalize_image(pixels, n_rows, n_cols):
        """Make matrix of bytes"""
        img = []
        queue = deque(pixels)
        for i in range(n_rows):
            row = []
            for j in range(n_cols):
                row.append(queue.popleft())
            img.append(row)
        return img

    @staticmethod
    def denormalize_image(img):
        """Form string of bytes from matrix"""
        pixels = []
        for row in img:
            pixels += row
        return pixels