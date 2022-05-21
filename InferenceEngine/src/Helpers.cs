using System;
using System.Collections.Generic;

namespace InferenceEngine {
    
    static class Helpers {

        public static string RemoveWhitespace(this string str) {
            return string.Join("", str.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
        }

        public static List<T> CloneList<T>(List<T> l) where T : class {
            return new List<T>(l);
        }

    }

}
