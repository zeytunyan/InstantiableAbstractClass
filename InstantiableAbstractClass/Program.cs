using System.Runtime.CompilerServices;
using AbstractClassLibrary;


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



