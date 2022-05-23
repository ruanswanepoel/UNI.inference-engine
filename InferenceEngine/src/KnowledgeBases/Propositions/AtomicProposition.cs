using System;

namespace InferenceEngine {

    /// <summary>
    /// A Proposition that is atomic.
    /// </summary>
    class AtomicProposition : Proposition {

        public string Value { get; private set; }

        public AtomicProposition(string symbol) {

            Symbol = symbol;
            Value = symbol;

        }

    }

}
