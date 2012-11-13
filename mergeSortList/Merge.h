#include <iostream>
#include <stdio.h>
#include <stdlib.h>
#include <ctime>
#include "List.h"

template <typename T>
List<T>* mergeList(List<T>* l)
{
	List<T>* sorted = merge(l, l->first(), l->size());
//	delete l;
	return sorted;
}

template <typename T>
List<T>* merge(List<T>* input, typename List<T>::Position firstPos, int listSize)
{
	if (listSize == 1)
		return new List<T>(input->retrieve(firstPos));

	int middle = listSize / 2;
	List<T>* left = merge(input, firstPos, middle);
	for (int i = 0; i < middle; ++i) 
		firstPos = input->next(firstPos);
	List<T>* right = merge(input, firstPos, listSize - middle);
	List<T>* united = new List<T>();
	int cntl = 0, cntr = 0;
	typename List<T>::Position posl = left->first(), posr = right->first();

	while (cntl < middle && cntr < listSize - middle)
	{
		if (left->retrieve(posl) < right->retrieve(posr))
		{
			united->insert(left->retrieve(posl));
			posl = left->next(posl);
			++cntl;
		}
		else
		{
			united->insert(right->retrieve(posr));
			posr = right->next(posr);				
			++cntr;
		}
	}

	if (cntr != listSize - middle)
	{
		for (int i = cntr; i < listSize - middle; ++i)
		{
			united->insert(right->retrieve(posr));
			posr = right->next(posr);
		}
	}
	else
	{
		for (int i = cntl; i < middle; ++i)
		{
			united->insert(left->retrieve(posl));
			posl = left->next(posl);
		}
	}
	delete right;
	delete left;
	return united;
}
