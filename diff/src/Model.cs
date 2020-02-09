using System;
using System.Linq;
using System.Collections.Generic;

namespace diff
{
    public enum Edit
    {
        None, Add, Remove
    }

    public class LineItem
    {
        public Edit Change { get; set; }
        public string Text { get; set; }

        public void Print()
        {
            if (Change.Equals(Edit.None))
            {
                Console.WriteLine(Text);
            }
            else if (Change.Equals(Edit.Add))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"+ {Text}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"- {Text}");
                Console.ResetColor();
            }
        }
    }

    public class DiffResult
    {
        public Queue<LineItem> Changes { get; set; } = new Queue<LineItem>();

        public void Addition(string text) => Changes.Enqueue(new LineItem { Change = Edit.Add, Text = text });

        public void Deletion(string text) => Changes.Enqueue(new LineItem { Change = Edit.Remove, Text = text });

        public void Unchanged(string text) => Changes.Enqueue(new LineItem { Change = Edit.None, Text = text });

        public void Print()
        {
            if (!Changes.Any())
            {
                Console.WriteLine("No differences found");
                return;
            }

            while (Changes.Any())
            {
                var item = Changes.Dequeue();
                item.Print();
            }
        }
    }
}