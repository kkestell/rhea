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
    : name
    | literal
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

literal
	: integerLiteral
	| stringLiteral
	;

stringLiteral
	: STRING
	;

STRING
    : '"' (ESC | ~('\\'|'"'))* '"'
    ;

ESC
    : '\\' ('n' | 'r')
    ;

integerLiteral
    : INTEGER
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

INTEGER
    : [0-9]+
    ;

WS
    : [ \t\r\n]+ -> skip
    ;
