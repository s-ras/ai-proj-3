namespace ai_proj_3 {

    public static class Input {

        public static HeuristicFunction GetInput() {
            int choice = -1;

            do {
                Console.BackgroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("Which heuristic function do you want to use?");
                Console.ResetColor();
                Heuristics.ListHeuristics();

                if (int.TryParse(Console.ReadLine(), out choice) && choice > 0 && choice <= Heuristics.Count()) {
                    return Heuristics.Functions[choice - 1];
                }

                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid choice. Please try again.");
                Console.ResetColor();
            } while (true);
        }

    }

}