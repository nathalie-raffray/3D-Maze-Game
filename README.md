# 3D Maze Game

3D Maze constructed with Prim's Algorithm, where the 3rd dimension is time.
Dynamically and randomly generated to be (highly likely) different in each play.

*Controls*: 

jump: space

shoot: LMB

make cursor visible again: escape 

move: WASD

To gain access to the maze, shoot the 3 objects floating in the air and a key will fall down. Once you walk over the key, you will "pick it up" and the maze will be open.

Each step taken in the maze, will be one step taken in the time dimension and the maze will change. You can think of this as moving up a level in a cube. You may jump in the maze to look at the tiles around you. 

Practically, this means the maze is pretty hard to solve. I have colored the tiles that lead to the solution blue, so follow those tiles to win. If you take 16 steps without solving the maze, the maze will restart and you will be taken back to the start. 

This project was an exercise to think of time as a dimension in video games. 

*Questions*:

What algorithm have you used to create a maze? What makes it random and dynamic?

I decided to use Prim's algorithm to create the maze. What makes it random is that to build the MST, I pick a random frontier neighbour, and in turn that frontier neighbour will pick a random "in neighbour" (a neighbour that is already in the MST), and then join itself to that neighbour. For the 16 steps in the time dimension, I pregenerated the maze 16 times with Prim's algorithm, to assure there would be no situation where the player would get "stuck" -- this allows for expansive movement around the maze at each step (because every tile is accessible!)

How have you tracked the path solving it? 

What I ended up doing was creating the first iteration of the MST beforehand, and then randomly picking a exit tile. After this I ran DFS from the root tile, keeping track of the path, and stopping once I reached the exit tile. Because there are no cycles, there is only one path to the exit tile, so DFS would give me that path. 






