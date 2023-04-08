using System.Collections.Generic;
namespace BinaryTree;
public class BinaryTreeABB<TKey> : IBinaryTree<TKey> where TKey : IComparable<TKey>
{
    public ABBNode<TKey> Root;

    public BinaryTreeABB() { }
    public BinaryTreeABB(ABBNode<TKey> root)
    {
        Root = root;
    }
    /// <summary>Insertar un nuevo árbol en una posición válida.</summary>
    public bool Insert(TKey key)
    {
        var node = new ABBNode<TKey>(key);
        return Insert(node);
    }
    internal bool Insert(ABBNode<TKey> node)
    {
        if (Root is null)
        {
            Root = node;
            return true;
        }
        else
        {
            ABBNode<TKey> act = Root;
            while (true)
            {
                var compare = node.Key.CompareTo(act.Key);
                if (compare is 0) return false;
                else if (compare < 0)
                {
                    if (act.LChild is not null) act = act.LChild;
                    else
                    {
                        act.LChild = node;
                        return true;
                    }
                }
                else
                {
                    if (act.RChild is not null) act = act.RChild;
                    else
                    {
                        act.RChild = node;
                        return true;
                    }
                }
            }
        }
    }
    /// <summary>Verdadero si el nodo pertenece al árbol</summary>
    public bool Contains(TKey key) => !(Find_Node(key) is null);
    /// <summary>Encontrar un nodo en el árbol.</summary>
    public ABBNode<TKey> Find_Node(TKey key) => Find_Node(key, Root);
    internal ABBNode<TKey> Find_Node(TKey key, ABBNode<TKey> node)
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
    /// <summary>Recorrido InOrder en el árbol.</summary>
    public IEnumerable<TKey> InOrder() => InOrder(Root);
    internal IEnumerable<TKey> InOrder(ABBNode<TKey> node)
    {
        ABBNode<TKey> current = this.Root;
        Stack<ABBNode<TKey>> parentStack = new Stack<ABBNode<TKey>>();
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
    /// <summary>Nodo con el valor mínimo del árbol.</summary>
    public ABBNode<TKey> Min_Value() => Min_Value_Node(Root);
    /// <summary>Nodo con el valor máximo del árbol</summary>
    public ABBNode<TKey> Max_Value() => Max_Value_Node(Root);
    internal ABBNode<TKey> Max_Value_Node(ABBNode<TKey> root)
    {
        if (root.RChild == null) return root;
        return Max_Value_Node(root.RChild);
    }
    internal ABBNode<TKey> Min_Value_Node(ABBNode<TKey> root)
    {
        if (root.LChild == null) return root;
        return Min_Value_Node(root.LChild);
    }
    /// <summary>Remover un nodo del árbol.
    /// Devuelve verdadero si el nodo se eliminó correctamente.
    /// Devuelve falso si ocurrió algún error.
    ///</summary>
    public bool Remove(TKey key) => Remove(key, Root);
    internal bool Remove(TKey key, ABBNode<TKey> root)
    {
        bool result = true;
        ABBNode<TKey> parent = null;
        ABBNode<TKey> nodeToElmininate = root;
        while (nodeToElmininate is not null)
        {
            var compare = key.CompareTo(nodeToElmininate.Key);
            if (compare == 0) break;
            else if (compare < 0)
            {
                parent = nodeToElmininate;
                nodeToElmininate = nodeToElmininate.LChild;
            }
            else
            {
                parent = nodeToElmininate;
                nodeToElmininate = nodeToElmininate.RChild;
            }
        }
        if (nodeToElmininate is null) result = false;
        else Remove_Node(nodeToElmininate, parent);
        return result;
    }
    internal void Remove_Node(ABBNode<TKey> node, ABBNode<TKey> parent)
    {
        if (parent is null)
        {
            if (node.IsLeaf) node = null;
            else if (node.OnlyLeftSon)
            {
                Root = node.LChild;
            }
            else if (node.OnlyRightSon)
            {
                Root = node.RChild;
            }
            else
            {
                ABBNode<TKey> swapNode = Max_Value_Node(node.LChild);
                TKey temp = swapNode.Key;
                Remove(temp);
                Root.Key = temp;
            }
        }
        else if (node.IsLeaf)
        {
            if (node.Key.CompareTo(parent.Key) < 0)
            {
                parent.LChild = null;
            }
            else
            {
                parent.RChild = null;
            }
        }
        else if (node.OnlyLeftSon)
        {
            if (node.Key.CompareTo(parent.Key) < 0)
            {
                parent.LChild = node.LChild;
            }
            else
            {
                parent.RChild = node.LChild;
            }
        }
        else if (node.OnlyRightSon)
        {
            if (node.Key.CompareTo(parent.Key) < 0)
            {
                parent.LChild = node.RChild;
            }
            else
            {
                parent.RChild = node.RChild;
            }
        }
        else
        {
            var swapNode = Max_Value_Node(node.LChild);
            var temp = swapNode.Key;
            Remove(temp);
            node.Key = temp;
        }
    }
    /// <summary>Escribir en consola el árbol con todos sus respectivos nodos.</summary>
    // public void Print() => Print(Root);
    // internal void Print(ABBNode<TKey> root, string textFormat = "0", int spacing = 2, int topMargin = 0, int leftMargin = 1)
    // {
    //     if (root == null) return;
    //     int rootTop = Console.CursorTop + topMargin;
    //     var last = new List<NodeInfo<TKey>>();
    //     var next = root;
    //     for (int level = 0; next != null; level++)
    //     {
    //         var item = new NodeInfo<TKey>(next, next.Key.ToString());
    //         // var item = new NodeInfo { Node = next, Text = next.Key.ToString(textFormat) };
    //         if (level < last.Count)
    //         {
    //             item.StartPos = last[level].EndPos + spacing;
    //             last[level] = item;
    //         }
    //         else
    //         {
    //             item.StartPos = leftMargin;
    //             last.Add(item);
    //         }
    //         if (level > 0)
    //         {
    //             item.Parent = last[level - 1];
    //             if (next == item.Parent.Node.LChild)
    //             {
    //                 item.Parent.Left = item;
    //                 item.EndPos = Math.Max(item.EndPos, item.Parent.StartPos - 1);
    //             }
    //             else
    //             {
    //                 item.Parent.Right = item;
    //                 item.StartPos = Math.Max(item.StartPos, item.Parent.EndPos + 1);
    //             }
    //         }
    //         next = next.LChild ?? next.RChild;
    //         for (; next == null; item = item.Parent)
    //         {
    //             int top = rootTop + 2 * level;
    //             Prints(item.Text, top, item.StartPos);
    //             if (item.Left != null)
    //             {
    //                 Prints("/", top + 1, item.Left.EndPos);
    //                 Prints("_", top, item.Left.EndPos + 1, item.StartPos);
    //             }
    //             if (item.Right != null)
    //             {
    //                 Prints("_", top, item.EndPos, item.Right.StartPos - 1);
    //                 Prints("\\", top + 1, item.Right.StartPos - 1);
    //             }
    //             if (--level < 0) break;
    //             if (item == item.Parent.Left)
    //             {
    //                 item.Parent.StartPos = item.EndPos + 1;
    //                 next = item.Parent.Node.RChild;
    //             }
    //             else
    //             {
    //                 if (item.Parent.Left == null)
    //                     item.Parent.EndPos = item.StartPos - 1;
    //                 else
    //                     item.Parent.StartPos += (item.StartPos - 1 - item.Parent.EndPos) / 2;
    //             }
    //         }
    //     }
    //     Console.SetCursorPosition(0, rootTop + 2 * last.Count - 1);
    // }
    // internal void Prints(string s, int top, int left, int right = -1)
    // {
    //     Console.SetCursorPosition(left, top);
    //     if (right < 0) right = left + s.Length;
    //     while (Console.CursorLeft < right) Console.Write(s);
    // }
    public bool AssertValidTree(){
        if(Root is not null){
            bool first = true;
            TKey previous = default(TKey);
            foreach (var item in InOrder())
            {
                if(!first&&previous.CompareTo(item)>=0) throw new Exception("InvalidTree");
                previous = item;
                first = false;
            }
        }
        return true;
    }

    public void Print()
    {
        throw new NotImplementedException();
    }
}
