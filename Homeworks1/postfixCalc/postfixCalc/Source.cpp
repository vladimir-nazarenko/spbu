#include "Stack.h"

#include <iostream>

int main()
{
	const int bufSize = 80;
	Stack<double>* stack = new Stack<double>();
	std::cout << "Enter expression";
	unsigned char* buf = new unsigned char[bufSize];
	std::cin >> buf;
	unsigned char current = 255;
	int cnt = 0;
	while (current != 0)
	{
		if (current == '*' || current == '+' || current == '-' || current == '/' || current >= '0' && current <= '9')
		{
			if (current >= '0') 
			{
				stack->push(current - '0');
			}
			else
			{
				//снять 2 значения со стека
				double temp2 = stack->pop(), temp1 = stack->pop();			
				switch (current)
				{
					case '+':					
						stack->push(temp1 + temp2);
						break;
					case '-':
						stack->push(temp1 - temp2);
						break;
					case '*':
						stack->push(temp1 * temp2);
						break;
					case '/':
						stack->push(temp1 / temp2);
						break;
				}
			}
		}
		current = buf[cnt++];
	}
	double result = stack->pop();
	delete [] buf;
	delete stack;
	std::cout << result;
	return 0;
}