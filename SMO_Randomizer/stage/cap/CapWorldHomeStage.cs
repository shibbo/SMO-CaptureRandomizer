using System;
using System.Collections.Generic;
using System.Text;

namespace SMO_Randomizer.stage.cap
{
    class CapWorldHomeStage : IStage
    {
        public CapWorldHomeStage() : base() { }

        public override void DoRandom(ref List<object> objectList, int scenarioNo)
        {
            Console.WriteLine($"Processing Scenario {scenarioNo}");

            if (scenarioNo == 0)
            {
                // Scenario 1 -- getting cappy
                // we can randomize pretty much everything here
                for (int i = 0; i < objectList.Count; i++)
                {
                    Dictionary<string, object> obj = objectList[i] as Dictionary<string, object>;

                    string objName = obj["UnitConfigName"] as string;

                    if (CaptureUtil.IsCapturable(objName))
                    {
                        string name = CaptureUtil.GetRandomAnyCapture();
                        obj["UnitConfigName"] = name;

                        Dictionary<string, object> unitConfig = obj["UnitConfig"] as Dictionary<string, object>;
                        unitConfig["ParameterConfigName"] = name;

                        objectList[i] = obj;
                    }
                }
            }
            else
            {
                int frogIdx = CaptureUtil.GetRandomIndexFromCapturesOfType(objectList, "Frog");

                if (CaptureUtil.GetCapturableNum(objectList) == 0)
                    return;

                // all KuriboWing captures must be horizontal flight captures
                for (int i = 0; i < objectList.Count; i++)
                {
                    // SPARE US OUR FROG
                    if (frogIdx == i)
                        continue;

                    Dictionary<string, object> obj = objectList[i] as Dictionary<string, object>;

                    string objName = obj["UnitConfigName"] as string;

                    if (CaptureUtil.IsCapturable(objName))
                    {
                        // if the object is KuriboWing, we have to randomize to a horizonal flight capture
                        if (objName == "KuriboWing")
                        {
                            string name = CaptureUtil.GetRandomHorizonalFlightCapture();
                            obj["UnitConfigName"] = name;

                            Dictionary<string, object> unitConfig = obj["UnitConfig"] as Dictionary<string, object>;
                            unitConfig["ParameterConfigName"] = name;
                        }
                        else
                        {
                            string name = CaptureUtil.GetRandomAnyCapture();
                            obj["UnitConfigName"] = name;
                        }

                        objectList[i] = obj;
                    }
                }
            }
        }
    }
}
