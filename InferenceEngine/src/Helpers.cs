using System;
using System.Collections.Generic;
using System.Linq;

namespace InferenceEngine {

	static class Helpers {

		public static List<T> CloneList<T>(List<T> l) where T : class {
			return new List<T>(l);
		}

		public static string SymbolListString(List<Symbol> l) {
			string[] s = l.Select(x => x.Value).ToArray();
			return string.Join(", ", s);
        }

		public static string RemoveWhitespace(this string str) {
			return string.Join("", str.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
		}

		public static List<int> IndexOfAll(this string str, string value) {
			List<int> indexes = new List<int>();
			int index = str.IndexOf(value);
			while (index != -1) {
				indexes.Add(index);
				index = str.IndexOf(value, index + value.Length);
			}
			return indexes;
		}

	}

}
