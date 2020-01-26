using System;

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
    }

    public class DiffResult
    {
        public LineItem[] Changes { get; set; }

        public void Display()
        {
            if (Changes?.Length == 0)
            {
                Console.WriteLine("No differences found");
                return;
            }

            foreach (var change in Changes)
            {
                if (change.Change == Edit.Add)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($">  {change.Text}{Environment.NewLine}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"<  {change.Text}{Environment.NewLine}");
                }
            }
        }
    }
}