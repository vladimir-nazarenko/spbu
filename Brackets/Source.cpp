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
		if (current == 40 || current == 91 || current == 123)
			stack->push(current);
		if (current == 41 || current == 93 || current == 125)
		{
			temp = stack->top();
			if (current == 41 && temp == 40 || current == 93 && temp == 91 || current == 125 && temp == 123)
				stack->pop();
		}
		current = buf[cnt++];
	}
	std::cout << (stack->isEmpty() ? "correct" : "incorrect");
	std::cin >> temp;
	delete [] buf;
	delete stack;	
	return 0;
}