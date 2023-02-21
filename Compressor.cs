namespace lab4_huffman;

public class Compressor
{
    public void BinaryCompression(string text, Dictionary<char, string> codes)
    {
        var compressedText = Compress(text, codes);
        // Console.WriteLine("Compressed text: " + compressedText);
        
        var decompressedText = Decompress(compressedText);
        Console.WriteLine("Decompressed text: " + decompressedText);
        
        OutputCodes(codes);
        OutputStatistics(text, compressedText);
    }

    public string Compress(string text, Dictionary<char, string> codes)
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
        var one = bytesString.Length;
        
        var bytesLength = (bytesString.Length / 8) + 1;
        bytesString = bytesString.PadLeft(bytesLength * 8, '0');
        
        var diff = bytesLength * 8 - one;
        bytesString = diff + bytesString;
        
        bytesString = CodesToString(codes) + bytesString;
        
        return bytesString;
    }

    public string Decompress(string compressedString)
    {
        var codesString = compressedString.Substring(0, compressedString.IndexOf('९') + 1);
        compressedString = compressedString.Substring(compressedString.IndexOf('९') + 1);
        var codes = StringToCodes(codesString);
        
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
    
    static string CodesToString(Dictionary<char, string> codes)
    {
        var sb = "";
        foreach (var pair in codes)
        {
            string key = pair.Key.ToString();
            if (key == "\n") key = @"\n";
            sb += @$"{key}अ{pair.Value}स";
        }
        
        sb += "९";

        return sb;
    }
    
    static Dictionary<char, string> StringToCodes(string codesString)
    {
        var codes = new Dictionary<char, string>();
        var pairs = codesString.Split('स');
        foreach (var pair in pairs)
        {
            var keyValue = pair.Split("अ");
            
            char key;
            if (keyValue[0] == @"\n")
            {
                key = '\n';
            }
            else
            {
                if (keyValue.Length == 1) continue;
                Console.WriteLine(keyValue[0].Length
                                  + " " + keyValue[0] + " " + keyValue[1]);
                key = Convert.ToChar(keyValue[0]);
            }
            
            codes.Add(key, keyValue[1]);
        }

        return codes;
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
}