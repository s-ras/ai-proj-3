using System.Data;
using System.Diagnostics;

namespace ai_proj_3 {

	internal class Program {

		public static bool finished = false;

		static void Main(string[] args) {
			Reader.ParseArgs(args);
			Reader.ReadDataSet(out List<int> a, out List<int> b, out List<int> c, out int g);
			HeuristicDefinitions.Init();
			HeuristicFunction h = Input.GetInput();
			Stopwatch sw = new();
			Thread t = new(Node.TrackNodeCount);
			t.Start();
			sw.Start();
			Node? result = RunAlgorithm(h, a, b, c, g);
			sw.Stop();
			TimeSpan ts = sw.Elapsed;
			finished = true;
			t.Join();
			Console.BackgroundColor = ConsoleColor.DarkBlue;
			Console.WriteLine($"Execution Time: {ts} ");
			if (result != null) {
				Console.BackgroundColor = ConsoleColor.Magenta;
				Console.WriteLine($"Total weight moved: {result.G} ");

			} else {
				Console.BackgroundColor = ConsoleColor.Magenta;
				Console.WriteLine("No soltion was found ");
			}
			Console.ResetColor();
			result?.PrintPath();
			result?.S.Print();
		}

		static Node? RunAlgorithm(HeuristicFunction h, List<int> a, List<int> b, List<int> c, int g) {
			State initialState = State.CreateRoot(a, b, c);

			Node start = new(initialState);

			PriorityQueue<Node, int> open = new();

			Dictionary<string, int> visited = [];

			open.Enqueue(start, start.F);

			while (open.Count > 0) {

				Node? current = open.Dequeue();

				if (current == null) {
					break;
				}

				if (State.GoalTest(current.S)) {
					return current;
				}

				List<Node> children = current.GenerateChildren(h);

				foreach (Node child in children) {

					if (visited.TryGetValue(child.S.Signiture, out int F)) {
						if (child.F < F) {
							child.Parent = current;
							open.Enqueue(child, child.F);
							visited.TryAdd(child.S.Signiture, child.F);

						}
					} else {
						child.Parent = current;
						open.Enqueue(child, child.F);
						visited.TryAdd(child.S.Signiture, child.F);

					}
				}

			}

			return null;
		}


	}

}