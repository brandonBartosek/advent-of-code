namespace Logger
{
    public static class Log
    {
        //Log to File
        public static Guid session = Guid.NewGuid();
        public static void AddToLog(string message, string day)
        {
            var path = $@"..\..\..\..\..\advent-of-code\{day}\{day}_{session}.txt";
            File.AppendAllText(path, message + "\n");
        }
    } 
}
