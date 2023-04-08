namespace BinaryTree;
/// <summary>   La interfaz común para todas las estructuras de datos
///   que sean Árbol Binario.
///</summary>

public interface IBinaryTree<TKey> where TKey : IComparable<TKey>
{
    IEnumerable<TKey> InOrder();
    bool Insert(TKey key);
    bool Contains(TKey key);
    void Print();
    bool Remove(TKey key);
    bool AssertValidTree();
}
