using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using InControl;
using System.Reflection;
namespace Overcooked2DS4BT
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        internal static new ManualLogSource Logger;

        private void Awake()
        {
            Logger = base.Logger;
            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");

            Harmony harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
            harmony.PatchAll();
        }
    }

    [HarmonyPatch(typeof(PlayStation4MacBTProfile))]
    [HarmonyPatch(MethodType.Constructor)]
    public class Patch_PS4MacBTProfile
    {
        static void Postfix(PlayStation4MacBTProfile __instance)
    {
        var field = typeof(UnityInputDeviceProfile).GetField("JoystickRegex", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        if (field != null)
        {
            field.SetValue(__instance, new string[]
            {
                ".*DUALSHOCK.*"
            });
            Plugin.Logger.LogInfo("[Mod] Patched JoystickRegex on PS4 BT profile.");
        }
        else
        {
            Plugin.Logger.LogWarning("[Mod] Failed to find JoystickRegex field.");
        }
    }
    }
}
