using System;
using System.Collections.Generic;

namespace InferenceEngine {

    /// <summary>
    /// Represents a collection of Symbols and Clauses that are known.
    /// </summary>
    class KnowledgeBase {

        public List<Symbol> Symbols { get; private set; }
        public List<Clause> Clauses { get; private set; }
        public bool IsEmpty => Symbols.Count == 0 && Clauses.Count == 0;

        public KnowledgeBase(string[] rawClauses) {

            Symbols = new List<Symbol>();
            Clauses = new List<Clause>();

            foreach (string s in rawClauses) {
                if (Clause.IsClause(s)) {
                    Clauses.Add(new Clause(s));
                }
                else {
                    Symbols.Add(new Symbol(s));
                }
            }

        }

        // Gets a dictionary of all the clauses as keys, where the value is the number of premises in the given clause.
        public Dictionary<Clause, int> GetClausePremiseCount() {

            Dictionary<Clause, int> result = new Dictionary<Clause, int>();

            foreach (Clause c in Clauses) {
                result.Add(c, c.Premises.Count);
            }

            return result;

        }

        // Gets a list of all the clauses where the symbol `p` appears in the premises.
        public List<Clause> GetWithPremise(Symbol p) {

            List<Clause> result = new List<Clause>();

            foreach (Clause c in Clauses) {
                if (c.Premises.Contains(p)) {
                    result.Add(c);
                }
            }

            return result;

        }

        public Clause GetWithHead(Symbol p) {

            foreach (Clause c in Clauses) {
                if (c.Head.Equals(p)) {
                    return c;
                }
            }

            return null;

        }

    }

}
