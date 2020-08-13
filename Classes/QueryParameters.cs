using System;

namespace Api.Classes
{
    public class QueryParameters<TEntity>
    {
        const int maxSize = 100;
        private int _size = 100;
        private string _sortOrder = "asc";

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
        public string SortBy { get; set; } = "Id";
        public string SortOrder
        {
            get { return _sortOrder; }
            set
            {
                if (value == "asc" || value == "desc")
                {
                    _sortOrder = value;
                }
            }
        }
        internal bool HasSortBy
        {
            get
            {
                if (string.IsNullOrEmpty(SortBy)) return false;
                if (typeof(TEntity).GetProperty(SortBy) == null) return false;
                return true;
            }
        }

    }
}