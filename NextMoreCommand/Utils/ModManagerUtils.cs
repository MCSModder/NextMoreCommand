using System;
using JetBrains.Annotations;
using SkySwordKill.Next.Mod;
using UnityEngine;

namespace SkySwordKill.NextMoreCommand.Utils
{
    public static class ModManagerUtils
    {
        public static bool TryGetModSetting( string key,out KeyCode keyCode)
        {
          
            if ( ModManager.TryGetModSetting(key,out string value))
            {
                if (Enum.TryParse(value, true, out keyCode))
                {
                    return true;
                }
            }
            keyCode = KeyCode.None;
            return false;
        }
    }
}