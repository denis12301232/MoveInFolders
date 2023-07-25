namespace Util
{
    public class Progress
    {
        private int chunks;
        private int total;
        private string? description;
        public Progress(int total, int chunks)
        {
            this.total = total;
            this.chunks = chunks;
        }

        public void draw(int progress, string desc)
        {
            Console.CursorVisible = false;
            Console.CursorLeft = 0;
            Console.Write("["); //start
            Console.CursorLeft = chunks + 1;
            Console.Write("]"); //end
            Console.CursorLeft = 1;
            double percent = Convert.ToDouble(progress) / total;
            int complete = Convert.ToInt16(chunks * percent);
            description = desc;
            //draw completed chunks
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.Write("".PadRight(complete));

            //draw incomplete chunks
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write("".PadRight(chunks - complete));

            //draw totals
            Console.CursorLeft = chunks + 5;
            Console.BackgroundColor = ConsoleColor.Black;

            string output = progress.ToString() + " of " + total.ToString();
            Console.Write(output.PadRight(15) + description); //pad the output so when changing from 3 to 4 digits we avoid text shifting
            Console.CursorVisible = true;
        }

        public void end(string message)
        {
            Console.WriteLine($"\n {message}");
        }
    }


}