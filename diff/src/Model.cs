using System;
using System.Linq;
using System.Collections.Generic;

namespace diff
{
    public enum Edit
    {
        Add, Remove
    }

    public class LineItem
    {
        public Edit Change { get; set; }
        public string Text { get; set; }
        public int Position { get; set; }
    }

    public class DiffResult
    {
        public List<LineItem> Changes { get; set; } = new List<LineItem>();
        // could be a dictionary with position -> change

        public void Addition(string text, int position)
        {
            Changes.Add(new LineItem { Change = Edit.Add, Text = text, Position = position });
        }

        public void Deletion(string text, int position)
        {
            Changes.Add(new LineItem { Change = Edit.Remove, Text = text, Position = position });
        }

        public void Apply(string initialValue)
        {
            if (Changes?.Count == 0)
            {
                Console.WriteLine("No differences found");
                return;
            }

            var sorted = Changes.OrderByDescending(c => c.Position) as IEnumerable<LineItem>;

            for (int i = 0; i < initialValue.Length; i++)
            {
                if (!sorted.Any())
                {
                    Console.WriteLine(initialValue.Substring(i));
                }

                var current = sorted.First();
                if (current.Position != i)
                {
                    Console.WriteLine(initialValue[i]);
                }
                else
                {
                    while (current != null && current.Position == i)
                    {
                        if (current.Change == Edit.Add)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"+  {current.Text}");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"-  {current.Text}");
                        }
                        Console.ResetColor();
                        sorted = sorted.Skip(1);
                        current = sorted.FirstOrDefault();
                    }
                }
            }
        }
    }
}