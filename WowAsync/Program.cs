using System;
using System.Threading.Tasks;

namespace WowAsync
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var loop = BackgroundLoop();
            while (true)
            {
                Console.WriteLine("Wow.");
                await Task.Delay(1000);
            }
        }

        static async Task BackgroundLoop()
        {
            while (true)
            {
                Console.WriteLine("Sneaky.");
                await Task.Delay(1000);
            }
        }
    }
}
