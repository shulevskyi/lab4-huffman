using lab4_huffman;

void Main()
{
    var text = File.ReadAllText("../../../sherlock.txt");
    var tree = new Tree(text);
    var codes = tree.GetBinaryCodes();
    BinaryCompression(text, codes);
}

void BinaryCompression(string text, Dictionary<char, string> codes)
{
    var compressedText = Compress(text, codes);
    // Console.WriteLine("Compressed text: " + compressedText);
    
    var decompressedText = Decompress(compressedText, codes);
    // Console.WriteLine("Decompressed text: " + decompressedText);
    
    OutputCodes(codes);
    OutputStatistics(text, compressedText);
}

string Compress(string text, Dictionary<char, string> codes)
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
    
    // // To handle the last byte
    var bytesString = BytesToString(bytes) + buffer;
    var one = bytesString.Length;
    
    var bytesLength = (bytesString.Length / 8) + 1;
    bytesString = bytesString.PadLeft(bytesLength * 8, '0');
    
    var diff = bytesLength * 8 - one;
    bytesString = diff + bytesString;
    
    var initialSize = text.Length * 8;
    var compressedSize = bytesString.Length + 8;
    
    return bytesString;
}

string Decompress(string compressedString, Dictionary<char, string> codes)
{
    var reverseCodes 
        = codes.ToDictionary(x => x.Value, x => x.Key);
    
    var uncompressedText = "";
    var buffer = "";
    
    compressedString = compressedString.Substring(compressedString[0] + 1);
    foreach (var bit in compressedString)
    {
        buffer += bit;
        while (reverseCodes.ContainsKey(buffer))
        {
            uncompressedText += reverseCodes[buffer];
            buffer = "";
        }
    }

    return uncompressedText;
}

void OutputStatistics(string text, string compressedText)
{
    var initialSize = text.Length * 8;
    var compressedSize = compressedText.Length;
    Console.WriteLine("Initial bits: " + initialSize);
    Console.WriteLine("Compressed bits: " + compressedSize);
    Console.WriteLine("Compression rate: " +  (initialSize - compressedSize) * 100 / initialSize + "%");
}

void OutputCodes(Dictionary<char, string> codes)
{
    Console.WriteLine("Binary codes:");
    foreach (var pair in codes) {
        Console.WriteLine($"{pair.Key}: {pair.Value}");
    }
}

string BytesToString(List<byte> bytes)
{
    var sb = "";
    foreach (var b in bytes)
    {
        sb += Convert.ToString(b, 2).PadLeft(8, '0');
    }

    return sb;
}