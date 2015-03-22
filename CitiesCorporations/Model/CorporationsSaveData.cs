using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ICities;
using ColossalFramework.IO;
using CitiesCorporations.Utils;

namespace CitiesCorporations.Model
{
    public class CorporationsSaveData
    {
        private const uint SAVE_DATA_VERSION = 1;
        private const string MISSION_LIST_KEY = "CorporationsMissionList";
        
        public bool Loaded { get; private set; }

        public List<Mission> MissionList { get; set; }

        private readonly SerializationHelper m_serializationHelper;

        public static CorporationsSaveData CreateLoadData(ISerializableData serializableData)
        {
            CorporationsSaveData data = new CorporationsSaveData(serializableData);
            data.Load();
            return data;
        }

        public CorporationsSaveData(ISerializableData serializableData)
        {
            m_serializationHelper = new SerializationHelper(serializableData, SAVE_DATA_VERSION);
        }

        public void Save()
        {
            m_serializationHelper.SerializeArray<Mission>(MISSION_LIST_KEY, MissionList.ToArray());
        }

        public void Load()
        {
            MissionList = m_serializationHelper.DeserializeArray<Mission>(MISSION_LIST_KEY);
        }
    }
}
