using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PMLabsTask
{
    public class Solution
    {
        private readonly string _fileUrl;

        public Solution(string fileUrl)
        {
            _fileUrl = fileUrl;
        }

        public async Task<string> GetMagicPhrase()
        {
            var sequence = await GetCharSequence();

            var dictCharCount = new Dictionary<char, int>();
            var tempSb = new StringBuilder();
            const int allowedFreq = 10;

            await foreach (var ch in sequence)
            {
                if (dictCharCount.ContainsKey(ch))
                {
                    dictCharCount[ch]++;
                    if (dictCharCount[ch] <= allowedFreq) tempSb.Append(ch);
                }
                else
                {
                    dictCharCount.Add(ch, 1);
                    tempSb.Append(ch);
                }
            }

            var rareChars = dictCharCount.Where(x => x.Value <= allowedFreq).Select(x => x.Key).ToList();
            var resultSb = new StringBuilder();

            foreach (var ch in tempSb.ToString())
            {
                if (rareChars.Contains(ch))
                    resultSb.Append(ch);
            }

            var result = resultSb.ToString();

            return result;
        }

        private async Task<CharEnumerable> GetCharSequence()
        {
            using var httpClient = new HttpClient();
            var stream = await httpClient.GetStreamAsync(_fileUrl);
            return new CharEnumerable(stream);
        }
    }
}