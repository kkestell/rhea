grammar Rhea;

module
    : (functions+=function | structs+=struct)*
    ;

struct
    : 'struct' structName=ID '{' members+=member+ '}'
    ;

member
    : memberName=ID ':' type
    ;

function
    : 'fun' name '(' parameters+=parameter* (',' parameters+=parameter)* ')' '->' type block
    ;

parameter
    : name ':' type
    ;

block
    : '{' statements+=statement* '}'
    ;

statement
    : 'var' name '=' expression                        # variableInitialization
	| 'var' name ':' type                              # variableDeclaration
	| 'return' expression?                             # returnStatement
	| 'if' '(' expression ')' block                    # ifStatement
	| 'for' '(' 'var' name 'in' range ')' block        # forRange
	| variableName=ID '=' expression                   # assignment
	| variableName=ID '.' memberName=ID '=' expression # memberAssignment
	;

range
	: start=expression '..' end=expression
	;

expression
   : '(' expression ')'                                                          # parensExpression
   | op=('+' | '-') expression                                                   # unaryExpression
   | left=expression op=('*' | '/' | '%') right=expression                       # infixExpression
   | left=expression op=('+' | '-') right=expression                             # infixExpression
   | left=expression op=('<' | '<=' | '>' | '>=') right=expression               # infixExpression
   | left=expression op=('==' | '!=') right=expression                           # infixExpression
   | numericType=NUMERIC_TYPE '(' value=SCIENTIFIC_NUMBER ')'                    # numberWithPrecision
   | functionName=ID '(' arguments+=expression* (',' arguments+=expression)* ')' # functionCall
   | value=atom                                                                  # valueExpression
   ;

atom
	: number
	| bool
	| memberAccess
	| variable
	;

number
	: value=SCIENTIFIC_NUMBER
	;

bool
	: 'true'  # true
	| 'false' # false
	;

memberAccess
    : structName=ID '.' memberName=ID
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
    : '^'? (NUMERIC_TYPE | ID)
    ;

NUMERIC_TYPE
    : ('uint' | 'int') ('8' | '16' | '32' | '64')
    | 'float' ('32' | '64')
    ;

ID
    : [a-zA-Z][a-zA-Z0-9]*
    ;

SCIENTIFIC_NUMBER
   : NUMBER (('E' | 'e') ('+' | '-')? NUMBER)?
   ;

fragment NUMBER
   : ('0' .. '9') + ('.' ('0' .. '9') +)?
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

OP_MOD
	: '%'
	;

OP_EQ
	: '=='
	;

OP_NE
	: '!='
	;

OP_LT
	: '<'
	;

OP_LT_EQ
	: '<='
	;

OP_GT
	: '>'
	;

OP_GT_EQ
	: '>='
	;

COMMENT
	: ('#' ~('\r' | '\n')* '\r'? '\n') -> skip
	;

WS
    : (' ' | '\t' | '\n')+ -> skip
    ;
