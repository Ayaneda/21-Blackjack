# Black jack
 
## Vad programmet gör och hur man startar det

1. **Program** skapar ett **Game** och kör Run().
2. **Game** visar menyn via **ConsoleUi** (ShowMenu()). Vid "Spela" frågar Game antal spelare och namn (AskPlayerCount(), AskName()) och skapar en **Player** för varje, med ui.AskMove inskickad.
3. Game skapar en **Deck** och en **Dealer** och startar en **Round**.
4. **Round** blandar kortleken och delar ut två **Card** till varje spelare och givaren. Korten hamnar i varje deltagares **Hand**.
5. **Spelarnas tur:** Round anropar player.GetMove() för en spelare i taget. Player kör AskMove(this), så **ConsoleUi** visar handen och frågar Hit eller Stand. Vid Hit ger Round ett nytt kort, tills spelaren stannar eller går över 21.
6. **Givarens tur:** **Dealer** tar kort enligt regeln (ShouldHit(), under 17) utan att fråga någon.
7. **Resultat:** Round jämför varje spelares summa från **Hand** med givarens och ger ett **Outcome** (Win, Lose, Push, Blackjack).
8. **Tillbaka i Game:** resultatet visas med ShowOutcome() och sparas med **GameStorage** (SaveResult()). Sedan visas menyn igen.
9. **"Visa tidigare resultat"** läser in dem med LoadResults() och visar dem via ConsoleUi. **"Avsluta"** avslutar loopen i Run().

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
- Klasen Round

### Aythami

### Emelianna
- Klassen Participant
- Klassen Player
- Klassen ConsoleUi tillsammans med Danial
- Program.cs

### Danial


## Vilka regler som gäller, om ni avvikit från specifikationen på någon punkt


## Kända brister

