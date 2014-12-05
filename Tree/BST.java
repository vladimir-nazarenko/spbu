import java.lang.Comparable;

public class BST<T extends Comparable<T>> implements Iterable<T> {
	public BST() {
		this(null, null, null);
	}
	
	public BST(T value) {
		this(value, null, null);
	}
	
	public TreeIterator<T> iterator() {
		TreeIterator<T> iter = new TreeIterator<T>(this);
		return iter;
	}
	
	public void remove(T key) {
		if (isEmpty) return;
		if (key.compareTo(_value) == 0) {
			BST<T> newNode = detach();
			if (newNode != null) {
				this._left = newNode.left();
				this._right= newNode.right();
				this._value = newNode.value();
			}
		}
		isEmpty = true;
		if (key.compareTo(_value) < 0) {
			if (left() != null) {
				if (left().value().compareTo(key) == 0)
					_left = left().detach();
				else
					left().remove(key);
			} else {
				return;
			}
		} else { 
			if (key.compareTo(_value) > 0) {
				if (right() != null) {	
					if (right().value().compareTo(key) == 0)
						_right = right().detach();
					else
						right().remove(key);
				} else {
					return;
				}
			}
		}
	}
	
	public boolean find(T key) {
		if (isEmpty) return false;
		if (key.compareTo(_value) < 0) {
			if (left() != null) {
				return left().find(key);
			} else {
				return false;
			}
		} else { 
			if (key.compareTo(_value) > 0) {
				if (right() != null) {	
					return right().find(key);
				} else {
					return false;
				}
			}
		}
		return true;
	}
	
	public void add(T key) {
		if (isEmpty){
			this._value = key;
			isEmpty = false;
		}
		if (_value == null) {
			_value = key;
			return;
		}
		if (key == _value) return;
		if (key.compareTo(_value) < 0) {
			if (left() != null) left().add(key); else _left = new BST<T>(key);
		} else {
			if (right() != null) right().add(key); else _right = new BST<T>(key);
		}
	}
	
	public BST<T> left() {
		return _left;
	}
	
	public BST<T> right() {
		return _right;
	}
	
	public T value() {
		return _value;
	}
	
	private BST<T> detach() {
		if (hasRight()) {
			if (hasLeft()) {
				// has both
				BST<T> rightLeft = right();
				if (!rightLeft.hasLeft()) return rightLeft; 
				while (rightLeft.left().hasLeft()) rightLeft = rightLeft.left();
				BST<T> newNode = new BST<T>(rightLeft.left().value(), left(), rightLeft.left().right());
				rightLeft._left = null;
				return newNode;
			} else {
				// has right
				return right();
			}
		} else {
			if (hasLeft()) {
				// has left
				return left();
			} else {
				// has none
				return null;
			}
		}
		
	}
	
	private boolean hasLeft() {
		return left() != null;
	}
	
	private boolean hasRight() {
		return right() != null;
	}
	
	private BST(T value, BST<T> left, BST<T> right) {
		_left = left;
		_right = right;
		_value = value;
		isEmpty = value == null;
	}
	
	private BST<T> _left;
	private BST<T> _right;
	private T _value;
	private boolean isEmpty;
}
