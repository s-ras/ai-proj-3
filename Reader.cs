namespace ai_proj_3 {

    public static class Reader {
        public const string filePath = "dataset.txt";

        private static string fp = filePath;

        public static void ParseArgs(string[] args) {
            if (args.Length == 0) {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.WriteLine("No arguments given. Defaulting to file \"dataset.txt\" ");
                Console.ResetColor();
                return;
            } else if (args.Length > 1) {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Too many arguments!");
                Console.ResetColor();
                Environment.Exit(1);
            } else {
                fp = args[0];
            }
        }

        public static void ReadDataSet(out List<int> a, out List<int> b, out List<int> c, out int g) {
            List<List<int>> dataset = [];

            int sum = 0;

            if (!File.Exists(fp)) {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Dataset file does not exist!");
                Console.ResetColor();
                Environment.Exit(1);
            }

            foreach (string line in File.ReadLines(fp)) {
                string[] stringNumbers = line.Split(' ');

                List<int> numbers = [];

                foreach (string str in stringNumbers) {
                    if (int.TryParse(str, out int number)) {
                        numbers.Add(number);
                        sum += number;
                    }
                }

                dataset.Add(numbers);

            }

            a = dataset[0];
            b = dataset[1];
            c = dataset[2];
            g = sum / 2;
        }
    }

}