namespace BinaryTree;

public class AVL<TKey> : IBinaryTree<TKey> where TKey : IComparable<TKey>
{
    public AVLNode<TKey> Root;
    public AVL(){}
    public AVL(AVLNode<TKey> root)
    {
        Root = root;
    }

    public bool AssertValidTree()
    {
        if (this.Root != null)
        {
            AVLNode<TKey> previousNode = null;
            foreach (AVLNode<TKey> node in this.InOrderNode())
            {
                if (previousNode != null && previousNode.Key.CompareTo(node.Key) >= 0)
                    throw new Exception("InvalidTree");
                node.ResetHeight();
                if (node.Balance < -1 || node.Balance > 1)
                    throw new Exception("InvalidTree");
                
                previousNode = node;
            }
        }
        return true;
    }
    public AVLNode<TKey> Find_Node(TKey key) => Find_Node(key, Root);
    internal AVLNode<TKey> Find_Node(TKey key, AVLNode<TKey> node)
    {
        if (key.CompareTo(node.Key) == 0) return node;
        if (key.CompareTo(node.Key) < 0)
        {
            if (node.LChild is null) return null;
            return Find_Node(key, node.LChild);
        }
        else
        {
            if (node.RChild is null) return null;
            return Find_Node(key, node.RChild);
        }
    }
    /// <summary>Nodo con el valor mínimo del árbol</summary>
    public AVLNode<TKey> Min_Value() => Min_Value_Node(Root);
    /// <summary>Nodo con el valor máximo del árbol</summary>
    public AVLNode<TKey> Max_Value() => Max_Value_Node(Root);
    internal AVLNode<TKey> Max_Value_Node(AVLNode<TKey> root)
    {
        if (root.RChild == null) return root;
        return Max_Value_Node(root.RChild);
    }
    internal AVLNode<TKey> Min_Value_Node(AVLNode<TKey> root)
    {
        if (root.LChild == null) return root;
        return Min_Value_Node(root.LChild);
    }
    public bool Contains(TKey key) => !(Find_Node(key) is null);

