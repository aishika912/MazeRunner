using System;
using System.Drawing;
using System.Collections.Generic;

namespace maze
{
    /*
     * This class represents the graph I create.  It contains all the nodes that 
     * I created using the pixels.  The container holding all the nodes is a 
     * dictionary, because of its quick and easy lookup.  This class also 
     * contains the algorithm to find the solution to the maze.
     */
    public class Graph {
        Dictionary<Tuple<int, int>, Node> _nodes; //holds all nodes in the graph 

        public Graph()  {
            _nodes = new Dictionary<Tuple<int, int>, Node>();
        }

        //returns the dictionary containing all values
        public Dictionary<Tuple<int, int>, Node> Nodes {
            get {return _nodes;}
        }
        /* 
         * checks if node is already in graph. If it is, return node,
         * else create node, add to graph, and return that node
         */
        public Node AddNode(Tuple<int, int> location, Color pixCol) {
            if (_nodes.ContainsKey(location))
            {
                return _nodes[location];
            }

            Node newNode = new Node(pixCol, location);
            _nodes.Add(location, newNode);
            return _nodes[location];
        }

        /*
         * ShortestPath uses Breadth First Search to find the shortest path
         * from the start of the maze to the end.  
         * Input: startNode, the node that represents the beginning of the 
         *        maze
         * Output: endNode, the node that represents the end of the maze
         */
        public Node ShortestPath(Node startNode) {
            Queue<Node> nodeQ = new Queue<Node>();
            nodeQ.Enqueue(startNode);
            startNode.Visited = true;

            while (nodeQ.Count > 0) {
                Node currNode = nodeQ.Dequeue();
                List<Node> neighbors = currNode.Neighbors;
                for (int i = 0; i < neighbors.Count; i++) {
                    Node currNeighbor = neighbors[i];
                    if (!currNeighbor.Visited)  {
                        currNeighbor.Prev = currNode;
                        currNeighbor.Visited = true;
                        if (currNeighbor.PixelColor.ToArgb() ==
                                                        Color.Blue.ToArgb()) {
                            return currNeighbor;
                        }
                        nodeQ.Enqueue(currNeighbor);
                    }
                }
            }

            //return null if end of the maze is not found
            return null;
        }
    }
}
