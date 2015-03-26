using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ColossalFramework.IO;
using ICities;

namespace CitiesCorporations.Utils
{
    public class SerializationHelper
    {
        public ISerializableData SerializableData { get; private set; }
        public uint SaveDataVersion { get; private set; }

        public SerializationHelper(ISerializableData serializableData, uint saveDataVersion)
        {
            SerializableData = serializableData;
            SaveDataVersion = saveDataVersion;
        }

        public void SerializeArray<T>(string storageKey, T[] array) where T : IDataContainer
        {
            LogHelper.LogFormat("arraysize {0}", array.Length); 
            LogHelper.LogFormat("SerializeArray"); 
            MemoryStream memoryStream = new MemoryStream();
            LogHelper.LogFormat("memoryStream"); 
            DataSerializer.SerializeArray<T>(memoryStream, DataSerializer.Mode.Memory, SaveDataVersion, array);
            LogHelper.LogFormat("SerializeArray"); 
            SerializableData.SaveData(storageKey, memoryStream.ToArray());
            LogHelper.LogFormat("SerializableData.SaveData"); 
        }

        public List<T> DeserializeArray<T>(string storageKey) where T : IDataContainer
        {
            return new List<T>(DataSerializer.DeserializeArray<T>(CreateLoadStream(storageKey), DataSerializer.Mode.Memory));
        }

        private Stream CreateLoadStream(string storageKey)
        {
            byte[] bytes = SerializableData.LoadData(storageKey);
            Stream stream = null;
            if (bytes != null)
            {
                stream = new MemoryStream(bytes);
            }

            return stream;
        }
    }
}
