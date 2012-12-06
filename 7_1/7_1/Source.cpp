#include <iostream>
#include "myList.h"
#include "Merge.h"
#include <stdlib.h>

int main(int argc, char* argv[])
{
	srand(time(0));
	myList* l = new myList();
	for (int i = 0; i < 15; ++i)
		l->insert(rand() % 50);
	l->printList(std::cout, " ");
	std::cout << std::endl;
	l = mergeList(l);
	std::cout << std::endl;
	l->printList(std::cout, " ");
	int a = 0;
	std::cin >> a;
	return 0;
}