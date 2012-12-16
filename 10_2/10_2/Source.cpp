#include <iostream>
#include <fstream>
#include "List.h"
#include "Merge.h"

//structure embodies path in graph with compare functions
struct Path
{
	int first;
	int second;
	int length;

	bool operator > (const Path& another)
	{
		return this->length > another.length;
	}

	bool operator < (const Path& another)
	{
		return this->length < another.length;
	}

	bool operator == (const Path& another)
	{
		return this->length == another.length;
	}

	bool operator != (const Path& another)
	{
		return this->length != another.length;
	}

	friend std::ostream& operator << (std::ostream& stream, Path& item)
	{
		stream << item.first << " " << item.second << " " << item.length;
		return stream;
	}
};

//data structure for dynamic connectivity
struct quickFindUF
{
	quickFindUF(size_t size)
	{
		count = size;
		id = new unsigned int[size]();
		for (unsigned int i = 0; i < size; ++i)
		{
			id[i] = i;
		}
	}

	~quickFindUF()
	{
		delete[] id;
	}

	bool connected(int a, int b)
	{
		return id[a] == id[b];
	}

	void unite(int first, int second)
	{
		if (connected(first, second))
			return;
		for (unsigned int i = 0; i < count; ++i)
		{
			if (id[i] == id[second])
				id[i] = id[first];
		}
	}

	size_t size()
	{
		return count;
	}
private:
	unsigned int* id;
	size_t count;
};

//adjacency matrix for undirected graph
struct AdjacencyMatrix
{

	AdjacencyMatrix(size_t size)
	{
		matrix = new int*[size]();
		for (size_t i = 0; i < size; ++i)
			matrix[i] = new int[size]();
		matrixSize = size;
	}

	~AdjacencyMatrix()
	{
		for (size_t i = 0; i < matrixSize; ++i)
			delete[] matrix[i];
		delete[] matrix;
	}

	void setLink(unsigned int first, unsigned int second, unsigned int length = 1)
	{
		matrix[first][second] = length;
		matrix[second][first] = length;
	}

	void deleteLink(unsigned int first, unsigned int second)
	{
		matrix[first][second] = 0;
		matrix[second][first] = 0;
	}

	int getDistance(unsigned int first, unsigned int second)
	{
		return matrix[first][second];
	}

	void printMatrix()
	{
		for (unsigned int i = 0; i < matrixSize; ++i)
		{
			for (unsigned int j = 0; j < matrixSize; ++j)
				std::cout << matrix[i][j] << ' ';
			std::cout << std::endl;
		}
	}
	size_t matrixSize;
protected:
	int** matrix;
};

//get next number from input stream
int getNext(std::ifstream& f)
{
	const int bSize = 5;
	char* buf = new char[bSize]();
	char current = 0;
	current = f.get();
	int cnt = 0;
	while (current >= '0' && current <= '9')
	{
		buf[cnt++] = current;
		current = f.get();
	}
	buf[cnt] = '\0';
	int num = atoi(buf);
	delete[] buf;
	return num;
}

//check if file isn't ended or empty
bool hasNext(std::ifstream& f)
{
	return !f.eof();
}

//return filled from stream matrix, set by the name of a file
AdjacencyMatrix*& fillMatrix(char* fileName)
{
	std::ifstream input;
	input.open(fileName, std::ios_base::in);
	int size = getNext(input);
	AdjacencyMatrix* matrix = new AdjacencyMatrix(size);
	for (int i = 0; i < size; ++i)	
	{
		for (int j = 0; j < size; ++j)
		{
			matrix->setLink(i, j, getNext(input));
		}
	}
	return matrix;
}

//return sorted list of paths for given matrix
List<Path>*& convertToPaths(AdjacencyMatrix* matrix)
{
	List<Path>* lst = new List<Path>();
	for (unsigned int i = 0; i < matrix->matrixSize; ++i)
	{
		for (unsigned int j = i + 1; j < matrix->matrixSize; ++j)
		{
			if (matrix->getDistance(i, j))
			{
				Path p;
				p.first = i;
				p.second = j;
				p.length = matrix->getDistance(i, j);
				lst->insert(p);
			}
		}
	}
	lst = mergeList(lst);
	return lst;
}

int main()
{
	AdjacencyMatrix* matrix = fillMatrix("input.txt");
	quickFindUF* qF = new quickFindUF(matrix->matrixSize);
	List<Path>* paths = convertToPaths(matrix);
	delete matrix;
	List<Path>::Position currentNode = paths->first();
	//use qF to delete extra paths
	while (currentNode != paths->end())
	{
		Path currentPath = paths->retrieve(currentNode);
		if (!qF->connected(currentPath.first, currentPath.second))
		{
			qF->unite(currentPath.first, currentPath.second);
			currentNode = paths->next(currentNode);
		}
		else
			currentNode = paths->remove(currentNode);
	}
	std::cout << "list of  of links of the minimal spanning tree:\n";
	paths->printList(std::cout, "\n");
	delete qF;
	delete paths;
	char buf = 0;
	std::cin >> buf;
}