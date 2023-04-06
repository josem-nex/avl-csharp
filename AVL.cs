namespace AVLTree;

public class AVLNode<TKey> where TKey : IComparable<TKey>{
    /// <summary>The main value of the node </summary>
    public TKey Key {get; set;}
    public AVLNode(TKey key, AVLNode<TKey> parent){
        Parent = parent;
        Key = key;
        LChild = null;
        RChild = null;
    }
    public AVLNode(TKey key, AVLNode<TKey> lChild, AVLNode<TKey> rChild, AVLNode<TKey> parent){
        RChild = rChild;
        LChild = lChild;
        Key = key;
        Parent = parent;
    }
    public override string ToString() => Key.ToString();
    /// <summary>If it is the root node then the parent is null.</summary>
    public AVLNode<TKey> Parent {get; set;} 
    public AVLNode<TKey> LChild {get; set;}
    public AVLNode<TKey> RChild {get; set;}
    public void TreeLeft(TKey key){
        this.LChild = new AVLNode<TKey>(key, this);
    }
    public void TreeRight(TKey key){
        this.RChild = new AVLNode<TKey>(key, this);
    }
    
    /// <summary>Is true if the node is leaf</summary>
    public bool IsLeaf{
        get{
            if((LChild==null)&&(RChild==null)) return true;
            else return false;
        }
    }
}