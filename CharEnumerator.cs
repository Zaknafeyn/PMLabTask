using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PMLabsTask
{
    public class CharEnumerator : IAsyncEnumerator<char>
    {
        //private const int BufferSize = 1024 + 347;
        private const int BufferSize = 2 * 1024;
        private readonly StreamReader _reader;
        private char[] _arr = new char [BufferSize];
        private int _bufferCounter = BufferSize-1;
        private int _pageItems = 0;
        
        public CharEnumerator(Stream stream)
        {
            _reader = new StreamReader(stream);
        }

        public ValueTask DisposeAsync()
        {
            return new ValueTask(Task.Run(() => _reader.Dispose()));
        }

        public async ValueTask<bool> MoveNextAsync()
        {
            _bufferCounter++;

            if (_reader.EndOfStream && _bufferCounter >= _pageItems)
                return false;

            if (_bufferCounter == BufferSize || _bufferCounter == _pageItems)
            {
                // read next portion
                _pageItems = await _reader.ReadAsync(_arr, 0, BufferSize);
                _bufferCounter = 0;
            }

            return true;
        }

        public char Current => _arr[_bufferCounter];
    }
}