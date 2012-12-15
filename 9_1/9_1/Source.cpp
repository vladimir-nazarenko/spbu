#include <iostream>
#include <fstream>

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


struct Map : public AdjacencyMatrix
{
	Map(size_t size) : AdjacencyMatrix(size)
	{
		nextCapitalKey = 1;
		country = new unsigned int[size]();
	}
	
	~Map()
	{
		delete[] country;
	}

	//let city with number numberOfCity be capital of new country
	void setCapital(int numberOfCity)
	{
		if (country[numberOfCity] == 0)
			country[numberOfCity] = nextCapitalKey++;
		else
			throw std::out_of_range("reset Capital");
	}

	//add nearest city to Capital
	bool addToCapital(unsigned int numberOfCapital)
	{
		int currentNearest = -1;
		int currentMinDistance = INT_MAX;
		for (unsigned int i = 0; i < matrixSize; ++i)
		{
			if (country[numberOfCapital] == country[i])
			{
				for (unsigned int j = 0; j < matrixSize; ++j)
				{
					if (j != i && country[i] == 0)
					{
						if (matrix[i][j] < currentMinDistance)
						{
							currentMinDistance = matrix[i][j];
							currentNearest = j;
						}
					}

				}
			}
		}
		if (currentNearest >= 0)
			country[currentNearest] = country[numberOfCapital];
		return currentNearest >= 0 ? 1 : 0;
	}

private:
	unsigned int nextCapitalKey;
	unsigned int* country;
};

int getNext(std::ifstream& f);
bool hasNext(std::ifstream& f);

Map*& fillMatrix(std::ifstream& input)
{
	int numberOfCities = 0;
	if (hasNext(input))
		numberOfCities = getNext(input);
	else 
		throw std::bad_exception("wrong input");
	Map* map = new Map(numberOfCities);
	int numberOfRoads;
	if (hasNext(input))
		numberOfRoads = getNext(input);
	else 
		throw std::bad_exception("wrong input");
	for (int i = 0; i < numberOfRoads; ++i)
	{
		int firstCity = 0;
		int secondCity = 0;
		int length = 0;
		if (hasNext(input))
			firstCity = getNext(input);
		else 
			throw std::bad_exception("wrong input");
		if (hasNext(input))
			secondCity = getNext(input);
		else 
			throw std::bad_exception("wrong input");
		if (hasNext(input))
			length = getNext(input);
		else 
			throw std::bad_exception("wrong input");
		map->setLink(firstCity, secondCity, length);
	}
	return map;
}

int main(int argc, char* argv[])
{
	const char* inpPath = "input.txt";
	std::ios_base::openmode mode = std::ios_base::in;
	std::ifstream input (inpPath, mode);
	
	Map* map = fillMatrix(input);

	int numberOfCapitals = 0;
	if (hasNext(input))
		numberOfCapitals = getNext(input);
	else 
		throw std::bad_exception("wrong input");
	for (int i = 0; i < numberOfCapitals; ++i)
	{
		if (hasNext(input))
			map->setCapital(getNext(input));
		else 
			throw std::bad_exception("wrong input");
	}
	bool hasClearCities = true;
	int capitalNumber = 0;
	while (hasClearCities)
	{
		hasClearCities = map->addToCapital(capitalNumber);
		if (++capitalNumber >= numberOfCapitals)
			capitalNumber = 0;
	}
	map->printMatrix();
	delete map;
	return 0;
}

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