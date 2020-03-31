grammar Dice;

/*
 * Parser Rules
 */

expression
	: expression  (PLUS | MINUS) expression
	| (PLUS | MINUS) atom
	;

atom
	: diceGroup
	| NUMBER
	;

diceGroup
	: D NUMBER
	| NUMBER D NUMBER
	;

/*
 * Lexer Rules
 */

NUMBER
	: DIGIT +
	;

SIGN
	: ('+' | '-')
	;


DIGIT
	: ('0'..'9')
	;

D
	: ('d' | 'D')
	;

PLUS
	: '+'
	;

MINUS
	: '-'
	;

WS
	: [ \r\n\t] + -> skip
	;