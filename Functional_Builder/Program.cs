namespace Functional_Builder
{
    // Functional builder 
    internal class Program
    {
        public class Person
        {
            public string Name, Position;
        }
        // sealed class => you cannot inherit from it
        // => if you want to extend it somehow - you cannot use inheritance 
        // but you can use extension methods instead
        public sealed class PersonBuilder
        {
            //<Person, Person> => fluent 
            // we are going to affect the person somehow and we're going to return a reference to that person
            private readonly List<Func<Person, Person>> actions
                = new List<Func<Person, Person>>();
        }
        static void Main(string[] args)
        {
            
        }
    }
}
