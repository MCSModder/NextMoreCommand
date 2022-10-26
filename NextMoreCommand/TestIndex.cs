using JetBrains.Annotations;
using SkySwordKill.Next;

namespace SkySwordKill.NextMoreCommand
{
    public struct TestStruct
    {
        public  static TestStruct Instance { get; } = new TestStruct();
        public void Test2()
        {
            Main.LogInfo("测试2");
        }
        public object this[string index]
        {
            get
            {
                Main.LogInfo(this);
                Main.LogInfo($"TestStruct index:{index}");
                return this[index];
            }
        }
    }
    public class TestIndex
    {
        public  static TestIndex Instance { get; } = new TestIndex();
         [CanBeNull] public object this[string index]
        {
            get
            {
                Main.LogInfo(this);
                Main.LogInfo($"TestIndex index:{index}");
                return this[index];
            }
        }

         public void Test()
         {
             Main.LogInfo("测试");
         }
    }
}