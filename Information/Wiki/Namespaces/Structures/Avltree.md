>- [Home](https://github.com/53V3N1X/SevenFramework/wiki)<br />
>  - [Structures](https://github.com/53V3N1X/SevenFramework/wiki/Structures)<br />
>    - Avl Tree

##### Summary

>An AVL tree is a sorted binary tree. It keeps itself sorted by rotating its branches
>as items are added or removed. When to rotate a branch is determined by analysing the
>difference in height between nodes. There are only four necessary manipulations of an
>AVL tree: rotate right, rotate left, double rotate right, and double rotate left. See
>the source code for more specific details on how it keeps itself sorted.
>
>The most important property of an AVL tree is how it is sorted:
>* the left child (and all its children) of any node contains a lesser value than its parent
>* the right child (and all its children) of any node contains a greater value than its parent

##### Functions

>* `void Add(Type addition)`<br />
>Adds an item to the AVL tree.
>* `bool TryAdd(Type addition)`<br />
>Tries to add an item to the tree. If the item exists returns false.
>* `void Remove(Type removal)`<br />
>Removes an item from the AVL Tree
>* `bool TryRemove(Type removal)`<br />
>Tries to remove an item to the tree. If the item doesn't exist returns false.
>* `int Count`<br />
>The number of items currently in the tree.
>* `bool IsEmpty`<br />
>Returns true if (Count == 0).
>* `void Clear()`<br />
>Returns the tree to an empty state.
>* `bool Contains<Key>(Key key, Compare<Type, Key> comparison)`<br />
>Checks for an occurence in the tree. (Note: fast for set theory)
>* `Type Get<Key>(Key get, Compare<Type, Key> comparison)`<br />
>Does a look up in the tree.
>* `bool TryGet<Key>(Key get, Compare<Type, Key> comparison, out Type item)`<br />
>Tries to do a look up in the tree. Returns false if does not exist.
>* `void Remove<Key>(Key removal, Compare<Type, Key> comparison)`<br />
>Removes an item from the tree base on a key.
>* `bool TryRemove<Key>(Key removal, Compare<Type, Key> comparison)`<br />
>Tries to remove an item from the tree. Returns false if does not exits.
>* `void Foreach(Foreach<T> function, Type minimum, Type maximum)`<br />
>Foreach loop that takes advantage of the AvlTree structure by culling.
>* `void Foreach(ForeachRef<T> function, Type minimum, Type maximum)`<br />
>Foreach loop that takes advantage of the AvlTree structure by culling.
>* `ForeachStatus Foreach(ForeachBreak<T> function, Type minimum, Type maximum)`<br />
>Foreach loop that takes advantage of the AvlTree structure by culling.
>* `ForeachStatus Foreach(ForeachRefBreak<T> function, Type minimum, Type maximum)`<br />
>Foreach loop that takes advantage of the AvlTree structure by culling.
>* _Every function from_ `Structure`

##### Polymorphism Tree

>- [Structure](https://github.com/53V3N1X/SevenFramework/wiki/Structure)
>  - Avltree
>     - Avltree_Array
>     - Avltree_Linked

##### Implementations

>**Avltree_Linked**
>
>An AVL tree represented in a typical node structure through recursive
>techniques.
>
>**Avltree_Array**
>
>Still in development. Sorry.

##### Usage

>AVL trees are sorted in a way that provides relatively quick look ups.
>Avl trees never have to re-hash their structure as with hash-tables, but
>Hash tables have faster average look up times. Thus, with a constantly
>changing size of data, AVL trees should be generally favored over hash-tables.
>Also, the structure of an AVL tree provides fast functionality
>for getting all the items between a minimum and maximum value along which 
>the tree is sorted.
>
>So, use an AVL tree for:
>* look-ups in very dynamic data sets
>* getting all values in specific ranges (minimum-maximum)

##### Runtimes and Memory

>See in-code xml documentations.