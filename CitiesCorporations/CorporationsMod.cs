using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using CitiesCorporations.Model;
using CitiesCorporations.Utils;
using ColossalFramework.IO;
using ICities;

namespace CitiesCorporations
{
    public class CorporationsMod1 : ICities.IUserMod
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
        private ILoading m_loading;
        private bool m_loaded = false;
        void ILoadingExtension.OnCreated(ILoading loading)
        {
            m_loading = loading;
            if (loading.loadingComplete && !m_loaded)
            {
                CorporationsCore.Instance.Initiate(loading.managers);
            }
        }

        void ILoadingExtension.OnLevelLoaded(LoadMode mode)
        {
            if (m_loading.loadingComplete)
            {
                UnityEngine.Debug.Log("OnLoaded");
                CorporationsSaveData data = CorporationsSerializing.SerializingInstance.SaveData;
                if (data != null)
                {
                    UnityEngine.Debug.Log("data");
                    CorporationsCore.Instance.RestoreFromSaveData(data);
                }
            }           
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
        public static CorporationsSerializing SerializingInstance;
        public CorporationsSaveData SaveData { get; private set; }

        public void OnCreated(ISerializableData serializedData)
        {
            //UnityEngine.Debug.Log("CorporationsSerializing created");
            m_serializableData = serializedData;
            SerializingInstance = this;
        }

        public void OnReleased()
        {
            
        }

        public void OnLoadData()
        {
            //LogHelper.LogFormat("OnLoadData"); 
            
            //UnityEngine.Debug.Log("Test");
            //ChirpLog.Debug(String.Format("OnLoadData"));
            //LogHelper.Log("OnLoad");
            SaveData = CorporationsSaveData.CreateLoadData(m_serializableData);
            //LogHelper.Log("PostLoad");
            //ChirpLog.Debug(String.Format("Restored mission count {0}", saveData.MissionList.Count));
            //ChirpLog.Flush();
            //foreach (Mission mission in saveData.MissionList)
            //{
            //    ChirpLog.Debug(String.Format("Restored mission {0} {1}", mission.MissionId, mission.CreatedTimestamp));
            //}
            //CorporationsCore.Instance.RestoreFromSaveData(saveData);    
        }

        public void OnSaveData()
        {
            CorporationsSaveData saveData = CorporationsCore.Instance.CreateSaveData(m_serializableData);
            LogHelper.LogFormat("Created save data"); 
            saveData.Save();
            LogHelper.LogFormat("Saved"); 
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
            this.m_core.Initiate(threading.managers);
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
