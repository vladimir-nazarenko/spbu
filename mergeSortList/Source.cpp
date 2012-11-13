#include "List.h"
#include "Merge.h"

int main()
{
	List<int>* l = new List<int>(2);
	l->insert(1, l->first());
	l = mergeList(l);
	return 0;
}