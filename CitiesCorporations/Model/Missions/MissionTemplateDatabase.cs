using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CitiesCorporations.Model.Missions;
using CitiesCorporations.Model.Missions.Rules;

namespace CitiesCorporations.Model
{
    class MissionTemplateDatabase
    {
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
            templates.Add(new MissionTemplate(GetNextId(), "Time is running out!", 
                new List<MissionObjectiveTemplate>
                {
                    new MissionObjectiveTemplate(GetNextObjectiveId(), "Do stuff before the time runs out.", 
                        new List<MissionObjectiveRule>
                    {
                        new TimeoutRule(300)
                    })
                })
            );

            return templates;
        }

        MissionTemplate GetTemplate(uint templateId)
        {
            MissionTemplate template = null;
            m_templates.TryGetValue(templateId, out template);
            return template;
        }

        private uint GetNextId()
        {
            return m_currentMissionId++;
        }

        private uint GetNextObjectiveId()
        {
            return m_currentObjectiveId++;
        }
    }
}
