using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WithLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly  asm = Assembly.Load(AssemblyName.GetAssemblyName("ClassLibrary.dll"));
            Module mod = asm.GetModule("ClassLibrary.dll");
            Console.WriteLine("Оголошення типу даних");
            foreach(Type t in mod.GetTypes())
            {
                Console.WriteLine(t.FullName);
            }
            Type Person = mod.GetType("SampleLibrary.Person") as Type; // namespace from dll
            object person = Activator.CreateInstance(Person,new object[]{"Іван","Іванов",30});
            Person.GetMethod("Print").Invoke(person,null); // Invoke run method
                

        }
    }
}
