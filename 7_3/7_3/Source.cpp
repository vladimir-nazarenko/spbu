#include <iostream>
#include <fstream>

enum TokenType {num, operation};

//notion of a tree
struct Node
{
	Node* left;
	Node* right;
	char* value;
	TokenType type;
	Node() : left(nullptr), right(nullptr) {}
	~Node() 
	{
		if (!left)
			delete left;
		if (!right)
			delete right;
		delete[] value;
	}
};

char* getNext(const char* expression);
int getLen(const char* expression);
bool isOperation(const char* str);
int count(Node*& tree);
void parse(Node*& node, char* expression);
void getStringTree(Node*& tree, char*& destination);



int main(int argc, char* argv[])
{
	const char* inpPath = "input.txt";
	std::ios_base::openmode mode = std::ios_base::in;
	std::ifstream input (inpPath, mode);
	const int bufSize = 80;
	char* str = new char[bufSize];
	int write = 0;
	char current = input.get();
	while (!input.eof())
	{
		if (current != '\n')
			str[write++] = current;
		current = input.get();
	}
	str[write] = 0;
	input.close();
	Node* head = new Node();
	char* result = new char[bufSize]();
	parse(head, str);
	getStringTree(head, result);
	std::cout << "tree: " << result << std::endl << "result of calculation " << count(head);
	std::cin.get();
	return 0;
}

//get next operator or number from string
char* getNext(const char* expression)
{
	static int read;
	int write = 0;
	const size_t bSize = 4;
	char* buf = new char[bSize]();
	bool itsOK = true;
	if (read < getLen(expression))
		while (itsOK)
		{
			switch (expression[read])
			{
				case '(':
					++read;
					break;
				case ')':
					++read;
					if (buf[0] != 0)
						return buf;
					break;
				case ' ':
					++read;
					if (buf[0] != 0)
						return buf;
					break;
				case '+':
				case '-':
				case '*':
				case '/':
				case '0':
				case '1':
				case '2':
				case '3':
				case '4':
				case '5':
				case '6':
				case '7':
				case '8':
				case '9':
					buf[write++] = expression[read];
					++read;
			}
		}
	return nullptr;
}

int getLen(const char* expression)
{
	int len =  0;
	while (expression[len++] != '\0');
	return len - 1;
}

//check if string is in set {+, -, *, /}
bool isOperation(const char* str)
{
	if (getLen(str) == 1)
		if (*str == '+' || *str == '-' || *str == '*' || *str == '/')
			return true;
	return false;
}

//build tree with head node on data of expression
void parse(Node*& node, char* expression)
{
	char* command = getNext(expression);
	if (isOperation(command))
	{
		node->value = command;
		node->left = new Node();
		node->right = new Node();
		node->type = TokenType::operation;
		parse(node->left, expression);
		parse(node->right, expression);
	}
	else
	{
		node->value = command;
		node->type = TokenType::num;
	}
}

//rebuild string notion of a tree in destination with tree from memory
void getStringTree(Node*& tree, char*& destination)
{
	static int write = 0;
	if (tree->type == TokenType::operation)
	{
		destination[write++] = '(';
		destination[write++] = *tree->value;
		if (tree->left)
			getStringTree(tree->left, destination);
		if (tree->right)
			getStringTree(tree->right, destination);
		destination[write++] = ')';
		destination[write] = '\0';
	}
	else
	{
		destination[write++] = *tree->value;
	}
}

//return result of calculation an arithemitical tree from memory
int count(Node*& tree)
{
	if (tree->type == TokenType::operation)
	{
		switch (tree->value[0])
		{
		case '+':
			return count(tree->left) + count(tree->right);
			break;
		case '*':
			return count(tree->left) * count(tree->right);
			break;
		case '-':
			return count(tree->left) - count(tree->right);
			break;
		case '/':
			return count(tree->left) / count(tree->right);
			break;
		}
	}
	else
	{
		return atoi(tree->value);
	}
	return ~0;
}
