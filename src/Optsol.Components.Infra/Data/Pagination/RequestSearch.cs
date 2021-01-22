using System;

namespace Optsol.Components.Infra.Data
{
    public class RequestSearch
    {
        private uint page;

        public uint Page
        {
            get
            {
                return page;
            }

            set
            {
                page = SetPageValue(value);
            }
        }

        public uint? PageSize { get; set; }

        private static uint SetPageValue(uint value)
        {
            var menorIgualZero = value <= 0;
            if (menorIgualZero)
                return 1;

            return value;
        }

    }

    public class RequestSearch<TSearch> : RequestSearch
    {
        public TSearch Search { get; set; }
    }
}
