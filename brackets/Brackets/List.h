#pragma once

#include <iostream>

template <typename T>
class List
{
public:

	struct Node
	{
		T item;
		Node* next;
	};

	typedef Node* Position;

	List()			
	{
		count = 0;
		head = new Node();
		head->next = nullptr;
		e = nullptr;
	}

	//Constructor, initializes list with value
	List(T value)
	{
		head = new Node;
		head->next = new Node;
		head->next->item = value;
		head->next->next = nullptr;
		count = 1;
		e = nullptr;
	}

	~List()
	{
		if (count != 0)
		{
			Position current = head;
			Position following = head->next;
			while (following != e)
			{
				delete current;
				current = following;
				following = current->next;
			}
			delete current;
		}
		else
		{
			delete head;
		}
	}

	//insert item to position after element on p-th position
	void insert(T element, Position p)
	{
		Node* newElem = new Node;
		newElem->item = element;
		newElem->next = p->next;
		p->next = newElem;
		++count;
	} 

	//insert just after head
	void insert(T element)
	{
		insert(element, head);
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
		return p->item;
	}

	//delete element, returns position of the element after deleted element 
	Position remove(Position p) 
	{
		if (count != 0)
		{
			Position get = p->next;
			Position temp = head;
			while (temp->next != p) 
				temp = temp->next;
			temp->next = p->next;
			if (p == end()) 
				setEnd(get);
			delete p;
			--count;
			return get;
		}
		return nullptr;			
	}

	//position of first element in the list, head if list is empty
	Position first()	
	{
		if (count != 0)
			return head->next;
		Node* const PointerToHead = const_cast<Node*>(head);
		return PointerToHead;
	}
	
	//send list to a stream with separator
	void printList(std::ostream &stream, char* separator = "\0")	
	{
		Position toPrint = first();
		while (toPrint != end())
		{
			stream << toPrint->item << separator;
			toPrint = toPrint->next;
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
			return p->next;
		else
			return end();
	}

	//change end() value
	void setEnd(Position p) 
	{
		if (count != 0)
		{
			Position i = first(); 
			while (i->next != end())
				i = i->next;
			i->next = p;
		}
		e = p;
	}

	//apply action on every list element
	void iterate(void (*action)(T&))
	{
		Position seek = first();
		while (seek != end())
		{
			action(seek->item);
			seek = seek->next;
		}
	}

private:
	int count;
	Node* head;
	Position e;
};
