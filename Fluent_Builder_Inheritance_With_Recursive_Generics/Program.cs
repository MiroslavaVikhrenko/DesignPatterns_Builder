namespace Fluent_Builder_Inheritance_With_Recursive_Generics
{
    //when builders inherit from other builders

    public class Person
    {
        public string Name;
        public string Position;
        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
        }
    }

    public class PersonInfoBuilder
    {
        protected Person person = new Person(); //protected instead of private because we are using inheritance

        //fluent method
        public PersonInfoBuilder Called(string name)
        {
            person.Name = name;
            return this;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            
        }
    }
}
