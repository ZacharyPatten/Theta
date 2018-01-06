>- [Home](https://github.com/53V3N1X/SevenFramework/wiki)<br />
>  - [Structures](https://github.com/53V3N1X/SevenFramework/wiki/Structures)<br />
>     - Structure

##### Summary

>Structure is the polymorphism base of all data structures in the Theta framework.
>A structure simply represents a storage object for multiple instances of a given 
>type. The fundamental property of all structures is that they can be traversed.
>
>In the .NET Framework, the IEnumerable interface is the base of all traversable
>objects. IEnumerable works, but there is a better way to implement traversable objects
>than using the IEnumerable interface. That way is delegate traversals. Structure, from 
>the Theta framework, includes the method "Foreach" which enforces that every structure 
>must contain a Foreach method. The Foreach methods used in the Theta framework achieves 
>the same affect as using the foreach keyword (type of structure can be switch without 
>re-writing loops), but with the use of delegate traversals, it tends to be much faster 
>than IEnumerable patterns and does not require extra memory (for example: tree 
>traversal requires a stack for IEnumerable patterns).

##### Functions

>* `int SizeOf`<br />
>The current allocation size of the structure.
>* `void Foreach(Foreach<T> function)`<br />
>Invokes a delegate for each entry in the data structure.
>* `void Foreach(ForeachRef<T> function)`<br />
>Invokes a delegate for each entry in the data structure.
>* `ForeachStatus Foreach(ForeachBreak<T> function)`<br />
>Invokes a delegate for each entry in the data structure.
>* `ForeachStatus Foreach(ForeachRefBreak<T> function)`<br />
>Invokes a delegate for each entry in the data structure.
>* `Structure<T> Clone()`<br />
>Creates a shallow clone of this data structure.
>* `Type[] ToArray()`<br />
>Converts the structure into an array.

##### Polymorphism Tree

> - [Structure](https://github.com/53V3N1X/SevenFramework/wiki/Structure)
>   - [Array](https://github.com/53V3N1X/SevenFramework/wiki/Array)
>   - [List](https://github.com/53V3N1X/SevenFramework/wiki/List)<br />
>   - Stack
>   - Queue
>   - Heap
>   - HashTable
>   - AvlTree
>   - ReadBlackTree
>   - Quadtree
>   - Octree
