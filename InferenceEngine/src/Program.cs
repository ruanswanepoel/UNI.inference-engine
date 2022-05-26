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

            List<string> lst = new List<string> {
                "a",
                "b",
                "c",
                "d",
                "e",
                "f",
                "g",
                "h",
                "p1",
                "p2",
                "p3"
            };

            foreach (string a in lst) {
                List<Symbol> result = InferenceEngine.PL_FC_Entails(kb, new Symbol(a));
                Console.Write("ASK(" + a + ")");
                if (result == null) {
                    Console.WriteLine("NO");
                }
                else {
                    Console.WriteLine("YES: " + Helpers.SymbolListString(result));
                }
            }

            Console.ReadKey();

        }

    }

}
