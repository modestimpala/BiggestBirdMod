using HarmonyLib;
using UnityEngine;

namespace BiggestBirdMod.Patches
{
    [HarmonyPatch(typeof(RandomFlyingBird), nameof(RandomFlyingBird.Start))]
    public class BirdStartPatch
    {
        static void Prefix(RandomFlyingBird __instance)
        {
            System.Random rnd = new System.Random();
            bool variable = rnd.Next(0, 101) <= BiggestBirdPlugin.configBigChance.Value;

            if(variable)
            {
                
                var audio = __instance.gameObject.AddComponent<AudioSource>();
                audio.clip = BiggestBirdPlugin.birdClip;
                audio.loop = true;
                audio.maxDistance = BiggestBirdPlugin.configBigDistance.Value;
                audio.volume = 1f;
                audio.spatialBlend = 1;
                audio.spatialize = true;
                audio.spatializePostEffects = true;
                audio.Play();
                Vector3 scale = Vector3.one * 10;
                __instance.transform.localScale = scale;
            }
        }
    }
}
