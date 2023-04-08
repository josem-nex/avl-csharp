namespace BinaryTree;

public class ABBNode<TKey> where TKey : IComparable<TKey>
{
    public TKey Key { get; internal set; }
    internal ABBNode(TKey key)
    {
        Key = key;
        LChild = null;
        RChild = null;
    }
    internal ABBNode(TKey key, ABBNode<TKey> lChild, ABBNode<TKey> rChild)
    {
        RChild = rChild;
        LChild = lChild;
        Key = key;
    }
    public override string ToString() => Key.ToString();
    public ABBNode<TKey> LChild { get; internal set; }
    public ABBNode<TKey> RChild { get; internal set; }
    /// <summary>Retorna verdadero si el nodo es hoja. Falso en caso contrario.</summary>
    internal bool IsLeaf
    {
        get
        {
            if ((LChild == null) && (RChild == null)) return true;
            else return false;
        }
    }
    /// <summary>Retorna verdadero si el nodo solo tiene hijo izquierdo. 
    /// Falso en caso contrario.</summary>
    internal bool OnlyLeftSon
    {
        get
        {
            if ((LChild is not null) && (RChild is null)) return true;
            else return false;
        }
    }
    /// <summary>Retorna verdadero si el nodo solo tiene hijo derecho.
    /// Falso en caso contrario.</summary>
    internal bool OnlyRightSon
    {
        get
        {
            if ((RChild is not null) && (LChild is null)) return true;
            else return false;
        }
    }
}
