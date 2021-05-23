using System;
using System.Collections.Generic;
using System.Text;

namespace SMO_Randomizer
{
    public static class CaptureUtil
    {
        public static List<string> sCapturableActors = new List<string>()
        {
            "AnagramAlphabetCharacter",
            "BazookaElectric",
            "BossKnuckleHand",
            //"BreedaWanwan",
            "Bubble",
            "Bull",
            "Byugo",
            "Cactus",
            //"HackCar",
            "CarryMeat",
            //"ElectricWire",
            "Fastener",
            "FireBros",
            "Frog",
            "Fukankun",
            //"FukuwaraiFacePartsKuribo",
            //"FukuwaraiFacePartsMario",
            "Gamane",
            "GotogotonCity",
            "GotogontonLake",
            "Guidepost",
            "HackFork",
            "HammerBros",
            "Hosui",
            "Imomu",
            "JugemFishing",
            "Kakku",
            "KaronWing",
            //"Killer",
            //"KillerMagnum",
            //"Koopa",
            "Kuribo",
            "KuriboWing",
            "Manhole",
            "Megane",
            "PackunFire",
            "PackunPoison",
            "Pukupuku",
            "PukupukuSnow",
            "Radicon",
            "RockForest",
            "Senobi",
            "Statue",
            "StatueKoopa",
            "Tank",
            "Tree",
            "TRex",
            "Tsukkun",
            "Wanwan",
            "WanwanBig",
            "Yoshi",
            "Yukumaru"
        };

        public static List<string> sTrueRandomCaptures = new List<string>()
        {

        };

        public static List<string> sDestroyCaptures = new List<string>()
        {

        };

        public static List<string> sHeightGainCaptures = new List<string>()
        {
            "Frog",
            "Senobi",
            "PoleJump",
            "HackFork",
            "Hosui",
            "Tsukkun",
            "Yoshi"
        };

        public static List<string> sHorizFlightCaptures = new List<string>()
        {
            "KuriboWing", // paragoomba
            "Killer", // bullet bill
            "KillerMagnum", // banzai bill
            "Kakku", // glydon
            "JugemFishing", // fishing lakitu
            "Byugo", // Ty-Foo
            "Hosui", // gushen
            "KaronWing" // para dry bone
        };

        public static List<string> sVertFlightCaptures = new List<string>()
        {
            "KuriboWing", // paragoomba
            "KaronWing", // para dry bone
            "Hosui" // gushen
        };

        public static bool IsCapturable(string actor)
        {
            return sCapturableActors.Contains(actor);
        }

        public static int GetCapturableNum(List<object> objectList)
        {
            int count = 0;

            for (int i = 0; i < objectList.Count; i++)
            {
                var obj = objectList[i] as Dictionary<string, object>;

                if (IsCapturable(obj["UnitConfigName"].ToString()))
                    count++;
            }

            return count;
        }

        public static List<int> GetCapturableIndicies(List<object> objectList)
        {
            List<int> indicies = new List<int>();

            for (int i = 0; i < objectList.Count; i++)
            {
                var obj = objectList[i] as Dictionary<string, object>;

                if (IsCapturable(obj["UnitConfigName"].ToString()))
                    indicies.Add(i);
            }

            return indicies;
        }

        public static int GetRandomIndexFromCapturables(List<object> objectList)
        {
            Random rnd = new Random();
            List<int> indicies = GetCapturableIndicies(objectList);
            int idx = rnd.Next(0, indicies.Count);
            return indicies[idx];
        }

        public static string GetRandomAnyCapture()
        {
            Random rnd = new Random();
            int idx = rnd.Next(0, sCapturableActors.Count);
            return sCapturableActors[idx];
        }

        public static string GetRandomHorizonalFlightCapture()
        {
            Random rnd = new Random();
            int idx = rnd.Next(0, sHorizFlightCaptures.Count);
            return sHorizFlightCaptures[idx];
        }
    }
}
