import java.util.Iterator;
import java.lang.Comparable;

public class TreeIterator<T extends Comparable<T>> implements Iterator<T> 
{
	public TreeIterator(BST<T> tree) {
		_hasNext = tree != null;
		_tree = tree;
		if (hasLeft(_tree))
			_leftIterator = _tree.left().iterator();
		if (hasRight(_tree))
			_rightIterator = _tree.right().iterator();
	}
	
	public T next() {
		if (hasLeft(_tree))
			if (_leftIterator.hasNext()) {
				return _leftIterator.next();
			}
		if (hasRight(_tree))
			if (_rightIterator.hasNext()) {
				return _rightIterator.next();
			}
		_hasNext = false;
		return _tree.value();
	}
	
	public boolean hasNext() {
		return _hasNext;
	}
	
	public void remove() {
		throw new Error("Not implemented.");
	}
	
	private boolean hasLeft(BST<T> tree)
	{
		return tree.left() != null;
	}
	
	private boolean hasRight(BST<T> tree)
	{
		return tree.right() != null;
	}
	
	private boolean _hasNext;
	private BST<T> _tree;
	private Iterator<T> _leftIterator;
	private Iterator<T> _rightIterator;
}
