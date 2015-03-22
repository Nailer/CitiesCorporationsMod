using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ICities;
using ColossalFramework.IO;

namespace CitiesCorporations.Model
{
    
    public class CorporationsSaveData
    {
        private const string MISSION_LIST_KEY = "CorporationsMissionList";

        private List<Mission> m_missionList; 
        public List<Mission> MissionList
        {
            get
            {
                if (m_missionList == null)
                {
                    m_missionList = DeserializeArray<Mission>(MISSION_LIST_KEY);
                }

                return m_missionList;
            }
        }

        private ISerializableData m_serializableData;

        public CorporationsSaveData(ISerializableData serializableData)
        {
            m_serializableData = serializableData;
        }


        private List<T> DeserializeArray<T>(string storageKey)
        {
            return new List<T>(DataSerializer.DeserializeArray<T>(CreateLoadStream(storageKey), DataSerializer.Mode.Memory));
        }

        private Stream CreateLoadStream(string storageKey)
        {
            byte[] bytes = m_serializableData.LoadData(storageKey);
            Stream stream = null;
            if (bytes != null)
            {
                stream = new MemoryStream(bytes);
            }

            return stream;
        }

        public void Save()
        {
               
        }
    }
}
