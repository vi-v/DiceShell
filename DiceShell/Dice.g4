grammar Dice;

/*
 * Parser Rules
 */

expression
	: expression signedAtom
	| signedAtom
	;

signedAtom
	: SIGN atom
	| atom
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


fragment DIGIT
	: ('0'..'9')
	;

D
	: ('d' | 'D')
	;

WS
	: [ \r\n\t] + -> skip
	;