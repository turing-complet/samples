using System.Collections.Generic;

public class Node
{
    public Dictionary<char, Node> Children { get; } = new Dictionary<char, Node>();
    public bool IsWord { get; set;}
}

public class Trie {

    private Node root;
    /** Initialize your data structure here. */
    public Trie() {
        root = new Node();
    }
    
    /** Inserts a word into the trie. */
    public void Insert(string word) {
        Node curr = root;
        foreach (var ch in word)
        {
            if (!curr.Children.ContainsKey(ch))
            {
                curr.Children[ch] = new Node();
            }
            curr = curr.Children[ch];
        }
        curr.IsWord = true;
    }
    
    /** Returns if the word is in the trie. */
    public bool Search(string word) {
        Node curr = root;
        foreach (var ch in word)
        {
            if (!curr.Children.ContainsKey(ch))
            {
                return false;
            }
            curr = curr.Children[ch];
        }
        return curr.IsWord;
    }
    
    /** Returns if there is any word in the trie that starts with the given prefix. */
    public bool StartsWith(string prefix) {
        Node curr = root;
        foreach (var ch in prefix)
        {
            if (!curr.Children.ContainsKey(ch))
            {
                return false;
            }
            curr = curr.Children[ch];
        }
        return true;
    }
}

/**
 * Your Trie object will be instantiated and called as such:
 * Trie obj = new Trie();
 * obj.Insert(word);
 * bool param_2 = obj.Search(word);
 * bool param_3 = obj.StartsWith(prefix);
 */