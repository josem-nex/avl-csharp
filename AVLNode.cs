namespace BinaryTree;

public class AVLNode<TKey> where TKey : IComparable<TKey>
{
    public AVLNode(TKey key)
    {
        Key = key;
        RChild = null;
        LChild = null;
        _height = int.MinValue;
        Parent = null;
    }
    /// <summary>Value del nodo, debe ser un valor IComparable.</summary>
    public TKey Key { get; internal set; }
    /// <summary>El padre del nodo.</summary>
    public AVLNode<TKey> Parent { get; internal set; }
    /// <summary>El hijo derecho del nodo.</summary>
    public AVLNode<TKey> RChild { get; internal set; }
    /// <summary>El hijo izquierdo del nodo.</summary>
    public AVLNode<TKey> LChild { get; internal set; }
    private int _height;
    /// <summary>Altura del árbol.</summary>
    internal int Height
    {
        get
        {
            if (this._height == int.MinValue)
            {
                this._height = this.GetHeight();
            }
            return this._height;
        }
    }
    /// <summary>El factor de balance del árbol, balanceado es -1,0,1.</summary>
    internal int Balance { get => GetBalance(); }
    public override string ToString() => Key.ToString();
    // Rotar el árbol sobre R                   Resulta en:
    // 
    //        R                                        B
    //       / \                                      / \
    //      A    B                                   R   F
    //     / \  / \                                 / \
    //    C   D E  F                               A   E
    //                                            / \    
    //                                           C   D
    //                                           
    /// <summary>
    /// Rota el árbol en contra de las manecillas del reloj.
    /// </summary>
    /// <returns>La nueva raíz del árbol.</returns>
    internal AVLNode<TKey> RotateLeft()
    {
        AVLNode<TKey> pivote = this.RChild;
        if (pivote.LChild is not null)
        {
            this.RChild = pivote.LChild;
            this.RChild.Parent = this;
        }
        else
        {
            this.RChild = null;
        }
        pivote.LChild = this;
        this.Parent = pivote;
        if (this.Parent is not null)
        {
            this.Parent = pivote;
        }
        //arreglar altura
        pivote.ResetHeight();
        this.ResetHeight();
        return pivote;
    }
    // Rotar el árbol sobre R                   Resulta en:
    // 
    //        R                                        A
    //       / \                                      / \
    //      A    B                                   C   R
    //     / \  / \                                     / \
    //     C  D E  F                                   D   B
    //                                                    / \          
    //                                                   E   F
    //                                           
    /// <summary>
    /// Rota el árbol en contra de las manecillas del reloj.
    /// </summary>
    /// <returns>La nueva raíz del árbol.</returns>
    internal AVLNode<TKey> RotateRight()
    {
        AVLNode<TKey> pivote = this.LChild;
        if (pivote.RChild is not null)
        {
            this.LChild = pivote.RChild;
            this.LChild.Parent = this;
        }
        else
        {
            this.LChild = null;
        }
        pivote.RChild = this;
        this.Parent = pivote;
        if (this.Parent is not null)
        {
            this.Parent = pivote;
        }
        //arreglar altura
        pivote.ResetHeight();
        this.ResetHeight();
        return pivote;
    }
    /// <returns>La altura de un nodo, -1 si es null.</returns>
    private int GetChildHeight(AVLNode<TKey> node) => (node is null) ? -1 : node.Height;
    /// <returns>La altura del nodo actual calculada como Max(Hijoizq,Hijoder)+1.</returns>
    internal int GetHeight() => (System.Math.Max(GetChildHeight(this.LChild), GetChildHeight(this.RChild)) + 1);
    /// <returns>
    /// El balance del nodo calculado como la altura del nodo izquierdo - altura del nodo derecho.
    /// </returns>
    internal int GetBalance() => (GetChildHeight(this.RChild) - GetChildHeight(this.LChild));
    /// <summary>Resetea el valor de la altura. En la próxima operación habrá que recalcularlo.</summary>
    internal void ResetHeight()
    {
        this._height = int.MinValue;
    }
    /// <summary>Verifica si un nodo es hoja o no.</summary>
    /// <returns>Verdadero si el nodo es hoja, falso en caso contrario.</returns>
    internal bool IsLeaf
    {
        get
        {
            if ((LChild == null) && (RChild == null)) return true;
            else return false;
        }
    }
    /// <summary>Verifica si el nodo solo tiene hijo izquierdo.</summary>
    /// <returns>Verdadero si el nodo solo tiene hijo izquierdo, falso en caso contrario.</returns>
    internal bool OnlyLeftSon
    {
        get
        {
            if ((LChild is not null) && (RChild is null)) return true;
            else return false;
        }
    }
    /// <summary>Verifica si el nodo solo tiene hijo derecho.</summary>
    /// <returns>Verdadero si el nodo solo tiene hijo derecho, falso en caso contrario.</returns>
    internal bool OnlyRightSon
    {
        get
        {
            if ((RChild is not null) && (LChild is null)) return true;
            else return false;
        }
    }

}