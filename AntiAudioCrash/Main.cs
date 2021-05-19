using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using VRCSDK2;
using UnhollowerRuntimeLib.XrefScans;
using UnityEngine;
using MelonLoader;

namespace AntiAudioCrash
{
    public class Main : MelonMod
    {

        private static bool Enabled = true, DeleteAudioSources = false, DeletePlayerModel = true;
        private static int MaxAudioSources = 0;

        public override void OnApplicationStart()
        {
            try
            {
                MelonPreferences.CreateCategory("AntiAudioCrash", "Anti Audio Crash");
                MelonPreferences.CreateEntry<bool>("AntiAudioCrash", "AntiAudioEnabled", true, "Enabled", false);
                MelonPreferences.CreateEntry<bool>("AntiAudioCrash", "AntiAudioDeleteAudioSources", false, "Delete Audio Sources (DELETES ALL AUDIO SOURCES  - May cause reallllly bad lag)", false);
                MelonPreferences.CreateEntry<bool>("AntiAudioCrash", "AntiAudioDeletePlayerModel", false, "Delete Player Model (Makes the model a infinite loading avatar)", false);
                MelonPreferences.CreateEntry<int>("AntiAudioCrash", "AntiAudioLimit", 30, "Audio Limit (Recommended Setting)", false);


                List<_Patch> patches = new List<_Patch>();

                MethodInfo m = typeof(VRC.Core.AssetManagement).GetMethods().Where(mb => mb.Name.StartsWith("Method_Public_Static_Object_Object_Boolean_Boolean_Boolean_")).First();
                patches.Add(new _Patch("Patch_1", m, _Patch.GetLocalPatch("OnAvatarAssetBundleLoaded")));

                foreach (_Patch p in patches)
                    p.ApplyPatch();
            }
            catch
            {
                _Console.Msg(_Console.Colors.Red, "Something went wrong with patching the method. Please report this to NullifiedCode");
            }
        }

        private static bool OnAvatarAssetBundleLoaded(ref UnityEngine.Object __0)
        {
            GameObject obj = __0.TryCast<GameObject>();

            if (obj == null)
            {
                return true;
            }

            if (!obj.name.ToLower().Contains("avatar"))
            {
                return true;
            }

            if (Enabled)
            {
                AudioSource[] sources = obj.GetComponentsInChildren<AudioSource>();

                if (sources.Length > MaxAudioSources)
                {
                    if (DeleteAudioSources)
                    {
                        for (int i = 0; i < sources.Length; i++)
                        {
                            AudioSource source = sources[i];
                            GameObject.DestroyImmediate(source, true);
                        }

                        _Console.Msg(_Console.Colors.Red, "Deleted audiosources due to the audiosources count being over " + MaxAudioSources.ToString() + $" ({sources.Length})");
                    }

                    if (DeletePlayerModel)
                    {
                        _Console.Msg(_Console.Colors.Red, $"Avatar deleted due to audiosources count being over {MaxAudioSources} ({sources.Length})");
                        GameObject.DestroyImmediate(obj, true);
                    }

                }
            }
            return true;
        }
        public override void OnPreferencesSaved()
        {
            Enabled = MelonPreferences.GetEntryValue<bool>("AntiAudioCrash", "AntiAudioEnabled");
            DeletePlayerModel = MelonPreferences.GetEntryValue<bool>("AntiAudioCrash", "AntiAudioDeletePlayerModel");
            DeleteAudioSources = MelonPreferences.GetEntryValue<bool>("AntiAudioCrash", "AntiAudioDeleteAudioSources");
            MaxAudioSources = MelonPreferences.GetEntryValue<int>("AntiAudioCrash", "AntiAudioLimit");
        }
    }
}
