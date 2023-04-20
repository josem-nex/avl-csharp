using BinaryTree;
class Program
{
    public static void Main(string[] args)
    {
        Console.Clear();

        // var binary = new AVL<int>();
        var x = new Random();
        int count = 0;
        for (int j = 0; j < 50; j++)
        {
            var binary = new AVL<int>();
            for (int i = 0; i < 150; i++)
            {
                int w = x.Next(0, 200);
                // System.Console.WriteLine("Valor a insertar: "+ w);
                binary.Insert(w);
            }
            if(binary.AssertValidTree()) {
                System.Console.WriteLine("SIUU");
                count++;
            }
            System.Console.WriteLine();
            foreach (var item in binary.InOrder())
            {
                System.Console.Write(item+"|");
            }
            System.Console.WriteLine();
        }
        
        System.Console.WriteLine();
        System.Console.WriteLine("Árboles válidos: "+ count); 

    }
    
}
