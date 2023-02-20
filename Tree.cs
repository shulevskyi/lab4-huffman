namespace lab4_huffman;

public class Tree
{
    private Node Root { get; set; }
    
    public Tree(string text)
    {
        var nodes = GetCharFrequencies(text);
        BuildTree(nodes);

        Root = nodes[0];
    } 

    private static void BuildTree(List<Node> nodes)
    {
        while (nodes.Count > 1)
        {
            // Sort the nodes by frequency
            nodes.Sort((x, y) => x.Frequency.CompareTo(y.Frequency));

            // Take the two nodes with the lowest frequency
            var first = nodes[0];
            var second = nodes[1];

            // Create a parent node with the sum of the two frequencies
            var parent = new Node
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
    }

    private static List<Node> GetCharFrequencies(string text)
    {
        // Creating a dictionary of chars and their frequencies
        var dict = new Dictionary<char, int>();
        foreach (var c in text.Where(char.IsLetter))
        {
            if (dict.ContainsKey(char.ToUpper(c)))
            {
                // Incrementing the value of the letter if it is already in the dictionary
                // Simplified version of CountFrequency
                dict[char.ToUpper(c)]++;
            }
            else
            {
                dict[char.ToUpper(c)] = 1;
            }
        }

        // Creating a list of nodes
        var nodes = dict.Select(pair => new Node { Symbol = pair.Key, Frequency = pair.Value }).ToList();
        return nodes;
    }

    // Get the binary codes for each letter
    public Dictionary<char, string> GetBinaryCodes() {
        var codes = new Dictionary<char, string>();
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

    // TODO: For myself (find the function or just write it again UpdatedBits)
}