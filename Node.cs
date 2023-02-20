namespace lab4_huffman;

class Node : IHeapable {
    public char? Symbol { get; set; }
    public int Frequency { get; set; }
    public Node Left { get; set; }
    public Node Right { get; set; }

    public int GetNumber => Frequency;
}