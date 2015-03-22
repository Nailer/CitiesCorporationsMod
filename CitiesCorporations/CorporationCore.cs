using CitiesCorporations.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using ICities;
using UnityEngine.UI;

namespace CitiesCorporations
{
    public class CorporationsCore
    {
        static private CorporationsCore _sInstance = null;
        static public CorporationsCore Instance
        {
            get { return _sInstance ?? (_sInstance = new CorporationsCore()); }
        }

        static public void DestroyInstance()
        {
            _sInstance = null;
        }

        public bool Initiated { get; private set; }

        public ICities.IManagers Managers { get; set; }

        public void Initiate()
        {
            Initiated = true;
            CorporationManager = new CorporationManager();
        }

        public CorporationManager CorporationManager { get; private set; }

        public void OnUpdate(float realTimeDelta, float simulationTimeDelta)
        {
            CorporationManager.OnUpdate(simulationTimeDelta);
        }

        internal void RestoreFromSaveData(CorporationsSaveData saveData)
        {
            Debug.Assert(saveData.Loaded);
            CorporationManager.MissionList = saveData.MissionList;
        }

        public CorporationsSaveData CreateSaveData(ISerializableData serializableData)
        {
            CorporationsSaveData saveData = new CorporationsSaveData(serializableData);
            saveData.MissionList = CorporationManager.MissionList;
            return saveData;
        }
    }

}
