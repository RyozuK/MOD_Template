using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using BepInEx;
using HarmonyLib;
using ReflexCLI;

//Change this to mach your project
namespace MOD_Template;

internal static class ModInfo
{
    /* First, let's edit these details here.  The Guid is arbitrary, but should be unique.
     * One way to achieve that is to use what would be the path to your GitHub project.
     * In this case, change the 3rd part to your GitHub username, and the last part to your project's name
     * Let's also change the Name itself.  Version can be changed as you see fit
     */
    internal const string Guid = "com.github.ryozuk.template";
    internal const string Name = "Template";
    internal const string Version = "1.0.0.0";
    //This is used for development, allowing you to include more informative debug messages
    //That are easily turned off by switching this variable to false for release.
    internal const bool DevMode = true;
}

[BepInPlugin(ModInfo.Guid, ModInfo.Name, ModInfo.Version)]
public class MOD_TemplatePlugin : BaseUnityPlugin
{
    
    internal static MOD_TemplatePlugin? Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
        Assembly executingAssembly = Assembly.GetExecutingAssembly();
        CommandRegistry.assemblies.Add(executingAssembly);
    }
    
    public static void LogDev(object payload, [CallerLineNumber] int line = 0)
    {
        //Useful for temporary debugging messages during development
        if (ModInfo.DevMode)
        {
            Instance!.Logger.LogWarning(ModInfo.Name + "::" + GetCallerInfo() + $"({line})" + "::" + payload);
        }
    }

    public static void LogInfo(object payload, [CallerMemberName] string caller = "", [CallerLineNumber] int line = 0)
    {
        //Useful for information that will be helpful to debug when a *user* encounters problems.
        Instance!.Logger.LogInfo(ModInfo.Name +"::" + caller + $"({line})" + "::" + payload);
    }
    
    private static string GetCallerInfo()
    {
        StackTrace stackTrace = new StackTrace(true);
        StackFrame stackFrame = stackTrace.GetFrame(2); // Skip the current and calling method

        Type reflectedType = stackFrame.GetMethod().ReflectedType;
        if (reflectedType != null)
            return $"{reflectedType.Name}.{stackFrame.GetMethod().Name}";
        return stackFrame.GetMethod().Name;
    }
    
    //Sometimes, you just want to see what information an object contains.  This can be a useful way to see information
    public static string GetFields<T>(T baseItem) where T : new() {
        System.Reflection.FieldInfo[] fields = baseItem.GetType().GetFields();
        string baseText = baseItem.ToString();
        foreach (System.Reflection.FieldInfo fieldInfo in fields) {
            baseText += "\n" + fieldInfo.Name + " : " + baseItem.GetField<object>(fieldInfo.Name);
        }
        return baseText;
    }
    
    private void Start()
    {
        MOD_TemplatePlugin.LogInfo("Mod Start()");
        Harmony harmony = new Harmony(ModInfo.Guid);
        harmony.PatchAll();
    }
}

//This serves as an example of a postfix patch.
[HarmonyPatch(typeof(Zone))]
[HarmonyPatch(nameof(Zone.Activate))]
class ZonePatch : EClass {
    static void Postfix(Zone __instance) {
        MOD_TemplatePlugin.LogDev("Now entering " + __instance.source.id);
    }
}

