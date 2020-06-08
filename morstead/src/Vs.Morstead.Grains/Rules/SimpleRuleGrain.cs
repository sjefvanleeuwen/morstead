using FastMember;
using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vs.Morstead.Grains.Interfaces;

namespace Belastingdienst.Zorg.Toeslagen.Zorgtoeslag
{
    public class State
    {
        public double? a, f;
        public bool? b, c, d;
        public string woonland;
        public double? factor;
    }

    public class SimpleRuleGrain : Grain, IRule<State>
    {
        public static string hBron = "https://download.belastingdienst.nl/toeslagen/docs/berekening_zorgtoeslag_2019_tg0821z91fd.pdf";
        public static string hDomein = "zorg";
        public static string hPeriode = "2019";
        public static string hOnderwerp = "zorgtoeslag";
        public static string hOrganisatie = "belastingdienst";
        public static string hStatus = "ontwikkel";
        public static string hType = "toeslagen";
        public static string hVersie = "5.0";

        public State state = new State();
        private List<Question> _questions;
        private static TypeAccessor _accessor = TypeAccessor.Create(typeof(State));

        private static List<dynamic> woonlandfactoren = new List<dynamic>()
        {
            new {woonland="Nederland", factor=1.0 },
            new {woonland="België", factor=0.753 },
        };

        public async Task<Tuple<List<Question>,State>> Execute(Answer[] answers)
        {
            _questions = new List<Question>();
            if (answers != null)
            {
                for (int i=0;i<answers.Length;i++)
                {
                    _accessor[state, answers[i].Name] = answers[i].Value;
                }
            }
            // flow
            stap1();
            if (_questions.Any()) return new Tuple<List<Question>, State>(_questions, state);
            stap2();
            if (_questions.Any()) return new Tuple<List<Question>, State>(_questions, state);
            stap3a();
            if (_questions.Any()) return new Tuple<List<Question>, State>(_questions, state);
            stap3b();
            if (_questions.Any()) return new Tuple<List<Question>, State>(_questions, state);
            stap4();
            if (_questions.Any()) return new Tuple<List<Question>, State>(_questions, state);
            // end of flow
            return new Tuple<List<Question>, State>(_questions, state);
        }

        private void stap1()
        {
            if (state.Equals(null) || state.a == null)
                _questions.Add(new Question() { Name = "a" });
        }

        private void stap2()
        {
            // Choice
            if (state.d == null)
                _questions.Add(new Question() { Name = "d" });
            if (state.c==null)
                _questions.Add(new Question() { Name = "c" });
            if (state.b==null)
                _questions.Add(new Question() { Name = "b" });
            // choices are not pushed to a formula context.
        }

        private void stap3a()
        {
            if (!state.b.Value) return;
            //state.f = Evaluate("10*a");
            state.f = 10 * state.a;
        }

        private void stap3b()
        {
            if (!state.c.Value && !state.d.Value) return;
            // function f.
            state.f = 20 * state.a;
            //state.f = Evaluate("10*a");
        }

        private void stap4()
        {
            // formule woonlandfactor
            // woonland is a field, based on value it has a certain type (string) and should be generated in the state object.
            // lookup('woonlandfactoren', woonland, 'woonland', 'factor', 0)
            // factor should be generated in the state object.
            // transpiles to:
            if (state.woonland == null)
                _questions.Add(new Question() { Name = "woonland", Table = woonlandfactoren });
            if (state.factor == null)
                state.factor = woonlandfactoren.Find(p => p.woonland == state.woonland).factor;
        }
    }
}
