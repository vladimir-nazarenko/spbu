#include <iostream>
#include <fstream>

int main()
{
	std::ifstream input;
	input.open("input.txt", std::ios_base::in);
	const int bSize = 500;
	int writeIndex = 0;
	char* comments = new char[bSize]();
	enum states 
	{
		start, read, skip, end
	} state = start;
	char current = input.get();
	while (state != end)
	{
		switch (state)
		{
		case start:
			if (current == '/')
				if (input.get() =='*')
				{
					input.unget();
					input.unget();
					state = read;
				}
				else
				{
					state = skip;
				}
			break;
		case read:
			{
				if (current == '*')
					if (input.get() == '/')
					{
						comments[writeIndex++] = '*';
						comments[writeIndex++] = '/';
						comments[writeIndex++] = '\n';
						state = skip;
					}
					else
					{
						input.unget();
						comments[writeIndex++] = '*';
					}
				else
					comments[writeIndex++] = current;
			}
			break;
		case skip:
			if (current == '/')
			if (input.get() =='*')
			{
				input.unget();
				input.unget();
				state = read;
			}
			break;
		}
			current = input.get();
		if (current == std::char_traits<char>::eof())
			state = end;
	}
	input.close();
	std::cout << comments;
	delete[] comments;
	std::cin >> writeIndex;
	return 0;
}