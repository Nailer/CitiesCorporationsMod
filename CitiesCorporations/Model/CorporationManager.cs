using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CitiesCorporations.Model
{
    public class CorporationManager
    {
        private List<Corporation> m_corporations;
        private List<Mission> m_missions; 

        private const float MISSION_GENERATION_THRESHOLD = 60;
        private float _timeSinceLastMissionGenerated;

        internal void OnUpdate(float simulationTimeDelta)
        {
            _timeSinceLastMissionGenerated += simulationTimeDelta;
            if (_timeSinceLastMissionGenerated >= MISSION_GENERATION_THRESHOLD)
            {
                GenerateMission();
            }
        }

        private void GenerateMission()
        {
            
        }
    }
}
