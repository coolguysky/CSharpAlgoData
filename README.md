# PERSONAL REFERENCE

## C# Algorithms and Data Structures - Review

### Data Structures

| TIME | get | find | add | remove | *worst space* |
| :---: | :---: | :---: | :---: | :---: | :---: |
| Array | O(1) | O(n) | O(n) | O(n) | *O(n)* |
| Singly-Linked List | O(n) | O(n) | O(1) | O(1) | *O(n)* |
| Doubly-Linked List | O(n) | O(n) | O(1) | O(1) | *O(n)* |
| Stack | O(n) | O(n) | O(1) | O(1) | *O(n)* |
| Queue | O(n) | O(n) | O(1) | O(1) | *O(n)* |
| Hash Table | n/a | O(1) | O(1) | O(1) | *O(n)* |
| Dictionary | n/a | O(1) | O(1) | O(1) | *O(n)* |
| Sorted Dictionary | n/a | O(log n~n) | O(log n~n) | O(log n~n) | *O(n)* |
| Hash Set | n/a | O(1) | O(1) | O(1) | *O(n)* |
| Sorted Set | n/a | O(log n~n) | O(log n~n) | O(log n~n) | *O(n)* |
| AVL* | O(log n) | O(log n) | O(log n) | O(log n) | *O(n)* |
| Red-Black Tree | O(log n) | O(log n) | O(log n) | O(log n) | *O(n)* |


*need to find good AVL C# implementation 

### Sorting Algorithms

| TIME | best | average | worst | *worst space* |
| :---: | :---: | :---: | :---: | :---: |
| Selection Sort | Ω(n^2) | Θ(n^2) | O(n^2) | *O(1)* |
| Insertion Sort | Ω(n) | Θ(n^2) | O(n^2) | *O(1)* |
| Bubble Sort | Ω(n) | Θ(n^2) | O(n^2) | *O(1)* |
| Quicksort | Ω(n log n) | Θ(n log n) | O(n^2) | *O(log n)* |
| Heapsort | Ω(n * log n) | Θ(n * log n) | O(n * log n) | *O(n)* |

### O - Big Oh (worst): f(n) = O(g(n))
- f(n) = O(g(n)) if and only if there exist a positive constant, c and a positive integer, n0 , such that 0<= f(n)<= c*g(n) for all n, n>= n0
- g(n) is the Upper bound of f(n)
### Ω - Omega (best) - f(n) = Ω(g(n))
- if and only if there exist a positive constant c and a positive integer n0 such that f(n) >= f(n) = Ω(g(n)) ~ f(n) >= c*g(n) >=0 for all n, n >= n0 
- g(n) is Lower bound of f(n)
### Θ - Theta (average): f(n) = Θ(g(n))
- f(n) = Θ(g(n)) if and only if there exist positive constants c1 and c2 and a positive integer, n 0 , such that 0<= c1* g(n) <= f(n) <=c2 * g(n) for all n, n >= n 0
- g(n) is both an upper and a lower bound of f(n)

Textbook: C# Data Structures and Algorithms by Marcin Jamro

bigocheatsheet.com

http://geekswithblogs.net/BlackRabbitCoder/archive/2011/06/16/c.net-fundamentals-choosing-the-right-collection-class.aspx
