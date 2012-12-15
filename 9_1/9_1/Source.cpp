#include <iostream>

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
	}

	void deleteDoubleLink(unsigned int first, unsigned int second)
	{
		matrix[first][second] = 0;
		matrix[second][first] = 0;
	}

	int getDistance(unsigned int first, unsigned int second)
	{
		return matrix[first][second];
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

	void setCapital(int numberOfCity)
	{
		if (country[numberOfCity] == 0)
			country[numberOfCity] = nextCapitalKey++;
		else
			throw std::out_of_range("reset Capital");
	}

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

int main(int argc, char* argv[])
{
	Map* m = new Map(5);
	return 0;
}