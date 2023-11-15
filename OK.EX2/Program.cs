using CommonTools;
using Google.OrTools.LinearSolver;
using System.Text;

namespace OK.EX2;
public static class Program
{
    public static void Main(string[] args)
	{
		string[] words = {
			"LQCGAPPJAIQNNKHJNFFE",
			"LHCGAPPJABQNNKHJBFAE",
			"RQCGSPPJAIQNNKHJNFFE",
			"LQCGAPPJAIQNNKHJNFFE",
			"LQCGAPDJFIQNNKHJNFFE"
		};

		int m = words[0].Length;
		int n = words.Length;   
		int k = 7;              

		Solver solver = Solver.CreateSolver("GLOP_LINEAR_PROGRAMMING");

		Variable[] y = new Variable[m]; // y[j] zmienna binarna wskazująca, czy kolumna j jest usuwana

		for (int j = 0; j < m; ++j)
		{
			y[j] = solver.MakeBoolVar($"y[{j}]");
		}

		// suma zmiennych y[j] musi być równa k
		Constraint kConstraint = solver.MakeConstraint(k, k);
		foreach (Variable var in y)
		{
			kConstraint.SetCoefficient(var, 1);
		}

		LinearExpr objective = new LinearExpr();
		for (int i = 0; i < n; ++i)
		{
			for (int j = i + 1; j < n; ++j)
			{
				for (int l = 0; l < m; ++l)
				{
					Variable match = solver.MakeBoolVar($"match_{i}_{j}_{l}");
					objective += match;

					solver.Add(match <= 1 - y[l]);
					solver.Add(match <= (words[i][l] == words[j][l] ? 1 : 0));
					solver.Add(match >= (1 - y[l]) - (words[i][l] != words[j][l] ? 1 : 0));
				}
			}
		}

		solver.Maximize(objective);
		Solver.ResultStatus resultStatus = solver.Solve();

		if (resultStatus == Solver.ResultStatus.OPTIMAL)
		{
			string resultWord = "";
			StringBuilder sb = new StringBuilder();

			Console.WriteLine("Solution:");
			for (int j = 0; j < m; ++j)
			{
				if (y[j].SolutionValue() > 0.5)
				{
					Console.WriteLine($"The column {j + 1} should be removed");
				}
				else
				{
					sb.Append(words[1][j]);
				}
			}

			resultWord = sb.ToString();
			Console.WriteLine($"The result word: {resultWord}");
		}
		else
		{
			Console.WriteLine("The problem does not have an optimal solution.");
		}
	}
}
