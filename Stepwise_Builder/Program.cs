namespace Stepwise_Builder
{
    // Stepwise Builder 
    // How do you actually get the builder to perform a set of steps?
    // (one after another in a specific order)

    public enum CarType
    {
        Sedan,
        Crossover
    }
    public class Car
    {
        public CarType Type;
        public int WheelSize;

        // assuming that depending on a car type - you can only select a certain wheel size
        // Sedan: 15 to 17 in
        // Crossover: 17 to 20 in
        // This means that if you want to use builder to construct a car => you have to construct it in a specific order
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            
        }
    }
}
