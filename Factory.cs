namespace BinaryTree;
public class BinaryTree<TKey> where TKey : IComparable<TKey>{
    public ABBNode<TKey> Root;

    public BinaryTree(){}
    public BinaryTree(ABBNode<TKey> root)
    {
        Root = root;
    }

    public void Insert(TKey key){
        Root = Insert(key,Root);
    }
    private ABBNode<TKey> Insert(TKey key, ABBNode<TKey> node){
        if(node is null) node = new ABBNode<TKey>(key, null);
        else{
            if(key.CompareTo(node.Key)==0) throw new Exception("This node already exists");
            if(key.CompareTo(node.Key)<0){
                node.LChild = Insert(key, node.LChild);
            }else{
                node.RChild = Insert(key, node.RChild);
            }
        }
        return node;
    }
    public ABBNode<TKey> Find_Node(TKey key) => Find_Node(key, Root );
    private ABBNode<TKey> Find_Node(TKey key, ABBNode<TKey> node){
        if(key.CompareTo(node.Key)==0) return node;
        if(key.CompareTo(node.Key)<0){
            if(node.LChild is null) return null;
            return Find_Node(key, node.LChild);
        }else{
            if(node.RChild is null) return null;
            return Find_Node(key, node.RChild);
        }
    }
    public void InOrder()=> InOrder(Root);
    private void InOrder(ABBNode<TKey> node){
        if(node.LChild is not null) InOrder(node.LChild);
        System.Console.WriteLine(node.Key);
        if(node.RChild is not null) InOrder(node.RChild);
    }
    public ABBNode<TKey> Max_Value() => Max_Value_Node(Root);
    public ABBNode<TKey> Min_Value() => Min_Value_Node(Root);

    private ABBNode<TKey> Max_Value_Node(ABBNode<TKey> root){
        if(root.RChild == null) return root;
        return Max_Value_Node(root.RChild);
    }
    private ABBNode<TKey> Min_Value_Node(ABBNode<TKey> root){
        if(root.LChild == null) return root;
        return Min_Value_Node(root.LChild);
    }
    public bool Remove_Node(TKey key) => Remove_Node(key, Root);

    private bool Remove_Node(TKey key, ABBNode<TKey> root){   // ME QUEDE TRABAJANDO AQUI
        //This is a simplistic delete implementation, 
        //it could improve knowing the levels and the number of children of each node
        var node = Find_Node(key, root);
        if(root is null || root.Parent is null) return false;
        var parent = root.Parent;
        if(parent.LChild == node){
            
            // if node is the LChild child
            if(node.IsLeaf){
                // if is leaf is trivial
                parent.LChild = null;
                return true;
            }
            if(node.LChild == null){
                // if the node to eliminate has no LChild child 
                parent.LChild = node.RChild;
                node.RChild.Parent = parent;
                return true;
            }
            if(node.RChild == null){
                // if the node to eliminate has no RChild child 
                parent.LChild = node.LChild;
                node.LChild.Parent = parent;
                return true;
            }
            var newnode = Max_Value_Node(node.LChild);
            node.Key = newnode.Key;
            if(newnode.Parent.Key.CompareTo(node.Key)!=0 ){
                // act the reference
                newnode.Parent.RChild = null;
            }
            return true;
        }else{
            // if node is the RChild child
            if(node.IsLeaf){
                // if is leaf is trivial
                parent.RChild = null;
                return true;
            }
            if(node.LChild == null){
                // if the node to eliminate has no LChild child 

                parent.RChild = node.RChild;
                node.RChild.Parent = parent;
                return true;
            }
            if(node.RChild == null){
                // if the node to eliminate has no RChild child 
                parent.RChild = node.LChild;
                node.LChild.Parent = parent;
                return true;
            }
            var newnode = Max_Value_Node(node.LChild);
            node.Key = newnode.Key;
            if(newnode.Parent.Key.CompareTo(node.Key)!=0 ){
                // act the reference
                newnode.Parent.RChild = null;
            }
            return true;
        }
    } 

    #region REMOVE_TEST
     /* 
    private ABBNode<TKey> Remove_Node(TKey key,ABBNode<TKey> node)
        {
            if (node == null) return null;
            
            if (key.CompareTo(node.Key) < 0)
            {
                if (node.LChild == null) return node;
                
                node.LChild = Remove_Node(key, node.LChild);
            }
            else if (key.CompareTo(node.Key) > 0)
            {
                if (node.RChild == null) return node;
                
                node.RChild = Remove_Node(key, node.RChild);
            }
            else if (key.CompareTo(node.Key) == 0)
            {
                var findNode = Remove_Node(key,node.LChild);

                node = Move(node, findNode);
            }
            

            return node;
        }
    private ABBNode<TKey> Move(ABBNode<TKey> node, ABBNode<TKey> findNode)
        {
            ABBNode<TKey> moveNode;

            if (findNode != null)
            {
                if (findNode.RChild != null)
                {
                    moveNode = findNode.RChild;

                    findNode.RChild = null;
                }
                else
                {
                    findNode.LChild = null;

                    moveNode = findNode;
                }
                
                if (node.LChild != moveNode) moveNode.LChild = node.LChild;

                if (node.RChild != moveNode) moveNode.RChild = node.RChild;
            }
            else
            {
                moveNode = null;
            }

            node.LChild = null;

            node.RChild = null;

            node.Key = default(TKey);

            return moveNode;
        }
     */
    #endregion //ENDTEST
    public void Print() => Print(Root);
    private void Print(ABBNode<TKey>  root, string textFormat = "0", int spacing = 2, int topMargin = 0, int leftMargin = 1)
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
    private void Prints(string s, int top, int left, int right = -1)
    {
        Console.SetCursorPosition(left, top);
        if (right < 0) right = left + s.Length;
        while (Console.CursorLeft < right) Console.Write(s);
    }
}
