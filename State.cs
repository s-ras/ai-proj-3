namespace ai_proj_3 {

    using Pole = System.Collections.Generic.Stack<int>;

    public enum P {
        a,
        b,
        c
    }

    public class State {

        #region Static Values

        private static Dictionary<string, State> CreatedStates { get; set; } = [];

        public static State CreateRoot(List<int> a, List<int> b, List<int> c) {
            State s = new(a, b, c);
            return s;
        }

        public static State CreateOrGet(Pole a, Pole b, Pole c) {
            string signiture = State.GenerateSigniture(a, b, c);
            if (CreatedStates.TryGetValue(signiture, out State? value)) {
                return value;
            } else {
                State s = new(a, b, c);
                CreatedStates.Add(signiture, s);
                return s;
            }
        }

        private static string GenerateSigniture(Pole a, Pole b, Pole c) {
            string a_pole = String.Join(", ", a.ToArray().Reverse());
            string b_pole = String.Join(", ", b.ToArray().Reverse());
            string c_pole = String.Join(", ", c.ToArray().Reverse());

            return a_pole + "|" + b_pole + "|" + c_pole;

        }

        public static bool GoalTest(State s) {
            int sumA = s.Poles[P.a].Sum();
            int sumB = s.Poles[P.b].Sum();
            int sumC = s.Poles[P.c].Sum();
            if (sumA == sumB && sumC == 0) return true;
            if (sumB == sumC && sumA == 0) return true;
            if (sumA == sumC && sumB == 0) return true;
            return false;
        }

        public static int GetCount() {
            return CreatedStates.Count;
        }

        #endregion

        #region Values

        public Dictionary<P, Pole> Poles { get; } = [];
        public string Signiture { get; }

        private State(List<int> a, List<int> b, List<int> c) {
            this.Poles[P.a] = new(a);
            this.Poles[P.b] = new(b);
            this.Poles[P.c] = new(c);
            this.Signiture = GenerateSigniture(this.Poles[P.a], this.Poles[P.b], this.Poles[P.c]);
        }

        private State(Pole a, Pole b, Pole c) {
            this.Poles[P.a] = a;
            this.Poles[P.b] = b;
            this.Poles[P.c] = c;
            this.Signiture = GenerateSigniture(this.Poles[P.a], this.Poles[P.b], this.Poles[P.c]);
        }

        public void Print() {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("Pole A ");
            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.Write($"Pole weight: {this.Poles[P.a].Sum()}");
            Console.ResetColor();
            Console.WriteLine(" " + String.Join(" ", this.Poles[P.a]) + " ");

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("Pole B ");
            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.Write($"Pole weight: {this.Poles[P.b].Sum()}");
            Console.ResetColor();
            Console.WriteLine(" " + String.Join(" ", this.Poles[P.b]) + " ");

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("Pole C ");
            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.Write($"Pole weight: {this.Poles[P.c].Sum()}");
            Console.ResetColor();
            Console.WriteLine(" " + String.Join(" ", this.Poles[P.c]) + " ");
        }

        #endregion

    };
}