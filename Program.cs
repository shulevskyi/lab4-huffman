using lab4_huffman;

string text = File.ReadAllText("../../../sherlock.txt");

// Print char and its frequency in the Dictionary

Tree tree = new Tree(text);
Dictionary<char, string> codes = tree.GetBinaryCodes();

Console.WriteLine("Initial bits: " + text.Length * 8);

Console.WriteLine("Binary codes:");
foreach (KeyValuePair<char, string> pair in codes) {
    Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
}