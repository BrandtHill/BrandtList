# BrandtList

This is my implementation of ICollection<T> for fun. 
Its underlying data structure is an array of structs that contain a generic object and a boolean. The boolean represents if the current cell is in use or not.
The order invisible to the user, and the implementation aims to be super space efficient: 
If an item (generic) is removed from the list, that cell's boolean 'InUse' flag is set to false, and thus the cell would be used before any other empty cell of a higher index. 
The list dynamically grows. Maybe I'll make it dynamically shrink if the count is significantly less than the current size.

## Tests
Here are the results from the tests, which are included in the repo as a runnable command line program. They don't go over performance, they just show that it works:
```
-----------------TEST 1: 60 integers 0-9, mostly small values------------------

0 0 1 1 1 2 4 6 6 2
0 0 0 1 3 4 1 4 4 1
0 0 0 2 1 0 0 1 5 6
0 0 1 0 1 3 4 6 2 4
0 0 0 2 3 2 1 3 0 3
0 0 0 1 3 2 0 6 0 3

Count................. counted: 60

ContainsHowMany(0)... contains: 22
RemoveAll(0).......... removed: 22
ContainsHowMany(0)... contains: 0

ContainsHowMany(2)... contains: 7
RemoveAll(2).......... removed: 7
ContainsHowMany(2)... contains: 0

Count................. counted: 31

1 1 1 4 6 6 1 3 4 1 4 4 1 1 1 5 6 1 1 3 4 6 4 3 1 3 3 1 3 6 3



-------------------TEST 2: 100 chars 'a'-'g'------------------

c c b a g f f a a a
b b c a b g c d g g
f d b c g b f c d f
d g f g b e a b f a
e a c a b e e c c g
b e g a d f b g b f
a c e c d b c d g e
f a f g c f a b b b
e c a e e g g f d c
c a a a g b d a d g

Count................... counted: 100

ContainsHowMany('g')... contains: 16
RemoveAll('g').......... removed: 16
ContainsHowMany('g')... contains: 0

ContainsHowMany('a')... contains: 18
RemoveAll('a').......... removed: 18
ContainsHowMany('a')... contains: 0

ContainsHowMany('e')... contains: 10
RemoveAll('e').......... removed: 10
ContainsHowMany('e')... contains: 0

Count................... counted: 56

c c b f f b b c b c d f d b c b f c d f d f b b f c b c c b d f b b f c c d b c d f f c f b b b c f d c c b d d
```