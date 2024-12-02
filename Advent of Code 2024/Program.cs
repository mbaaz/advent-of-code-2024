using System.Reflection;

namespace AoC.Y24
{
    public static class Program
    {
        private const int MAX_TRIES = 3;
        private const string EXIT = "exit";

        public static void Main(string[] input)
        {
            Console.WriteLine("Welcome to Advent of Code 2024!\n\n");
            var solvers = GetSolvers();

            var greeting = $"Which day do you wish to run? [{solvers.Keys.Min()}-{solvers.Keys.Max()}]: ";
            var @try = 0;

            while (true)
            {
                if (@try > 0)
                {
                    Console.Write("Invalid input");

                    if (@try < MAX_TRIES)
                    {
                        Console.WriteLine(", try again...");
                    }
                    else
                    {
                        Console.WriteLine("\nMaximum number of invalid tries");
                        return;
                    }
                }

                @try++;

                Console.Write(greeting);
                var dayToRunInput = Console.ReadLine();

                if (string.Equals(dayToRunInput, EXIT, StringComparison.InvariantCultureIgnoreCase))
                {
                    Console.WriteLine("\n\nQuitting, thanks for this time!");
                    return;
                }

                IDaySolutionImplementation? solver = null;
                if (string.IsNullOrEmpty(dayToRunInput))
                {
                    solver = solvers[solvers.Keys.Max()];
                }
                else if (int.TryParse(dayToRunInput, out var dayToRun) && solvers.TryGetValue(dayToRun, out var solver1))
                {
                    solver = solver1;
                }
                
                if (solver != null)
                {
                    @try = 0;
                    try
                    {
                        solver?.Run(Console.WriteLine);
                        Console.WriteLine("\n");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Exception was thrown in solver:\n\t{ex.Message}");
                    }
                }
            }
        }
        
        private static IDictionary<int, IDaySolutionImplementation?> GetSolvers()
        {
            var types = GetSolverTypes();
            var solvers = types
                .Select(t => Activator.CreateInstance(t) as IDaySolutionImplementation)
                .Where(s => 
                    s != null && 
                    s.IsActive
                )
                .ToDictionary(s => s!.Day)
            ;
            return solvers;
        }

        private static IEnumerable<Type> GetSolverTypes()
        {
            var solverType = typeof(IDaySolutionImplementation);
            var types = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => 
                    solverType.IsAssignableFrom(type) && 
                    !type.IsInterface && 
                    type.IsClass && 
                    !type.IsAbstract
                )
                .ToList()
            ;
            return types;
        }
    }
}