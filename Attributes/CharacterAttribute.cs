using System;
using System.Windows;

namespace Polaris_Charactergenerator.Attributes
{
    public class CharacterAttribute : NotifyPropertyChanges
    {
        private string attributeName;
        private int spentAttributePoints = 0;
        private int actualAttributeValue;
        private int attributeModifier = 0;
        private int specialModifier;
        public CharacterAttribute(string name)
        {
            attributeName = name;
        }
        public int StartingAttributeValue
        {
            get => 7;
        }
        public int SpentAttributePoints
        {
            get => spentAttributePoints;
            set
            {
                spentAttributePoints = value;
                NotifyPropertyChanged(nameof(SpentAttributePoints));
                NotifyPropertyChanged(nameof(ActualAttributeValue));
                NotifyPropertyChanged(nameof(NaturalAbility));
                NotifyCallback?.Invoke();
            }
        }
        public int NaturalAbility
        {
            get
            {
                int naturalAbility = -1;

                switch (ActualAttributeValue)
                {
                    case int n when (n < 4):
                        naturalAbility = -4;
                        break;
                    case int n when (n < 5):
                        naturalAbility = -3;
                        break;
                    case int n when (n < 6):
                        naturalAbility = -2;
                        break;
                    case int n when (n < 8):
                        naturalAbility = -1;
                        break;
                    case int n when (n < 10):
                        naturalAbility = 0;
                        break;
                    case int n when (n < 13):
                        naturalAbility = 1;
                        break;
                    case int n when (n < 16):
                        naturalAbility = 2;
                        break;
                    case int n when (n < 19):
                        naturalAbility = 3;
                        break;
                    case int n when (n < 22):
                        naturalAbility = 4;
                        break;
                    case int n when (n < 25):
                        naturalAbility = 5;
                        break;
                    case 25:
                        naturalAbility = 6;
                        break;
                }
                return naturalAbility;
            }
        }
        public int ActualAttributeValue
        {
            get
            {
                int sum = StartingAttributeValue + SpentAttributePoints;

                if (sum < 16)
                {
                    actualAttributeValue = sum;
                }
                else
                {
                    sum -= 15;
                    if (sum < 7)
                    {
                        actualAttributeValue = 15 + sum / 2;
                    }
                    else
                    {
                        sum -= 6;
                        if (18 + sum / 3.0 > 20)
                        {
                            MessageBox.Show("At this point, the maximum for an attribute is 20");
                        }
                        else
                        {
                            actualAttributeValue = 18 + sum / 3;
                        }
                    }
                }
                int actualSum = actualAttributeValue + attributeModifier;

                return Math.Max(actualSum, 3) + specialModifier;
            }
        }
        public int AttributeModifier
        {
            get => attributeModifier;
            set
            {
                attributeModifier = value;
                NotifyPropertyChanged(nameof(AttributeModifier));
                NotifyPropertyChanged(nameof(ActualAttributeValue));
                NotifyPropertyChanged(nameof(NaturalAbility));
            }
        }
        public int SpecialModifier
        {
            get => specialModifier;
            set
            {
                specialModifier = value;
                NotifyPropertyChanged(nameof(SpecialModifier));
                NotifyPropertyChanged(nameof(ActualAttributeValue));
                NotifyPropertyChanged(nameof(NaturalAbility));
            }
        }
    }
}
