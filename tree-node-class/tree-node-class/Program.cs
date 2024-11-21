namespace tree_node_class
{
    internal class Program
    {
        //tree_node_class

        class TreeNode
        {
            public string Data { get; set; }
            public TreeNode YesChild { get; set; }
            public treeNode NoChild { get; set; }

            //class builder that assigns string arg question to data
            public TreeNode(string data)
            {
                Data = data;
            }

            static void Main(string[] args)
            {

            }
        }
    }
}
