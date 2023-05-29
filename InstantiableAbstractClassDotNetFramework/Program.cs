using System;
using System.Reflection;
using AbstractClassLibrary;


namespace InstantiableAbstractClassDotNetFramework
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var allocateMethod = typeof(RuntimeTypeHandle).GetMethod("Allocate", BindingFlags.Static | BindingFlags.NonPublic);
            var abstractClassUninitializedInstance = allocateMethod.Invoke(null, new object[] { AbstractClass.Type });
            var abstractClassInstance = (abstractClassUninitializedInstance as AbstractClass)?.Initialize();

            Console.WriteLine(abstractClassInstance);
            Console.Read();
        }
    }
}
