using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICities;

namespace CitiesCorporations.Model.Missions.Rules
{
    class TimeoutRule : MissionObjectiveRule
    {
        private float m_timeoutDuration;

        public TimeoutRule(float timeoutDuration)
        {
            m_timeoutDuration = timeoutDuration;
        }

        public override bool EvaluateMissionObjectiveRule(MissionObjective objective, IManagers managers)
        {
            return ElapsedMissionObjectiveTime(objective, managers.threading.simulationTime) >= m_timeoutDuration;
        }

        public string CreateObjectiveStringFromObjective(MissionObjective objective, IManagers managers)
        {
            float elapsedTime = ElapsedMissionObjectiveTime(objective, managers.threading.simulationTime);
            float timeLeft = m_timeoutDuration - elapsedTime;
            return String.Format("(Time left: {1})", timeLeft);
        }

        private float ElapsedMissionObjectiveTime(MissionObjective objective, DateTime simulationTime)
        {
            return simulationTime.Ticks - objective.StartedTimestamp;
        }
    }
}
