using System.Collections.Generic;

namespace CitiesCorporations.Model.Missions
{
    public class MissionTemplate
    {
        public uint TemplateId { get; private set; }
        public string MissionText { get; private set; }
        public List<MissionObjectiveTemplate> MissionObjectiveTempates { get; private set; }

        public MissionTemplate(uint templateId, string missionText, List<MissionObjectiveTemplate> missionObjectiveTempates)
        {
            TemplateId = templateId;
            MissionText = missionText;
            MissionObjectiveTempates = missionObjectiveTempates;
        }
    }
}
