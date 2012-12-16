#include <iostream>
#include <map>
#include <fstream>
#include <string>

//Boyer-Moore algorithm
int subIndex(char* str, char* substring)
{
	//make table of stop-symbols
	std::map<char, int> stopTable;
	int substringLen = strlen(substring);
	for (int i = 0; i <= 255; i++) 
		stopTable[(char) i] = substringLen;
	
	for (int i = 0; i <= substringLen; i++) 
		stopTable[substring[i]] = substringLen - i - 1;

    int sampleIndex = substringLen - 1;
	int textIndex = sampleIndex;
	int textLen = strlen(str);
	//variable to avoid infinite cycle
	int lastCmpIndex = textIndex;
	//algorithm itself
	while (sampleIndex >= 0 && textIndex < textLen)
	{
		if (substring[sampleIndex] == str[textIndex])
		{
			--sampleIndex;
			--textIndex;
		}
		else
		{
			sampleIndex = substringLen - 1;
			textIndex += stopTable[str[textIndex]];
			//check if cycled
			if (lastCmpIndex > textIndex)
				textIndex = lastCmpIndex + 1;
			lastCmpIndex = textIndex;
		}
	}

	if (sampleIndex < 0)
		return textIndex + 1;
	else
		return -1;
}

int main()
{
	std::ifstream input;
	input.open("input.txt", std::ios_base::in);
	const int bSize = 80;
	char* str = new char[bSize + 1]();
	input.getline(str, bSize);
	input.close();
	char* sample = new char[80]();
	std::cout << "Enter substring to find\n";
	std::cin >> sample;
	int found = subIndex(str, sample);
	if (found < 0)
		std::cout << "there is no such substring";
	else
		std::cout << "substring found at " << found << "-th position";
	delete[] str;
	delete[] sample;
	char buf = 0;
	std::cin >> buf;
	return 0;
}