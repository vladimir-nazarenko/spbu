#include <iostream>
#include <stdlib.h>
#include "List.h"

template <typename T>
List<T>* mergeList(List<T>* l)
{
	List<T>* sorted = merge(l, l->first(), l->size());
	delete l;
	return sorted;
}

template <typename T>
List<T>* merge(List<T>* inputList, typename List<T>::Position firstPosition, int listSize)
{
	//return condition
	if (listSize == 1)
		return new List<T>(inputList->retrieve(firstPosition));					

	int middle = listSize / 2;
	//sorting left half
	List<T>* left = merge(inputList, firstPosition, middle);						
	for (int i = 0; i < middle; ++i) 
		firstPosition = inputList->next(firstPosition);
	//sorting right half
	List<T>* right = merge(inputList, firstPosition, listSize - middle);
	//some vars			
	List<T>* united = new List<T>();									
	int viewedInLeftList = 0, viewedInRightList = 0;
	List<T>::Position positionToReadLeft = left->first(), positionToReadRight = right->first();
	List<T>::Position posWriteUnited = united->first();
	//comparing and merge
	while (viewedInLeftList < middle && viewedInRightList < listSize - middle)					
	{
		if (left->retrieve(positionToReadLeft) < right->retrieve(positionToReadRight))
		{
			united->insert(left->retrieve(positionToReadLeft), posWriteUnited);
			positionToReadLeft = left->next(positionToReadLeft);
			posWriteUnited = united->next(posWriteUnited);
			++viewedInLeftList;
		}
		else
		{
			united->insert(right->retrieve(positionToReadRight), posWriteUnited);
			positionToReadRight = right->next(positionToReadRight);
			posWriteUnited = united->next(posWriteUnited);
			++viewedInRightList;
		}
	}
	//writing extra values
	if (viewedInRightList != listSize - middle)						
	{
		for (int i = viewedInRightList; i < listSize - middle; ++i)
		{
			united->insert(right->retrieve(positionToReadRight), posWriteUnited);
			positionToReadRight = right->next(positionToReadRight);
			posWriteUnited = united->next(posWriteUnited);
		}
	}
	else
	{
		for (int i = viewedInLeftList; i < middle; ++i)
		{
			united->insert(left->retrieve(positionToReadLeft), posWriteUnited);
			positionToReadLeft = left->next(positionToReadLeft);
			posWriteUnited = united->next(posWriteUnited);
		}
	}
	delete right;
	delete left;
	return united;
}