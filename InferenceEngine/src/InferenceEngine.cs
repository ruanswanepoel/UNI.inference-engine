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
        public static List<Symbol> PL_FC_Entails(KnowledgeBase kb, Symbol q) {

            Dictionary<Clause, int> count = kb.GetClausePremiseCount();
            Queue<Symbol> agenda = new Queue<Symbol>(kb.Symbols);
            Dictionary<Symbol, bool> inferred = agenda.ToDictionary(x => x, x => false);

            // Loop until there are no more symbols
            while (agenda.Count != 0) {
                // Pop the next symbol from the agenda
                Symbol p = agenda.Dequeue();
                // If we haven't already checked the current symbol
                if (!inferred[p]) {
                    inferred[p] = true;
                    // If we've found the goal
                    if (p.Equals(q)) {
                        // Return the list of all inferred symbols in order
                        var beenInferred = inferred.Where(x => x.Value == true).ToDictionary(x => x.Key, x => x.Value);
                        return new List<Symbol>(beenInferred.Keys);
                    }
                    // Loop over each clause where `p` appears in the premises
                    foreach (Clause c in kb.GetWithPremise(p)) {
                        // Reduce the current clause's premise count
                        count[c]--;
                        // If all the premises of the current clause are found
                        if (count[c] == 0) {
                            // Otherwise, add the current symbol to the agenda
                            agenda.Enqueue(c.Head);
                            inferred.Add(c.Head, false);
                        }
                    }
                }
            }

            // Goal was not found
            return null;

        }

        // Backward chaining algorithm
        public static bool PL_BC_Entails(KnowledgeBase kb, Symbol q) {

            Dictionary<Clause, int> count = kb.GetClausePremiseCount();
            Queue<Symbol> agenda = new Queue<Symbol>();
            Dictionary<Symbol, bool> inferred = agenda.ToDictionary(x => x, x => false);

            agenda.Enqueue(q);

            while (agenda.Count != 0) {
                // Get next symbol in queue
                Symbol p = agenda.Dequeue();
                //
                if (!inferred[p]) {
                    inferred[p] = true;
                    // Get clause with current symbol as the head
                    Clause c = kb.GetWithHead(p);
                    // 
                    foreach (Symbol s in c.Premises) {
                        agenda.Enqueue(s);
                        inferred.Add(s, false);
                    }
                }
            }

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
