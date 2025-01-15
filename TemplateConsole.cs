using System;
using MOD_Template;
using ReflexCLI.Attributes;



[ConsoleCommandClassCustomizer("")]
public class TemplateConsole
{
    [ConsoleCommand("SpawnObj")]
    public static string SpawnObj(int objID)
    {
        try
        {
            int x = EClass.pc.pos.x;
            int z = EClass.pc.pos.z;
            EClass._map.SetObj(x, z, objID);
            return $"Setting {x}, {z} position to obj with id {objID}";
        }
        catch (Exception e)
        {
            return "Failed to spawn Obj: " + e;
        }
    }

    [ConsoleCommand("ShowPC")]
    public static string ShowPC()
    {
        String PcInfo = MOD_TemplatePlugin.GetFields(EClass.pc);
        MOD_TemplatePlugin.LogDev(PcInfo);
        MOD_TemplatePlugin.LogInfo(PcInfo);
        return PcInfo;
    }
}

[ConsoleCommandClassCustomizer("Zone")]
public class ZoneConsole
{
    [ConsoleCommand("SaveAs")]
    public static string SaveAs()
    {
        EClass._zone.ExportDialog();
        return $"Saving {EClass._zone}:{EClass._zone.pathSave}";
    }
} 