    public IEnumerable<TKey> InOrder()
    {
        AVLNode<TKey> current = this.Root;
        Stack<AVLNode<TKey>> parentStack = new Stack<AVLNode<TKey>>();
        while (current != null || parentStack.Count != 0)
        {
            if (current != null)
            {
                parentStack.Push(current);
                current = current.LChild;
            }
            else
            {
                current = parentStack.Pop();
                yield return current.Key;
                current = current.RChild;
            }
        }
    }
    public IEnumerable<AVLNode<TKey>> InOrderNode(){
        AVLNode<TKey> current = this.Root;
        Stack<AVLNode<TKey>> parentStack = new Stack<AVLNode<TKey>>();
        while (current != null || parentStack.Count != 0)
        {
            if (current != null)
            {
                parentStack.Push(current);
                current = current.LChild;
            }
            else
            {
                current = parentStack.Pop();
                yield return current;
                current = current.RChild;
            }
        }
    }
    public void RebalanceNode(AVLNode<TKey> node, AVLNode<TKey> posD){
        var parent = Father(node);
        if(node.Balance>=-1 && node.Balance <= 1) return;
        else if(node.Balance>1){
            if(node.RChild.Balance>=1) parent.RChild =node.RotateLeft();
            else{
                // System.Console.WriteLine("aaaaaaaaaaaaa");
                node.RChild = posD.RotateRight();
                parent.RChild = node.RotateLeft();
            }
        }else{
            if(node.LChild.Balance<=1) parent.LChild= node.RotateRight();
            else
            {
                node.LChild = node.LChild.RotateLeft();
                parent.LChild = node.RotateRight();
            }
        }
    }
    public AVLNode<TKey> Father(AVLNode<TKey> node){
        AVLNode<TKey> parent = null;
        var key = node.Key;    
        AVLNode<TKey> nodeC = Root;
        while (nodeC is not null)
        {
            var compare = key.CompareTo(nodeC.Key);
            if (compare == 0) break;
            else if (compare < 0)
            {
                parent = nodeC;
                nodeC = nodeC.LChild;
            }
            else
            {
                parent = nodeC;
                nodeC = nodeC.RChild;
            }
        }
        return parent;
    }
    public bool Insert(TKey key)
    {
        var node = new AVLNode<TKey>(key);
        var act = Insert(node);
        if(act is null) return false;
        var parent = Father(act);
        if((act.Balance != 0)&&(act!= Root)&&(parent.Balance!=0)){
            Console.Clear();
            System.Console.WriteLine(parent.Key+ "|" + parent.Balance + "____"+ act.Key+"|"+act.Balance);
            Print(Root);
            RebalanceNode(parent, act);
        }
        return true;
    }
    internal AVLNode<TKey> Insert(AVLNode<TKey> node)
    {
        if (Root is null)
        {
            Root = node;
            return Root;
        }
        else
        {
            AVLNode<TKey> act = Root;
            while (true)
            {
                act.ResetHeight();
                var compare = node.Key.CompareTo(act.Key);
                if (compare is 0) return null;
                else if (compare < 0)
                {
                    if (act.LChild is not null) act = act.LChild;
                    else
                    {
                        act.LChild = node;
                        break;
                    }
                }
                else
                {
                    if (act.RChild is not null) act = act.RChild;
                    else
                    {
                        act.RChild = node;
                        break;
                    }
                }
            } 
            return act;
        }
    }
    public void Print() => Print(Root);
    internal void Print(AVLNode<TKey> root, string textFormat = "0", int spacing = 2, int topMargin = 0, int leftMargin = 1)
    {
        if (root == null) return;
        int rootTop = Console.CursorTop + topMargin;
        var last = new List<NodeInfo<TKey>>();
        var next = root;
        for (int level = 0; next != null; level++)
        {
            var item = new NodeInfo<TKey>(next, next.Key.ToString());
            // var item = new NodeInfo { Node = next, Text = next.Key.ToString(textFormat) };
            if (level < last.Count)
            {
                item.StartPos = last[level].EndPos + spacing;
                last[level] = item;
            }
            else
            {
                item.StartPos = leftMargin;
                last.Add(item);
            }
            if (level > 0)
            {
                item.Parent = last[level - 1];
                if (next == item.Parent.Node.LChild)
                {
                    item.Parent.Left = item;
                    item.EndPos = Math.Max(item.EndPos, item.Parent.StartPos - 1);
                }
                else
                {
                    item.Parent.Right = item;
                    item.StartPos = Math.Max(item.StartPos, item.Parent.EndPos + 1);
                }
            }
            next = next.LChild ?? next.RChild;
            for (; next == null; item = item.Parent)
            {
                int top = rootTop + 2 * level;
                Prints(item.Text, top, item.StartPos);
                if (item.Left != null)
                {
                    Prints("/", top + 1, item.Left.EndPos);
                    Prints("_", top, item.Left.EndPos + 1, item.StartPos);
                }
                if (item.Right != null)
                {
                    Prints("_", top, item.EndPos, item.Right.StartPos - 1);
                    Prints("\\", top + 1, item.Right.StartPos - 1);
                }
                if (--level < 0) break;
                if (item == item.Parent.Left)
                {
                    item.Parent.StartPos = item.EndPos + 1;
                    next = item.Parent.Node.RChild;
                }
                else
                {
                    if (item.Parent.Left == null)
                        item.Parent.EndPos = item.StartPos - 1;
                    else
                        item.Parent.StartPos += (item.StartPos - 1 - item.Parent.EndPos) / 2;
                }
            }
        }
        Console.SetCursorPosition(0, rootTop + 2 * last.Count - 1);
    }
    internal void Prints(string s, int top, int left, int right = -1)
    {
        Console.SetCursorPosition(left, top);
        if (right < 0) right = left + s.Length;
        while (Console.CursorLeft < right) Console.Write(s);
    }

    public bool Remove(TKey key)
    {
        throw new NotImplementedException();
    }
}
