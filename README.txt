What algorithm have you used to create a maze?
What makes it random and dynamic?
How have you tracked the path solving it? 
Why have you chosen pre-existing assets over making something yourself? Make sure to cite any sources for assets, code or otherwise, so we're sure you're not claiming you've done a 4K realistic texture of grass on your own.
Any challenges you found in the assignment?

I decided to use Prim's algorithm to create the maze. What makes it random is that to build the MST, I pick a random frontier neighbour, and in turn that frontier neighbour will pick a random "in neighbour" (a neighbour that is already in the MST), and then join itself to that neighbour. 
What I ended up doing was creating the first iteration of the MST beforehand, and then randomly picking a exit tile. After this I ran DFS from the root tile, keeping track of the path, and stopping once I reached the exit tile. Because there are no cycles, there is only one path to the exit tile, so DFS would give me that path. 
Why have I chosen pre-existing assets? Lack of time and because, I want the game to be good looking, partly because I am spending ~40 hours looking at it and debugging it :-)
Here are the list of Assets I have used:
TREE: 
KEY:
ZOMBIE:
GUN:

I think the most difficult challenge was understanding the assignment instructions. I only really understood what I was supposed to code for the maze after talking to a TA. 





