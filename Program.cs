namespace AVL;
class Program
{
    public static void Main(string[] args)
    {
        Console.Clear();

        var test2 = new TreeAVL(20, null);

        test2.TreeLeft(16);
        test2.TreeRight(30);
        test2.Left.TreeLeft(10);
        test2.Left.TreeRight(18);
        test2.Right.TreeLeft(27);
        test2.Right.TreeRight(40);
        test2.Left.Left.TreeLeft(8);
        test2.Left.Left.TreeRight(12);
        test2.Left.Right.TreeLeft(17);
        test2.Left.Right.TreeRight(19);
        test2.Right.Left.TreeLeft(25);
        test2.Right.Left.TreeRight(29);
        test2.Right.Right.TreeLeft(37);
        test2.Right.Right.TreeRight(45);

        var fact = new Factory();
        fact.Print(test2);
        fact.Delete(30,test2);
        fact.Print(test2);

    }
    
}
