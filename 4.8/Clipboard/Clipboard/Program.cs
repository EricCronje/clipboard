using System;
using cb = ClipboardUtility.ClipboardUtility;

namespace Clipboard
{
    internal class Program
    {
        [STAThreadAttribute]
        static void Main(string[] args)
        {
            using (cb clipboard = new cb())
            {
                clipboard.Use(args);
                Console.WriteLine(clipboard.Output);
            }
        }

    }
}
