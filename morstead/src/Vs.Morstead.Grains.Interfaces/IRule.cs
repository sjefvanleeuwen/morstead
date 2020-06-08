using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vs.Morstead.Grains.Interfaces
{
    /// <summary>
    /// Interface that all Rules must implement.
    /// The is a worker grain and does not persist state.
    /// The previouse answered questions must be retained at the client.
    /// Because of this the worker grain can scale-out as multiple grains instances.
    /// </summary>
    public interface IRule<TState>
    {
        /// <summary>
        /// Executes a rule. It returns a (set) of question(s).
        /// </summary>
        /// <returns></returns>
        public Task<Tuple<List<Question>, TState>> Execute(Answer[] answers);
    }

    public struct Answer
    {
        public string Name;
        public object Value;
    }

    public struct Question
    {
        public string Name;
        public List<dynamic> Table;
    }
}
