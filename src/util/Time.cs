namespace Util
{
    class Time
    {
        public static string datePath = $"{getYear()}/{getMonth()}/{getDay()}";


        private static DateTime Now
        {
            get => DateTime.Now;

        }
        private static string getDay()
        {
            return Now.Day < 10 ? $"0{Now.Day}" : Now.Day.ToString();
        }

        private static string getMonth()
        {
            return Now.Month < 10 ? $"0{Now.Month}" : Now.Month.ToString();
        }

        private static string getYear()
        {
            return Now.Year.ToString();
        }

    }
}