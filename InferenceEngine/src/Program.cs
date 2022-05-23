using System;
using System.Collections.Generic;

namespace InferenceEngine {

    class Program {

        static void Main(string[] args) {

            string ask = "d";

            string line = "p2=> p3; p3 => p1; c => e; b&e => f; f&g => h; p1=>d; p1&p3 => c; a; b; p2";
            line = line.RemoveWhitespace();
            string[] clauses = line.Split(';');

            KnowledgeBase kb = new KnowledgeBase(clauses);

            bool a = InferenceEngine.PL_FC_Entails(kb, ask);

        }

    }

}
