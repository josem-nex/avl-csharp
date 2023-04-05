namespace AVL
{
    /// <summary>The info necessary to print the tree</summary>
    class NodeInfo
    {
        public TreeAVL Node;
        public string Text;
        public int StartPos;
        public int Size { get { return Text.Length; } }
        public int EndPos { get { return StartPos + Size; } set { StartPos = value - Size; } }
        public NodeInfo Parent, Left, Right;
    }

    
}
