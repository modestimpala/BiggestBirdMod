using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;

namespace BiggestBirdMod
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    //[NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
    internal class BiggestBirdPlugin : BaseUnityPlugin
    {
        public const string PluginGUID = "com.moddy.biggestbirdmod";
        public const string PluginName = "BiggestBirdMod";
        public const string PluginVersion = "1.0.0";

        private readonly Harmony harmony = new Harmony(PluginGUID);
        public static AudioClip birdClip;
        public static ConfigEntry<int> configBigChance { get; private set; }
        public static ConfigEntry<float> configBigDistance { get; private set; }

        private void Awake()
        {
            configBigChance = Config.Bind("Biggest Bird",      // The section under which the option is shown
                                        "Chance",  // The key of the configuration option in the configuration file
                                        5, // The default value
                                        "1-100% chance of a bird becoming The Biggest Bird."); // Description 
            configBigDistance = Config.Bind("Biggest Bird",      // The section under which the option is shown
                                        "Audio Distance",  // The key of the configuration option in the configuration file
                                        850f, // The default value
                                        "The audio distance of The Biggest Bird music."); // Description 

            var bundle = Utils.LoadAssetBundle("biggestbirdassets", Assembly.GetExecutingAssembly());
            if(bundle != null )
            {
                birdClip = bundle.LoadAsset<AudioClip>("bird");
                harmony.PatchAll();
            } else
            {
                Logger.LogFatal("Error loading assetbundle");
            }

            //StartCoroutine(ClipCreator(path));
            bundle.Unload(false);
        }

        void OnDestroy()
        {
            harmony.UnpatchSelf();
            birdClip.UnloadAudioData();
        }

    }
}