# basic-kelly-criterion-w-simulator app
kelly criterion simulator - VS C#


Built on VisualStudio 2013, C#

+ This app created to calculate the kelly criterion based on binomial distribution and several assumptions, then simulate it.
+ The main output of this app is:
  - value of the kelly fraction
  - number of players which win the maximum pot
  - the average player's cash after playing this game
  - data record of the simulation

Q: Can any player get busted on this simulation?
A: Well...
- No, this app assumed that players can bet on every fraction of cash
- Since players always set their amount of bet in their total cash' fraction at each game, the amount of cash of player after the game      cannot converge to zero or less 

Q: It takes a long time when looking at the detailed record, why?
A: List view will take 90.000 records on the worst case (for 300 players and games), not mentioning there is any external database management system though...
