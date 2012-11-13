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

	List(T value)	//�����������, ���������������� ������ ���������
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

	void insert(T element, Position p)	//������� �� �������
	{
		Node* newElem = new Node;
		newElem->item = element;
		newElem->next = p->next;
		p->next = newElem;
		++count;
	} 

	void insert(T element)	//������� ����� ������ ���������
	{
		insert(element, head);
	}

	Position find(T element)	//����� �� ��������(������� ����� ������ � ������ ���������� ��������)
	{
		Position toFind = first();
		while (toFind != end() && toFind->item != element)
		{
			toFind = toFind->next;
		}
		return toFind;
	}

	T retrieve(Position p)	//������� �������� �� �������
	{
		return p->item;
	}

	Position remove(Position p) //��������(���������� 0 ��� �������� �� ������� ������)
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

	Position first()	//������� 1-�� ��������(0 � ������ ������)
	{
		if (count != 0)
			return head->next;
		return nullptr;
	}
	
	void printList(std::ostream &stream, char* separator = "\0")	//������ ������ � ����� � �������� ������������
	{
		Position toPrint = first();
		while (toPrint != end())
		{
			stream << toPrint->item << separator;
			toPrint = toPrint->next;
		}
	}

	Position end()	//������� ����� ������
	{
		return e;
	}

	int size() //���������� ��������� ������
	{
		return count;
	}

	Position next(Position p) //������� ���������� ��������
	{
		if (p != end())
			return p->next;
		else
			return end();
	}

	void setEnd(Position p) //��������� �������� ����� ������
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
