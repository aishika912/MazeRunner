using System;
using System.Drawing;
using System.Collections.Generic;
namespace maze
{
    /*
     * This class represents a node in the graph I create.  Each node is a
     * representative of a pixel in the image.  The node class holds info such
     * as the color and location of a pixel.  It also holds info such as when 
     * it was visited and during BFS and it's previous Node value.
     */
    public class Node
    {
        bool _visited;              // tells where a node has been visited
        Node _prev;                 // holds prev Node when BFS is done
        Color _pixelColor;          // holds pixel color
        Tuple<int, int> _location;  // holds x,y location of the pixel
        List<Node> _neighbors;      // holds up, down, right, left neighbors

        // constructors
        public Node() {
            _neighbors = new List<Node>();
        }

        public Node(Color pixCol, Tuple<int, int> pixLoc) {
            _pixelColor = pixCol;
            _location = pixLoc;
            _neighbors = new List<Node>();
            _visited = false;
        }

        // get and set methods for variables
        public List<Node> Neighbors {
            get { return _neighbors; }
        }

        public Node Prev {
            get { return _prev; }
            set { _prev = value; }
        }

        public bool Visited {
            get { return _visited; }
            set { _visited = value; }
        }

        public Tuple<int, int> Location {
            get { return _location; }
        }

        public Color PixelColor {
            get { return _pixelColor; }
        }

        //add a neighbor to the neighbors list
        public void AddNeighbor(Node neighbor) {
            _neighbors.Add(neighbor);
        }
    }
}
