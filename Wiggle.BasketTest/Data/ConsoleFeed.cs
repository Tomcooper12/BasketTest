using System;

namespace Wiggle.BasketTest.Data
{
    public class ConsoleFeed : IUserFeed
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void WriteLine(string line)
        {
            Console.WriteLine(line);
        }
    }
}
