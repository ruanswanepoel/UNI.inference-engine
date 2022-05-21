using System;
using System.Collections.Generic;
using InferenceEngine.KnowledgeBases.Propositions;

namespace InferenceEngine {

    class Program {

        static void Main(string[] args) {

            List<Proposition> propositions = new List<Proposition>();

            string clause = "p2=> p3; p3 => p1; c => e; b&e => f; f&g => h; p1=>d; p1&p3 => c; a; b; p2;";
            clause = clause.RemoveWhitespace();
            string[] props = clause.Split(';');

            foreach (string s in props) {
                propositions.Add(Proposition.Create(s));
            }

        }

    }

}
