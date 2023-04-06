namespace BinaryTree;
class Program
{
    public static void Main(string[] args)
    {
        Console.Clear();

        var test2 = new ABBNode<int>(20, null);
        var binary = new BinaryTree<int>(test2);
        binary.Insert(15);
        binary.Insert(30);
        binary.Insert(10);
        binary.Insert(18);
        binary.Insert(27);
        binary.Insert(40);
        binary.Insert(8);
        binary.Insert(12);
        binary.Insert(17);
        binary.Insert(19);
        binary.Insert(25);
        binary.Insert(29);
        binary.Insert(37);
        binary.Insert(45);




        Random x = new Random();
        var rand = x.Next(1,50);

        // binary.Print();





        /* #region TEST_INSERT
        binary.Print();
        System.Console.WriteLine("El valor a intentar insertar será: "+ rand);
        try
        {
            binary.Insert(rand);
            binary.Print();
        }
        catch 
        {
            System.Console.WriteLine("Ya existe ese valor");
        }
        #endregion */

        /* #region TEST_FIND
        binary.Print();
        System.Console.WriteLine("El nodo a buscar es: "+rand);
        var test = binary.Find_Node(rand);
        if(test is not null) System.Console.WriteLine("Sí contiene al nodo: "+ test.Key);
        else System.Console.WriteLine("No lo contiene");


        #endregion */
        
        #region TEST_Remove
        binary.Print();
        System.Console.WriteLine("El nodo a remover es: "+rand);
        if(!(binary.Find_Node(rand) is null)) System.Console.WriteLine("Sí lo contiene");
        else System.Console.WriteLine("No lo contiene");
        binary.Remove_Node(rand);
        System.Console.WriteLine("Everything OK");
        binary.Print();


        #endregion




    }
    
}
