namespace FileInput
{
    public static class FileInputHandler
    {
        public static List<string> GetFileData(string fileName)
        {
            var data = new List<string>();
            if (fileName != null)
            {
                var basePath = @"C:\Users\MistFir3\source\repos\2024-advent-of-code\Day1\FileInput\DataImport\";
                using (StreamReader sr = new StreamReader(basePath + fileName))
                {
                    do
                    {
                        data.Add(sr.ReadLine() ?? string.Empty);
                    }
                    while (sr.Peek() != -1);
                }
            }
            return data;
        }
    }
}
