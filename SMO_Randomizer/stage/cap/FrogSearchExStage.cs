using System;
using System.Collections.Generic;
using System.Text;

namespace SMO_Randomizer.stage.cap
{
    class FrogSearchExStage : IStage
    {
        public FrogSearchExStage() : base() { }

        public override void DoRandom(ref List<object> objectList, int scenarioNo)
        {
            Console.WriteLine($"Processing Scenario {scenarioNo}");

            Random rng = new Random();

            for (int i = 0; i < objectList.Count; i++)
            {
                Dictionary<string, object> obj = objectList[i] as Dictionary<string, object>;

                string objName = obj["UnitConfigName"] as string;

                if (CaptureUtil.IsCapturable(objName))
                {
                    string name = CaptureUtil.sHeightGainCaptures[rng.Next(0, CaptureUtil.sHeightGainCaptures.Count)];
                    obj["UnitConfigName"] = name;

                    objectList[i] = obj;
                }
            }
        }
    }
}
