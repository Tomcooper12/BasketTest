using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wiggle.BasketTest.Data
{
    public interface IUserFeed
    {
        void WriteLine(string line);
        string ReadLine();
    }
}
