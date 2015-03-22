using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CitiesCorporations.Model.Missions
{
    public class MissionObjectiveTemplate
    {
        public uint TemplateId { get; private set; }
        public string ObjectiveText { get; private set; }
        public List<MissionObjectiveRule> Rules { get; private set; }

        // The mission objectives that have to be completed for this objective to show
        public List<uint> DependencyList { get; private set; }

        public MissionObjectiveTemplate(uint templateId, string objectiveText, List<MissionObjectiveRule> rules, List<uint> dependencyList)
        {
            TemplateId = templateId;
            ObjectiveText = objectiveText;
            Rules = rules;
            DependencyList = dependencyList;
        }

        public MissionObjectiveTemplate(uint templateId, string objectiveText, List<MissionObjectiveRule> rules)
            : this(templateId, objectiveText, rules, null)
        {
 
        }
    }
}
