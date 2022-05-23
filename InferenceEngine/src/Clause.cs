using System;
using System.Collections.Generic;

namespace InferenceEngine {

	class Clause {

		public List<Symbol> Premises { get; private set; }
		public Symbol Head { get; private set; }

		public Clause(string clause) {

			int implicationIndex = clause.IndexOf("=>");
			List<int> connectiveIndexes = clause.IndexOfAll("&");
			connectiveIndexes.Add(implicationIndex);

			Premises = new List<Symbol>();
			Head = new Symbol(clause.Substring(implicationIndex + 2));

			int lastIndex = 0;
			foreach (int i in connectiveIndexes) {
				Premises.Add(new Symbol(clause.Substring(lastIndex, i)));
				lastIndex = i + 1;
			}

		}

		public static bool IsClause(string s) {

			return s.Contains("=>");

		}

	}

}
