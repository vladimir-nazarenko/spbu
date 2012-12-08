#include <iostream>
#include "HashTable.h"
#include <fstream>
#include <string>

unsigned __int64 FNVHash(std::string value)
{
	const unsigned __int64 prime = 2365347734339;
	unsigned __int64 hval = 2166135261;
	for (size_t i = 0; i < value.length(); ++i)
	{
		hval *= prime;
		hval ^= (unsigned __int64)(value[i] + 128);
	}
	return hval;
}

std::string getNextRecord(std::ifstream& f);
bool hasNextRecord(std::ifstream& f);
char lower(char letter);
bool isJunk(char symbol);

int main(int argc, char* argv[])
{
	int tmp = 0;
	HashTable<std::string>* ht = new HashTable<std::string>(400, *FNVHash);
	const char* inpPath = "input.txt";
	std::ios_base::openmode mode = std::ios_base::in;
	std::ifstream input (inpPath, mode);
	//read data
	while (hasNextRecord(input))
		ht->insert(getNextRecord(input));
	input.close();
	std::ofstream output;
	output.open("output.txt", std::ios_base::out);
	//write data
	ht->print(output, "\r\n");
	output.close();
	delete ht;
	return 0;
}

//gets string from file stream without spaces or punctuation
std::string getNextRecord(std::ifstream& f)
{
	const int bSize = 40;
	std::string buf;
	buf.resize(bSize);
	char current = 0;
	current = f.get();
	int cnt = 0;
	while (current != ' ' && current != '\n' && !f.eof())
	{
		if (!isJunk(current))
			buf[cnt++] = lower(current);
		current = f.get();
	}
	buf[cnt] = '\0';
	return buf;
}

//check if file isn't ended or empty
bool hasNextRecord(std::ifstream& f)
{
	return !f.eof();
}

//check if symbol is not either a letter or a number
bool isJunk(char symbol)
{
	if (lower(symbol) >= 'a' && lower(symbol) <= 'z' || symbol <= '9' && symbol >= '0')
		return false;
	return true;
 }

//if parameter is a letter upper cased returns it in lower case else returns parameter
char lower(char letter)
{
	if (letter >= 'A' && letter <= 'Z')
		return letter - 'A' + 'a';
	return letter;
}