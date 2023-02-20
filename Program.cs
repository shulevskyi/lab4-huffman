

using lab4_huffman;

string text = File.ReadAllText("/Users/danielshulevskiy/RiderProjects/lab4-huffman/lab4-huffman/sherlock.txt");

// Print char and its frequency in the Dictionary

Dictionary<string, int> diction = Tree.CountFrequency(text);

foreach (var (key, value) in Tree.CountFrequency(text))
{
    Console.WriteLine(key + "-" + value);
}

Console.WriteLine("Initial bits: " + Tree.InitialBits(text));


// Create a leaf node
var dict = Tree.CreateLeaf(diction);

foreach (var (key, value) in dict)
{
    Console.WriteLine(key + "-" + value);
}


// CreateTree
var tree = Tree.CreateTree(dict);

foreach (var (key, value) in tree)
{
    Console.WriteLine(key + "--" + value);
}