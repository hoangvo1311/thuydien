using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowMac
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var address = LicenseHelper.License.GetHardwareId();
            Console.WriteLine(address);
            Console.ReadLine();
        }
    }
}
