#include "List.h"

//stack based on a linked list
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
	//returns top value from stack without removing
	T top()
	{
		return lst->retrieve(lst->first());
	}
	//returns top value from stack and removes it
	T pop()
	{
		T temp = lst->retrieve(lst->first());
		lst-> remove(lst->first());
		return temp;
	}
	//put value at the top of the stack
	void push(T item)
	{
		lst->insert(item);
	}
	//check if stack is empty
	bool isEmpty()
	{
		return lst->size() == 0;
	}

private:
	//linked list for inner notion of a stack
	List<T>* lst; 
};