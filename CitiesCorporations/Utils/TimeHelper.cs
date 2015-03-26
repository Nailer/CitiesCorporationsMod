using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICities;

namespace CitiesCorporations.Utils
{
    class TimeHelper
    {
        static readonly DateTime _nullTime = new DateTime(1970, 1, 1);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="time"></param>
        /// <returns>number of seconds since 00:00 jan 1. 1970 </returns>
        public static float ToSystemTime(DateTime time)
        {
            return (float)(time - _nullTime).TotalSeconds;
        }

        public static float ToSystemTime(IThreading threading)
        {
            LogHelper.Log("Called thingy");
            if (threading == null)
            {
                LogHelper.Log("Threading is nil");
            }

            LogHelper.Log("Threading is not null");
            
            return (float)(threading.simulationTime - _nullTime).TotalSeconds;
        }

    }
}
