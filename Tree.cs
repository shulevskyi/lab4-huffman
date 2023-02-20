namespace lab4_huffman;

public class Tree
{
    
    // Root node of the tree 
    private Node Root { get; set; }
    
    public Tree(string text)
    {
        
        // Creating a dictionary of chars and their frequencies
        Dictionary<char, int> dict = new Dictionary<char, int>();
        foreach (char c in text)
        {
            // Checking if the char is a letter
            if (Char.IsLetter(c))
            {
                if (dict.ContainsKey(Char.ToUpper(c)))
                {
                    // Incrementing the value of the letter if it is already in the dictionary
                    // Simplified version of CountFrequency
                    dict[Char.ToUpper(c)]++;
                }
                else
                {
                    dict[Char.ToUpper(c)] = 1;
                }
            }
        }

        // Creating a list of nodes
        List<Node> nodes = new List<Node>();
        foreach (KeyValuePair<char, int> pair in dict)
        {

            // Adding a new node to the list of nodes
            nodes.Add(new Node
            {
                Symbol = pair.Key,
                Frequency = pair.Value
            });
        }

        while (nodes.Count > 1)
        {
            // Sort the nodes by frequency
            nodes.Sort((x, y) => x.Frequency.CompareTo(y.Frequency));

            // Take the two nodes with the lowest frequency
            Node first = nodes[0];
            Node second = nodes[1];

            // Create a parent node with the sum of the two frequencies
            Node parent = new Node
            {
                Frequency = first.Frequency + second.Frequency,
                Left = first,
                Right = second
            };

            // Remove the two lowest nodes and add the parent node
            nodes.RemoveAt(0);
            nodes.RemoveAt(0);
            nodes.Add(parent);
        }

        Root = nodes[0];

    }
    
    // Get the binary codes for each letter
    public Dictionary<char, string> GetBinaryCodes() {
        Dictionary<char, string> codes = new Dictionary<char, string>();
        Traverse(Root, "", codes);
        return codes;
    }
    
    // Traverse the tree and add the binary code to the dictionary
    private void Traverse(Node node, string code, Dictionary<char, string> codes) {
        if (node.Left == null && node.Right == null) {
            codes[node.Symbol.Value] = code;
        } else {
            Traverse(node.Left, code + "0", codes);
            Traverse(node.Right, code + "1", codes);
        }
    }
    
    // Func that returns bits of the string, with each char being 8 bits
    public static int InitialBits(string text)
    {
        int initialBits = 0;
        foreach (char c in text)
        {
            if (Char.IsLetter(c)) // Delete it, missed up 
            {
                initialBits += 8;
            }
        }
        
        return initialBits;
    }
    
    // For myself (find the function or just write it again UpdatedBits)
    
    

}