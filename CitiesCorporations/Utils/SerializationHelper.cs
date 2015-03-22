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
            MemoryStream memoryStream = new MemoryStream();
            DataSerializer.SerializeArray<T>(memoryStream, DataSerializer.Mode.Memory, SaveDataVersion, array);
            SerializableData.SaveData(storageKey, memoryStream.ToArray());
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
