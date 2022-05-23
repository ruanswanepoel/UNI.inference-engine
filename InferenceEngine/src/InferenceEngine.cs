using System;
using System.Collections.Generic;
using System.Linq;

namespace InferenceEngine {

	class InferenceEngine {

		// Truth table algorithm
		public static bool TT_Entails(KnowledgeBase kb, string a) {

			List<Symbol> symbols = new List<Symbol>(kb.Symbols);
			return TT_Check_All(kb, a, symbols, new List<int>());

		}

		// Forward chaining algorithm
		public static bool PL_FC_Entails(KnowledgeBase kb, string q) {

			Dictionary<Clause, int> count = kb.GetClausePremiseCount();
			List<Symbol> agenda = kb.Symbols;
			Dictionary<Symbol, bool> inferred = agenda.ToDictionary(x => x, x => false);

			// Loop until there are no more symbols
			while (agenda.Count != 0) {
				// Pop the next symbol
				Symbol p = agenda[0];
				agenda.RemoveAt(0);
				// If we haven't already checked the current symbol
				if (!inferred[p]) {
					// Loop over each clause where `p` appears in the premises
					foreach (Clause c in kb.GetWithPremise(p)) {
						// Reduce the current clause's premise count
						count[c]--;
						// If all the premises of the current clause are found
						if (count[c] == 0) {
							// If we've found the goal
							if (c.Head.Value == q) {
								return true;
							}
							// Otherwise, add the current symbol to the agenda
							agenda.Add(c.Head);
							inferred.Add(c.Head, false);
						}
					}
				}
			}

			// Goal was not found
			return false;

		}

		// Backward chaining algorithm
		public static bool PL_BC_Entails(KnowledgeBase kb, string q) {

			return false;

		}

		static bool TT_Check_All(KnowledgeBase kb, string a, List<Symbol> symbols, List<int> model) {

			return false;

			//if (kb.IsEmpty) {
			//	return PL_True(kb, model) ? PL_True(kb, a) : true;
			//}
			//else {
			//	Proposition p = symbols[0];
			//	symbols.RemoveAt(0);
			//	return TT_Check_All(kb, a, symbols, Extend(p, true, model)) &&
			//		TT_Check_All(kb, a, symbols, Extend(p, false, model));
			//}

		}

	}

}
