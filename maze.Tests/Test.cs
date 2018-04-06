using NUnit.Framework;
using System;
using System.Drawing;
using System.Collections.Generic;
namespace maze.Tests
{
    [TestFixture()]
    public class Test
    {
        [Test()]
        //check whether the neighors are added correctly to a node
        public void CheckNeighborsAdded()
        {
            //arrange
            Node testNode = new Node();
            Node neighbor1 = new Node();
            Node neighbor2 = new Node();
            Node neighbor3 = new Node();
            List<Node> expected = new List<Node>() {neighbor1,neighbor2,
                                                                    neighbor3};

            //act
            testNode.AddNeighbor(neighbor1);
            testNode.AddNeighbor(neighbor2);
            testNode.AddNeighbor(neighbor3);

            List<Node> actual = testNode.Neighbors;

            //assert
            Assert.AreEqual(expected, actual);
        }

        [Test()]
        // since there is not a way to compare two dictionaries, I just check
        // that the amount of keys/values are the same as that in the actual 
        // (there is a chance that there are extra nodes, because it might not
        // recognize that a node was already add
        public void CheckNodesAddedToGraph() {
            //arrange
            Graph testGraph = new Graph();
            Node node1 = new Node(Color.White,Tuple.Create(1,2));
            Node node2 = new Node(Color.Blue, Tuple.Create(7, 4));
            Node node3 = new Node(Color.Red, Tuple.Create(3, 9));
            Dictionary<Tuple<int, int>, Node> expected = 
                                    new Dictionary<Tuple<int, int>, Node>();
            expected.Add(Tuple.Create(1, 2),node1);
            expected.Add(Tuple.Create(7, 4),node2);
            expected.Add(Tuple.Create(3, 9), node3);

            //act
            testGraph.AddNode(Tuple.Create(1, 2), Color.White);
            testGraph.AddNode(Tuple.Create(7, 4), Color.Blue);
            testGraph.AddNode(Tuple.Create(3,9), Color.Red);
            //this shouldn't be added because it was already added
            testGraph.AddNode(Tuple.Create(7, 4), Color.Blue);

            Dictionary<Tuple<int, int>, Node> actual = testGraph.Nodes;
            Assert.AreEqual(expected.Count,actual.Count);
            Assert.AreEqual(expected.Count,actual.Count);
        }

        [Test()] 
        /*created a small graph to test shortest path
         * node1 = (1,2) and red
         * node2 = (5,6) and white
         * node3 = (10,10) and blue
         * node4 = (11,12) and white
         * node5 = (1,1) and white
         * node1 neighbors: 2,4
         * node2 neighbors: 1,3
         * node3 neighbors: 2,5
         * node4 neighbors: 1,5
         * node5 neighbors: 3,4
         * 
         * need to check whether the prev values are correct for each node
         */
        public void CheckShortestPath () {

            Graph actualG = new Graph();
            Node node1 = actualG.AddNode(Tuple.Create(1, 2), Color.Red);
            Node node2 = actualG.AddNode(Tuple.Create(5, 6), Color.White);
            Node node3 = actualG.AddNode(Tuple.Create(10, 10), Color.Blue);
            Node node4 = actualG.AddNode(Tuple.Create(11, 12), Color.White);
            Node node5 = actualG.AddNode(Tuple.Create(1, 1), Color.White);

            node1.AddNeighbor(node2);
            node1.AddNeighbor(node4);

            node2.AddNeighbor(node1);
            node2.AddNeighbor(node3);

            node3.AddNeighbor(node2);
            node3.AddNeighbor(node5);

            node4.AddNeighbor(node1);
            node4.AddNeighbor(node5);

            node5.AddNeighbor(node4);
            node5.AddNeighbor(node3);
            Node actualEnd = actualG.ShortestPath(node1);

            //Assert
            //checking if the correct ending node and if the last node was 
            //chosen from the correct path (the shorter one)
            Assert.AreEqual(node3, actualEnd);
            Assert.AreEqual(node2, actualEnd.Prev);
        }
    }
}
