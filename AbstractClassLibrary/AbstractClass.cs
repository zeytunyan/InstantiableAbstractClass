using System;
using System.Reflection;


namespace AbstractClassLibrary
{
    public abstract class AbstractClass
    {
        public int intField = 1;
        public string stringField = "string";

        public int intProperty { get; } = 100;
        public string stringProperty { get; } = string.Empty;

        public static Type Type => typeof(AbstractClass);

        public AbstractClass Instance => this;

        protected AbstractClass() => Console.WriteLine("The abstract class constructor has worked out!");

        private sealed class InnerClass : AbstractClass { }

        public static AbstractClass GetInstance() => new InnerClass();

        public AbstractClass Initialize()
        {
            var innerClassInstance = new InnerClass();
            var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;

            var fields = Type.GetFields(bindingFlags);

            foreach (var field in fields)
            {
                field.SetValue(this, field.GetValue(innerClassInstance) ?? default);
            }

            return this;
        }
    }

}
