using lab4_huffman;

string text = File.ReadAllText("/Users/danielshulevskiy/RiderProjects/lab4-huffman/lab4-huffman/sherlock.txt");

// Print char and its frequency in the Dictionary

Tree tree = new Tree(text);
Dictionary<char, string> codes = tree.GetBinaryCodes();

Console.WriteLine("Binary codes:");
foreach (KeyValuePair<char, string> pair in codes) {
    Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
}