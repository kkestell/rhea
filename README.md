# Rhea

Rhea is a simple, type safe programming language which compiles to C.

## Example

```
fun fib(n : int64) -> int64 {
  if(n == 0) {
    return 0
  }
  if(n == 1) {
    return 1
  }
  return fib(n - 2) + fib(n - 1)
}

fun main() -> int64 {
  return fib(10)
}
```

## Syntax

### Comments

Line comments start with `#` and end at the end of the line:

```
# This is a comment.
```

### Reserved Words

```
fun if return var true false
```

### Identifiers

Identifiers begin with a letter and may contain letters, numbers, and underscores.

### Blocks

Rhea uses curly braces to define blocks. You can use a block anywhere a statement is allowed, like in control flow statements. Function bodies are also blocks.

### Precedence and Associativity

| Precidence | Operator          | Description              | Associates |
|------------|-------------------|--------------------------|------------|
| 1          | `()`              | Grouping                 | Left       |
| 2          | `-`               | Negate                   | Right      |
| 3          | `*` `/` `%`       | Multiply, Divide, Modulo | Left       |
| 4          | `+` `-`           | Add, Subtract            | Left       |
| 5          | `<` `<=` `>` `>=` | Comparison               | Left       |
| 6          | `==` `!=`         | Equal, Not Equal         | Left       |

## Values

### Booleans

A boolean value represents truth or falsehood. There are two boolean literals, `true` and `false`. 

### Numbers

Rhea has eight integer types. Four signed: `int8`, `int16`, `int32`, and `int64`, and four unsigned: `uint8`, `uint16`, `uint32`, and `uint64`.

There are two floating point types: `float32`, and `float64`.

## Functions

Function are declared using the `fun` keyword:

```
fun foo(int32 : bar) -> int64 {
  # ...
}
```

This creates a new function which accepts a 32-bit integer as an argument and returns a 64-bit integer.

There is no need to declare functions before calling them.

## Variables

Variables are declared using the `var` keyword:

```
var x : int64 = 100
```

This creates a new variable `x` in the current scope and initializes it with the result of the expression following the `=`.
