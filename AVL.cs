namespace BinaryTree;

public class ABBNode<TKey> where TKey : IComparable<TKey>{
    /// <summary>The main value of the node </summary>
    public TKey Key {get; set;}
    public ABBNode(TKey key, ABBNode<TKey> parent){
        Parent = parent;
        Key = key;
        LChild = null;
        RChild = null;
    }
    public ABBNode(TKey key, ABBNode<TKey> lChild, ABBNode<TKey> rChild, ABBNode<TKey> parent){
        RChild = rChild;
        LChild = lChild;
        Key = key;
        Parent = parent;
    }
    public override string ToString() => Key.ToString();
    /// <summary>If it is the root node then the parent is null.</summary>
    public ABBNode<TKey> Parent {get; set;} 
    public ABBNode<TKey> LChild {get; set;}
    public ABBNode<TKey> RChild {get; set;}
    public void TreeLeft(TKey key){
        this.LChild = new ABBNode<TKey>(key, this);
    }
    public void TreeRight(TKey key){
        this.RChild = new ABBNode<TKey>(key, this);
    }
    
    /// <summary>Is true if the node is leaf</summary>
    public bool IsLeaf{
        get{
            if((LChild==null)&&(RChild==null)) return true;
            else return false;
        }
    }
}