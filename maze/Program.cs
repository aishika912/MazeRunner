using System;
using System.Drawing;
using System.Collections.Generic;

namespace maze {
    class MainClass {
        public static void Main(string[] args) {
            //open up maze
            Bitmap inImg;
            String inputPath;
            String outputPath;

            //make sure correct amount of arguements are given
            if (args.Length != 2) {
                Console.Write("Not enough arguements given.\n");
                Console.Write("Please re-enter the input file: ");
                inputPath = Console.ReadLine();
                Console.Write("Please re-enter the output file: ");
                outputPath = Console.ReadLine();
            }
            else {
                inputPath = args[0];
                outputPath = args[1];
            }

            //make sure input filename is valid
            inImg = InputValid(inputPath);

            //make sure output filename is valid
            while (!EndsWithOneOf(outputPath)) {
                Console.Write("Please make sure to have your output file " +
                              "extension be .png, .bmp, or .jpg\n");
                Console.Write("Please re-enter the output filename: ");
                outputPath = Console.ReadLine();
            }

            //create a new graph
            Graph thegraph = new Graph();
            Node startNode = new Node();

            //add each pixel into the graph
            for (int x = 0; x < inImg.Width; x++) {
                for (int y = 0; y < inImg.Height; y++) {

                    Color currColor = inImg.GetPixel(x, y);
                    Tuple<int, int> currLoc = Tuple.Create(x, y);

                    //only add pixel into graph if the pixel is not black
                    if (currColor.ToArgb() != Color.Black.ToArgb()) {
                        Node currNode = thegraph.AddNode(currLoc, currColor);
                        if (currColor.ToArgb() == Color.Red.ToArgb()) {
                            startNode = currNode;
                        }

                        //add neighbors of node
                        Queue<Tuple<int, int>> neighbors =
                                                new Queue<Tuple<int, int>>();
                        Tuple<int, int> topNeigh;
                        Tuple<int, int> botNeigh;
                        Tuple<int, int> lefNeigh;
                        Tuple<int, int> rigNeigh;

                        //only add neighbor pixels if they exist and not black
                        if (((y - 1) >= 0) && (inImg.GetPixel(x, y - 1).ToArgb() 
                                                    != Color.Black.ToArgb())) {
                            topNeigh = Tuple.Create(x, y - 1);
                            neighbors.Enqueue(topNeigh);
                        }
                        if (((y + 1) < inImg.Height) && (inImg.GetPixel(x, y+1)
                                        .ToArgb() != Color.Black.ToArgb())) {
                            botNeigh = Tuple.Create(x, y + 1);
                            neighbors.Enqueue(botNeigh);
                        }
                        if (((x - 1) >= 0) && (inImg.GetPixel(x - 1, y).ToArgb() 
                                                    != Color.Black.ToArgb())) {
                            lefNeigh = Tuple.Create(x - 1, y);
                            neighbors.Enqueue(lefNeigh);
                        }
                        if (((x + 1) < inImg.Width) && (inImg.GetPixel(x + 1, y)
                                        .ToArgb() != Color.Black.ToArgb())) {
                            rigNeigh = Tuple.Create(x + 1, y);
                            neighbors.Enqueue(rigNeigh);
                        }

                        while (neighbors.Count > 0) {
                            Tuple<int, int> neighLoc = neighbors.Dequeue();
                            Color neighColor =
                                inImg.GetPixel(neighLoc.Item1, neighLoc.Item2);
                            Node neighNode =
                                        thegraph.AddNode(neighLoc, neighColor);
                            currNode.AddNeighbor(neighNode);
                        }
                    }
                }
            }
            /*
            //run shortest path to solve maze
            Node lastNode = thegraph.ShortestPath(startNode);

            //color the path on the bitmap green
            Node currPathNode = lastNode;
            Color newClr = Color.FromArgb(0, 255, 0);
            while (currPathNode.Location != startNode.Location) {
                Tuple<int, int> pathLoc = currPathNode.Location;
                inImg.SetPixel(pathLoc.Item1, pathLoc.Item2, newClr);
                currPathNode = currPathNode.Prev;
            }

            //save new image
            inImg.Save(outputPath);*/
            Console.Write("Done");
        }

        /*
         * Checks to see if an input file name is valid
         * input: input file name
         * output: bitmap created using input file name
         */
        public static Bitmap InputValid(String input) {
            Bitmap newBitmap;
            try {
                newBitmap = new Bitmap(input);
            }
            catch {
                Console.Write("Incorrect input file. Please enter a valid one:  ");
                String newInput = Console.ReadLine();
                newBitmap = InputValid(newInput);
            }
            return newBitmap;
        }
        /*
         * Checks to see if an output file name is valid
         * input: output file name
         * output: true if valid, else false
         */
        public static bool EndsWithOneOf(String output) {
            StringComparison sc = StringComparison.CurrentCulture;
            if (output.EndsWith(".png", sc))
                return true;
            if (output.EndsWith(".bmp", sc))
                return true;
            if (output.EndsWith(".jpg", sc))
                return true;
            return false;
        }
    }
}