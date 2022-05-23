using System;

namespace InferenceEngine {

    class Proposition {

        public string Symbol { get; protected set; }

        public static Proposition Create(string symbol) {

            if (CompoundProposition.IsCompoundProposition(symbol)) {
                return new CompoundProposition(symbol);
            }

            return new AtomicProposition(symbol);

        }

    }

}
