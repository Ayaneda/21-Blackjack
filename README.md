# Black jack
 
## Vad programmet gör och hur man startar det

1. **Program** skapar ett **Game** och kör Run().
2. **Game** skapar en **Deck** (som byggs och blandas direkt), en **Dealer**, **ConsoleUi**, **Round** och **GameStorage**. Sedan visas menyn via ConsoleUi (ShowMenu()).
3. **Första gången man väljer "Spela en runda"** frågar Game efter antal spelare och namn (AskPlayerCount(), AskName()) och skapar en **Player** för varje. Varje Player får metoden AskMoveWithDealer inskickad, som frågar ConsoleUi om spelarens drag. Finns det lediga platser (max 5 spelare totalt) frågar Game hur många datorspelare man vill ha (AskComputerPlayerCount()). Varje **ComputerPlayer** får ett namn, en slumpad strategi (**SimpleStrategy** eller **BasicStrategy**, som båda följer gränssnittet **IPlayerStrategy**) och givaren, så att den kan se givarens öppna kort.
4. **Inför varje runda** töms allas händer och varje datorspelare får en ny slumpad strategi (SetStrategy()). Givarens andra kort döljs igen (HideHoleCard()) och Deck kontrollerar att det finns tillräckligt med kort kvar (CardLeft()). Finns det färre än 15 kort byggs och blandas en ny kortlek.
5. **Round** delar ut två **Card** till varje spelare och givaren. Korten hamnar i varje deltagares **Hand**. Givarens andra kort är dolt.
6. **Spelarnas tur:** Round anropar player.GetMove() för en spelare i taget. För en mänsklig spelare visar **ConsoleUi** givarens öppna kort och alla spelares händer som kortbilder och frågar Hit eller Stand. För en datorspelare visas först dess startkort, och sedan bestämmer strategin draget (ChooseMove()) utan att någon tillfrågas. Efter en kort paus visas datorns drag. Vid Hit ger Round ett nytt kort och visar det, tills spelaren stannar, får 21 eller går över 21. Den som får 21 på de två första korten (naturlig tjugoett) hoppar över sin tur.
7. **Givarens tur:** **Dealer** vänder sitt dolda kort (RevealHoleCard()) och tar kort enligt regeln (ShouldHit()) utan att fråga någon. Givaren drar på 16 eller lägre och stannar på 17 eller högre, även mjuk 17.
8. **Resultat:** Round jämför varje spelares summa från **Hand** med givarens och ger ett **Outcome** (Win, Lose, Push, Blackjack).
9. **Tillbaka i Game:** allas händer och givarens hand visas. Varje spelares resultat visas med ShowOutcome() och sparas med **GameStorage** (SaveResult()). Sedan visas menyn igen, och nästa runda spelas med samma spelare.
10. **"Visa tidigare resultat"** läser in dem med LoadResults() och visar dem via ConsoleUi. **"Avsluta"** avslutar loopen i Run().

## En kort översikt över klasserna och hur de hänger ihop
Vi har utgått ifrån förslaget på klasser som fanns i uppgiftsbeskrivningen. Namespace har använts för att göra strukturen tydligare. 

Namespace *Tjugoett.UI* håller **ConsoleUi** och **Game**. ConsoleUi står för kommunikationen via Console och visar meny för användaren. Game står för att hålla igång spelet och ser till att rätt sak händer i rätt ordning. Den skapar spelarna och startar en Round för varje omgång. Game använder sig av ConsoleUi och GameStorage.

*Tjugoett.Cards* håller klasserna **Card**, **Deck** och **Hand** samt enumarna **Rank** och **Suit**. Dessa har och hanterar data som rör korten. Som att bygga och blanda kortlek, räkna ut kortens värde.  

Namespace *Tjugoett.Participants* håller klasserna **Dealer** och **Player** samt basklassen **Participant** som de två andra ärver av. Basklassen har namn och hand, den kan ta emot kort och tömma handen efter varje runda. Den använder sig av metoder som finns i Cards. Dealer och Player har sina respektive specifika beslut och regler som behövs för spelet.     

Namespace *Tjugoett.Rules* håller enumarna **Move** och **Outcome** samt klassen **Round**. I Move finns de två valen en spelare kan göra, Player använder sig av den. I Outcome finns de olika sätt en runda kan sluta på. Round sköter en spelrunda och använder sig av Deck och Cards och returnerar Outcome för spelare.

Endast **GameStorage** är av namespace *Tjugoett.Storage*. Den kommunicerar med filen där data sparas genom att spara resultat och ladda resultat. 

 
## Vem som gjort vad — vilket tillval var och en har byggt
### Aldo
- Klassen Move
- Klassen Outcome
- Klassen Round

### Aythami
- Enum Suit
- Enum Rank
- Klassen Card
- Klassen Deck
- klassen Game
- **Tillval 1 - Kortgrafik i konsolen**

### Emelianna
- Klassen Participant
- Klassen Player
- Klassen ConsoleUi tillsammans med Danial
- Program.cs
- **Tillval 2 – Datorspelare med strategi**

### Danial
- Klassen Hand
- Klassen Dealer
- Klassen GameStorage
- Klassen ConsoleUi tillsammans med Emelianna
- **Tillval 4 – Satsningar och marker**


## Vilka regler som gäller, om ni avvikit från specifikationen på någon punkt


## Kända brister

###Kvar att göra 21/10
- GameStorage sparar bara rader som “Namn - Win”. Spelarna läses inte in igen vid nästa start, och ingenstans räknas vinster, förluster och lika ihop per spelare. Det är det största hålet i grunden.
- Round skriver “Du har fått Blackjack!!!” även när spelaren når 21 på tre kort. Det är bara texten som är fel, inte uträkningen.
