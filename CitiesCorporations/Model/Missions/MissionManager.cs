using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CitiesCorporations.Model
{
    public class MissionManager
    {
        public List<Mission> MissionList { get; set; }

        private const float MISSION_GENERATION_THRESHOLD = 60;
        private float m_timeSinceLastMissionGenerated;

        internal void OnUpdate(float simulationTimeDelta)
        {
            m_timeSinceLastMissionGenerated += simulationTimeDelta;
            if (m_timeSinceLastMissionGenerated >= MISSION_GENERATION_THRESHOLD)
            {
                GenerateMission();
            }
        }

        private void GenerateMission()
        {

        }
    }
}
