using System;
using System.Collections.Generic;

namespace AVL;
/// <summary>All the operations in the TreeAVL </summary>
public class Factory
{
    public void InOrder(TreeAVL root  ){
        if(root.Left is not null) InOrder(root.Left);
        System.Console.WriteLine(root);
        if(root.Right is not null) InOrder(root.Right);
    }
    public bool Exists(int value_find, TreeAVL root){
        var n = Find_Node(value_find, root);
        if(n is not null && n.Value == value_find) return true;
        else return false;
    }
    public TreeAVL Find_Node(int value_find, TreeAVL root){
        if(root==null) return null;
        if(root.Value == value_find) return root;
        if(value_find<root.Value){
            root = root.Left;
            return Find_Node(value_find, root);
        }
        if(value_find>root.Value){
            root=root.Right;
            return Find_Node(value_find, root);
        }
        return null;
    }
    public bool Insert(int value_find, TreeAVL root){
        var n = Find_Node(value_find, root);
        if(n is not null && n.Value==value_find) {
            return false;
            }
        if(root.Value < value_find){
            if(root.Right == null){
                root.TreeRight(value_find);
                return true;
            }
            root = root.Right;
            Insert(value_find,root);
        }
        if(root.Value > value_find){
            if(root.Left == null){
                root.TreeLeft(value_find);
                return true;
            }
            root = root.Left;
            Insert(value_find,root);
        }
        return false;
        
    }
    public void Print(TreeAVL  root, string textFormat = "0", int spacing = 2, int topMargin = 0, int leftMargin = 1)
    {
        if (root == null) return;
        int rootTop = Console.CursorTop + topMargin;
        var last = new List<NodeInfo>();
        var next = root;
        for (int level = 0; next != null; level++)
        {
            var item = new NodeInfo { Node = next, Text = next.Value.ToString(textFormat) };
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
                if (next == item.Parent.Node.Left)
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
            next = next.Left ?? next.Right;
            for (; next == null; item = item.Parent)
            {
                int top = rootTop + 2 * level;
                Print(item.Text, top, item.StartPos);
                if (item.Left != null)
                {
                    Print("/", top + 1, item.Left.EndPos);
                    Print("_", top, item.Left.EndPos + 1, item.StartPos);
                }
                if (item.Right != null)
                {
                    Print("_", top, item.EndPos, item.Right.StartPos - 1);
                    Print("\\", top + 1, item.Right.StartPos - 1);
                }
                if (--level < 0) break;
                if (item == item.Parent.Left)
                {
                    item.Parent.StartPos = item.EndPos + 1;
                    next = item.Parent.Node.Right;
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
    private void Print(string s, int top, int left, int right = -1)
    {
        Console.SetCursorPosition(left, top);
        if (right < 0) right = left + s.Length;
        while (Console.CursorLeft < right) Console.Write(s);
    }
    public TreeAVL Max_Value_Node(TreeAVL root){
        if(root.Right == null) return root;
        return Max_Value_Node(root.Right);
    }
    public TreeAVL Min_Value_Node(TreeAVL root){
        if(root.Left == null) return root;
        return Min_Value_Node(root.Left);
    }
    public bool Delete(int value_find, TreeAVL root){
        //This is a simplistic delete implementation, 
        //it could improve knowing the levels and the number of children of each node
        var node = Find_Node(value_find, root);
        if(node is null) return false;
        if(node.Parent is null) return false;
        var parent = node.Parent;
        if(parent.Left == node){
            
            // if node is the left child
            if(node.IsLeaf){
                // if is leaf is trivial
                parent.Left = null;
                return true;
            }
            if(node.Left == null){
                // if the node to eliminate has no left child 
                parent.Left = node.Right;
                node.Right.Parent = parent;
                return true;
            }
            if(node.Right == null){
                // if the node to eliminate has no right child 
                parent.Left = node.Left;
                node.Left.Parent = parent;
                return true;
            }
            var newnode = Max_Value_Node(node.Left);
            node.Value = newnode.Value;
            if(newnode.Parent.Value!= node.Value){
                // act the reference
                newnode.Parent.Right = null;
            }
            return true;
        }else{
            // if node is the right child
            if(node.IsLeaf){
                // if is leaf is trivial
                parent.Right = null;
                return true;
            }
            if(node.Left == null){
                // if the node to eliminate has no left child 

                parent.Right = node.Right;
                node.Right.Parent = parent;
                return true;
            }
            if(node.Right == null){
                // if the node to eliminate has no right child 
                parent.Right = node.Left;
                node.Left.Parent = parent;
                return true;
            }
            var newnode = Max_Value_Node(node.Left);
            node.Value = newnode.Value;
            if(newnode.Parent.Value!= node.Value){
                // act the reference
                newnode.Parent.Right = null;
            }
            return true;
        }
        return false;
    }
}