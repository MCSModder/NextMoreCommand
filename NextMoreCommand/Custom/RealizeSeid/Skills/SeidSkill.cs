using System;
using SkySwordKill.Next;
using SkySwordKill.Next.Lua;
using XLua;

namespace SkySwordKill.NextMoreCommand.Custom.RealizeSeid.Skills;

public class SeidSkill
{
    private Action<SeidSkillData> _prefix;
    private Action<SeidSkillData> _postfix;
    private LuaManager LuaManager => Main.Lua;
    // private SeidSkillData _seidSkillData;
    // public void Prefix()
    // {
    //     _prefix?.Invoke(_seidSkillData);
    // }
    // public void Postfix()
    // {
    //     _postfix?.Invoke(_seidSkillData);
    // }
    public void Init(string luaFilename )
    {
       var objArray = LuaManager.DoString($"return require '{luaFilename}'");
       if (objArray.Length != 0 && objArray[0] is LuaTable luaTable)
       {
           foreach (var key in luaTable.GetKeys<string>())
           {
               var str = key.ToLower();
              MyPluginMain.LogInfo(str);
               switch (str)
               {
                   case "prefix":
                       _prefix = luaTable.Get<Action<SeidSkillData>>(key);
                       break;
                   case "postfix":
                       _postfix = luaTable.Get<Action<SeidSkillData>>(key);
                       break;
               }
           }
           return;
       }
      MyPluginMain.LogError((object) ("读取Lua " + luaFilename + " 失败"));
    }
}