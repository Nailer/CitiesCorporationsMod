using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICities;

namespace CitiesCorporations.Model.Missions
{
    public abstract class MissionObjectiveRule
    {
        public enum RuleEvaluationResult
        {
            Incomplete,
            Passed,
            Failed
        }

        public abstract RuleEvaluationResult EvaluateMissionObjectiveRule(MissionObjective objective, IManagers managers);
    }
}
