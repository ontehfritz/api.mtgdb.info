api.mtgdb.info beta

A card database API for "Magic: The Gathering". Ultra simple. Open source. JSON API to create your own "Magic: the gathering database".


New release coming in Feb. 2014. There are some changes in the JSON formatting. It will follow the google guildlines for JSON. Guidelines here This is going to break code using the old format. The new format is available now. We will be locking this format, and there will be no field format changes in the future. Yes fields will be added, but the naming and format will not change.


Card
	id                  Integer     : multiverse Id
	setNumber           Integer     : card number in the set
	name                String      : name of the card
	description         String      : the cards actions
	flavor              String      : flavor text adds story, does not effect game
	colors              String[]    : colors of card
	manacost            String      : the description of mana to cast spell
	convertedManaCost   Integer     : the amount of mana needed to cast spell
	cardSetName         String      : the set or expansion the card belongs to
	type                String      : the type of card
	subType             String      : subtype of card
	power               Integer     : attack strength
	toughness           Integer     : defense strength 
	loyalty             Integer     : loyalty points usually on planeswalkers
	rarity              String      : the rarity of the card
	artist              String      : artist of the illustration
	cardImage           String      : the url of the card image
	cardSetId           String      : the abbreviated name of the set
	releasedAt          Date        : when the card was released
	
Get all cards

Get http://api.mtgdb.info/cards/ 
Get a card

Get http://api.mtgdb.info/cards/[id] 
Filters

Filters can be applied on any field /?field_name=value. Multple fields can be used. Note that each field is "And" logic. "Or" logic has not yet been implmented.

Get http://api.mtgdb.info/cards/?colors=white,black
CardSet
	id          String    : abbreviated name of set   
	name        String    : name of set
	block       String    : name of block the set is part of
	description String    : information about the set
	wikipedia   String    : link to wiki page
	common      Integer   : amount of common cards in set
	uncommon    Integer   : amount of uncommon cards in set
	rare        Integer   : amount of rare cards in set
	mythicRare  Integer   : amount of mythic rare cards in set
	basicLand   Integer   : amount of basic land cards in set
	releasedAt  Date      : date set was released
	cardIds     Integer[] : multiverse ids of all the cards in the set
	
Get all sets

Get http://api.mtgdb.info/sets/
Get a set

Get http://api.mtgdb.info/sets/[id]
Get all cards in a set

Get http://api.mtgdb.info/sets/[id]/cards/
All information provided is copyrighted by Wizards of the Coast. This api and/or web applications on this domain are not in anyway affiliated with Wizards of the Coast.

© SuperSimpleSuperRad (Triple S Rad) 2013