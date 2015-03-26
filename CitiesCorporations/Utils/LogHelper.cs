using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICities;

namespace CitiesCorporations.Utils
{
    class LogHelper
    {
        public static void LogFormat(string msg, params object[] args)
        {
            CorporationsCore.Instance.Managers.threading.QueueMainThread(() => UnityEngine.Debug.LogFormat(msg, args));
        }

        public static void Log(string msg)
        {
            CorporationsCore.Instance.Managers.threading.QueueMainThread(() => UnityEngine.Debug.Log(msg));
        }

        public static void Log(object obj)
        {
            CorporationsCore.Instance.Managers.threading.QueueMainThread(() => UnityEngine.Debug.Log(obj));
        }
    }
}
