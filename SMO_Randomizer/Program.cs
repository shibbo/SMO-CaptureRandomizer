using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ByamlExt.Byaml;
using EveryFileExplorer;
using SARCExt;
using System.Linq;
using SMO_Randomizer.stage.cap;
using SMO_Randomizer.stage;

namespace SMO_Randomizer
{
    class Program
    {
        public static Dictionary<string, Func<IStage>> sStages = new Dictionary<string, Func<IStage>>()
        {
            { "CapWorldHomeStageMap", () => new CapWorldHomeStage() },
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
                // skip design and sound files
                if (f.EndsWith("Design.szs") || f.EndsWith("Sound.szs"))
                    continue;

                string stagename = Path.GetFileNameWithoutExtension(f);

                DoStageChange(stagename, f);
            }
        }

        public static void DoStageChange(string stage, string path)
        {
            if (!sStages.ContainsKey(stage))
            {
                Console.WriteLine($"{stage} is not supported for randomization yet! Skipping...");
                return;
            }

            Console.WriteLine($"Processing {stage}...");

            var data = SARC.UnpackRamN(YAZ0.Decompress(File.ReadAllBytes(path)));

            // find the map
            var mapFile = data.Files.First(a => a.Key.Contains("Map") && a.Key.Contains("byml"));

            var byml = ByamlFile.LoadN(new MemoryStream(mapFile.Value));

            List<object> nodes = byml.RootNode;

            for (int i = 0; i < nodes.Count; i++)
            {
                List<string> capturesPrinted = new List<string>();

                Dictionary<string, object> scenarioDict = nodes[i] as Dictionary<string, object>;

                if (scenarioDict.ContainsKey("ObjectList"))
                {
                    List<object> objList = scenarioDict["ObjectList"] as List<object>;

                    foreach (Dictionary<string, object> shit in objList)
                    {
                        if (sStages.ContainsKey(stage))
                        {
                            IStage stg = sStages[stage].Invoke();
                            stg.DoRandom(ref objList, i);
                        }

                        scenarioDict["ObjectList"] = objList;
                    }
                }

                nodes[i] = scenarioDict;
            }

            byml.RootNode = nodes;

            data.Files[mapFile.Key] = ByamlFile.SaveN(byml);

            var sarc_save = SARC.PackN(data);
            var saved_data = YAZ0.Compress(sarc_save.Item2, 3, (uint)sarc_save.Item1);

            File.WriteAllBytes($"out/{stage}.szs", saved_data);
        }
    }
}
