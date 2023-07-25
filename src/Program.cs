using Newtonsoft.Json;
using Util;

namespace Project
{
    class Program
    {
        static readonly string configPath = Helpers.combinePaths(Directory.GetCurrentDirectory(), "config", "config.json");
        private static readonly string version = "1.0";

        public static void Main(string[] args)
        {
            try
            {
                watchArgs(args);
                Console.WriteLine(Directory.GetCurrentDirectory());
                Config? config = readConfig(configPath);

                if (config == null || config?.path == null)
                {
                    throw new Exception("Config now found!");
                }

                string[] dirs = getDirectiories(@config.path);
                moveFiles(dirs, config.path);
            }
            catch (System.Exception Error)
            {
                Console.WriteLine(Error.Message);
                Environment.Exit(1);
            }

        }

        public static void watchArgs(string[] args)
        {
            if (args.Contains("-v"))
            {
                Console.WriteLine(version);
                Environment.Exit(1);
            }
        }

        public static void moveFiles(in string[] dirs, in string path)
        {
            string[] exts = new string[3] { ".xlsx", ".txt", " PLI.xlsx" };
            Progress progress = new Progress(dirs.Length, 50);

            for (int i = 0; i < dirs.Length; i++)
            {
                string folderName = new DirectoryInfo(dirs[i]).Name;

                foreach (string ext in exts)
                {

                    if (!Directory.Exists(Helpers.combinePaths(dirs[i], Time.datePath)))
                    {
                        Directory.CreateDirectory(Helpers.combinePaths(dirs[i], Time.datePath));
                    }

                    if (File.Exists(dirs[i] + ext))
                    {

                        File.Move(dirs[i] + ext, Helpers.combinePaths(dirs[i], Time.datePath, folderName) + ext);
                    }
                }
                progress.draw(i + 1, $"Watch: {folderName}");
            }

            progress.end("\nComplete!");
        }

        public static Config? readConfig(in string path)
        {
            StreamReader stream = new StreamReader(path);
            string json = stream.ReadToEnd();
            return JsonConvert.DeserializeObject<Config>(json);
        }

        public static string[] getDirectiories(in string path)
        {
            string[] dirs = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);
            return dirs;
        }

        public class Config
        {
            public string? path = null;

        }

    }
}
