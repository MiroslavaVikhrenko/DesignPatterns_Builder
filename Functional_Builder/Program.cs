namespace Functional_Builder
{
    // Functional builder 
    // Open - Closed principle
    public class Person
    {
        public string Name, Position;
    }
    // sealed class => you cannot inherit from it
    // => if you want to extend it somehow - you cannot use inheritance 
    // but you can use extension methods instead


    //Generalize previous approach
    public abstract class FunctionalBuilder<TSubject, TSelf>
        where TSelf : FunctionalBuilder<TSubject, TSelf>
        where TSubject : new()
    {
        private readonly List<Func<Person, Person>> actions
            = new List<Func<Person, Person>>();

        private TSelf AddAction(Action<Person> action)
        {
            actions.Add(p =>
            {
                action(p);
                return p;
            });
            return (TSelf) this;
        }

        public TSelf Do(Action<Person> action)
            => AddAction(action);
        public Person Build()
            => actions.Aggregate(new Person(), (p, f) => f(p));
    }
    public sealed class PersonBuilder
        : FunctionalBuilder<Person, PersonBuilder>
    {
        public PersonBuilder Called(string name)
            => Do(p => p.Name = name);
    }

    //public sealed class PersonBuilder
    //{
    //    //<Person, Person> => fluent 
    //    // we are going to affect the person somehow and we're going to return a reference to that person
    //    private readonly List<Func<Person, Person>> actions
    //        = new List<Func<Person, Person>>();

    //    //fluent method 
    //    //take an action and turn it into func to preserve a fluent interface because at some point we want
    //    //to aggregae link method in order to apply all the funcs one after another 
    //    //private method (hidden)
    //    //=> we have to build some sort of wrapper around it to perform some work
    //    private PersonBuilder AddAction(Action<Person> action)
    //    {
    //        actions.Add(p =>
    //        {
    //            action(p);
    //            return p;
    //        });
    //        //we take an action, but we store it as a function which takes a product, returns a product 
    //        //make it fluent by returning this
    //        return this;
    //    }

    //    //public fluent method - adding action to the list (publicly exposed)
    //    public PersonBuilder Do(Action<Person> action)
    //        => AddAction(action);
    //    //give a name to Person (here re-use Do() method with lambda
    //    public PersonBuilder Called(string name)
    //        => Do(p => p.Name = name);
    //    //build all together p - person, f - function
    //    public Person Build()
    //        => actions.Aggregate(new Person(), (p, f) => f(p));
    //}
    public static class PersonBuilderExtensions
    {
        // add Position 
        public static PersonBuilder WorksAs
            (this PersonBuilder builder, string position)
            => builder.Do(p => p.Position = position);
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            // example of functional approach
            var person = new PersonBuilder()
                .Called("Tom")
                .WorksAs("Developer")
                .Build();
        }
    }
}
