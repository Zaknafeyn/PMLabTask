using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace PMLabsTask
{
    public class CharEnumerable : IAsyncEnumerable<char>
    {
        private readonly Stream _fileStream;

        public CharEnumerable(Stream fileStream)
        {
            _fileStream = fileStream;
        }

        public IAsyncEnumerator<char> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken())
        {
            return new CharEnumerator(_fileStream);
        }
    }
}