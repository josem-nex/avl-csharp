namespace AVLTree;

    /// <summary>The info necessary to print the tree</summary>
    public class NodeInfo<TKey> where TKey : IComparable<TKey>
    {
        public AVLNode<TKey> Node;
        public string Text;
        public int StartPos;
        public int Size { get { return Text.Length; } }
        public int EndPos { get { return StartPos + Size; } set { StartPos = value - Size; } }
        public NodeInfo<TKey> Parent, Left, Right;

    public NodeInfo(AVLNode<TKey> node, string text)
    {
        this.Node = node;
        Text = text;
    }
}

    
