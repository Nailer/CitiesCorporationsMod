using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CitiesCorporations.Model
{
    public class CorporationDatabase
    {
        private static uint CURRENT_ID;
        public static readonly uint CORPORATION_ID_MANDRILL = CURRENT_ID++;
        public static readonly uint CORPORATION_ID_PETROIL = CURRENT_ID++;

        private static CorporationDatabase _instance;
        public CorporationDatabase Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CorporationDatabase();
                }

                return _instance;
            }
        }

        private List<Corporation> m_corporations; 

        public CorporationDatabase()
        {
            Initialize();
        }

        private void Initialize()
        {
            m_corporations = CreateCorporations();
        }

        private List<Corporation> CreateCorporations()
        {
            List<Corporation> corporations = new List<Corporation>()
            {
                new Corporation(),
            };

            return corporations;
        }

        Corporation GetCorporation(uint id)
        {
            return m_corporations.FirstOrDefault(corporation => corporation.Id == id);
        }
    }
}
