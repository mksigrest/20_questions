namespace _20_questions_homework
{
    internal class Program
    {
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

        }

        static void CreateDefaultTree()
        {
            //default question tree

            root = new TreeNode;

            string filepath = questionTree.txt;
            string line;
            while ((line = ReaderWriterLock.ReadLine()) != null) 
            {
                //Process line as data, yesChild, noChild
                string[] parts = line.Split(',');

                root = new TreeNode(parts[0]);
                root.YesChild = new TreeNode(parts[1]);
                root.NoChild = new TreeNode(parts[2]);

            }



         
        }
    }
}
