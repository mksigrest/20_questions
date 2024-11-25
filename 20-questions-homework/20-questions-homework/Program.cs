using System;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;
using tree_node_class;
using static System.Formats.Asn1.AsnWriter;
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
        // dictionary to hold references to TreeNode objects
        static Dictionary<string, TreeNode> nodes = new Dictionary<string, TreeNode>();
       
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
            string filepath = @"../../../questionTree.txt";

            
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
                        Console.WriteLine("Saving Code...");
                        //let the thread pause for a moment before returning to main
                        SaveTree();
                        Thread.Sleep(2000);
                        Console.WriteLine("File Saved\n");
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

        //function that goes through all tree data and writes to file
        static void SaveTree()
        {
            //Saving Code with StreamWriter
            try
            {
                StreamWriter sw = new StreamWriter(@"../../../questionTree.txt", false);
                foreach ((string key, TreeNode value) in nodes)
                {
                    if ((value.YesChild != null) && (value.NoChild != null))
                    {
                        sw.WriteLine('"' + key + "\", \"" + value.YesChild.Data + "\", \"" + value.NoChild.Data);
                    }

                }

                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("File not found");
                Console.WriteLine(e.ToString());
                return;
            }
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
                        // If incorrect, start learning
                        Console.WriteLine("You stumped me!");
                        Console.WriteLine("What were you thinking of? Ex: 'It is a *blank*'");
                        string newAnswer = Console.ReadLine()?.Trim();

                        // Ask for a new question to distinguish the user's thought
                        Console.WriteLine($"What question can help distinguish {newAnswer} from {node.Data}?");
                        string newQuestion = Console.ReadLine()?.Trim();

                        // Determine whether the answer to the new question is "yes" or "no" for the new thought
                        Console.WriteLine($"For {newAnswer}, what is the answer to your question? (yes/no)");
                        string newAnswerResponse = Console.ReadLine()?.Trim().ToLower();

                        // Create a new node for the new answer
                        TreeNode newAnswerNode = new TreeNode(newAnswer);
                        // Create a new node for the old incorrect answer
                        TreeNode oldAnswerNode = new TreeNode(node.Data);

                        // Update the current node to hold the new question
                        node.Data = newQuestion;

                        // Link the new answer and old answer nodes to the current node
                        // Depending on the response, assign the new answer to either the Yes or No child
                        if (newAnswerResponse == "yes")
                        {
                            node.YesChild = newAnswerNode;
                            node.NoChild = oldAnswerNode;

                        }
                        else
                        {
                            node.YesChild = oldAnswerNode;
                            node.NoChild = newAnswerNode;
                        }

                        //Removes old Leaf Node and adds in the question above it, to make saving easier
                        nodes.Remove(oldAnswerNode.Data);
                        nodes.Add(node.Data, node);
                        
                        
                        
                        

                        // Inform the user that the program has learned the new information
                        Console.WriteLine("Got it! I'll make sure I remember that!!!\n");
                    }
                    // Exit the method after handling the leaf node
                    //let the thread pause for a moment before going back to main menu
                    Thread.Sleep(1000);
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


