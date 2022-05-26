using System;
using System.Collections.Generic;

namespace InferenceEngine {

    /// <summary>
    /// Represents a Horn Clause, with list of premise symbols that point to a resulting head symbol.
    /// </summary>
    class Clause {

        public List<Symbol> Premises { get; private set; }  // The conditional premises
        public Symbol Head { get; private set; }            // The resulting symbol

        /// <summary>
        /// Constructs a Clause given the string value.
        /// </summary>
        /// <param name="clause">The Clause string value</param>
        public Clause(string clause) {

            int implicationIndex = clause.IndexOf("=>");
            List<int> connectiveIndexes = clause.IndexOfAll("&");
            connectiveIndexes.Add(implicationIndex);

            Premises = new List<Symbol>();
            Head = new Symbol(clause.Substring(implicationIndex + 2));

            int lastIndex = 0;
            foreach (int i in connectiveIndexes) {
                Premises.Add(new Symbol(clause.Substring(lastIndex, i - lastIndex)));
                lastIndex = i + 1;
            }

        }

        /// <summary>
        /// Checks if the given string value is a valid Clause.
        /// </summary>
        /// <param name="s">The value to check</param>
        /// <returns>True if the value is a Clause.</returns>
        public static bool IsClause(string s) {

            return s.Contains("=>");

        }

    }

}
