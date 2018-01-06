>- [Home](https://github.com/53V3N1X/SevenFramework/wiki)<br />
>  - [Structures](https://github.com/53V3N1X/SevenFramework/wiki/Structures)<br />
>    - List

##### Summary

>A list is the most primitive data structure with dynamic size. It can be
>added to or removed from without worry of reaching the end of allocated
>memory (as oposed to an array).

##### Functions

>All "List" implementations contain the collowing:
>* `void Add(Type addition)` <br />
>Adds an item to the end of the list.
>* `void RemoveFirst(Type removal, Compare<T> compare)` <br />
>Removes the first occurrence of an item in the list.
>* `bool TryRemoveFirst(Type removal, Compare<T> compare)` <br />
>Removes the first occurrence of an item in the list or returns false.
>* `void RemoveAll(Type removal, Compare<T> compare)` <br />
>Removes all occurrences of an item in the list.
>* `int Count` <br />
>Returns the number of items in the list.
>* `bool IsEmpty` <br />
>Returns true if (Count == 0).
>* `void Clear()` <br />
>Resets the list to an empty state.
>* _Every function from_ `Structure`

##### Polymorphism Tree

>- [Structure](https://github.com/53V3N1X/SevenFramework/wiki/Structure)
>  - List
>     - List_Array
>     - List_Linked

##### Implementations

>**List_Array**
>
>Visual: | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
>
>This implementation of a list uses an array. It resizes to double its current
>size each time adding to it would otherwise cause an IndexOutOfBounds exception.
>It resizes to half its current size each time removing an element would result
>in the count being a fourth of its current size. The resizing can be controlled
>by setting a "MinimumCapacity." The minimum capacity will prevent downsizing of
>the list, but if use incorrectly could cause large, unused memory allocations.
>
>**List_Linked**
>
>Visual: 0 -> 1 -> 2 -> 3 -> 4 -> 5 -> 6 -> 7
>
>This implementation of a list uses nodes that are allocated each time an item
>is added to the list. Because it does not require re-allocations, it can be
>multi-threaded without the overhead of reader-writer locking.

##### Usage

>Lists are very basic data structures, and they are often used when another data 
>structure should be chosen instead. A list should be used when:
>
>- processing unkown amounts of data
>- all the data will have to be processed every usage
>- the data can't/shouldn't be sorted

##### Runtimes and Memory

>See in-code xml documentations.
