# Rhea

Rhea is a simple, type safe, partially type inferred, garbage collected programming language which compiles to C.

## Example

```
fun fib(n : int64) -> int64 {
  if (n == 0) {
    return 0
  }
  if (n == 1) {
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
extern false for fun if return struct true var
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

### Strings

Strings have a type of `string` and are always allocated on the heap.

Strings are technically structs. The Rhea standard library provides a number of methods to make working with strings easier.

#### `string#dup() -> string`

Duplicates a string.


```
var duplicate = "Hello World".dup()
```

#### `string#trim() -> string`

Trims leading and trailing whitespace from a string.

```
var trimmed = "  Hello World  ".trim()
```

#### `string#length() -> int64`

Returns the length of a string.

```
var length = "Hello World".length()
```

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
var name : string
```

This creates a new string, `name`, in the current scope.

Variables can also be initialized when they're created. In this case, their type can be inferred:

```
var name = "Glarpy"
```

## Control Flow

### If Statements

```
if (x == 0) {
  # ...
}
```

### For Statements

#### Ranges

```
for (var x in 0..10) {
  # ...
}
```

## Structs

### Declaration

```
struct point {
  x : int64
  y : int64
}
```

### Member Access

```
var origin : point
point.x = 1
```

## Calling C from Rhea

Use the `extern` keyword.

```
extern fun some_c_function() -> int32

fun main() -> int32 {
  return some_c_function()
}
```
