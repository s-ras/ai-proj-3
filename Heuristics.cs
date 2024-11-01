using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace ai_proj_3 {

    public class HeuristicFunction {

        public string Title { get; }
        public Func<List<Stack<int>>, int> Run { get; }

        public HeuristicFunction(string title, Func<List<Stack<int>>, int> run) {
            Title = title;
            Run = run;
            Heuristics.AddHeuristic(this);
        }

    }

    public static class Heuristics {

        public static List<HeuristicFunction> Functions { get; } = [];

        public static void AddHeuristic(HeuristicFunction heuristic) {
            Functions.Add(heuristic);
        }

        public static int Count() {
            return Functions.Count;
        }

        public static void ListHeuristics() {
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Total Heuristics: {Functions.Count}");
            Console.ResetColor();
            for (int i = 0; i < Functions.Count; i++) {
                Console.WriteLine($"{i + 1}: {Functions[i].Title}");
            }
        }

    }

    public static class HeuristicDefinitions {

        public static readonly HeuristicFunction HeuristicA;
        public static readonly HeuristicFunction HeuristicB;

        public static void Init() {
            return;
        }

        static HeuristicDefinitions() {
            HeuristicA = new("h = 0",
                (p) => {
                    return 0;
                }
            );

            HeuristicB = new("h = |max_poll - min_poll|", (p) => {
                int min = int.MaxValue;
                int max = -1;

                foreach (Stack<int> poll in p) {
                    int sum = poll.Sum();
                    if (sum < min) {
                        min = sum;
                    }
                    if (sum > max) {
                        max = sum;
                    }
                }

                return Math.Abs(max - min);
            });
        }

    }

}