using System;
using System.Collections.Generic;
using System.Text;
using ByamlExt.Byaml;
using EveryFileExplorer;
using SARCExt;

namespace SMO_Randomizer
{
    /// <summary>
    /// implements an actor that is capturable
    /// </summary>
    public abstract class ICapturable
    {
        public ICapturable(ref Dictionary<string, object> e)
        {
            mEntry = e;
        }

        public T Get<T>(string key)
        {
            return (T)mEntry[key];
        }

        public void Set<T>(T val, string key)
        {
            mEntry[key] = val;
        }

        public bool IsReplacementAcceptable(string replacement)
        {
            return mCompat.Contains(replacement);
        }

        private Dictionary<string, object> mEntry;
        public List<string> mCompat;
        public Dictionary<string, object> mStructure;
    }
}
