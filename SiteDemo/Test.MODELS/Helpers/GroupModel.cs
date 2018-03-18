using System;
using System.Collections.Generic;
using System.Text;

namespace Test.MODELS.Helpers
{
    public class GroupModel<T>
    {
        public T Key { get; set; }
        public int Count { get; set; }
    }
}
