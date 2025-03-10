namespace Stepwise_Builder
{
    // Stepwise Builder + Interface segregation principle
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
        // First specify the type and after that specify the wheel size and maybe apply some validation

        // 1. return one interface for onfiguring the type,
        // 2. then another for wheel size
        // 3. and then the 3rd one for actually building the Car
    }

    public interface ISpecifyCarType
    {
        ISpecifyWheelSize OfType(CarType type);
    }

    public interface ISpecifyWheelSize
    {
        IBuildCar WithWheels(int size);
    }

    public interface IBuildCar
    {
        public Car Build();
    }

    public class CarBuilder
    {
        // the class is public => if we implement methods from interfaces
        // those methods would be invokable in whatever order you wish
        // you just need to provide the right interface as an argument
        // which is not what we want
        // that's why we want a private class Impl

        private class Impl :
            ISpecifyCarType,
            ISpecifyWheelSize,
            IBuildCar
        {
            private Car car = new Car();
            public ISpecifyWheelSize OfType(CarType type)
            {
                car.Type = type;
                return this; // ISpecifyWheelSize
            }
            public IBuildCar WithWheels(int size)
            {
                // validation 
                switch (car.Type)
                {
                    case CarType.Crossover when size < 17 || size > 20:
                    case CarType.Sedan when size < 15 || size > 17:
                        throw new ArgumentException($"Wrong size of wheel for {car.Type}");
                }

                car.WheelSize = size;
                return this; // this is casted to IBuildCar 
            }
            public Car Build()
            {
                return car;
                // now we have a builder and a way of enforcing the order of execution
            }                     
        }
        public static ISpecifyCarType Create()
        {
            return new Impl(); // Impl casted to ISpecifyCarType
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            
        }
    }
}
