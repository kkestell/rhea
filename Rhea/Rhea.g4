grammar Rhea;

program
    : function+
    ;

function
    : 'fun' name '(' parameterList ')' '->' type block
    ;

parameterList
    : parameter? | parameterList ',' parameter
    ;

parameter
    : name ':' type
    ;

block
    : '{' statements+=statement* '}'
    ;

statement
    : 'var' name ':' type '=' expression # variableInitialization
	| 'var' name ':' type # variableDeclaration
	| 'return' expression # returnStatement
	| 'if' '(' expression ')' block # ifStatement
	;

expression
   : '(' expression ')' # parensExpression
   | op=('+' | '-') expression # unaryExpression
   | left=expression op=('*' | '/') right=expression # infixExpression
   | left=expression op=('+' | '-') right=expression # infixExpression
   | left=expression op=('==' | '!=') right=expression # infixExpression
   | functionName=ID '(' arguments=argumentList ')' # functionCall
   | value=atom # valueExpression
   ;

argumentList
    : argument? | argumentList ',' argument
    ;

argument
    : expression
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

STRING
    : '"' (ESC | ~( '\\' | '"' ))* '"'
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

OP_EQ
	: '=='
	;
WS
    : [ \t\r\n]+ -> skip
    ;
