using System.Security.Cryptography.X509Certificates;

namespace ai_proj_3 {

    public enum PoleNames {
        a,
        b,
        c
    }

    public class State {
        public State? Parent { get; }

        public Dictionary<PoleNames, Stack<int>> Poles { get; }

        public int G { get; }
        public int H { get; }

        public string Description { get; }
        public string Signiture { get; }

        public State(List<int> a, List<int> b, List<int> c) {
            this.Parent = null;
            this.Poles = [];
            this.Poles[PoleNames.a] = new Stack<int>(a);
            this.Poles[PoleNames.b] = new Stack<int>(b);
            this.Poles[PoleNames.c] = new Stack<int>(c);
            this.G = 0;
            this.H = 0;
            this.Description = "Start";
            this.Signiture = this.GenerateSigniture();

        }

        public State(State pa, Dictionary<PoleNames, Stack<int>> pl, int ng, int nh, string desc) {
            this.Parent = pa;
            this.Poles = pl;
            this.G = ng;
            this.H = nh;
            this.Description = desc;
            this.Signiture = this.GenerateSigniture();
        }

        private string GenerateSigniture() {
            string a = String.Join(", ", this.Poles[PoleNames.a].ToArray());
            string b = String.Join(", ", this.Poles[PoleNames.b].ToArray());
            string c = String.Join(", ", this.Poles[PoleNames.c].ToArray());

            return a + "|" + b + "|" + c;
        }

        private State? Move(PoleNames from, PoleNames to, HeuristicFunction h) {
            if (from == to) return null;

            if (this.Poles[from].Count == 0) {
                return null;
            }

            Stack<int> newA = new(this.Poles[PoleNames.a].Reverse());
            Stack<int> newB = new(this.Poles[PoleNames.b].Reverse());
            Stack<int> newC = new(this.Poles[PoleNames.c].Reverse());

            Dictionary<PoleNames, Stack<int>> newPoles = [];

            newPoles[PoleNames.a] = newA;
            newPoles[PoleNames.b] = newB;
            newPoles[PoleNames.c] = newC;

            int movingItem = newPoles[from].Pop();

            newPoles[to].Push(movingItem);

            return new State(this, newPoles, this.G + movingItem, h.Run([newA, newB, newC]), $"{from.ToString()} -> {to.ToString()}");

        }

        public List<State?> GenerateChildren(HeuristicFunction h) {

            List<State?> children = [];
            foreach (PoleNames f in Enum.GetValues(typeof(PoleNames))) {
                foreach (PoleNames t in Enum.GetValues(typeof(PoleNames))) {
                    children.Add(this.Move(f, t, h));
                }
            }
            return children;
        }

        public int GetF() {
            return G + H;
        }

        public void PrintPath() {
            if (this.Parent == null) {
                Console.WriteLine(this.Description);
                return;
            }
            this.Parent.PrintPath();
            Console.WriteLine(this.Description);
        }

        public void Print() {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("Pole A: ");
            Console.ResetColor();
            Console.Write(String.Join(" ", this.Poles[PoleNames.a]) + " ");
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"Pole weight: {this.Poles[PoleNames.a].Sum()}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("Pole B: ");
            Console.ResetColor();
            Console.Write(String.Join(" ", this.Poles[PoleNames.b]) + " ");
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine($"Pole weight: {this.Poles[PoleNames.b].Sum()}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("Pole C: ");
            Console.ResetColor();
            Console.Write(String.Join(" ", this.Poles[PoleNames.c]) + " ");
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"Pole weight: {this.Poles[PoleNames.c].Sum()}");
            Console.ResetColor();

        }

        public bool IsGoal(int g) {
            if (this.Poles[PoleNames.a].Count == 0) {
                if (this.Poles[PoleNames.b].Sum() == this.Poles[PoleNames.c].Sum() && this.Poles[PoleNames.b].Sum() == g) {
                    return true;
                }
            } else if (this.Poles[PoleNames.b].Count == 0) {
                if (this.Poles[PoleNames.a].Sum() == this.Poles[PoleNames.c].Sum() && this.Poles[PoleNames.a].Sum() == g) {
                    return true;
                }
            } else if (this.Poles[PoleNames.c].Count == 0) {
                if (this.Poles[PoleNames.a].Sum() == this.Poles[PoleNames.b].Sum() && this.Poles[PoleNames.a].Sum() == g) {
                    return true;
                }
            }
            return false;
        }

        public static bool IsEqual(State s1, State s2) {
            if (String.Join(", ", s1.Poles[PoleNames.a].ToArray()) != String.Join(", ", s2.Poles[PoleNames.a].ToArray())) {
                return false;
            }

            if (String.Join(", ", s1.Poles[PoleNames.b].ToArray()) != String.Join(", ", s2.Poles[PoleNames.b].ToArray())) {
                return false;
            }

            if (String.Join(", ", s1.Poles[PoleNames.c].ToArray()) != String.Join(", ", s2.Poles[PoleNames.c].ToArray())) {
                return false;
            }

            return true;

        }



    }

}