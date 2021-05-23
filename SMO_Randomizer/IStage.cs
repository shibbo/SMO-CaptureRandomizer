using ByamlExt.Byaml;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMO_Randomizer
{
    public abstract class IStage
    {
        public IStage() { }
        /// <summary>
        /// Does the actual randomization based on the arguments given.
        /// </summary>
        /// <param name="objectList">The list of objects to modify.</param>
        /// <param name="scenarioNo">The scenario number. -1 if the scenario doesn't matter.</param>
        public abstract void DoRandom(ref List<object> objectList, int scenarioNo);
    }
}
