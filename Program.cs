using lab4_huffman;

class Program
{
    static void Main(string[] args)
    {
        var text = File.ReadAllText("../../../sherlock.txt");
        var tree = new Tree(text);
        var codes = tree.GetBinaryCodes();
        
        var compression = new Compressor();
        compression.BinaryCompression(text, codes);
    }
}