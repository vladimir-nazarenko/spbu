#include "List.h"

template <typename T>
class Stack
{
public:
	Stack()
	{
		lst = new List<T>(); 
	}

	~Stack()
	{
		delete lst;
	}

	T top()
	{
		return lst->retrieve(lst->first());
	}

	T pop()
	{
		T temp = lst->retrieve(lst->first());
		lst-> remove(lst->first());
		return temp;
	}

	void push(T item)
	{
		lst->insert(item);
	}

	bool isEmpty()
	{
		return lst->size() == 0 ? true : false;
	}

private:
	List<T>* lst; 
};