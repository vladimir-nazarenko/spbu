#include <iostream>
#include <stdio.h>
#include <stdlib.h>
#include <ctime>
#include "List.h"

template <typename T>
List<T>* mergeList(List<T>* l)
{
	List<T>* sorted = merge(l, l->first(), l->size());
	delete l;
	return sorted;
}

template <typename T>
List<T>* merge(List<T>* input, typename List<T>::Position firstPos, int listSize)
{
	if (listSize == 1)
		return new List<T>(input->retrieve(firstPos));					//returning condition

	int middle = listSize / 2;
	List<T>* left = merge(input, firstPos, middle);						//sorting left half
	for (int i = 0; i < middle; ++i) 
		firstPos = input->next(firstPos);
	List<T>* right = merge(input, firstPos, listSize - middle);			//sorting right half
	List<T>* united = new List<T>();									//some vars
	int cntl = 0, cntr = 0;
	List<T>::Position posl = left->first(), posr = right->first();
	List<T>::Position posWrite = united->first();

	while (cntl < middle && cntr < listSize - middle)					//comparing and merge
	{
		if (left->retrieve(posl) < right->retrieve(posr))
		{
			united->insert(left->retrieve(posl), posWrite);
			posl = left->next(posl);
			posWrite = united->next(posWrite);
			++cntl;
		}
		else
		{
			united->insert(right->retrieve(posr), posWrite);
			posr = right->next(posr);
			posWrite = united->next(posWrite);
			++cntr;
		}
	}

	if (cntr != listSize - middle)						//writing extra values
	{
		for (int i = cntr; i < listSize - middle; ++i)
		{
			united->insert(right->retrieve(posr), posWrite);
			posr = right->next(posr);
			posWrite = united->next(posWrite);
		}
	}
	else
	{
		for (int i = cntl; i < middle; ++i)
		{
			united->insert(left->retrieve(posl), posWrite);
			posl = left->next(posl);
			posWrite = united->next(posWrite);
		}
	}
	delete right;
	delete left;
	return united;
}