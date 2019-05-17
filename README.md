# monkey-lang-vm

A [C#](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/)
/ [.NET Core](https://docs.microsoft.com/en-us/dotnet/core/about)
implementation of C-like language based on excellent
[Writing A Compiler In Go](https://compilerbook.com/) book.

## How To Install
0. Download and install [.NET Core](https://docs.microsoft.com/en-us/dotnet/core/)
1. Open Terminal
2. `$ git clone git@github.com:michalholasek/monkey-lang-vm.git`
3. `$ dotnet publish ./src/Monkey.Repl/Monkey.Repl.csproj -o ../../bin`

## How To Run
1. Open Terminal
2. `$ cd <monkey-lang-vm directory>`
2. `$ dotnet ./bin/Monkey.Repl.dll`

## REPL
```
Welcome to monkey-lang REPL!
> 5
5
> let let = 5;
unexpected token: let let<-- = 5;
> quit
Exiting monkey-lang REPL...
```

## Features
- C-like syntax
- Integer, boolean, and string primitive data types
- Array and hashtable support
- Variable bindings
- Let and Return statements
- If-Else conditionals
- Basic arithmetic for integer expressions
- First class and higher-order functions

### Types
| Type     | Examples                    |
|----------| ----------------------------|
|`int`     | `0`, `123`,  `-29`          |
|`boolean` | `true`, `false`             |
|`string`  | `"Astralis"`                |
|`array`   | `[]`, `[1, 2, 3]`, `[fn (x) { return x; }, [], ""]` |
|`hash`    | `{}`, `{ "key": "value" }`, `{ false: true, 1: "Yes!" }` |

### Variable Bindings
```
> let five = 5;
> five
5
```

### Integer Arithmetics
```
> let ten = 5 + 10 - 5;
> let eleven = (5 * 2) + 1;
> ten
10
> eleven
11
```

### Let and Return Statements
```
> let one = 1;
> one
1
> return 42;
42
```

### If-Else Conditionals
```
> if (1 < 2) {
    return true;
  } else {
    return "Alternative.";
  }
true
```

### Functions
```
> let returnsOne = fn() { 1; };
> let returnsOneReturner = fn() { returnsOne; };
> returnsOneReturner()();
1
> let identity = fn(a) { return a; };
> identity(42);
42
```

## License
MIT
