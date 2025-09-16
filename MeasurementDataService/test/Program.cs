using RainService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var a = RainFallHelper.GetRainFall(Plant.DakSrong3A);
        }
    }
}
