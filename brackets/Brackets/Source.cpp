#include "Stack.h"

#include <iostream>

int main()
{
	const int bufSize = 80;
	Stack<char>* stack = new Stack<char>();
	std::cout << "Enter expression";
	unsigned char* buf = new unsigned char[bufSize];
	std::cin >> buf;
	unsigned char current = 255;
	unsigned char temp = 0;
	int cnt = 0;
	while (current != 0)
	{
		if (current == '(' || current == '[' || current == '{')
			stack->push(current);
		if (current == ')' || current == ']' || current == '}')
		{
			temp = stack->top();
			if (current == ')' && temp == '(' || current == ']' && temp == '[' || current == '}' && temp == '{')
				stack->pop();
			else
			{
				stack->push(0);
				break;
			}
		}
		current = buf[cnt++];
	}
	std::cout << (stack->isEmpty() ? "correct" : "incorrect");
	std::cin >> temp;
	delete [] buf;
	delete stack;	
	return 0;
}