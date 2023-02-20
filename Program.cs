using lab4_huffman;

var text = "Hello my"; // File.ReadAllText("../../../sherlock.txt");

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
    
    // To handle the last byte
    var bytesString = BytesToString(bytes) + buffer;
    var diff = 8 - buffer.Length;
    var bytesLength = (bytesString.Length / 8) + 1;
    bytesString = bytesString.PadLeft(bytesLength * 8, '0');
    bytesString = diff + bytesString;

    var initialSize = text.Length * 8;
    var compressedSize = bytesString.Length + 8;
    Console.WriteLine("Initial bits: " + initialSize + " | Compressed bits: " + compressedSize + " | Compression rate: " +  (initialSize - compressedSize) * 100 / initialSize + "%");
    Console.WriteLine("Compressed bytes:" + bytesString);
    
    var reverseCodes = codes.ToDictionary(x => x.Value, x => x.Key);
    var uncompressedText = "";
    buffer = "";

    var compressedString = bytesString;
    compressedString = compressedString.Substring(diff + 1);
    foreach (var bit in compressedString)
    {
        buffer += bit;
        while (reverseCodes.ContainsKey(buffer))
        {
            uncompressedText += reverseCodes[buffer];
            buffer = "";
        }
    }

    Console.WriteLine("Decompressed text: " + uncompressedText);
}

string BytesToString(List<byte> bytes)
{
    var sb = "";
    foreach (var b in bytes)
    {
        sb += Convert.ToString(b, 2);
    }

    return sb;
}