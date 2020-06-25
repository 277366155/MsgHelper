using System;
using System.Collections.Generic;
using System.Text;
using MH.Common;
using Microsoft.Extensions.DependencyInjection;

namespace MH.ConsoleTest
{
    public class ServiceCollectioinTest
    {

        public static void TestRun()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IAnimal, Animal>();
            serviceCollection.AddSingleton(s => new Person("sjbo"));
            var p1 = serviceCollection.BuildServiceProvider();
             
            serviceCollection.AddSingleton<ICat, Cat>();
            serviceCollection.AddSingleton<IOpertion, Opertion>();

            var pro = serviceCollection.BuildServiceProvider();
            var opertion = pro.GetService<IOpertion>();
            opertion.Printf();
        }
    }
    public interface IOpertion
    {
        void Printf();
    }

    public class Opertion : IOpertion
    {
        ICat _cat;
        public Opertion(ICat cat)
        {
            _cat = cat;
        }
        public Opertion()
        {
            _cat = null;
        }

        public void Printf()
        {
            if (_cat != null)
                _cat.CatSay();
            else
                Console.WriteLine("cat is null");
        }
    }

    public interface IAnimal
    {
        void Print();
    }

    public class Animal : IAnimal
    {
        public void Print()
        {
            Console.WriteLine("Animal...");
        }
    }
    public interface IPerson
    {
        void PersonPrint();
    }
    public class Person : Animal, IPerson
    {
        public string PersonName { get; set; }

        public int Age { get; set; }

        public void PersonPrint()
        {
            Console.WriteLine($"I am a person... my name is {PersonName}");
        }

        public  Person(string name)
        {
            this.PersonName = name;
        }
    }

    public interface ICat
    {
        void CatSay();
    }
    public class Cat : Animal, ICat
    {
        Person _master;
        IAnimal _animal;
        public Cat(Person master, IAnimal animal)
        {
            _master = master;
            _animal = animal;
        }
        public string Name { get; set; }


        public void CatSay()
        {
            Console.WriteLine("in cat say methed");
            _animal.Print();
            _master.PersonPrint();
        }
    }


}
