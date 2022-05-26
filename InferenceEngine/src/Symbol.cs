using System;

namespace InferenceEngine {

    /// <summary>
    /// Represents an atomic propositional logic symbol.
    /// </summary>
    class Symbol : IEquatable<Symbol> {

        public string Value { get; private set; }   // The string representation of the symbol

        public Symbol(string value) {

            Value = value;

        }

        /// <summary>
        /// Checks if two Symbols are equivalent. They are equivalent when the string values are equal.
        /// </summary>
        /// <param name="other">The other Symbol to compare with.</param>
        /// <returns>True if they are equal.</returns>
        public bool Equals(Symbol other) {

            return Value == other.Value;

        }
    }

}
