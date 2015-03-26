using CitiesCorporations.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using ICities;
using UnityEngine.UI;
using UnityEngine;

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
        public GameObject CoreObject { get; private set; }
        //public GroogyLib.Core.Debugger DebuggerComponent { get; private set; }

        public ICities.IManagers Managers { get; private set; }

        public void Initiate(IManagers managers)
        {
            Managers = managers;
            Initiated = true;
            CorporationManager = new CorporationManager();
            MissionManager = new MissionManager(Managers);

            GameObject coreTemplate = new GameObject("CorporationCore");
            CoreObject = GameObject.Instantiate(coreTemplate);
            //DebuggerComponent = CoreObject.AddComponent<GroogyLib.Core.Debugger>();
            //string[] namespaces = { "CitiesCorporations" };
            //DebuggerComponent.Initiate(namespaces);
            //DebuggerComponent.SetupGUI(new Rect(Screen.width - 60, 65, 50, 50), "Debug", new Vector2(800, 600));
            //DebuggerComponent.OpenLog("CorporationCore.log");
            //DebuggerComponent.enabled = true;
        }

        public CorporationManager CorporationManager { get; private set; }
        public MissionManager MissionManager { get; private set; }

        public void OnUpdate(float realTimeDelta, float simulationTimeDelta)
        {
            return;

//            Managers.threading.QueueMainThread(() =>
//            {
////UnityEngine.Debug.LogFormat("simulationTimeDelta {0}", simulationTimeDelta);
//            });
            Managers.threading.QueueSimulationThread(() =>
            {
                CorporationManager.OnUpdate(simulationTimeDelta);
                MissionManager.OnUpdate(simulationTimeDelta);
            });
        }

        internal void RestoreFromSaveData(CorporationsSaveData saveData)
        {
            System.Diagnostics.Debug.Assert(saveData.Loaded);
            MissionManager.MissionList = saveData.MissionList;
        }

        public CorporationsSaveData CreateSaveData(ISerializableData serializableData)
        {
            CitiesCorporations.Utils.LogHelper.LogFormat("Saving {0}", (object)serializableData); 
            CorporationsSaveData saveData = new CorporationsSaveData(serializableData);
            saveData.MissionList = MissionManager.MissionList;
            return saveData;
        }
    }

}
