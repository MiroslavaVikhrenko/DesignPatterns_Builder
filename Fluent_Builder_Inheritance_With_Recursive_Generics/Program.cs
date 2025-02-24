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
    internal class Program
    {
        static void Main(string[] args)
        {
            
        }
    }
}
