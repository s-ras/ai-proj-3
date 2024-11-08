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
            for (int i = 0; i < Functions.Count; i++) {
                Console.WriteLine($"{i + 1}: {Functions[i].Title}");
            }
        }

    }

    public static class HeuristicDefinitions {

        public static readonly HeuristicFunction HeuristicA;
        public static readonly HeuristicFunction HeuristicB;
        public static readonly HeuristicFunction HeuristicC;

        public static void Init() {
            return;
        }

        static HeuristicDefinitions() {
            HeuristicA = new("h = 0",
                (p) => {
                    return 0;
                }
            );

            HeuristicB = new("h = |max_pole - min_pole|",
                (p) => {
                    int min = int.MaxValue;
                    int max = -1;

                    foreach (Stack<int> pole in p) {
                        int sum = pole.Sum();
                        if (sum < min) {
                            min = sum;
                        }
                        if (sum > max) {
                            max = sum;
                        }
                    }

                    return Math.Abs(max - min);
                }
            );

            HeuristicC = new("h = min (|a - b| + c, |b - c| + a, |c - a| + b)",
                (p) => {
                    int a_sum = p[0].Sum();
                    int b_sum = p[1].Sum();
                    int c_sum = p[2].Sum();

                    int x_1 = Math.Abs(a_sum - b_sum) + c_sum;
                    int x_2 = Math.Abs(b_sum - c_sum) + a_sum;
                    int x_3 = Math.Abs(c_sum - a_sum) + b_sum;

                    List<int> x = [x_1, x_2, x_3];

                    return x.Min();
                }
            );
        }

    }

}