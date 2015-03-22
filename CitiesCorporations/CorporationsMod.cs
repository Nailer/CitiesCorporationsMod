using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using CitiesCorporations.Model;
using ColossalFramework.IO;
using ICities;

namespace CitiesCorporations
{
    public class CorporationsMod : ICities.IUserMod
    {
        public string Description
        {
            get { return "Corporations is a mod that incorporates corporations into Cities: Skylines"; }
        }

        public string Name
        {
            get { return "MegaCorps"; }
        }
    }

    public class CorporationsSetup : ILoadingExtension
    {
        void ILoadingExtension.OnCreated(ILoading loading)
        {
            
        }

        void ILoadingExtension.OnLevelLoaded(LoadMode mode)
        {

            CorporationsCore.Instance.Initiate();
        }

        void ILoadingExtension.OnLevelUnloading()
        {
            CorporationsCore.DestroyInstance();
        }

        void ILoadingExtension.OnReleased()
        {
        }
    }

    public class CorporationsSerializing : ISerializableDataExtension
    {
        private ISerializableData m_serializableData;

        public void OnCreated(ISerializableData serializedData)
        {
            m_serializableData = serializedData;
        }

        public void OnReleased()
        {
            
        }

        public void OnLoadData()
        {
            CorporationsSaveData saveData = new CorporationsSaveData(m_serializableData);
            CorporationsCore.Instance.RestoreFromSaveData(saveData);    
        }

        public void OnSaveData()
        {
            CorporationsSaveData saveData = CorporationsCore.Instance.CreateSaveData(m_serializableData);
            saveData.Save();
        }
    }

    public class CorporationsMainThread : IThreadingExtension
    {
        private IThreading m_threading = null;
        private CorporationsCore m_core = null;

        public void OnAfterSimulationFrame()
        {
        }

        public void OnAfterSimulationTick()
        {
        }

        public void OnBeforeSimulationFrame()
        {
        }

        public void OnBeforeSimulationTick()
        {
        }

        public void OnCreated(IThreading threading)
        {
            
            this.m_threading = threading;
            this.m_core = CorporationsCore.Instance;
            this.m_core.Managers = threading.managers;
        }

        public void OnReleased()
        {
            this.m_threading = null;
            this.m_core = null;
        }

        public void OnUpdate(float realTimeDelta, float simulationTimeDelta)
        {
            if (this.m_core.Initiated)
            {
                this.m_core.OnUpdate(realTimeDelta, simulationTimeDelta);
                //this.core.CitizenIssueDatabase.Update();
                //this.core.Milestones.Update();
            }
        }
    }
}
