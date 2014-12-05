__author__ = 'vladimir'
import arff


class ArffWriter:
    def __init__(self, rel_name, filename, n_cols, n_rows):
        """Set writer to appropriate position"""
        col_names = []
        for i in range(n_cols):
            for j in range(n_rows):
                col_names.append('x=%iy=%i' % (j, i))
        col_names.append('class')
        self.writer = arff.Writer(filename, relation=rel_name, names=col_names)
        # set trigger: if we have str in column, interpret it as a class with such values
        self.writer.pytypes[str] = set([str(i) for i in range(10)])

    def write_next_row(self, pixels):
        self.writer.write(pixels)

    def close(self):
        self.writer.close()