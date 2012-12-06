#pragma once

#include <iostream>

template <typename T>
class ArrayList
{
public:

	struct Node
	{
		T item;
		int next;
	};

	typedef int Position;

	ArrayList()			
	{
		aSize = 1;
		storage = new Node[aSize];
		count = 0;
		e = -1;
		empty = e;
		storage[0].next = e;
	}

	//Constructor, initializes list with value
	ArrayList(T value)			
	{
		aSize = 2;
		storage = new Node[aSize];
		count = 2;
		e = -1;
		empty = e;
		storage[0].next = 1;
		storage[1].item = value;
		storage[1].next = e;
	}

	~ArrayList()
	{
		delete[] storage;
	}

	//insert item to position after element on p-th position
	void insert(T element, Position p)
	{
		if (empty == e)
			empty = resize(aSize * 2);
		Node* newElem = new Node;
		newElem->item = element;
		newElem->next = storage[p].next;
		storage[p].next = empty;
		storage[empty] = *newElem;
		delete newElem;
		++empty;
		if (empty == aSize)
			empty = e;
		++count;
	} 

	//insert just after head
	void insert(T element)	
	{
		insert(element, 0);
	}

	//find by value, end() if there is no such item
	Position find(T element)
	{
		Position toFind = first();
		while (toFind != end() && toFind->item != element)
		{
			toFind = toFind->next;
		}
		return toFind;
	}

	//get value on position
	T retrieve(Position p)
	{
		return storage[p].item;
	}

	//delete element, returns value of the element
	T remove(Position p)
	{
		if (aSize != 1)
		{
			T get = storage[p].item;
			Position temp = 0;
			while (storage[temp].next != p) 
				temp = storage[temp].next;
			storage[temp].next = storage[p].next;
			--count;
			if (aSize > 2.1 * count)
				empty = resize(aSize / 2);
			return get;
		}
		return 0;			
	}

	//position of first element in the list, head if list is empty
	Position first()	
	{
		return storage[0].next == e ? 0 : storage[0].next;
	}
	
	//send list to a stream with separator
	void printList(std::ostream &stream, char* separator = "\0")	
	{
		Position toPrint = first();
		while (toPrint != end())
		{
			stream << storage[toPrint].item << separator;
			toPrint = storage[toPrint].next;
		}
	}

	//position of the end of the list
	Position end()
	{
		return e;
	}

	//number of elements in the list
	int size()
	{
		return count;
	}

	//position of the element right after element on a gotten position
	Position next(Position p) 
	{
		if (p != end())
			return storage[p].next;
		else
			return end();
	}

private:
	int empty;
	int start;
	Node* storage;
	int stSize;
	int count;
	Position e;
	int aSize;

	//set new size of storage array with making corresponding links
	int resize(int newSize)
	{
		Node* aux = new Node[newSize];
		Position pointer = 0;
		int i = 0;
		aux[0].next = e;
		while (storage[pointer].next != e)
		{
			pointer = storage[pointer].next;
			aux[++i].item = storage[pointer].item;
			aux[i - 1].next = i;
		}
		aux[i].next = e;
		delete[] storage;
		storage = aux;
		aSize = newSize;
		return ++i;
	}
};
