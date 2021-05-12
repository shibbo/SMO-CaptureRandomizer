using System;
using System.Collections.Generic;
using System.Text;

namespace SMO_Randomizer.captures
{
    public class Frog : ICapturable
    {
        public Frog(ref Dictionary<string, object> o) : base(ref o)
        {
            mCompat = new List<string>()
            {
                "Any"
            };

            mStructure = new Dictionary<string, object>()
            {

            };
        }
    }
}
