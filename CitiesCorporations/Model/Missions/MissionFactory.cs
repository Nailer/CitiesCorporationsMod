using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CitiesCorporations.Utils;

namespace CitiesCorporations.Model.Missions
{
    public class MissionFactory
    {
        private uint m_missionId; //TODO: Initialize from mission list so that it doesn't overlap
        private uint GetNextMissionId()
        {
            return m_missionId++;
        }

        private uint m_missionObjectiveId;
        private uint GetNextMissionObjectiveId()
        {
            return m_missionObjectiveId++;
        }

        public MissionFactory(uint initialMissionId, uint initialObjectiveId)
        {
            m_missionId = initialMissionId;
            m_missionObjectiveId = initialObjectiveId;
        }

        public Mission InstantiateTemplate(MissionTemplate template, uint issuerId, float createdTimeStamp)
        {
            LogHelper.Log("InstantiateTemplate");
            List<MissionObjective> missionObjectives = 
                template.MissionObjectiveTempates.Select(objectiveTemplate => InstanciateObjectiveTemplate(objectiveTemplate)).ToList();

            Mission mission = new Mission(GetNextMissionId(), template.TemplateId, issuerId, createdTimeStamp, missionObjectives);
            return mission;
        }

        private MissionObjective InstanciateObjectiveTemplate(MissionObjectiveTemplate objectiveTemplate)
        {
            return new MissionObjective(GetNextMissionObjectiveId(), objectiveTemplate.TemplateId);
        }
    }
}
