// Hello World! program
using System;

namespace HelloWorld
{
    class Hello
    {
        static void Main(string[] args)
        {
            var counter = 0;
            while (true)
            {
                Console.WriteLine($"Counter: {++counter}");
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}