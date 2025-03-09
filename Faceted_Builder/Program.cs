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

        //expose other builders
    }

    public class PersonJobBuilder
    {

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            
        }
    }
}
