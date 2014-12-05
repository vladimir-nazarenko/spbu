#include <cstring>
#include <stdexcept>
#include <iostream>


bool isNumber(char test);
bool isSign(char test);
bool isCorrect(char*& str);


int main()
{
	const int bSize = 80;
	std::cout << "enter expression to check:";
	char* num = new char[bSize]();
	std::cin >> num;
	std::cout << (isCorrect(num) ? "correct" : "incorrect");
	char buf = 0;
	std:: cin >> buf;
	return 0;
}

bool isCorrect(char*& str)
{
	int len = strlen(str);
	enum states 
	{
		start, integral, point, sign, exp, end, error, fractional, expnumber, expsign
	} state = start;
	int readIndex = 0;
	char current = str[readIndex++];
	while (state != end && state != error)
	{
		switch (state)
		{
		case start:
			if (isNumber(current))
				state = integral;
			else 
				if (isSign(current))
					state = sign;
				else
					state = error;
			break;
		case sign:
			if (isNumber(current))
				state = integral;
			else 
				state = error;
			break;
		case integral:
			if (!isNumber(current))
				if (current == '.')
					state = point;
				else
					if (current == 'e' || current == 'E')
						state = exp;
					else
						state = error;
			break;
		case point:
			if (isNumber(current))
				state = fractional;
			else 
				state = error;
			break;
		case fractional:
			if (!isNumber(current))
				if (current == 'E' || current == 'e')
					state = exp;
				else 
					state = error;
			break;
		case exp:
			if (isSign(current))
				state = expsign;
			else 
				if (isNumber(current))
					state = expnumber;
			break;
		case expsign:
			if (isNumber(current))
				state = expnumber;
			else
				state = error;
		case expnumber:
			if (!isNumber(current))
				state = error;
		}
		if (readIndex >= len)
			if (state == sign || state == point || state == exp || state == expsign || state == error)
				state = error;
			else 
				state = end;
		else
			current = str[readIndex++];
	}
	if (state == end)
		return true;
	else 
		if (state == error)
			return false;
		else
			throw std::bad_exception("Something went wrong");
}

bool isSign(char test)
{
	if (test == '-' || test == '+')
		return true;
	return false;
}

bool isNumber(char test)
{
	if (test >= '0' && test <= '9')
		return true;
	return false;
}