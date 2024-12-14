namespace Simulator
{
    public static class Validator
    {
        public static int Limiter(int value, int min, int max) 
        {
            return Math.Clamp(value, min, max);
        }

        public static string Shortener(string value, int min, int max, char placeholder)
        {
            if (string.IsNullOrWhiteSpace(value))
                return new string(placeholder, min);

            value = value.Trim(); 

            if (value.Length > max)
            {
                return value.Substring(0, max);
            }

            while (value.Length < min)
            {
                value += placeholder;
            }

            return value;
        }
    }
}
