

using lab4_huffman;

string text = File.ReadAllText("/Users/danielshulevskiy/RiderProjects/lab4-huffman/lab4-huffman/sherlock.txt");

// Print char and its frequency in the Dictionary

foreach (var (key, value) in Tree.CountFrequency(text))
{
    Console.WriteLine(key + "-" + value);
}