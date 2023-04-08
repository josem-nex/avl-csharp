namespace BinaryTree;

    /// <summary>La información necesario para imprimir el árbol en pantalla</summary>
    internal class NodeInfo<TKey> where TKey : IComparable<TKey>
    {
        internal AVLNode<TKey> Node;
        internal string Text;
        internal int StartPos;
        internal int Size { get { return Text.Length; } }
        internal int EndPos { get { return StartPos + Size; } set { StartPos = value - Size; } }
        internal NodeInfo<TKey> Parent, Left, Right;

    internal NodeInfo(AVLNode<TKey> node, string text)
    {
        this.Node = node;
        Text = text;
    }
}

    
