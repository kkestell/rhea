grammar Rhea;

program
    : function+
    ;

function
    : 'fun' name '(' argumentList ')' '->' type block
    ;

argumentList
    : argument? | argumentList ',' argument
    ;

argument
    : name ':' type
    ;

block
    : '{' statement* '}'
    ;

statement
	: variableInitialization
    | variableDeclaration
	| returnStatement
	;

expression
   : '(' expression ')'                              # parensExpression
   | op=('+' | '-') expression                       # unaryExpression
   | left=expression op=('*' | '/') right=expression # infixExpression
   | left=expression op=('+' | '-') right=expression # infixExpression
   | value=atom                                      # valueExpression
   ;

atom
   : number
   | variable
   ;

number
   : value=SCIENTIFIC_NUMBER
   ;

variable
   : value=ID
   ;

returnStatement
	: 'return' expression
	;

variableInitialization
	: 'var' name ':' type '=' expression
	;

variableDeclaration
	: 'var' name ':' type
	;

STRING
    : '"' (ESC | ~('\\'|'"'))* '"'
    ;

ESC
    : '\\' ('n' | 'r')
    ;

name
    : '&'? ID
    ;

type
    : '^'? ID
    ;

ID
    : [a-zA-Z][a-zA-Z0-9]*
    ;

SCIENTIFIC_NUMBER
   : NUMBER (E SIGN? NUMBER)?
   ;

fragment NUMBER
   : ('0' .. '9') + ('.' ('0' .. '9') +)?
   ;

fragment E
   : 'E' | 'e'
   ;

fragment SIGN
   : ('+' | '-')
   ;

LPAREN
   : '('
   ;

RPAREN
   : ')'
   ;

OP_ADD
   : '+'
   ;

OP_SUB
   : '-'
   ;

OP_MUL
   : '*'
   ;

OP_DIV
   : '/'
   ;

GT
   : '>'
   ;

LT
   : '<'
   ;

EQ
   : '='
   ;

POINT
   : '.'
   ;

POW
   : '^'
   ;

WS
    : [ \t\r\n]+ -> skip
    ;
