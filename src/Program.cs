using Newtonsoft.Json;
using Util;

namespace Project
{
    class Program
    {
        private static readonly string configPath = Helpers.combinePaths(Directory.GetCurrentDirectory(), "config", "config.json");
        private static readonly string version = "1.0";

        private static string datePath
        {
            get => DateTime.Now.ToString("yyyy.MM.dd").Replace('.', '/');
        }

        public static void Main(string[] args)
        {
            try
            {
                watchArgs(args);
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

        private static void watchArgs(string[] args)
        {
            if (args.Contains("-v"))
            {
                Console.WriteLine(version);
                Environment.Exit(1);
            }
        }

        private static void moveFiles(in string[] dirs, in string path)
        {
            string[] exts = new string[3] { ".xlsx", ".txt", " PLI.xlsx" };
            Progress progress = new Progress(dirs.Length, 50);

            for (int i = 0; i < dirs.Length; i++)
            {
                string folderName = new DirectoryInfo(dirs[i]).Name;

                foreach (string ext in exts)
                {

                    if (!Directory.Exists(Helpers.combinePaths(dirs[i], datePath)))
                    {
                        Directory.CreateDirectory(Helpers.combinePaths(dirs[i], datePath));
                    }

                    if (File.Exists(dirs[i] + ext))
                    {

                        File.Move(dirs[i] + ext, Helpers.combinePaths(dirs[i], datePath, folderName) + ext);
                    }
                }
                progress.draw(i + 1, $"Watch: {folderName}");
            }

            progress.end("\nComplete!");
        }

        private static Config? readConfig(in string path)
        {
            StreamReader stream = new StreamReader(path);
            string json = stream.ReadToEnd();
            return JsonConvert.DeserializeObject<Config>(json);
        }

        private static string[] getDirectiories(in string path)
        {
            string[] dirs = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);
            return dirs;
        }

        private class Config
        {
            public string? path = null;

        }

    }
}
