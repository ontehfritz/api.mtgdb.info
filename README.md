API mtgdb.info beta : http://api.mtgdb.info/

A card database API for "Magic: The Gathering". Ultra simple. Open source. JSON API to create your own "Magic: the gathering database". This is not meant to be a full search api, but rather an api to copy and sync your own MTG database. However, basic filtering is supported and will be expanded, but is not the main focus of the api

Please send feature request, comments, corrections or anything else to: planeswalker@mtgdb.info

Card

	Id                  Integer     : multiverse Id
	
	set_number     		Integer     : card number in the set
	
	name                String      : name of the card
	
	description         String      : the cards actions
	
	flavor              String      : flavor text adds story, does not effect game
	
	colors              String Array: colors of card
	
	manacost            String      : the description of mana to cast spell
	
	convertedmanacost   Integer     : the amount of mana needed to cast spell
	
	card_set_name       String      : the set or expansion the card belongs to
	
	type                String      : the type of card
	
	subtype             String      : subtype of card
	
	power               Integer     : attack strength
	
	toughness           Integer     : defense strength 
	
	loyalty             Integer     : loyalty points usually on planeswalkers
	
	rarity              String      : the rarity of the card
	
	artist              String      : artist of the illustration
	
	card_image          String      : the url of the card image
	
	card_set_id         String      : the abbreviated name of the set
	
	card_released_at    Date        : when the card was released
	
	
Get all cards

Get http://api.mtgdb.info/cards/ 
Get a card

Get http://api.mtgdb.info/cards/[id] 
Filters

Filters can be applied on any field /?field_name=value. Multple fields can be used. Note that each field is "And" logic. "Or" logic has not yet been implmented.

Get http://api.mtgdb.info/cards/?colors=white,black
CardSet

	Id          String    : abbreviated name of set  
	 
	name        String    : name of set
	
	block       String    : name of block the set is part of
	
	description String    : information about the set
	
	wikipedia   String    : link to wiki page
	
	common      Integer   : amount of common cards in set
	
	uncommon    Integer   : amount of uncommon cards in set
	
	rare        Integer   : amount of rare cards in set
	
	mythic_rare Integer   : amount of mythic rare cards in set
	
	basic_land  Integer   : amount of basic land cards in set
	
	released_at Date      : date set was released
	
	card_ids    Int Array : multiverse ids of all the cards in the set
	
Get all sets

Get http://api.mtgdb.info/sets/
Get a set

Get http://api.mtgdb.info/sets/[id]
Get all cards in a set

Get http://api.mtgdb.info/sets/[id]/cards/
All information provided is copyrighted by Wizards of the Coast. This api and/or web applications on this domain are not in anyway affiliated with Wizards of the Coast.
