using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ColossalFramework.IO;

namespace CitiesCorporations.Model
{
    public class Mission : IDataContainer
    {
        public enum State
        {
            Pending,
            Accepted,
            Completed,
            Failed,
        }

        public Mission(MissionTemplate template, float createdTimestamp)
        {
            Template = template;
            CreatedTimestamp = createdTimestamp;
            MissionState = State.Pending;
        }

        public State MissionState { get; private set; }

        public float CreatedTimestamp { get; private set; }
   
        public MissionTemplate Template { get; private set; }

        public void Serialize(DataSerializer s)
        {
            throw new NotImplementedException();
        }

        public void Deserialize(DataSerializer s)
        {
            throw new NotImplementedException();
        }

        public void AfterDeserialize(DataSerializer s)
        {
            throw new NotImplementedException();
        }
    }
}
