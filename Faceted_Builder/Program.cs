namespace Faceted_Builder
{
    // Faceted Builder 
    // Sometimes one Builder is not enough and you need several builders which are responsible for building up several
    // different aspects of a particular object and you also want some sort of facade (which is another design pattern)

    public class Person
    {
        // address
        public string StreetAddress, Postcode, City;
        // and you might want a builder for building up the address in a fluent way 

        //employment info 
        public string CompanyName, Position;
        public int AnnualIncome;

        public override string ToString()
        {
            return $"{nameof(StreetAddress)}: {StreetAddress}, {nameof(Postcode)} : {Postcode}, {nameof(City)}: {City}, {nameof(CompanyName)}: {CompanyName}, {nameof(Position)}: {Position}, {nameof(AnnualIncome)}: {AnnualIncome}";
        }

        //now we want builder for address info and a builder for employment info and for jumping from building one to building another 
        //for this we have 2 builder facets - a facet for address and a facet for employment
    }

    // PersonBuilder isn't really a builder in the full sense of the word => it's a FACADE
    // Facade for another builders, it doesn't build up a Person by itself
    // But it keeps a reference to a Person that's being built up +
    // It allows you access to those sub-builders 
    public class PersonBuilder
    {
        // this is a REFERENCE to the object that's being built up (important!)
        protected Person person = new Person();

        //expose other builders (public property) => takes the original person that's being built up
        public PersonJobBuilder Works => new PersonJobBuilder(person);
        public PersonAddressBuilder Lives => new PersonAddressBuilder(person);

        // to get an actual person info in the end =>
        // introduce an implicit conversion operator to Person 
        public static implicit operator Person(PersonBuilder pb)
        {
            return pb.person;
        }
    }

    // Designed for building up job info on a Person object
    public class PersonJobBuilder : PersonBuilder
    {
        // why inhereting from PersonBuilder?

        //take reference to person in constructor and store it in field that is inherited
        public PersonJobBuilder(Person person)
        {
            this.person = person;
        }

        // big fluent interface:
        public PersonJobBuilder At(string companyName)
        {
            person.CompanyName = companyName;
            return this;
        }

        public PersonJobBuilder AsA(string position)
        {
            person.Position = position;
            return this;
        }

        public PersonJobBuilder Earning(int amount)
        {
            person.AnnualIncome = amount;
            return this;
        }
    }

    // Designed for building up address info on a Person object
    public class PersonAddressBuilder : PersonBuilder
    {
        // might not work with a VALUE TYPE (!) struct X
        public PersonAddressBuilder(Person person)
        {
            this.person = person;
        }
        public PersonAddressBuilder At(string streetAddress)
        {
            person.StreetAddress = streetAddress;
            return this;
        }
        public PersonAddressBuilder WithPostcode(string postcode)
        {
            person.Postcode = postcode;
            return this;
        }
        public PersonAddressBuilder In(string city)
        {
            person.City = city;
            return this;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var pb = new PersonBuilder();
            //building up a person => no methods available => need to expose PersonJobBuilder from PersonBuilder (!)
            //once exposed => we can access them
            var person = pb
                .Lives
                      .At("123 London Road")
                      .In("London")
                      .WithPostcode("SW12AC")
                .Works.At("Google")
                      .AsA("Engineer")
                      .Earning(123000);

            Console.WriteLine(person.ToString()); // we will not get the info as for now person is actually a PersonJobBuilder (from the last call)
            //actual output:
            //Faceted_Builder.PersonJobBuilder


            //implicit conversion operator to Person in PersonBuilder allows us to do the following:
            Person person2 = pb
                .Lives
                      .At("123 London Road")
                      .In("London")
                      .WithPostcode("SW12AC")
                .Works.At("Google")
                      .AsA("Engineer")
                      .Earning(123000);

            Console.WriteLine(person2.ToString());
            //actual output:
            //StreetAddress: 123 London Road, Postcode : SW12AC, City: London, CompanyName: Google, Position: Engineer, AnnualIncome: 123000

            Console.ReadKey();
        }
    }
}
