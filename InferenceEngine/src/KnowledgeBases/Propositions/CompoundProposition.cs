using System;
using System.Collections.Generic;
using InferenceEngine.KnowledgeBases.Connectives;

namespace InferenceEngine.KnowledgeBases.Propositions {

    /// <summary>
    /// A proposition that is a compound formula of atomic propositions.
    /// </summary>
    class CompoundProposition : Proposition {

        public List<AtomicProposition> Conditions { get; private set; } // Propositions on the left side of the implication
        public List<Connective> Conditionals { get; private set; } // Connectives on the left side of the implication
        public Connective Implication { get; private set; } // The implication connective
        public AtomicProposition Result { get; private set; } // The resulting proposition on the right side of the implication

        public CompoundProposition(string symbol) {

            Symbol = symbol;

            // Get all connectives and their indices
            List<KeyValuePair<int, Connective>> conditionalIndexs = new List<KeyValuePair<int, Connective>>();
            KeyValuePair<int, Connective> implicationIndex = default;

            foreach (string s in Connective.AllSymbols) {
                int i = symbol.IndexOf(s);
                if (i != -1) {
                    Connective c = Connective.Create(s);
                    KeyValuePair<int, Connective> kv = new KeyValuePair<int, Connective>(i, c);
                    if (c.IsImplication) {
                        implicationIndex = kv;
                    }
                    else {
                        conditionalIndexs.Add(kv);
                    }
                }
            }

            if (implicationIndex.Equals(default(KeyValuePair<int, Connective>))) {
                throw new Exception("No implication symbol found in given proposition string");
            }

            // Get all conditions propositions
            List<AtomicProposition> conditions = new List<AtomicProposition>();
            List<KeyValuePair<int, Connective>> _tmp = new List<KeyValuePair<int, Connective>>(conditionalIndexs);
            _tmp.Add(implicationIndex);
            int next_index = 0;

            foreach (KeyValuePair<int, Connective> kv in _tmp) {
                int l = kv.Key - next_index;
                string s = symbol.Substring(next_index, l);
                conditions.Add(new AtomicProposition(s));
                next_index += kv.Key + kv.Value.Length;
            }

            // Flatten conditionals list
            List<Connective> conditionals = new List<Connective>();

            foreach (KeyValuePair<int, Connective> kv in conditionalIndexs) {
                conditionals.Add(kv.Value);
            }

            Conditions = conditions;
            Conditionals = conditionals;
            Implication = implicationIndex.Value;
            Result = new AtomicProposition(symbol.Substring(implicationIndex.Key + implicationIndex.Value.Length));

        }

        // Whether the given symbol is a compund proposition
        // TODO: check only for implication symbols
        public static bool IsCompoundProposition(string symbol) {

            foreach (string s in Connective.AllSymbols) {
                if (symbol.Contains(s)) {
                    return true;
                }
            }

            return false;

        }

    }

}
