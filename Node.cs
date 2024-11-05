namespace ai_proj_3 {

	using Pole = System.Collections.Generic.Stack<int>;

	public class Node {
		private static int _nodeCount;
		private static bool _changed = false;

		public static int NodeCount {
			get => _nodeCount;
			set {
				_nodeCount = value;
				_changed = true;
			}
		}

		public Node? Parent { get; set; }

		public State S { get; }

		public int G { get; }
		public int H { get; }

		public int F { get => this.G + this.H; }

		public string Description { get; }

		public P? From;
		public P? To;

		public Node(State s) {
			this.S = s;
			this.Parent = null;
			this.G = 0;
			this.H = 0;
			this.Description = "Start";
			this.From = null;
			this.To = null;
			NodeCount = 1;
		}

		private Node(State s, int g, int h, string description, P? from, P? to) {
			this.S = s;
			this.G = g;
			this.H = h;
			this.Description = description;
			this.From = from;
			this.To = to;
			NodeCount++;
		}

		private Node? Move(P from, P to, HeuristicFunction h) {

			if (from == to) return null;

			if (from == this.To && to == this.From) return null;

			if (this.S.Poles[from].Count < 1) return null;

			Dictionary<P, Pole> newPoles = new() {
				{ P.a, new(this.S.Poles[P.a].Reverse()) },
				{ P.b, new(this.S.Poles[P.b].Reverse()) },
				{ P.c, new(this.S.Poles[P.c].Reverse()) }
			};

			int moving = newPoles[from].Pop();

			newPoles[to].Push(moving);

			string newDesc = $"{moving} : {from} -> {to}";

			State newS = State.CreateOrGet(newPoles[P.a], newPoles[P.b], newPoles[P.c]);

			int newG = this.G + moving;

			int newH = h.Run([newPoles[P.a], newPoles[P.b], newPoles[P.c]]);

			return new(newS, newG, newH, newDesc, from, to);

		}

		public List<Node> GenerateChildren(HeuristicFunction h) {
			List<Node> children = [];

			for (P from = P.a; from <= P.c; from++) {
				for (P to = P.a; to <= P.c; to++) {
					Node? child = this.Move(from, to, h);

					if (child != null) children.Add(child);
				}
			}

			return children;
		}

		public void PrintPath() {
			this.Parent?.PrintPath();
			Console.WriteLine(this.Description);
		}

		public static void TrackNodeCount() {
			try {
				Console.CursorVisible = false;

				List<string> phases = [
					"(-*--------)", "(-----*----)", "(---------*)", "(--------*-)", "(---*------)", "(*---------)"
				];

				int phase = 0;

				while (!Program.finished) {

					string output = "";

					output += phases[phase] + "\t";

					if (phase == phases.Count - 1) {
						phase = 0;
					} else {
						phase++;
					}

					if (_changed) {
						_changed = false;

						output += $"Nodes created : {NodeCount} | States created : {State.GetCount()}";
					}

					Console.SetCursorPosition(0, Console.CursorTop - 1);
					Console.WriteLine(output);

					Thread.Sleep(100);
				}

			} finally {
			}
			Console.SetCursorPosition(0, Console.CursorTop - 1);
			Console.Write(new string(' ', Console.WindowWidth));
			Console.BackgroundColor = ConsoleColor.DarkGray;
			Console.WriteLine($"{NodeCount} nodes created ");
			Console.ResetColor();
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;
			Console.WriteLine($"{State.GetCount()} states created ");
			Console.ResetColor();

		}

	}

}