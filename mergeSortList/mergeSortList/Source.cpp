#include "List.h"
#include "Merge.h"
#include <iostream>
#include <stdlib.h>
#include <time.h>
#include <cstring>
#include <fstream>

const bool byName = true;
const bool byNumber = false;

struct Record
{
	long long int number;
	char* name;
	//Controls sort order, by name or by number
	bool cmpType;
	//Constructor with parameters
	Record(long long int value, char* str, bool sortOrder = byName)
	{
		number = value;
		name = str;
		cmpType = sortOrder;
	}
	//Default constructor
	Record() : number(0), name(0), cmpType(0) {}	
	//////////////////////////////////////operators overloads
	bool operator > (const Record& a)
	{
		if (cmpType == byName)
			return strcmp(a.name, this->name) < 0 ? true : false;
		else
			return a.number < this->number;
	} 
	bool operator < (Record& a)
	{
		if (cmpType == byName)
			return strcmp(a.name, this->name) > 0 ? true : false;
		else
			return a.number > this->number;
	}
	bool operator <= (Record& a)
	{
		if (cmpType == byName)
			return strcmp(a.name, this->name) >= 0 ? true : false;
		else
			return a.number >= this->number;
	}
	bool operator >= (Record& a)
	{
		if (cmpType == byName)
			return strcmp(a.name, this->name) <= 0 ? true : false;
		else
			return a.number <= this->number;
	}
	bool operator == (Record& a)
	{
		if (cmpType == byName)
			return strcmp(a.name, this->name) == 0 ? true : false;
		else
			return a.number == this->number;
	}
	bool operator != (Record& a)
	{
		if (cmpType == byName)
			return strcmp(a.name, this->name) != 0 ? true : false;
		else
			return a.number != this->number;
	}
	friend std::ostream& operator << (std::ostream& stream, Record& item)
	{
		return stream << item.name << ' ' << item.number;
	}
	//////////////////////////////////////
};

Record* getNextRecord(std::ifstream& f);
bool hasNextRecord(std::ifstream& f);
void changeSortType(Record& a);

int main()
{
	const char* inpPath = "input.txt";
	const char* outPath = "output.txt";
	List<Record>* l = new List<Record>();
	std::ios_base::openmode mode = std::ios_base::in;
	std::ifstream input (inpPath, mode);
	std::ofstream output;
	while (hasNextRecord(input))
		l->insert(*getNextRecord(input));
	bool sortType = byName;
	int command = 0;
	std::cout << "Enter command (1 to stop, 2 to view sorted by name or \n" 
			  << "3 to view sorted by number) and see the result\n";
	bool stop = false;

	while (!stop)
	{
		std::cin >> command;
		if (command == 1)
			stop = !stop;
		if (command == 2)
		{
			if (!sortType)
				l->iterate(changeSortType);
			l = mergeList(l);
			l->printList(std::cout, "\n");
			std::cout << std::endl;
		}
		else 
			if (command == 3)
			{
				if (sortType)
					l->iterate(changeSortType);
				l = mergeList(l);
				l->printList(std::cout, "\n");
				std::cout << std::endl;
			}
	}
	std::cin.get();
	return 0;
}

//change sort order of distinct order 
void changeSortType(Record& a)
{
	a.cmpType = (!a.cmpType);
}

//gets data from file stream and returns Record with this data 
Record* getNextRecord(std::ifstream& f)
{
	const int bSize = 80;
	char* buf = new char[bSize]();
	char current = 0;
	current = f.get();
	int cnt = 0;
	while (current != ' ')
	{
		buf[cnt++] = current;
		current = f.get();
	}
	buf[cnt] = '\0';
	char* name = strcpy(new char[cnt + 1], buf);
	cnt = 0;
	current = f.get();
	while (current != '\n' && !f.eof())
	{
		buf[cnt++] = current;
		current = f.get();
	}
	buf[cnt] = '\0';
	long long int num = atoi(buf);
	Record* result = new Record(num, name);
	delete[] buf;
	return result;
}

//check if file isn't ended or empty
bool hasNextRecord(std::ifstream& f)
{
	return !f.eof();
}