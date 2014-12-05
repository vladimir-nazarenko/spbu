import static org.junit.Assert.*;

import org.junit.Before;
import org.junit.Test;

public class BSTTests {

	@Before
	public void setUp() throws Exception {
	}

	@Test
	public void testIterator() {
		int counter = 0;
		int sum = 0;
		for(int i: createTree()) {
			counter++;
			sum += i;
		}
		assert(counter == 8);
		assert(sum == 92);
	}

	@Test
	public void testRemove() {
		BST<Integer> tree = createTree();
		assertTrue(tree.find(42));
		tree.remove(42);
		assertFalse(tree.find(42));
		tree = new BST<Integer>();
		tree.add(7);
		assertTrue(tree.find(7));
		tree.remove(7);
		assertFalse(tree.find(7));
		tree = new BST<Integer>();
		tree.add(7);
		tree.remove(7);
	}
	
	@Test
	public void emptyRemove() {
		BST<Integer> tree = new BST<Integer>();
		tree.remove(5);
	}

	@Test
	public void testFind() {
		assertTrue(createTree().find(42));
	}

	@Test
	public void testAdd() {
		BST<Integer> tree = new BST<Integer>();
		tree.add(5);
		tree.add(2);
		tree.add(42);
		assert(tree.value() == 5);
		assert(tree.left().value() == 2);
		assert(tree.right().value() == 42);
	}

	@Test
	public void testLeft() {
		assert(createTree().left() != null);
	}

	@Test
	public void testRight() {
		assert(createTree().right() != null);
	}
	
	@Test
	public void testValue() {
		assert(createTree().value() == 5);
	}		
	
	private BST<Integer> createTree() {
		BST<Integer> tree = new BST<Integer>();
			tree.add(5);
			tree.add(9);
			tree.add(17);
			tree.add(6);
			tree.add(3);
			tree.add(8);
			tree.add(2);
			tree.add(42);
		return tree;
	}

}

