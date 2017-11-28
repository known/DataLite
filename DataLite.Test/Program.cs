using System;
using System.Reflection;

namespace DataLite.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            RunTest(typeof(Tests.DataTest));

            Assert.DisplaySummary();
            Console.WriteLine("按任意键结束！");
            Console.ReadKey();
        }

        static void RunTest(Type type)
        {
            var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Static);
            foreach (var item in methods)
            {
                Console.WriteLine($"开始测试 {item.Name}");
                try
                {
                    item.Invoke(null, null);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}
