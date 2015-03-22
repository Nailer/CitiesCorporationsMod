using CitiesCorporations.Model;
using System;
using System.Collections.Generic;
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
            this.Initiated = true;

            this.CorporationManager = new CorporationManager();
        }

        public CorporationManager CorporationManager { get; private set; }

        public void OnUpdate(float realTimeDelta, float simulationTimeDelta)
        {
            this.CorporationManager.OnUpdate(simulationTimeDelta);
        }

        internal void RestoreFromSaveData(CorporationsSaveData saveData)
        {
            throw new NotImplementedException();
        }

        public CorporationsSaveData CreateSaveData(ISerializableData serializableData)
        {
            throw new NotImplementedException();
        }
    }

}
