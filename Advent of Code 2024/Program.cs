namespace AoC.Y24;

public static class Program
{
    public static void Main(string[] input)
    {
        // Prepare Console
        Console.WindowWidth = 150;
        Console.WindowHeight = 60;

        // Start the Festive Program Runner
        var festiveRunner = new FestiveProgramRunner();
        festiveRunner.Run();
    }
}