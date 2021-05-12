using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ByamlExt.Byaml;
using EveryFileExplorer;
using SARCExt;
using System.Linq;

namespace SMO_Randomizer
{
    class Program
    {
        public static List<string> sCapturableActors = new List<string>()
        {
            "AnagramAlphabetCharacter",
            "BazookaElectric",
            "BossKnuckleHand",
            "BreedaWanwan",
            "Bubble",
            "Bull",
            "Byugo",
            "Cactus",
            "Car",
            "CarryMeat",
            //"ElectricWire",
            "Fastener",
            "FireBros",
            "Frog",
            "Fukankun",
            "FukuwaraiFacePartsKuribo",
            "FukuwaraiFacePartsMario",
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
            "Killer",
            "KillerMagnum",
            "Koopa",
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

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("SMO_Randomizer.exe inDir");
                Console.WriteLine("inDir is the directory of your Super Mario Odyssey filesystem dump (RomFS).");
                return;
            }

            string smo_directory = args[0];

            if (!Directory.Exists(smo_directory))
                throw new Exception("The input directory does not exist!");
            else
                Console.WriteLine("Directory exists! Continuing...");

            string[] files = Directory.GetFiles($"{smo_directory}/StageData");

            Console.WriteLine($"Found {files.Length} StageData entries");

            foreach (string f in files)
            {
                // skip design files
                if (f.EndsWith("Design.szs"))
                    continue;

                string stagename = Path.GetFileNameWithoutExtension(f);

                if (stagename.Contains("Design") || stagename.Contains("Sound"))
                    continue;

                Console.WriteLine($"Processing {stagename}...");

                DoStageChange(stagename, f);
            }
        }

        public static void DoStageChange(string stage, string path)
        {
            var data = SARC.UnpackRamN(YAZ0.Decompress(File.ReadAllBytes(path)));

            // find the map
            var mapFile = data.Files.First(a => a.Key.Contains("Map") && a.Key.Contains("byml"));

            var byml = ByamlFile.LoadN(new MemoryStream(mapFile.Value));

            List<object> nodes = byml.RootNode;

            for (int i = 0; i < nodes.Count; i++)
            {
                Dictionary<string, object> scenarioDict = (Dictionary<string, object>)nodes[i];

                if (scenarioDict.ContainsKey("ObjectList"))
                {
                    List<object> objList = (List<object>)scenarioDict["ObjectList"];

                    foreach (Dictionary<string, object> shit in objList)
                    {
                        if (Program.IsCapturable(shit["UnitConfigName"].ToString()))
                        {
                            var rand = new Random();
                            string capture = Program.sCapturableActors[rand.Next(Program.sCapturableActors.Count)];
                            shit["UnitConfigName"] = capture;

                            Dictionary<string, object> moreShit = shit["UnitConfig"] as dynamic;

                            if (moreShit.ContainsKey("ParameterConfigName"))
                            {
                                moreShit["ParameterConfigName"] = capture;
                            }
                        }
                    }

                    scenarioDict["ObjectList"] = objList;
                }

                nodes[i] = scenarioDict;
            }

            byml.RootNode = nodes;

            data.Files[mapFile.Key] = ByamlFile.SaveN(byml);

            var sarc_save = SARC.PackN(data);
            var saved_data = YAZ0.Compress(sarc_save.Item2, 3, (uint)sarc_save.Item1);

            File.WriteAllBytes($"out/{stage}.szs", saved_data);
        }

        public static bool IsCapturable(string actor)
        {
            return Program.sCapturableActors.Contains(actor);
        }

        public static List<string> FindAllCapturable(List<object> objList)
        {
            return null;
        }
    }
}
