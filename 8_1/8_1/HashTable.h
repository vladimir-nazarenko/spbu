#include "List.h"

//class embodies opened hash table by linked lists
template <typename T>
class HashTable
{
public:
	struct Cell
	{
	public:
		
		//standart constructor
		Cell() : elements(new List<Record>()) {}

		//destructor
		~Cell()
		{
			delete elements;
		}

		//return element from linked list with got number, 
		//useful to iterator
		T getElem(int number)
		{
			List<Record>::Position seek = elements->first();
			for (int i = 0; i < number; ++i)
				seek = elements->next(seek);
			return elements->retrieve(seek).getValue();
		}

		size_t getCount(int number)
		{
			List<Record>::Position seek = elements->first();
			for (int i = 0; i < number; ++i)
				seek = elements->next(seek);
			return elements->retrieve(seek).getCounter();
		}

		//simply put value in the list
		void putValue(T value)
		{
			if (checkValue(value))
			{
				Record toFind;
				toFind.setValue(value);
				elements->retrieve(elements->find(toFind)).incCounter();
			}
			else
			{
				Record toPut;
				toPut.setValue(value);
				toPut.resetCounter();
				toPut.incCounter();
				elements->insert(toPut);
			}

		}

		//true if value found in this cell
		bool checkValue(T value)
		{
			if (elements->size())
			{
				Record toCheck;
				toCheck.setValue(value);
				if (elements->find(toCheck))
					return true;
			}
			return false;
		}

		//delete value from list by its key
		void removeValue(T value)
		{
			Record toFind;
			toFind.setValue(value);
			elements->remove(elements->find(toFind));
		}

		//return number of elements in the list
		//useful to iterator
		int getListLength()
		{
			return elements->size();
		}

	private:
		//record that contains value itself and number of its appearences
		struct Record
		{
		public:

			void setValue(T value)
			{
				item = value;
			}

			T getValue()
			{
				return item;
			}

			void incCounter()
			{
				++counter;
			}

			void resetCounter()
			{
				counter = 0;
			}

			int getCounter()
			{
				return counter;
			}

			bool operator == (Record& another)
			{
				return this->item == another.item;
			}

			bool operator != (Record& another)
			{
				return this->item != another.item;
			}
		private:
			T item;
			int counter;
		};
		List<Record>* elements;	
	};

	HashTable(int size, unsigned __int64 (*hashF)(T value))
	{
		table = new Cell[size];
		tableLength = size;
		getHashCode = hashF;
	}

	~HashTable()
	{
		delete[] table;
	}

	void insert(T value)
	{
		int index = getHashCode(value) % tableLength;
		table[index].putValue(value);
	}

	int find(T value)
	{
		int index = getHashCode(value) % tableLength;
		return table[index].checkValue(value) ? index : -1;
	}

	void iterate(void (*action)(T))
	{
		for (size_t i = 0; i < tableLength; ++i)
		{
			for (int j = 0; j < table[i].getListLength(); ++j)
			{
				action(table[i].getElem(j));
			}
		}
	}

	void print(std::ostream &stream, char* separator = "\0")
	{
		for (size_t i = 0; i < tableLength; ++i)
		{
			for (int j = 0; j < table[i].getListLength(); ++j)
			{
				stream << table[i].getElem(j) << " " << table[i].getCount(j) << separator; 
			}
		}
	}
	
private:
	Cell* table;
	size_t tableLength;
	unsigned __int64 (*getHashCode)(T value);
};
