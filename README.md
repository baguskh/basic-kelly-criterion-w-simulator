# basic-kelly-criterion-w-simulator
kelly criterion simulator - VS C#


Built on VisualStudio 2013, C#

+ This app created to calculate kelly criterion based on binomial distribution and several assumptions, then simulate it.
+ The main output of this app is:
  - value of the kelly fraction
  - number of players which win the maximum pot
  - the average player's cash after playing this game
  - data record of the simulation

Q: Can any player bust on this simulation?
A: - No, this app made assumption that players can bet on every fraction of cash
   - Since players always set their amount of bet in their total cash' fraction at each game, the amount of cash of player after the game      cannot converge to zero or less 
