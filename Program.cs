using BinaryTree;
class Program
{
    public static void Main(string[] args)
    {
        Console.Clear();

        var binary = new AVL<int>();
        binary.Insert(20);
        binary.Insert(15);
        binary.Insert(30);
        binary.Insert(10);
        binary.Insert(18);
        binary.Insert(27);
        // binary.Print();

        binary.Insert(40);
        binary.Insert(8);
        binary.Insert(12);
        binary.Insert(17);
        binary.Insert(19);
        binary.Insert(25);
        binary.Insert(29);
        // binary.Print();

        binary.Insert(37);
        binary.Insert(45);
        binary.Insert(49);
        binary.Print();
        binary.Insert(47);

        // binary.Insert(59);




        binary.Print();

        System.Console.WriteLine(binary.AssertValidTree());
    }
    
}
