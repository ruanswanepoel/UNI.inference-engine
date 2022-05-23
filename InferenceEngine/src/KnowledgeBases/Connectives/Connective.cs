using System;
using System.Collections.Generic;

namespace InferenceEngine {

    class Connective {

        public enum Types {
            Negation,
            Conjunction,
            Disjunction,
            Implication
        }

        public Types Type { get; private set; } // The type of connective this is
        public bool IsImplication => Type == Types.Implication;
        public int Length => typeSymbolsMap[Type].Length; // The symbol string length of this connective

        private Connective(Types type) {

            Type = type;

        }

        // Get all available symbols
        public static List<string> AllSymbols { 
            get {
                return new List<string>(typeSymbolsMap.Values);
            }
        }

        // Creates a Connective given the symbol value
        public static Connective Create(string strValue) {

            foreach (KeyValuePair<Types, string> pair in typeSymbolsMap) {
                if (pair.Value == strValue) {
                    return new Connective(pair.Key);
                }
            }

            return null;

        }

        private static readonly Dictionary<Types, string> typeSymbolsMap = new Dictionary<Types, string>() {
            { Types.Negation, "~" },
            { Types.Conjunction, "&" },
            { Types.Disjunction, "|" },
            { Types.Implication, "=>" }
        };

    }

}
