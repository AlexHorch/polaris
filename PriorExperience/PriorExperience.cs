using Polaris_Charactergenerator.Skills;
using System.Collections.ObjectModel;

namespace Polaris_Charactergenerator.PriorExperiences
{
    public abstract class PriorExperience : NotifyPropertyChanges
    {
        private string priorExperienceName;
        private bool priorExperienceChoosen;
        private ObservableCollection<Skill> skillChanges;

        public PriorExperience(string name)
        {
            priorExperienceName = name;
        }

        public string PriorExperienceName
        {
            get => priorExperienceName;
            set
            {
                priorExperienceName = value;
                NotifyPropertyChanged(nameof(PriorExperienceName));
            }
        }
        public bool PriorExperienceChoosen
        {
            get => priorExperienceChoosen;
            set {; }
        }
        public ObservableCollection<Skill> SkillChanges
        {
            get => skillChanges;
            set
            {
                skillChanges = value;
                NotifyPropertyChanged(nameof(SkillChanges));
            }
        }
    }
}
