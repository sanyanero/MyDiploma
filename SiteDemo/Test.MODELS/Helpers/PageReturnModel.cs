using System.Collections.Generic;

namespace Test.MODELS.Helpers
{
    public class PageReturnModel<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
    }
}