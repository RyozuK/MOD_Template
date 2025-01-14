using System;
using ReflexCLI.Attributes;



[ConsoleCommandClassCustomizer("")]
public class SpawnObjsConsole
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
}