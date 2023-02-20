using lab4_huffman;

var text = File.ReadAllText("../../../sherlock.txt");

var tree = new Tree(text);
var codes = tree.GetBinaryCodes();

Console.WriteLine("Initial bits: " + text.Length * 8);

Console.WriteLine("Binary codes:");
foreach (var pair in codes) {
    Console.WriteLine($"{pair.Key}: {pair.Value}");
}