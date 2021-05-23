using ByamlExt.Byaml;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMO_Randomizer.stage.cap
{
    class CapWorldTowerStage : IStage
    {
        public CapWorldTowerStage() : base() { }

        public override void DoRandom(ref List<object> objectList, int scenarioNo)
        {
            Console.WriteLine($"Processing Scenario {scenarioNo}");

            Random rng = new Random();

            int heightKeeperIdx = CaptureUtil.GetRandomIndexFromCapturables(objectList);

            for (int i = 0; i < objectList.Count; i++)
            {
                Dictionary<string, object> obj = objectList[i] as Dictionary<string, object>;

                string objName = obj["UnitConfigName"] as string;

                if (CaptureUtil.IsCapturable(objName))
                {
                    // we only need to change a single capturable actor here into a height gain actor
                    if (i == heightKeeperIdx)
                    {
                        string name = CaptureUtil.sHeightGainCaptures[rng.Next(0, CaptureUtil.sHeightGainCaptures.Count)];
                        obj["UnitConfigName"] = name;
                    }
                    else
                    {
                        string name = CaptureUtil.sCapturableActors[rng.Next(0, CaptureUtil.sCapturableActors.Count)];
                        obj["UnitConfigName"] = name;
                    }

                    objectList[i] = obj;
                }
            }
        }
    }
}
