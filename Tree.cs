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
        
        // Pointer to the parent node
        var parent = first.Key + "->" + second.Key;
        
        dict.Add(parent, first.Value + second.Value);
        
        dict.Remove(first.Key);
        dict.Remove(second.Key);
        
        // sort the dictionary in descending order
        //dict = dict.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        
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