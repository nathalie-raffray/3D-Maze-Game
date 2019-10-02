
FOR UR BENEFIT: when you're playing the game, to make your cursor visible again, press "escape"

Note: Although you can move and jump higher than the maze itself from outside the maze, you will not be able to jump in the maze because I placed colliders over the trees. Whilst in the maze you will not be able to jump and move. 

What algorithm have you used to create a maze?
What makes it random and dynamic?
I decided to use Prim's algorithm to create the maze. What makes it random is that to build the MST, I pick a random frontier neighbour, and in turn that frontier neighbour will pick a random "in neighbour" (a neighbour that is already in the MST), and then join itself to that neighbour. For the 16 steps in the time dimension, I pregenerated the maze 16 times with Prim's algorithm, to assure there would be no situation where the player would get "stuck" -- this allows for expansive movement around the maze at each step (because every tile is accessible!)

How have you tracked the path solving it? 
What I ended up doing was creating the first iteration of the MST beforehand, and then randomly picking a exit tile. After this I ran DFS from the root tile, keeping track of the path, and stopping once I reached the exit tile. Because there are no cycles, there is only one path to the exit tile, so DFS would give me that path. 

Why have I chosen pre-existing assets? 
Lack of time and because, I want the game to be good looking, partly because I am spending ~40 hours looking at it and debugging it :-)
Here are the list of Assets I have used:
TREE: by PIXEL PROCESSOR
KEY: by ROBOCG
ZOMBIE: by PXLTIGER
GUN: by MARIO HABERLE

I think the most difficult challenge was understanding the assignment instructions. I only really understood what I was supposed to code for the maze after talking to a TA. Also my computer sucks and can barely run Unity, another challenge. 





