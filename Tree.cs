namespace lab4_huffman;

public class Tree
{
    
    // Count frequency of each char in text    
    public static Dictionary<char, int> CountFrequency(string text)
    {
        Dictionary<char, int> dict = new Dictionary<char, int>();
        foreach (char c in text)
        {
            if (Char.IsLetter(c))
            {
                if (dict.ContainsKey(Char.ToUpper(c)))
                {
                    dict[Char.ToUpper(c)]++;
                }
                else
                {
                    dict.Add(Char.ToUpper(c), 1);
                }
            }
        }
        return dict;
    }
    
    
}