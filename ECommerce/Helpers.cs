namespace ECommerce
{
    public static class Helpers
    {
        public static int ReadInt(string message)
        {
            while (true)
            {
                Console.Write(message);
                string? input = Console.ReadLine();

                try { return int.Parse(input!); }
                catch { Console.WriteLine("Invalid number. Try again."); }
            }
        }

        public static int ReadMenuChoice(string message, int min, int max)
        {
            while (true)
            {
                Console.Write(message);
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int value) && value >= min && value <= max)
                    return value;

                Console.WriteLine($"Please enter a number between {min} and {max}.");
            }
        }
    }
}