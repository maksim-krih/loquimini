using System;
using System.Collections.Generic;
using System.Linq;

namespace Loquimini.Common
{
    /// <summary>
    /// The state passed between the step function executions.
    /// </summary>
    public class State
    {
        public bool CalculateTimeLine { get; set; }

        /// <summary>
        /// The number of seconds to wait between calling the Salutations task and Greeting task.
        /// </summary>
        public int WaitInSeconds { get; set; } = 5;

        public StateError Error { get; set; }

        //TODO: FOR TESTING, REMOVE ON PRODUCTION
        public int CycleNumber { get; set; } = 0;
    }
}