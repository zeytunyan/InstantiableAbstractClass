using System.Reflection;
using System.Runtime.CompilerServices;

AbstractClass instance;
var typeHandleValue = AbstractClass.Type.TypeHandle.Value;

unsafe
{
    var size = ((int*)typeHandleValue)[1];
    byte* memory = stackalloc byte[size];
    IntPtr* ptr = (IntPtr*)memory;
    ptr++;
    *ptr = typeHandleValue;
    instance = Unsafe.AsRef<AbstractClass>(&ptr).Initialize();
}

Console.WriteLine(instance);
Console.WriteLine();

// P.S. При отсутствии абстрактных членов прокатит и так:
var noReflectionInstance = AbstractClass.GetInstance();
Console.WriteLine(noReflectionInstance);


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

        foreach (var field in Type.GetFields())
        {
            field.SetValue(this, field.GetValue(innerClassInstance) ?? default);
        }

        foreach (var property in Type.GetProperties())
        {
            var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            var backingField = Type.GetField($"<{property.Name}>k__BackingField", bindingFlags);
            backingField?.SetValue(this, property.GetValue(innerClassInstance) ?? default);
        }

        return this;
    }
}
