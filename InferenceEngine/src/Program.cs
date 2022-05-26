using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace InferenceEngine {

    class Program {

        static readonly string usage_msg = "Usage:\n iengine {method} {filename}\n Eg: iengine FC ./test.txt\n\n" +
            "Possible methods:\n TT   Truth table method\n FC   Forward chain algorithm\n BC   Backward chain algorithm\n";
        static readonly string[] methods = { "TT", "FC", "BC" };

        static void Main(string[] args) {

            if (args.Length != 2) {
                Console.WriteLine("Wrong number of arguments.\n\n" + usage_msg);
                return;
            }

            string method = args[0].ToUpper();

            if (!methods.Contains(method) && method != "TEST") {
                Console.WriteLine(method + " is not a valid method.\n\n" + usage_msg);
                return;
            }

            if (!File.Exists(args[1])) {
                Console.WriteLine("Could not find the file " + args[1] + "\n\n" + usage_msg);
                return;
            }

            string[] lines = File.ReadAllLines(args[1]);
            string[] clauses = lines[1].RemoveWhitespace().Split(';').RemoveEmpty();
            Symbol ask = new Symbol(lines[3]);

            // Create knowledgebase
            KnowledgeBase kb = new KnowledgeBase(clauses);

            // Run inference engine method
            List<Symbol> result;

            switch (method) {
                case "TT":
                    bool res = InferenceEngine.TT_Entails(kb, ask);
                    throw new NotImplementedException("The truth table method is not implemented");
                case "FC":
                    result = InferenceEngine.PL_FC_Entails(kb, ask);
                    break;
                case "BC":
                    result = InferenceEngine.PL_BC_Entails(kb, ask);
                    break;
                default:
                    RunTest(kb);
                    return;
            }

            string output = (result == null) ? "NO" : "YES: " + Helpers.SymbolListString(result);
            Console.WriteLine(output + "\n");

        }

        static void RunTest(KnowledgeBase kb) {

            // Get list of all symbols
            List<Symbol> lst = kb.GetAllSymbols();

            foreach (string m in methods) {
                Console.WriteLine("--- Method: " + m + " ---");
                foreach (Symbol a in lst) {
                    List<Symbol> result;
                    switch (m) {
                        case "TT":
                            Console.WriteLine("The truth table method is not implemented");
                            continue;
                        case "FC":
                            result = InferenceEngine.PL_FC_Entails(kb, a);
                            break;
                        case "BC":
                            result = InferenceEngine.PL_BC_Entails(kb, a);
                            break;
                        default:
                            throw new Exception("Impossible");
                    }
                    string output = (result == null) ? "NO" : "YES: " + Helpers.SymbolListString(result);
                    Console.Write("ASK = " + a + " --> ");
                    Console.WriteLine(output);
                }
                Console.WriteLine();
            }

        }

    }

}
