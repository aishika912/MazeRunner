MAZERUNNER

Summary:
Mazerunner is a C# program that takes an image of maze, solves it, and draws the solution onto the image.  It is a command line program, so to run, use the syntax:
maze.exe “source.[bmp,png,jpg]” “destination.[bmp,png,jpg]”

Files:
Program.cs: This is where the main function is. It takes the input and output file names, validates them, then creates a graph with the image to find the solution to the maze.  Then it draws the solution onto the image and saves it in the given output file name.

Node.cs:  This holds the Node class.  The Node class represents the nodes of the graph that is created.  I took each node to be a single in the pixel in the maze image.  Since black pixels were considered walls, I did not include them as nodes, as you can not use them.  The node class information about the the pixel such as its color, location, neighbors (up, down, right, and left that are black), etc.  It also had values to use later for finding a solution to the maze.

Graph.cs:  This holds the Graph class.  The Graph class contains all of the nodes created using the image.  I used a Dictionary to be the container of all the nodes because of its quick and easy lookup.  The Graph class also contained the algorithm to find the solution to the maze, ShortestPath.  Since I created a Graph of all nodes,  I knew that I would start from a red node and search through the graph to find a blue node.  I decided to use the Breadth First Search as it is known to find the shortest path from one node to another as long as the graph isn’t weighted, which in this case, it wasn’t.


Unit Testing:
This was my first time writing a unit test, so I wrote three basic ones using NUnit.  One checks whether the ShortestPath function works correctly, another was to check whether neighbors are added correctly to a node, and the last is to check whether nodes are correctly added to the graph.  

Author:
Aishika Kumar
using Visual Studio