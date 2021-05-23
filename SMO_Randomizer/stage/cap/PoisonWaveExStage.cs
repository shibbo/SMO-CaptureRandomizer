using System;
using System.Collections.Generic;
using System.Text;

namespace SMO_Randomizer.stage.cap
{
    class PoisonWaveExStage : IStage
    {
        public PoisonWaveExStage() : base() { }

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
                    string name = CaptureUtil.sVertFlightCaptures[rng.Next(0, CaptureUtil.sVertFlightCaptures.Count)];
                    obj["UnitConfigName"] = name;

                    objectList[i] = obj;
                }
            }
        }
    }
}
