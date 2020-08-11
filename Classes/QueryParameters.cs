using System;

namespace Api.Classes
{
    public class QueryParameters
    {
        const int maxSize = 100;
        private int _size = 100;

        public int Page { get; set; }
        public int Size
        {
            get { return _size; }
            set
            {
                _size = Math.Min(maxSize, value);
            }
        }
        public int RegisterSkip
        {
            get { return Size * (Page - 1); }
        }


    }
}
