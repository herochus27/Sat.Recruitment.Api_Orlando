using System.IO;

namespace Services
{
    public class FileService
    {
        public StreamReader ReadUsersFromFile()
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

            FileStream fileStream = new FileStream(path, FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }

        public void WriteUserFromFile(string line)
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";
            var lines = File.ReadAllText(path);
            lines += "\n" + line;
            File.WriteAllText(path, lines);
        }


    }
}