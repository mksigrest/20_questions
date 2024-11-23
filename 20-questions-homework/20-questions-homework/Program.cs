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

        

        

        
    }
}
