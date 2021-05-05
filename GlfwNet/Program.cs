namespace GlfwNet
{
    internal static class Program
    {
        private static void Main()
        {
            using (var game = new Game())
                game.Run();
        }
    }
}