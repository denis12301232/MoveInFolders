namespace Util
{
    class Helpers
    {
        public static string combinePaths(params string[] paths)
        {

            return paths.Aggregate(Path.Combine);
        }
    }
}