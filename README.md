# Calculator #

Group project: team of 2

We create a simple calculator that can perform addition, subtraction, multiplication, division, exponents, square roots, sine, cosine, and tangent. The calculator is a command line tool and the input must follow very strict rules. To solve the equations, we implemented an algorithm known as Djikstra's Two-Stack Algorithm.

There are several rules that users of your calculator must follow. If they input an equation that does not follow the rules, then the calculator will not work:

1. The calculator will accept a mathematical equation as a set of command line arguments.
2. The equation cannot contain any variables such as "x" or "y"
3. Each "token" in the input must be separated by a space. Tokens include parenthesis, numbers, and the operators.
4. The operators include +, -, *, /, ** (exponent), sqrt, sin, cos, and tan
5. Every operation must be enclosed with parenthesis. This means if there are three operators, there will be three sets of parenthesis (see the screenshot).

I have used my own version of the MyStack and MyQueue libraries, which have been designed to use the same API as the .NET versions.
