>- [Home](https://github.com/53V3N1X/SevenFramework/wiki)<br />
>  - [Structures](https://github.com/53V3N1X/SevenFramework/wiki/Structures)<br />
>    - Omni-Tree

##### Summary

>An Omni-Tree is a spacial partitioning data structure that can sort items along "N"
>dimensions. It is a generalization of the Quad-Tree and Oct-Tree data structures.
>Items are sorted into spacial nodes. Comparisons occur between and item and a node
>rather than between two items. This allows for fast grouping of items within close 
>proximity.

##### Functions

>`event Foreach<T> HandleOutOfBounds`;
>Event for handling items that go outside the bounds of the Omnitree. 
>Asigning this event will trigger the delegate instead of throwing an exception.
>`M[] Origin { get; }`
>Gets the dimensions of the center point of the Omnitree.
>`M[] Min { get; }`
>The minimum dimensions of the Omnitree.
>`M[] Max { get; }`
>The maximum dimensions of the Omnitree.
>`Compare<M> Compare { get; }`
>The compare function the Omnitree is using.
>`Omnitree.Locate<T, M> Locate { get; }`
>The location function the Omnitree is using.
>`Omnitree.Average<M> Average { get; }`
>The average function the Omnitree is using.
>`int Dimensions { get; }`
>The number of dimensions in this tree.
>`int Count { get; }`
>The current number of items in the tree.
>`bool IsEmpty { get; }`
>True (if Count == 0).
>`void Add(T addition)`
>Adds an item to the tree.
>`void Update()`
>Iterates through the entire tree and ensures each item is in the proper leaf.
>`void Update(M[] min, M[] max)`
>Iterates through the provided dimensions and ensures each item is in the proper leaf.
>`void Remove(M[] min, M[] max)`
>Removes all the items in a given space.
>`void Remove(M[] min, M[] max, Predicate<T> where)`
>Removes all the items in a given space where a predicate is met.
>`void Foreach(Foreach<T> function, M[] min, M[] max)`
>Performs and specialized traversal of the structure and performs a delegate on every node within the provided dimensions.
>`void Foreach(ForeachRef<T> function, M[] min, M[] max)`
>Performs and specialized traversal of the structure and performs a delegate on every node within the provided dimensions.
>`ForeachStatus Foreach(ForeachBreak<T> function, M[] min, M[] max)`
>Performs and specialized traversal of the structure and performs a delegate on every node within the provided dimensions.
>`ForeachStatus Foreach(ForeachRefBreak<T> function, M[] min, M[] max)`
>Performs and specialized traversal of the structure and performs a delegate on every node within the provided dimensions.
>`void Clear()`
>Returns the tree to an empty state.

##### Polymorphism Tree

>- [Structure](https://github.com/53V3N1X/SevenFramework/wiki/Structure)
>  - Omnitree
>     - Omnitree_Array
>     - Omnitree_Linked

##### Usage

>The Omnitree is a very good choice when you have items that will need to be
>accessed by various fields. For example, if you have a class "class Person"
>that has first name, last name, and date of birth, an Omnitree would allow you
>to quickly access people by either first name, last name, or date of birth (as
>opposed to a standard binary tree that only sorts along one dimension).
>
>Another common spacial partitioning data structure is an R-Tree. The R-Tree has
>generally faster look-up times, but the Omni-Tree will generally have much faster
>additions and removals.

##### Runtimes and Memory

>See in-code xml documentations.