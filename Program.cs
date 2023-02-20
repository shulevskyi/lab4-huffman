using lab4_huffman;

var text = "Hello"; // File.ReadAllText("../../../sherlock.txt");

var tree = new Tree(text);
var codes = tree.GetBinaryCodes();

Console.WriteLine("Binary codes:");
foreach (var pair in codes) {
    Console.WriteLine($"{pair.Key}: {pair.Value}");
}

// TODO: Add code dictionary to the compressed file 

// SimpleCompression(text);
BinaryCompression(text);

void SimpleCompression(string text) {
    // Archive the text
    var archivedText = "";
    foreach(var character in text.ToUpper())
    {
        archivedText += codes[character];
    }

    var initialSize = text.Length * 8;
    var compressedSize = archivedText.Length * 8;
    Console.WriteLine("Initial bits: " + initialSize);
    Console.WriteLine("Compressed bits: " + compressedSize);
    Console.WriteLine("Compression rate: " +  (initialSize - compressedSize) * 100 / initialSize + "%");

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

    //Console.WriteLine("Decompressed text: " + uncompressedText);
}

void BinaryCompression(string text)
{
    var bytes = new List<byte>();
    var buffer = "";
    foreach(var character in text.ToUpper())
    {
        buffer += codes[character];
        while (buffer.Length >= 8)
        {
            var byteString = buffer.Substring(0, 8);
            var b = Convert.ToByte(byteString, 2);
            bytes.Add(b);
            buffer = buffer.Substring(8);
        }
    }

    if (buffer.Length > 0)
    {
        // TODO: Get rid of PadRight
        // TODO: Restrict Tree from making codes "0"?
        var byteString = buffer.PadRight(8, '0');
        var b = Convert.ToByte(byteString, 2);
        bytes.Add(b);
    }

    var initialSize = text.Length * 8;
    var compressedSize = bytes.Count;
    Console.WriteLine("Initial bits: " + initialSize);
    Console.WriteLine("Compressed bits: " + compressedSize);
    Console.WriteLine("Compression rate: " +  (initialSize - compressedSize) * 100 / initialSize + "%");
    
    Console.WriteLine("Compressed bytes:" + string.Join(", ", bytes.Select(x => Convert.ToString(x, 2).PadLeft(8, '0'))));
    
    var reverseCodes = codes.ToDictionary(x => x.Value, x => x.Key);
    var uncompressedText = "";
    buffer = "";
    foreach (var b in bytes)
    {
        var byteString = Convert.ToString(b, 2);
        foreach (var bit in byteString)
        {
            buffer += bit;
            while (reverseCodes.ContainsKey(buffer))
            {
                uncompressedText += reverseCodes[buffer];
                buffer = "";
            }
        }
    }

    Console.WriteLine("Decompressed text: " + uncompressedText);
}