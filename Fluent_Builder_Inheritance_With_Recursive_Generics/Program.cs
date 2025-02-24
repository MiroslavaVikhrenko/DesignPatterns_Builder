namespace Fluent_Builder_Inheritance_With_Recursive_Generics
{
    //when builders inherit from other builders

    public class Person
    {
        public string Name;
        public string Position;

        public class Builder : PersonJobBuilder<Builder>
        {

        }
        public static Builder New => new Builder();
        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
        }
    }

    public abstract class PersonBuilder
    {
        protected Person person = new Person(); //protected instead of private because we are using inheritance
        public Person Build()
        {
            return person;
        }
    }
    //class Foo : Bar<Foo>
    public class PersonInfoBuilder<SELF> : PersonBuilder 
        where SELF : PersonInfoBuilder<SELF>
    {
        //we need to restrict <SELF> to ensure nothing unrelevant is passed
        //fluent method
        public SELF Called(string name)
        {
            person.Name = name;
            //cast manually
            return (SELF)this;
        }
    }

    //imagine you need to add additional info but don't want to chnage existing class
    //use generic argument <SELF>
    public class PersonJobBuilder<SELF> : PersonInfoBuilder<PersonJobBuilder<SELF>> 
        where SELF : PersonJobBuilder<SELF>
    {
        public SELF WorksAsA(string position)
        {
            person.Position = position;
            //cast manually
            return (SELF)this;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            //var builder = new PersonJobBuilder();
            //builder.Called("Yoko").WorksAsA("position");
            //=> not working as Called() returns PersonInfoBuilder, not PersonJobBuilder
            //and PersonInfoBuilder doesn't know about WorksAsA()

            var person = Person.New.Called("Yoko").WorksAsA("quant").Build();
            Console.WriteLine(person);
        }
    }
}
