using System.Data;
using System.Diagnostics;

namespace ai_proj_3 {

	internal class Program {

		static void Main(string[] args) {
			Reader.ParseArgs(args);
			Reader.ReadDataSet(out List<int> a, out List<int> b, out List<int> c, out int g);
			HeuristicDefinitions.Init();
			HeuristicFunction h = Input.GetInput();
			Stopwatch sw = new();
			sw.Start();
			State? result = RunAlgorithm(h, a, b, c, g);
			sw.Stop();
			TimeSpan ts = sw.Elapsed;
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
			result?.Print();
		}

		static State? RunAlgorithm(HeuristicFunction h, List<int> a, List<int> b, List<int> c, int g) {
			State start = new(a, b, c);

			List<State> open = [];
			List<State> closed = [];

			open.Add(start);

			while (open.Count > 0) {

				State? s = open.MinBy(s => s.GetF());
				if (s == null) {
					break;
				}
				open.Remove(s);
				List<State?> children = s.GenerateChildren(h);
				foreach (State? child in children) {
					if (child == null) {
						continue;
					}
					if (child.IsGoal(g)) {
						return child;
					}
					List<State> openEq = open.Where(s => State.IsEqual(s, child)).ToList();
					State? openEqMin = openEq.MinBy(s => s.GetF());
					if (openEqMin != null && openEqMin.GetF() < child.GetF()) {
						continue;
					}
					List<State> closedEq = closed.Where(s => State.IsEqual(s, child)).ToList();
					State? closedEqMin = closedEq.MinBy(s => s.GetF());
					if (closedEqMin != null && closedEqMin.GetF() < child.GetF()) {
						continue;
					}

					open.Add(child);
				}

				closed.Add(s);
			}


			return null;
		}

	}

}