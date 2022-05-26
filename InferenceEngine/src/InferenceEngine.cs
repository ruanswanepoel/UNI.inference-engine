using System;
using System.Collections.Generic;
using System.Linq;

namespace InferenceEngine {

    class InferenceEngine {

        /// <summary>
        /// Truth table algorithm
        /// </summary>
        /// <param name="kb">The knowledgebase</param>
        /// <param name="a">The ASK symbol</param>
        /// <returns></returns>
        public static bool TT_Entails(KnowledgeBase kb, Symbol a) {

            List<Symbol> symbols = new List<Symbol>(kb.Symbols);
            return TT_Check_All(kb, a, symbols, new List<int>());

        }

        /// <summary>
        /// Forward chaining algorithm
        /// </summary>
        /// <param name="kb">The knowledgebase</param>
        /// <param name="q">The ASK symbol</param>
        /// <returns></returns>
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

        /// <summary>
        /// Backward chaining algorithm
        /// </summary>
        /// <param name="kb">The knowledgebase</param>
        /// <param name="q">The ASK symbol</param>
        /// <returns></returns>
        public static List<Symbol> PL_BC_Entails(KnowledgeBase kb, Symbol q) {

            Queue<Symbol> agenda = new Queue<Symbol>();
            List<Symbol> visited = new List<Symbol>(agenda);

            agenda.Enqueue(q);

            // Loop until there are no more symbols
            while (agenda.Count != 0) {
                // Pop the next symbol from the agenda
                Symbol p = agenda.Dequeue();
                // If we haven't already checked the current symbol
                if (!visited.Contains(p)) {
                    visited.Add(p);
                    Clause c = kb.GetWithHead(p);
                    if (c == null) {
                        // If we've found that a symbol is unattainable, then the knowledgebase does not entail q
                        if (!kb.Symbols.Contains(p)) {
                            return null;
                        }
                    }
                    else {
                        // Add all premises of the clause to the queue
                        foreach (Symbol s in c.Premises) {
                            agenda.Enqueue(s);
                        }
                    }
                }
            }

            // Return the list of all visited symbols in reverse order
            visited.Reverse();
            return visited;

        }

        // Internal truth table recursive function
        static bool TT_Check_All(KnowledgeBase kb, Symbol a, List<Symbol> symbols, List<int> model) {
            return false;
            //if (symbols.Count == 0) {
            //    return PL_True(kb, model) ? PL_True(kb, a) : true;
            //}
            //else {
            //    Symbol p = symbols[0];
            //    symbols.RemoveAt(0);
            //    return TT_Check_All(kb, a, symbols, Extend(p, true, model)) &&
            //        TT_Check_All(kb, a, symbols, Extend(p, false, model));
            //}

        }

    }

}
