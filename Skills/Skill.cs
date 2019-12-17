using Polaris_Charactergenerator.Attributes;

namespace Polaris_Charactergenerator.Skills
{
    public class Skill : NotifyPropertyChanges
    {
        private string skillName;
        private int skillModifier;
        private int spendSkillPoints;
        private CharacterAttribute[] associatedAttributes = new CharacterAttribute[2];
        private int priorExperienceSkillValue;

        public Skill(string name)
        {
            skillName = name;
        }

        public int GlobalSkillValue
        {
            get => BaseSkillValue + MasteryValue;
        }
        public int MasteryValue
        {
            get => SkillModifier + SpendSkillPoints;
        }
        public int SkillModifier
        {
            get => skillModifier;
            set
            {
                skillModifier = value;
                NotifyPropertyChanged(nameof(SkillModifier));
                NotifyPropertyChanged(nameof(MasteryValue));
                NotifyPropertyChanged(nameof(GlobalSkillValue));
            }
        }
        public int SpendSkillPoints
        {
            get => spendSkillPoints;
            set
            {
                spendSkillPoints = value;
                NotifyPropertyChanged(nameof(SpendSkillPoints));
                NotifyPropertyChanged(nameof(MasteryValue));
                NotifyPropertyChanged(nameof(GlobalSkillValue));
            }
        }
        public int BaseSkillValue
        {
            get => associatedAttributes[0].NaturalAbility + associatedAttributes[1].NaturalAbility + PriorExperienceSkillValue;
        }
        public CharacterAttribute[] AssociatedAttributes
        {
            get => associatedAttributes;
            set
            {
                associatedAttributes = value;
                NotifyPropertyChanged(nameof(AssociatedAttributes));
                NotifyPropertyChanged(nameof(GlobalSkillValue));
            }
        }
        public int PriorExperienceSkillValue
        {
            get => priorExperienceSkillValue;
            set
            {
                priorExperienceSkillValue = value;
                NotifyPropertyChanged(nameof(PriorExperienceSkillValue));
                NotifyPropertyChanged(nameof(BaseSkillValue));
                NotifyPropertyChanged(nameof(GlobalSkillValue));
            }
        }
    }
}
