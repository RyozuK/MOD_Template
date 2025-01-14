using System;
using MOD_MoreObjs;
using ReflexCLI.Attributes;

[ConsoleCommandClassCustomizer("")]
public class MoreObjsConsole
{
    
    [ConsoleCommand("SpawnObj")]
    public static string SpawnObj(int obj_id)
    {
        try
        {
            int x = EClass.pc.pos.x;
            int z = EClass.pc.pos.z;
            EClass._map.SetObj(x, z, obj_id);
            return $"Setting {x}, {z} position to obj with id {obj_id}";
        }
        catch (Exception e)
        {
            return "Failed to spawn Obj: " + e.ToString();
        }
    }

    [ConsoleCommand("FindObj")]
    public static string FindObj(String obj_alias)
    {
        try
        {
            SourceObj.Row row = EClass.sources.objs.alias[obj_alias];
            MOD_TemplatePlugin.printFields(row);
            return $"Found Obj of alias [{obj_alias}] with id [{row.id}]";
        }
        catch (Exception e)
        {
            return $"Failed to find [{obj_alias}]: " + e.ToString();
        }
    }
}