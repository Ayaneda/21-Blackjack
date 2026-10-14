# Black jack
 
## Vad programmet gör och hur man startar det

## En kort översikt över klasserna och hur de hänger ihop
Vi har utgått ifrån förslaget på klasser som fanns i uppgiftsbeskrivningen. Namespace har använts för att göra strukturen tydligare. 

Tjugoett.Cards håller klasserna Card, Deck och Hand samt enumarna Rank och Suit. Dessa har och hanterar data som rör korten. Som att bygga och blanda kortlek, räkna ut kortens värde.  

Namespace Tjugoett.Participants håller klasserna Dealer och Player samt basklassen Participant som de två andra ärver av. Basklassen har namn och hand, den kan ta emot kort och tömma handen efter varje runda. Den använder sig av metoder som finns i Cards. Dealer och Player har sina respektive specifika beslut och regler som behövs för spelet.     

Namespace Tjugoett.Rules håller enumarna Move och Outcome samt klassen Round. I Move finns de två valen en spelare kan göra, Player använder sig av den. I Outcome finns de olika sätt en runda kan sluta på. Round sköter en spelrunda

Endast GameStorage är av namespace Tjugoett.Storage. Den kommunicerar med filen där data sparas genom att spara resultat och ladda resultat. 

Namespace Tjugoett.UI håller ConsoleUi och Game. ConsoleUi står för kommunikationen via Console och visar meny för användaren. Game står för att hålla igång spelet och ser till att rätt sak händer i rätt ordning. 

## Vem som gjort vad — vilket tillval var och en har byggt

## Vilka regler som gäller, om ni avvikit från specifikationen på någon punkt


## Kända brister

