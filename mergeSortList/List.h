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
		head = new Node;
		//head->item = NULL;
		head->next = nullptr;
		e = nullptr;
	}

	List(T value)	//конструктор, инициализирующий список значением
	{
		head = new Node;
		//head->item = static_cast<T>(0);
		head->next = new Node;
		head->next->item = value;
		head->next->next = nullptr;
		count = 1;
		e = nullptr;
	}

	~List()
	{
		Position* stack = new Position[count];
		stack[0] = first();
		int cnt = 0;
		while (stack[cnt]->next != end())
		{
			stack[cnt + 1] = stack[++cnt]->next;
		}
		for (int i = 0; i < count; ++i)
		{
			delete stack[i];
		}
		delete head;
//		delete this;
	}

	void insert(T element, Position p)	//вставка по позиции
	{
		Node* newElem = new Node;
		newElem->item = element;
		newElem->next = p->next;
		p->next = newElem;
		++count;
	} 

	void insert(T element)	//вставка перед первым элементом
	{
		insert(element, head);
	}

	Position find(T element)	//поиск по значению(выводит конец списка в случае отсутствия элемента)
	{
		Position toFind = first();
		while (toFind != end() && toFind->item != element)
		{
			toFind = toFind->next;
		}
		return toFind;
	}

	T retrieve(Position p)	//возврат значения по позиции
	{
		return p->item;
	}

	Position remove(Position p) //удаление(возвращает 0 при удалении из пустого списка)
	{
		if (count != 0)
		{
		Position get = p->next;
		Position temp = head;
		while (temp->next != p) 
			temp = temp->next;
		temp->next = p->next;
		if (p == end()) setEnd(get);
		delete p;
		--count;
		return get;
		}
		return nullptr;			
	}

	Position first()	//позиция 1-го элемента(0 в пустом списке)
	{
		if (count != 0)
			return head->next;
		return nullptr;
	}
	
	void printList(std::ostream &stream, char* separator = "\0")	//печать списка в поток с заданным разделителем
	{
		Position toPrint = first();
		while (toPrint != end())
		{
			stream << toPrint->item << separator;
			toPrint = toPrint->next;
		}
	}

	Position end()	//позиция конца списка
	{
		return e;
	}

	int size() //количество элементов списка
	{
		return count;
	}

	Position next(Position p) //позиция следующего элемента
	{
		if (p != end())
			return p->next;
		else
			return end();
	}

	void setEnd(Position p) //установка значения конца списка
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

private:
	int count;
	Node* head;
	Position e;
};
