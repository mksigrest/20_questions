using System;
using tree_node_class;
namespace _20_questions_homework
{
    internal class Program
    {
        class TreeNode
        {
            public string Data { get; set; }
            public TreeNode YesChild { get; set; }
            public TreeNode NoChild { get; set; }

            //class builder that assigns string arg question to data
            public TreeNode(string data)
            {
                Data = data;
                YesChild = null;
                NoChild = null;
            }
        }



        //Root node declaration
        static TreeNode root;

        static void Main(string[] args)
        {
            /*
             * Designing Node Tree Class & Testing the code: Meakalia - Sat Have something - finish Sun - ask for help if you can't finish the work
             * Building the question tree & Error Handling: Max - Sun have finished - ask for help if you can't finish the work
             * Traversal on input & Comments: Tommy - Tue have work done - Wed have finished ask for help if you can't finish the work
             * Multiple rounds & Error Handling: Shawn - Thur have finished - ask for help if you can't finish the work
             * Learning & Testing the Code: Presley - fri have something - sat Finished - ask for help if you can't finish the work
           *saving: Shawn - Finish on Sat - Hopefully all code is done on Sat, ask questions if we need help
             */


            //To-do: prompt for existing game file; if not, build default tree
            string filepath = "questionTree.txt";
            TreeNode root = BuildTreeFromFile(filepath);

            //Adding in stateInp to track is user is still playing, learning or exiting
            string stateInp = "";

            //Start the tree traversal and interactivity
            if (root != null)
            {
                while (!stateInp.Equals("Exit"))
                {
                    Console.WriteLine("Welcome to 20 Questions!");
                    Console.WriteLine("Would you like to 'Play', 'Save', or 'Exit'");
                    stateInp = Console.ReadLine();
                    if (stateInp.Equals("Play"))
                    {
                        Console.WriteLine("\nThink of something, and I'll try to guess it.\n");
                        TraverseTree(root);
                    }
                    else if (stateInp.Equals("Save"))
                    {
                        //save function
                        Console.WriteLine("Saving Code...\n");

                        //let the thread pause for a moment before returning to main
                        Thread.Sleep(2000);
                    }
                    else if(stateInp.Equals("Exit"))
                    {
                        //let the thread pause for a moment before ending the code
                        Thread.Sleep(1000);
                        Console.WriteLine("\nThanks for playing!");
                    }
                    else
                    {
                        Console.WriteLine("Please type your values carefully. 'Play', 'Save', or 'Exit'\n");
                        Thread.Sleep(500);
                    }
                }
            }
            else
            {
                Console.WriteLine("Error: Tree could not be loaded.");
            }

        }


        // recursive function to create default binary search tree from questionTree.txt
        // returns a TreeNode that can be traversed by accessing the YesChild or NoChild
        static TreeNode BuildTreeFromFile(string filePath)
        {
            // dictionary to hold references to TreeNode objects
            Dictionary<string, TreeNode> nodes = new Dictionary<string, TreeNode>();

            // read complete default tree file
            string[] lines = File.ReadAllLines(filePath);
            TreeNode root = null;

            foreach (string line in lines)
            {
                // parse line into parts
                string[] parts = line.Split(new[] { "\", \"" }, StringSplitOptions.None);
                string parentQuestion = parts[0].Trim('\"');
                string yesChildText = parts[1].Trim('\"');
                string noChildText = parts[2].Trim('\"');

                // create parent node if parentQuestion is not found
                if (!nodes.ContainsKey(parentQuestion))
                {
                    nodes[parentQuestion] = new TreeNode(parentQuestion);
                    // set root if null
                    if (root == null) root = nodes[parentQuestion];
                }

                TreeNode parent = nodes[parentQuestion];

                // create YesChild node if YesChild is not found
                if (!nodes.ContainsKey(yesChildText))
                {
                    nodes[yesChildText] = new TreeNode(yesChildText);
                }
                parent.YesChild = nodes[yesChildText];

                // create NoChild node if NoChild is not found
                if (!nodes.ContainsKey(noChildText))
                {
                    nodes[noChildText] = new TreeNode(noChildText);
                }
                parent.NoChild = nodes[noChildText];
            }

            return root;
        }

        //function that is able to traverse the tree and make the game playable
        static void TraverseTree(TreeNode node)
        {
            //makes sure that the nodes have something in them
            while (node != null)
            {
                //checks to see if there are anymore child nodes
                if (node.YesChild == null && node.NoChild == null)
                {
                    //Reached an answer node
                    Console.WriteLine($"{node.Data}? (Yes/No)");
                    string response = Console.ReadLine()?.Trim().ToLower();

                    if (response == "yes")
                    {
                        Console.WriteLine("I guessed it!");
                        //let the thread pause for a moment before going back to main menu
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        Console.WriteLine("You stumped me!");
                        //prompt with learn function
                        //let the thread pause for a moment before going back to main menu
                        Thread.Sleep(1000);
                    }
                    return;
                }
                else
                {
                    // Ask the question and move to the corresponding child
                    Console.WriteLine(node.Data);
                    string response = Console.ReadLine()?.Trim().ToLower();

                    if (response == "yes")
                    {
                        node = node.YesChild;
                    }
                    else if (response == "no")
                    {
                        node = node.NoChild;
                    }
                    else
                    {
                        Console.WriteLine("Please answer with 'Yes' or 'No'.");
                    }
                }
            }
        }




    }
}
