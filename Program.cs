using lab4_huffman;

var text = File.ReadAllText("../../../sherlock.txt");

var tree = new Tree(text);
var codes = tree.GetBinaryCodes();

Console.WriteLine("Binary codes:");
foreach (var pair in codes) {
    Console.WriteLine($"{pair.Key}: {pair.Value}");
}

// Archive the text
var archivedText = "";
foreach(var character in text.ToUpper())
{
    archivedText += codes[character];
}

var initialSize = text.Length * 8;
var compressedSize = archivedText.Length;
Console.WriteLine("Initial bits: " + initialSize);
Console.WriteLine("Compressed bits: " + compressedSize);
Console.WriteLine("Compression rate: " + initialSize * 100 / compressedSize + "%");

// Decompress the text
var reverseCodes 
    = codes.ToDictionary(x 
        => x.Value,x => x.Key);

string uncompressedText = "";
var buffer = "";
foreach(var character in archivedText.ToUpper())
{
    buffer += character;
    if (reverseCodes.ContainsKey(buffer))
    {
        uncompressedText += reverseCodes[buffer];
        buffer = "";
    }
}

Console.WriteLine("Decompressed text: " + uncompressedText);

