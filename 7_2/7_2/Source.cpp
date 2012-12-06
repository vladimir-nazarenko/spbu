#include <iostream>
#include "BST.h"

void print(int value)
{
	std::cout << value << " ";
}

int main(int argc, char* argv[])
{
	BST<int>* tree = new BST<int>();
	enum commands {end, add, remove, exists, printUp, printDown};
	bool stop = false;
	std::cout << "//command list:\n"
			  <<"//0-stop\n"
			  <<"//1-add value\n"
			  <<"//2-remove\n"
			  <<"//3-check if value is in the tree\n"
			  <<"//4-print tree in ascending order\n"
			  <<"//5-print tree in descending order\n";
	while (!stop)
	{
		std::cout << "Enter command\n";
		int a = 0;
		std::cin >> a;
		switch (a)
		{
		case commands::end:
			stop = true;
			break;
		case commands::add:
			std::cout << "Enter value to add\n";
			std::cin >> a;
			tree->insert(a);
			break;
		case commands::remove:
			std::cout << "Enter value to remove\n";
			std::cin >> a;
			tree->remove(a);
			break;
		case commands::exists:
			std::cin >> a;
			std::cout << tree->find(a) == 0 ? "there is no such element" : "there is such element";
			break;
		case commands::printUp:
			std::cout << std::endl;
			tree->traverseUp(print);
			break;
		case commands::printDown:
			std::cout << std::endl;
			tree->traverseDown(print);
		break;
		default:
			std::cout << "Invalid command\n";
			break;
		}
	}
	return 0;
}