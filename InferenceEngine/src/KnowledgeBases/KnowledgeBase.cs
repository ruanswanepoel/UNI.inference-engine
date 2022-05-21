using System;
using System.Collections.Generic;
using InferenceEngine.KnowledgeBases.Propositions;

namespace InferenceEngine.KnowledgeBases {

	class KnowledgeBase {

		List<Proposition> propositions = new List<Proposition>();

		public void Create(string rawClause) {

			string clause = rawClause.RemoveWhitespace();
			string[] props = clause.Split(';');

			foreach (string s in props) {
				propositions.Add(Proposition.Create(s));
			}

		}

	}

}
