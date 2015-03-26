using System;
using System.Collections.Generic;
using System.Diagnostics;
using CitiesCorporations.Model.Missions;
using CitiesCorporations.Utils;
using ColossalFramework.IO;

namespace CitiesCorporations.Model
{
    public class Mission : IDataContainer
    {
        private const float TIME_NOT_SET = -1;
        public enum State
        {
            Pending,
            Started,
            Completed,
            Failed,
        }

        public Mission(uint missionId, uint templateId, uint issuerId, float createdTimestamp, List<MissionObjective> objectives)
        {
            MissionId = missionId;
            TemplateId = templateId;
            IssuerId = issuerId;
            CreatedTimestamp = createdTimestamp;
            StartedTimestamp = TIME_NOT_SET;
            FailedTimestamp = TIME_NOT_SET;
            CompletedTimestamp = TIME_NOT_SET;
            Objectives = objectives;
        }

        public State MissionState
        {
            get
            {
                if (CompletedTimestamp != TIME_NOT_SET)
                {
                    return State.Completed;
                }
                else if (FailedTimestamp != TIME_NOT_SET)
                {
                    return State.Failed;
                }
                else if (StartedTimestamp != TIME_NOT_SET)
                {
                    return State.Started;
                }
                else
                {
                    return State.Pending;
                }
            }
        }

        public uint MissionId { get; private set; }
        public uint TemplateId { get; private set; }
        public uint IssuerId { get; private set; }
        public float CreatedTimestamp { get; private set; }
        public float StartedTimestamp { get; private set; }
        public float FailedTimestamp { get; private set; }
        public float CompletedTimestamp { get; private set; }
        public List<MissionObjective> Objectives { get; private set; }

        public void Start(float simulationTimeStamp)
        {
            Debug.Assert(StartedTimestamp == TIME_NOT_SET, "Mission must not be started when calling this method");
            StartedTimestamp = simulationTimeStamp;
        }

        public void Complete(float simulationTimeStamp)
        {
            Debug.Assert(StartedTimestamp != TIME_NOT_SET, "Mission must be started when calling this method");
            CompletedTimestamp = simulationTimeStamp;
        }

        public void Fail(float simulationTimeStamp)
        {
            Debug.Assert(StartedTimestamp != TIME_NOT_SET, "Mission must be started when calling this method");
            Debug.Assert(CompletedTimestamp == TIME_NOT_SET, "Mission must not be completed when calling this method");
            FailedTimestamp = simulationTimeStamp;
        }

        public void Serialize(DataSerializer s)
        {
            s.WriteUInt16(MissionId);
            s.WriteUInt16(TemplateId);
            s.WriteUInt16(IssuerId);
            s.WriteFloat(CreatedTimestamp);
            s.WriteFloat(StartedTimestamp);
            s.WriteFloat(FailedTimestamp);
            s.WriteFloat(CompletedTimestamp);
            SerializeObjectives(s);
        }

        public void Deserialize(DataSerializer s)
        {
            MissionId = s.ReadUInt16();
            TemplateId = s.ReadUInt16();
            IssuerId = s.ReadUInt16();
            CreatedTimestamp = s.ReadFloat();
            StartedTimestamp = s.ReadFloat();
            FailedTimestamp = s.ReadFloat();
            CompletedTimestamp = s.ReadFloat();
            Objectives = DeserializeObjectives(s);
        }

        private void SerializeObjectives(DataSerializer s)
        {
            if (s != null)
            {
                LogHelper.LogFormat("DataSerializer is a thing"); 
            }
            LogHelper.LogFormat("SerializeObjectives"); 
            s.WriteInt16(Objectives.Count);
            foreach (MissionObjective objective in Objectives)
            {
                objective.Serialize(s);
            }
        }

        private List<MissionObjective> DeserializeObjectives(DataSerializer s)
        {
            int objectiveCount = s.ReadInt16();
            List<MissionObjective> objectives = new List<MissionObjective>(objectiveCount);
            for (int i = 0; i < objectiveCount; ++i)
            {
                MissionObjective objective = new MissionObjective();
                objective.Deserialize(s);
            }

            return objectives;
        }

        public void AfterDeserialize(DataSerializer s)
        {
            
        }
    }
}
