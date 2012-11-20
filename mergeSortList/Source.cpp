#include "List.h"
#include "Merge.h"
#include <iostream>
#include <stdlib.h>

int main()
{
	srand(0);
	List<int>* l = new List<int>();
	for (int i = 0; i < 5; ++i)
	{
		l->insert(rand() % 20);
	}
	l = mergeList(l);
	l->printList(std::cout, " ");
	std::cin.get();
	return 0;
}