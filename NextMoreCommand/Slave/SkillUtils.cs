// using System;
// using System.Linq;
// using System.Reflection;
// using SkySwordKill.Next.DialogEvent;
// using SkySwordKill.Next.DialogSystem;
// using SkySwordKill.NextMoreCommand.Attribute;
// using SkySwordKill.NextMoreCommand.NextSeachNpcExtension;
// using SkySwordKill.NextMoreCommand.Utils;
//
// namespace SkySwordKill.NextMoreCommand.Slave
// {
//     public class SkillDelegateInfo
//     {
//         public MethodInfo MethodInfo;
//         public Delegate Delegate;
//         public bool IsNull => Delegate is null;
//         public RegisterCommandMethodsAttribute MethodsAttribute;
//         public SkillDelegateInfo(MethodInfo info)
//         {
//             Delegate = Delegate.CreateDelegate(typeof(SkillDelagate), info, false);
//             MethodInfo = info;
//             MethodsAttribute = info.GetCustomAttribute<RegisterCommandMethodsAttribute>();
//         }
//         public bool HasCustomAttribute => MethodsAttribute is not null;
//         public void RegisterCommand()
//         {
//             var type = new CommandSkillMethod();
//             type.SkillDelagate = Delegate as SkillDelagate;
//             DialogAnalysis.RegisterCommand(MethodInfo.Name, type);
//             if (HasCustomAttribute && !string.IsNullOrWhiteSpace(MethodsAttribute.Name))
//             {
//                 DialogAnalysis.RegisterCommand(MethodsAttribute.Name, type);
//             }
//         }
//
//     }
//
//     [AttributeUsage(AttributeTargets.Class)]
//     public class RegisterCommandSkillAttribute : System.Attribute
//     {
//         private static BindingFlags defaultBindingFlags = BindingFlags.Static | BindingFlags.Public;
//         private static BindingFlags allBindingFlags = defaultBindingFlags | BindingFlags.NonPublic;
//         public static void Init()
//         {
//             foreach (var types in AppDomain.CurrentDomain.GetAssemblies()
//                          .Select(assembly => assembly.GetTypes()))
//             {
//                 foreach (var type in types)
//                 {
//                     if (type.GetCustomAttribute(typeof(RegisterCommandSkillAttribute)) is not RegisterCommandSkillAttribute attribute) continue;
//                     var all = attribute.AllMethods;
//                     var methods = type
//                         .GetMethods(all ? allBindingFlags : defaultBindingFlags)
//                         .Select(info => new SkillDelegateInfo(info))
//                         .Where(info => all ? !info.IsNull : !info.IsNull && info.HasCustomAttribute);
//                     foreach (var method in methods)
//                     {
//                         method.RegisterCommand();
//                     }
//                 }
//             }
//         }
//         public bool AllMethods { get; private set; }
//         public RegisterCommandSkillAttribute(bool allMethods = false)
//         {
//             AllMethods = allMethods;
//         }
//     }
//
//     [AttributeUsage(AttributeTargets.Method)]
//     public class RegisterCommandMethodsAttribute : System.Attribute
//     {
//         public string Name { get; private set; }
//         public RegisterCommandMethodsAttribute(string name = default)
//         {
//             Name = name;
//         }
//     }
//
//     public delegate void SkillDelagate(int npc, int skill);
//
//     public class CommandSkillMethod : IDialogEvent
//     {
//         public SkillDelagate SkillDelagate;
//
//         public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
//         {
//             try
//             {
//                 var npc = NPCEx.NPCIDToNew(command.GetInt(0));
//                 var skill = command.GetInt(1);
//                 SkillDelagate?.Invoke(npc, skill);
//             }
//             catch (Exception e)
//             {
//                 if (SkillDelagate != null)
//                 {
//                     var method = SkillDelagate.Method;
//
//                     Console.WriteLine($"{method.DeclaringType?.FullName}{method.Name}");
//                 }
//                 Console.WriteLine(e);
//                 throw;
//             }
//             finally
//             {
//                 callback?.Invoke();
//             }
//         }
//     }
//
//     [RegisterCommandSkill(true)]
//     public static class SkillUtils
//     {
//         [RegisterCommandMethods("添加NPC技能")]
//         public static void AddSkillNpc(int npc, int skill)
//         {
//             MyLog.Log($"添加技能", $"角色id:{npc} 技能id:{skill}");
//         }
//         [RegisterCommandMethods]
//         public static void RemoveSkillNpc(int npc, int skill)
//         {
//             MyLog.Log($"添加技能", $"角色id:{npc} 技能id:{skill}");
//         }
//         
//         public static void HasSkillNpc(int npc, int skill)
//         {
//             MyLog.Log($"添加技能", $"角色id:{npc} 技能id:{skill}");
//         }
//     }
// }