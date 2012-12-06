#include "Stack.h"

#include <iostream>

int main()
{
	const int bufSize = 80;
	Stack<char>* stack = new Stack<char>();
	std::cout << "Enter expression";
	unsigned char* buf = new unsigned char[bufSize];
	unsigned char result[bufSize] = {0};
	std::cin >> buf;
	unsigned char current = 255;
	char temp = 0;
	int cntRead = 0, cntWrite = 0;
	while (current != 0)
	{
		if (current == '(' || current == ')' || current == '+' ||
			current == '-' || current == '*' || current == '/' || 
			current >= '0' && current <= '9')
		{
			if (current >= '0') 
			{
				result[cntWrite++] = current;
			}
			else
			{
				if (stack->isEmpty())
				{
					stack->push(current);
				}
				else
				{
					temp = stack->top();
					switch (current)
					{
						case '+':					
							if (temp == '*' || temp == '/') 
							{
								result[cntWrite++] = stack->pop();
							}
							stack->push('+');
							break;
						case '-':
							if (temp == '*' || temp == '/') 
							{
								result[cntWrite++] = stack->pop();
							}
							stack->push('-');
							break;
						case '*':
							stack->push('*');
							break;
						case '/':
							stack->push('/');
							break;
						case '(':
							stack->push('(');
							break;
						case ')':
							temp = stack->pop();
							while (temp != '(' && !stack->isEmpty())
							{
								result[cntWrite++] = temp;
								temp = stack->pop();
							}
							break;
					}
				}
			}
		}
		current = buf[cntRead++];
	}
	while (!stack->isEmpty())
		result[cntWrite++] = stack->pop();
	delete [] buf;
	delete stack;
	std::cout << result;
	std::cin >> temp;
	return 0;
}