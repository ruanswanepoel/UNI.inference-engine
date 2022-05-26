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

        /// <summary>
        /// Gets the clause with the given result/head
        /// </summary>
        /// <param name="p">The head symbol</param>
        /// <returns>The clause</returns>
        public Clause GetWithHead(Symbol p) {

            foreach (Clause c in Clauses) {
                if (c.Head.Equals(p)) {
                    return c;
                }
            }

            return null;

        }

        /// <summary>
        /// Gets all the possible symbols found in the knowledgebase
        /// </summary>
        /// <returns>All the symbols</returns>
        public List<Symbol> GetAllSymbols() {

            List<Symbol> result = new List<Symbol>(Symbols);

            foreach (Clause c in Clauses) {
                foreach (Symbol s in c.Premises) {
                    if (!result.Contains(s))
                        result.Add(s);
                }
                if (!result.Contains(c.Head))
                    result.Add(c.Head);
            }

            return result;

        }

    }

}
