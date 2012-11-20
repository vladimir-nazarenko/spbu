#include "List.h"

#include <iostream>

int main()
{
	const int bufSize = 80;
	List<double>* stack = new List<double>();
	std::cout << "Enter expression";
	unsigned char* buf = new unsigned char[bufSize];
	std::cin >> buf;
	unsigned char current = 255;
	double temp1 = 0, temp2 = 0;
	int cnt = 0;
	while (current != 0)
	{
		if (current >= '*' && current <= '9')
		{
			if (current >= '0') 
			{
				stack->insert(current - '0');
			}
			else
			{
				temp2 = stack->retrieve(stack->first());				//снять 2 значения со стека
				temp1 = stack->retrieve(stack->remove(stack->first()));
				stack->remove(stack->first());
				switch (current)
				{
					case '+':					
						stack->insert(temp1 + temp2);
						break;
					case '-':
						stack->insert(temp1 - temp2);
						break;
					case '*':
						stack->insert(temp1 * temp2);
						break;
					case '/':
						stack->insert(temp1 / temp2);
						break;
				}
			}
		}
		current = buf[cnt++];
	}
	double result = stack->retrieve(stack->first());
	delete [] buf;
	delete stack;
	std::cout << result;
	return 0;
}