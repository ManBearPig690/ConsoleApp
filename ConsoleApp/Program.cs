using System;
using Engine;


namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Engine.Engine.Initialize(30, 40, 5);
            Engine.Engine.Run();
        }

    }
}
