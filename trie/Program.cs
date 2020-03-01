using System;

namespace trie
{
    class Program
    {
        static void Main(string[] args)
        {
            var trie = new Trie();
            trie.Insert("apple");
            trie.Search("apple");   // returns true
            trie.Search("app");     // returns false
            trie.StartsWith("app"); // returns true
            trie.Insert("app");   
            trie.Search("app");     // returns true
        }
    }
}
