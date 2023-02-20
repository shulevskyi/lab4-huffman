namespace lab4_huffman;

public class Tree
{
    
    // Count frequency of each char in text    
    public static Dictionary<string, int> CountFrequency(string text)
    {
        Dictionary<string, int> dict = new Dictionary<string, int>();
        foreach (char c in text)
        {
            if (Char.IsLetter(c))
            {
                if (dict.ContainsKey(Char.ToUpper(c).ToString()))
                {
                    dict[Char.ToUpper(c).ToString()]++;
                }
                else
                {
                    dict.Add(Char.ToUpper(c).ToString(), 1);
                }
            }
        }
        
        // Sort in descending order
        dict = dict.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        
        return dict;
    }
    
    // Func that returns bits of the string, with each char being 8 bits
    
    public static int InitialBits(string text)
    {
        int initialBits = 0;
        foreach (char c in text)
        {
            if (Char.IsLetter(c))
            {
                initialBits += 8;
            }
        }
        
        return initialBits;
    }
    
    // Func that creates a leaf node     
    public static Dictionary<string, int> CreateLeaf(Dictionary<string, int> dict)
    {
        // first = lowest frequency
        // second = second lowest frequency
        
        var first = dict.Last();

        var second = dict.ElementAt(dict.Count - 2);
        
        // first = child node with binary number 0
        // second = child node with binary number 1
        
        
        // binaryCodes = dictionary with binary numbers for each char
        
        var binaryCodes = new Dictionary<string, int>();
        
        // Add binary numbers to the dictionary
        
        binaryCodes.Add(first.Key, 0);
        binaryCodes.Add(second.Key, 1);


        // Pointer to the parent node
        var parent = first.Key + "->" + second.Key;
        
        // Add parent node to the dictionary as an ordinary node 
        dict.Add(parent, first.Value + second.Value);
        
        var parentValue = dict[parent];
        
        dict.Remove(first.Key);
        dict.Remove(second.Key);
        
        
        return dict;
    }
    
    // Fucntion that firsly creates a leaf node, then creates a parent node
    
    public static Dictionary<string, int> CreateTree(Dictionary<string, int> dict)
    {
        // Create a tree
        while (dict.Count > 1)
        {
            dict = CreateLeaf(dict);
        }
        
        return dict;
    }
    



}