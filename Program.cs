using System;
using System.Threading.Tasks;

namespace PMLabsTask
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).Wait();
        }

        static async Task MainAsync(string[] args)
        {
            var fileUrl =
                "https://gist.githubusercontent.com/skalinets/23691610f9bbf590b6fba51e373375b4/raw/0b9cc1e97650f5204edbd7b1906e03435506eaf7/mess.txt";
            var sln = new Solution(fileUrl);
            var magicPhrase = await sln.GetMagicPhrase();

            Console.WriteLine($"Resulting string: '{magicPhrase}'");
        }
    }
}
