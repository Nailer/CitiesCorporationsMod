using System;
using System.Collections.Generic;
using System.Diagnostics;
using ColossalFramework.IO;

namespace CitiesCorporations.Model.Missions
{
    public class MissionObjective : IDataContainer
    {
        private const float TIME_NOT_SET = -1;

        public enum State
        {
            Pending,
            Started,
            Completed,
            Failed,
        }

        public MissionObjective(uint objectiveId, uint templateId)
        {
            ObjectiveId = objectiveId;
            TemplateId = templateId;
            StartedTimestamp = TIME_NOT_SET;
            FailedTimestamp = TIME_NOT_SET;
            CompletedTimestamp = TIME_NOT_SET;
        }

        public MissionObjective()
        {
            
        }

        public State ObjectiveState
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

        public uint ObjectiveId { get; private set; }
        public uint TemplateId { get; private set; }
        public float StartedTimestamp { get; private set; }
        public float FailedTimestamp { get; private set; }
        public float CompletedTimestamp { get; private set; }

        public void Start(float simulationTimeStamp)
        {
            Debug.Assert(StartedTimestamp == TIME_NOT_SET, "Objective must not be started when calling this method");
            StartedTimestamp = simulationTimeStamp;
        }

        public void Complete(float simulationTimeStamp)
        {
            Debug.Assert(StartedTimestamp != TIME_NOT_SET, "Objective must be started when calling this method");
            CompletedTimestamp = simulationTimeStamp;
        }

        public void Fail(float simulationTimeStamp)
        {
            Debug.Assert(StartedTimestamp != TIME_NOT_SET, "Objective must be started when calling this method");
            Debug.Assert(CompletedTimestamp == TIME_NOT_SET, "Objective must not be completed when calling this method");
            FailedTimestamp = simulationTimeStamp;
        }

        public void Serialize(DataSerializer s)
        {
            s.WriteUInt16(ObjectiveId);
            s.WriteUInt16(TemplateId);
            s.WriteFloat(StartedTimestamp);
            s.WriteFloat(FailedTimestamp);
            s.WriteFloat(CompletedTimestamp);
        }

        public void Deserialize(DataSerializer s)
        {
            ObjectiveId = s.ReadUInt16();
            TemplateId = s.ReadUInt16();
            StartedTimestamp = s.ReadFloat();
            FailedTimestamp = s.ReadFloat();
            CompletedTimestamp = s.ReadFloat();
        }

        public void AfterDeserialize(DataSerializer s)
        {
            
        }
    }
}
