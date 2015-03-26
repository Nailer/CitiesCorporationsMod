using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using CitiesCorporations.Model.Missions;
using CitiesCorporations.Utils;
using ICities;

namespace CitiesCorporations.Model
{
    public class MissionManager
    {
        public List<Mission> MissionList { get; set; }

        private const float MISSION_GENERATION_THRESHOLD = 5;
        private const float MISSION_UPDATE_THRESHOLD = 60;
        private float m_timeSinceLastMissionGenerated;
        private float m_timeSinceLastUpdate;
        private readonly IManagers m_managers;
        private readonly MissionFactory m_missionFactory;

        public MissionManager(IManagers managers)
        {
            MissionList = new List<Mission>();
            m_managers = managers;
            m_missionFactory = new MissionFactory(0, 0);
        }

        internal void OnUpdate(float simulationTimeDelta)
        {
            m_timeSinceLastMissionGenerated += simulationTimeDelta;
            //LogHelper.LogFormat("simulationTimeDelta {0}", simulationTimeDelta);
            //LogHelper.LogFormat("m_timeSinceLastMissionGenerated {0}", m_timeSinceLastMissionGenerated);
            if (m_timeSinceLastMissionGenerated >= MISSION_GENERATION_THRESHOLD)
            {
                LogHelper.LogFormat("Generated", simulationTimeDelta);    
                MissionList.Add(GenerateMission());
                m_timeSinceLastMissionGenerated -= MISSION_GENERATION_THRESHOLD;
            }

            return;
            m_timeSinceLastUpdate += simulationTimeDelta;
            if (m_timeSinceLastUpdate >= MISSION_UPDATE_THRESHOLD)
            {
                int updateCount = (int)(m_timeSinceLastUpdate / MISSION_UPDATE_THRESHOLD);
                for (int i = 0; i < updateCount; ++i) //Maybe uneeded?
                {
                    UpdateMissionTick();    
                }
                
                m_timeSinceLastUpdate -= (MISSION_UPDATE_THRESHOLD * updateCount);
            }
        }

        public void StartMission(Mission mission)
        {
            mission.Start(TimeHelper.ToSystemTime(m_managers.threading));
        }

        private void UpdateMissionTick()
        {
            float simulationTime = TimeHelper.ToSystemTime(m_managers.threading);
            foreach (Mission mission in MissionList)
            {
                if (mission.MissionState == Mission.State.Started)
                {
                    MissionTemplate missionObjectiveTemplate =
                                MissionTemplateDatabase.Instance.GetTemplate(mission.TemplateId);
                    int objectiveCount = missionObjectiveTemplate.MissionObjectiveTempates.Count;
                    for (int i = 0; i < objectiveCount; ++i)
                    {
                        MissionObjectiveTemplate objectiveTemplate = missionObjectiveTemplate.MissionObjectiveTempates[i];
                        MissionObjective objective = mission.Objectives[i];
                        int passedCount = 0;
                        foreach (MissionObjectiveRule rule in objectiveTemplate.Rules)
                        {
                            MissionObjectiveRule.RuleEvaluationResult result = rule.EvaluateMissionObjectiveRule(objective, m_managers);
                            if (result == MissionObjectiveRule.RuleEvaluationResult.Failed)
                            {
                                objective.Fail(simulationTime);
                                break;
                            } 
                            else if (result == MissionObjectiveRule.RuleEvaluationResult.Passed)
                            {
                                passedCount++;
                            }
                        }

                        if (passedCount == objectiveTemplate.Rules.Count)
                        {
                            objective.Complete(simulationTime);
                        }
                    }
                }
            }
        }

        //TODO: Move mission generation somewhere else. Mission manager is only for updating mission statuses
        private Mission GenerateMission()
        {
            LogHelper.Log("Works");
            MissionTemplate template = MissionTemplateDatabase.Instance.GetTemplate(MissionTemplateDatabase.MISSION_ID_MANDRILL_TIMEOUT);
            LogHelper.Log("Works2");
            LogHelper.Log(m_missionFactory);
            LogHelper.LogFormat("Managers {0}", m_managers);
            if (m_managers.threading == null)
            {
                LogHelper.Log("LolNull");
            }
            float t = TimeHelper.ToSystemTime(m_managers.threading);
            LogHelper.LogFormat("Work123 {0}", t);
            Mission mission = m_missionFactory.InstantiateTemplate(template, 0, t);
            LogHelper.Log("Works3");
            return mission;
        }

        private void NotifyMissionStateChanged(Mission mission)
        {
            
        }
    }
}
