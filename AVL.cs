namespace AVL;

public class TreeAVL{
    /// <summary>The main value of the node </summary>
    public int Value{get; set;}
    public int Balance_factor {get; set;}
    public TreeAVL(int value, TreeAVL parent){
        Parent = parent;
        Value = value;
        Left = null;
        Right = null;
    }
    public override string ToString() => Value.ToString();
    /// <summary>If it is the root node then the parent is null.</summary>
    public TreeAVL Parent {get; set;} 
    public TreeAVL Left {get; set;}
    public TreeAVL Right {get; set;}
    public void TreeLeft(int value){
        this.Left = new TreeAVL(value, this);
    }
    public void TreeRight(int value){
        this.Right = new TreeAVL(value, this);
    }
    
    /// <summary>Is true if the node is leaf</summary>
    public bool IsLeaf{
        get{
            if((Left==null)&&(Right==null)) return true;
            else return false;
        }
    }
    public override bool Equals(object obj) => this.ToString() ==obj.ToString();
    

}