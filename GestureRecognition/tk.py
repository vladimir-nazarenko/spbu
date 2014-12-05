from Tkinter import *


class FilenameDialog:
    def __init__(self, parent):
        top = self.top = Toplevel(parent)
        Label(top, text="Enter filename").pack()
        self.e = Entry(top)
        self.e.pack()
        b = Button(top, text="OK", command=self.ok)
        b.pack()
        self.result = ""
        
    def ok(self):
        self.result = self.e.get()
        self.top.destroy()
