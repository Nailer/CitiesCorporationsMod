using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CitiesCorporations.Model.Missions;
using CitiesCorporations.Model.Missions.Rules;
using CitiesCorporations.Utils;

namespace CitiesCorporations.Model
{
    class MissionTemplateDatabase
    {
        private static uint CURRENT_ID;
        public static readonly uint MISSION_ID_MANDRILL_TIMEOUT = CURRENT_ID++;
        private static readonly uint MISSION_ID_MANDRILL_TIMEOUT_OBJ1 = CURRENT_ID++;

        private static MissionTemplateDatabase _instance;

        public static MissionTemplateDatabase Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MissionTemplateDatabase();
                }

                return _instance;
            }
        }

        private uint m_currentMissionId;
        private uint m_currentObjectiveId;

        private Dictionary<uint, MissionTemplate> m_templates; 

        private MissionTemplateDatabase()
        {
            List<MissionTemplate> templates = CreateTemplates();
            m_templates = CreateTemplateMap(templates);
        }

        public MissionTemplate GetTemplate(uint templateId)
        {
            MissionTemplate template = null;
            m_templates.TryGetValue(templateId, out template);
            return template;
        }

        private Dictionary<uint, MissionTemplate> CreateTemplateMap(List<MissionTemplate> templates)
        {
            Dictionary<uint, MissionTemplate> dictionary = new Dictionary<uint, MissionTemplate>(templates.Count);
            foreach (MissionTemplate template in templates)
            {
                dictionary[template.TemplateId] = template;
            }

            return dictionary;
        }

        private List<MissionTemplate> CreateTemplates()
        {
            List<MissionTemplate> templates = new List<MissionTemplate>();
            templates.Add(new MissionTemplate(MISSION_ID_MANDRILL_TIMEOUT, "Time is running out!", 
                new List<MissionObjectiveTemplate>
                {
                    new MissionObjectiveTemplate(MISSION_ID_MANDRILL_TIMEOUT_OBJ1, "Do stuff before the time runs out.", 
                        new List<MissionObjectiveRule>
                    {
                        new TimeoutRule(300)
                    })
                })
            );

            LogHelper.Log(templates);
            return templates;

        }
    }
}
