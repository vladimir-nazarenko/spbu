
template <typename T>
class BST
{	
public:
	struct Node
	{
		T item;
		Node* left;
		Node* right;
		Node(T value) : left(nullptr), right(nullptr) {item = value;} 
		Node() : left(nullptr), right(nullptr) {}
	};

	typedef Node* Position;

	BST()
	{
		root = new Node();
		counter = 0;
	}

	~BST()
	{
		while (root != nullptr)
			root = removeNode(root);
	}

	//add element to the tree
	void insert(T value)
	{
		inject(root, value);
		++counter;
	}

	//get pointer to element of tree with needed key
	Position find(T value)
	{
		Position search = root;
		while (search != nullptr && search->item != value)
		{
			if (search->item <= value)
				search = search->right;
			else 
				search = search->left;
		}
		return search;
	}

	//delete item from the tree
	void remove(T toRemove)
	{
		if (counter != 0)
		{
			detach(toRemove, root);
		}
	}

	void traverseUp(void (*action)(T))
	{
		auxTraverseUp(root, action);
	}

	void traverseDown(void (*action)(T))
	{
		auxTraverseDown(root, action);
	}

private:	
	Node* root;
	size_t counter;

	//Recursively add value to tree
	void inject(Node* toAdd, T value)			
	{
		if (counter == 0)
		{
			//empty-tree case
			root = new Node(value);				
			return;
		}
		if (toAdd->item <= value)
			if (toAdd->right != nullptr)
				inject(toAdd->right, value);
			else 
				toAdd->right = new Node(value);
		else 
			if (toAdd->left != nullptr)
				inject(toAdd->left, value);
			else 
				toAdd->left = new Node(value);
	}

	//Recursively delete value from tree
	void detach(T value, Node* tree)			
	{
		if (root->item == value)
		{
			removeNode(root);
			--counter;
			return;
		}
		if (tree->left != nullptr && tree->left->item == value)		
		{
			tree->left = removeNode(tree->left);
			--counter;
			return;
		}
		if (tree->right != nullptr && tree->right->item == value)
		{
			tree->right = removeNode(tree->right);
			--counter;
			return;
		}
		if (tree->item > value)
		{
			if (tree->left != nullptr)
				detach(value, tree->left);
		}
		else
		{
			if (tree->right != nullptr)
				detach(value, tree->right);
		}
		return;
	}

	//exclude Node from tree
	Node* removeNode(Node* toDelete)
	{
		short hasRight = 5 * static_cast<short>(toDelete->right != nullptr);		
		short hasLeft = 3 * static_cast<short>(toDelete->left != nullptr);
		Position temp;
		/*depends on values of hasRight and hasLeft, 
		their sum can be either 0 or 3 or 5 or 8, as far as 
		static_cast<short>(toDelete->right != nullptr) can return either 0 or 1*/
		switch (hasLeft + hasRight)
		{
		case 0:
		//has no childs					
			delete toDelete;
			return nullptr;
			break;
		case 3:	
		//has left child				
			temp = toDelete->left;
			delete toDelete;
			return temp;
			break;
		case 5:
		//has right child					
			temp = toDelete->right;
			delete toDelete;
			return temp;
			break;
		case 8:					
		//has both childs
			temp = toDelete->right;
			while (temp->left != nullptr)
				temp = temp->left;
			toDelete->item = temp->item;
			toDelete->right = removeNode(temp);
			return toDelete;				
			break;
		}
		return 0;
	}

	//traverse to max element with some function
	void auxTraverseUp(Node* node, void (*action)(T))	
	{
		if (node->left != nullptr)
			auxTraverseUp(node->left, action);
		action(node->item);
		if (node->right != nullptr)
			auxTraverseUp(node->right, action);
		return;
	}

	//traverse to max element with some function
	void auxTraverseDown(Node* node, void (*action)(T))	
	{
		if (node->right != nullptr)
			auxTraverseDown(node->right, action);
		action(node->item);
		if (node->left != nullptr)
			auxTraverseDown(node->left, action);
		return;
	}
};