namespace Polaris_Charactergenerator.Specials
{
    public abstract class Special : NotifyPropertyChanges
    {
        private string specialName;
        private int cpCost;
        private bool specialTaken;


        public Special(string name)
        {
            specialName = name;
        }

        public string SpecialName
        {
            get => specialName;
            set
            {
                specialName = value;
                NotifyPropertyChanged(nameof(SpecialName));
            }
        }
        public int CpCost
        {
            get => cpCost;
            set
            {
                cpCost = value;
                NotifyPropertyChanged(nameof(CpCost));
            }
        }
        public bool SpecialTaken
        {
            get => specialTaken;
            set
            {
                specialTaken = value;
                NotifyPropertyChanged(nameof(SpecialTaken));
            }
        }
    }
}
