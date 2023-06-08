namespace Core
{
    public static class Time
    {
        
        private static System.DateTime start;
        public static float deltaTime => (System.DateTime.Now.Ticks - start.Ticks) / 10000000.0f;

        public static void Reset() => start = System.DateTime.Now;
    }
}

