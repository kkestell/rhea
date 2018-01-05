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
    : '{' expression* '}'
    ;

expression
    : '(' expression ')'
    | assignment
    | ID
    | INTEGER
    ;

assignment
    : name '=' expression
    ;

name
	: ID
	;

type
	: ID
	;

ID
    : [a-z][a-z0-9]*
    ;

INTEGER
    : [0-9]+
    ;

WS
    : [ \t\r\n]+ -> skip
    ;
