

using lab4_huffman;

string text = File.ReadAllText("/Users/danielshulevskiy/RiderProjects/lab4-huffman/lab4-huffman/sherlock.txt");

// Print char and its frequency in the Dictionary

var frequencies = Tree.CountFrequency(text);
Console.WriteLine("Frequencies:");
foreach (var kvp in frequencies)
{
    Console.WriteLine($"{kvp.Key}: {kvp.Value}");
}

Console.WriteLine("Initial bits: " + Tree.InitialBits(text));


// Create a Huffman tree
var tree = Tree.CreateTree(frequencies);

foreach (var (key, value) in tree)
{
    //Console.WriteLine(key + ": " + value);
    Console.WriteLine("Compressed bits: " + value);
    
    // % of compression
    
    Console.WriteLine("Compression: " + (Tree.InitialBits(text) - value) / (double) Tree.InitialBits(text) * 100 + "%");
    
}