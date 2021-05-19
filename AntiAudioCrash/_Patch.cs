using Harmony;
using MethodBase = System.Reflection.MethodBase;
using System.Reflection;
namespace AntiAudioCrash
{
    class _Patch
    {
        public string ID { get; set; }

        public MethodBase TargetMethod { get; set; }

        public HarmonyMethod AfterPatch { get; set; }

        public _Patch(string Identifier, MethodBase Target, HarmonyMethod After)
        {
            ID = Identifier;
            TargetMethod = Target;
            AfterPatch = After;
        }

        public void ApplyPatch()
        {
            try
            {
                HarmonyInstance instance = HarmonyInstance.Create(ID);
                instance.Patch(TargetMethod, AfterPatch, null);
            }
            catch
            {

            }
        }

        public static HarmonyMethod GetLocalPatch(string name) { return new HarmonyMethod(typeof(Main).GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic)); }
    }
}
