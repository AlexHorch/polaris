using Polaris_Charactergenerator.Attributes;
using Polaris_Charactergenerator.PriorExperiences;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;

namespace Polaris_Charactergenerator
{
    public class Character : NotifyPropertyChanges
    {
        #region Tab1_Variables
        private handedness handedness;
        private bool fertility;
        #endregion

        #region Tab2_Variables
        private int cpToAttributepoints;
        private int genotype = 0;
        private bool deserter;
        #endregion

        #region Tab3_Variables
        private int naturalResistanceFire;
        private int naturalResistanceCold;
        private int naturalResistanceDrugs;
        private int naturalResistanceDiseases;
        private int naturalResistancePoison;
        private int naturalResistanceRadiation;
        private int parasites;
        private int reinforcedSkeleton;
        private int symbionts;
        private int tailCost = 1;
        private int lightDeformity;
        private int severeDeformity;
        private int clawsCost = 1;
        private int fangsCost = 1;
        private int nightVisionCost = 2;
        private bool purulence;
        private bool felineGeneticTraits;
        private bool canineGeneticTraits;
        private bool reptilianGeneticTraits;
        private bool simianGeneticTraits;
        private bool polarisEffectInitialPower;
        private bool polarisEffectFirstPower;
        private bool polarisEffectSecondPower;
        private bool polarisEffectThridPower;
        #endregion

        #region Tab4_Variables
        private string geneticType;
        private int geographicOrigin = 4;
        private int socialOrigin = 4;
        private int initialTraining = 6;
        #endregion

        #region Tab5_Variables
        #endregion

        #region Tab6_Variables
        private int yearsAsAssassin;
        private int yearsAsBartender;
        private int yearsAsBountyHunter;
        private int yearsAsCraftsmanOrArtist;
        private int yearsAsDiplomat;
        private int yearsAsDocterOrSurgeon;
        private int yearsAsEliteSoldier;
        private int yearsAsFarmerOrLivestockFarmer;
        private int yearsAsFighterPilot;
        private int yearsAsMercenary;
        private int yearsAsMilitaryOfficer;
        private int yearsAsMiner;
        private int yearsAsNavalOfficerOrNavigator;
        private int yearsAsPirate;
        private int yearsAsPoliceOfficerOrInvestigator;
        private int yearsAsPriestOfTheTrident;
        private int yearsAsScholarOrArchaelogist;
        private int yearsAsScientistOrEngineer;
        private int yearsAsSmuggler;
        private int yearsAsSoldierOrMilitiaman;
        private int yearsAsSpy;
        private int yearsAsSubmariner;
        private int yearsAsTechnicanOrMechanic;
        private int yearsAsTechnoHybrid;
        private int yearsAsThiefOrCriminal;
        private int yearsAsTrader;
        private int yearsAsTravellingTraderOrStoryteller;
        private int yearsAsTridentHybrid;
        private int yearsAsWatcher;
        private int yearsAsWorkerOrLongshoreman;
        #endregion

        public Character()
        {
            Strength.NotifyCallback = NotifyCallbackHandler;
            Constitution.NotifyCallback = NotifyCallbackHandler;
            Coordination.NotifyCallback = NotifyCallbackHandler;
            Adaptation.NotifyCallback = NotifyCallbackHandler;
            Perception.NotifyCallback = NotifyCallbackHandler;
            Intelligence.NotifyCallback = NotifyCallbackHandler;
            Willpower.NotifyCallback = NotifyCallbackHandler;
            Presence.NotifyCallback = NotifyCallbackHandler;
        }
        public void NotifyCallbackHandler()
        {
            NotifyPropertyChanged(nameof(AttributePointsToSpend));
            NotifyPropertyChanged(nameof(StunThreshold));
            NotifyPropertyChanged(nameof(KnockoutThreshold));
            NotifyPropertyChanged(nameof(MeleeDamageModifier));
            NotifyPropertyChanged(nameof(Reaction));
            NotifyPropertyChanged(nameof(DamageResistance));
            NotifyPropertyChanged(nameof(NaturalResistance));
            NotifyPropertyChanged(nameof(NaturalResistance_Poison));
            NotifyPropertyChanged(nameof(NaturalResistance_Disease));
            NotifyPropertyChanged(nameof(NaturalResistance_Radiation));
            NotifyPropertyChanged(nameof(NaturalResistance_Drugs));
            NotifyPropertyChanged(nameof(Apnea));
        }
        public string ActualCP
        {
            get
            {
                // TODO ist es in C# okay, Logik in getter zu packen
                int cp = 20 - CpToAttributepoints - (genotype == 0 ? 0 : (Deserter ? 4 : 5))
                    - ComputeChoosenSpecialsCost() - SumOfYearsInProfessions();
                //- Kosten von Retractable Tentacle (2 oder 1)

                return $"Current: {cp} CP";
            }
        }
        private int ComputeChoosenSpecialsCost()
        {
            int sum = 0;
            // TODO refactoren -> zu viel Logik an einer Stelle

            sum += (Claws ? ClawsCost : 0) + (Fangs ? FangsCost : 0) + (NightVision ? NightVisionCost : 0) + Tail * TailCost;

            sum += (EnhancedSmell ? 1 : 0) + (EnhancedTouch ? 1 : 0) + (Horn ? 1 : 0) + (OutdoorAdaptation ? 1 : 0) + (SixthSense ? 1 : 0) + (PolarisEffectFirstPower ? 1 : 0) + (PolarisEffectSecondPower ? 1 : 0) + (PolarisEffectThridPower ? 1 : 0)
                + (Amphibious ? 2 : 0) + (FelineGeneticTraits ? 2 : 0) + (CanineGeneticTraits ? 2 : 0) + (ReptilianGeneticTraits ? 2 : 0) + (SimianGeneticTraits ? 2 : 0) + (Purulence ? 2 : 0) + (RetractableBoneGrowth ? 2 : 0)
                + (Contagion ? 3 : 0) + (Empathy ? 3 : 0) + (Sonar ? 3 : 0)
                + (MolecularInstability ? 4 : 0) + (ShapeShifter ? 4 : 0)
                + (PolarisEffectInitialPower ? 5 : 0);

            sum += NaturalResistanceBoni_Fire + NaturalResistanceBoni_Cold + NaturalResistanceBoni_Drugs + NaturalResistanceBoni_Diseases + NaturalResistanceBoni_Poison + NaturalResistanceBoni_Radiation + Parasites + RetractableTentacle
                + AdditionalEar * 2 + AdditionalEye * 2 + CorrosiveTouch * 2 + Regeneration * 2 + ReinforcedSkeleton * 2 + ReinforcedSkin * 2
                + Radiation * 3;

            sum -= (AtrophiedNose ? 1 : 0) - (AtrophiedTouch ? 1 : 0);

            sum -= LightDeformity
                - MissingEar * 2
                - SevereDeformity * 3 - MissingEye * 3;

            return sum;
        }
        private int SumOfYearsInProfessions()
        {
            int sum = YearsAsAssassin + YearsAsBartender + YearsAsBountyHunter + YearsAsCraftsmanOrArtist + YearsAsDiplomat + YearsAsDocterOrSurgeon + YearsAsEliteSoldier
                + YearsAsFarmerOrLivestockFarmer + YearsAsFighterPilot + YearsAsMercenary + YearsAsMilitaryOfficer + YearsAsMiner + YearsAsNavalOfficerOrNavigator + YearsAsPirate
                + YearsAsPoliceOfficerOrInvestigator + YearsAsPriestOfTheTrident + YearsAsScholarOrArchaelogist + YearsAsScientistOrEngineer + YearsAsSmuggler + YearsAsSoldierOrMilitiaman
                + YearsAsSpy + YearsAsSubmariner + YearsAsTechnicanOrMechanic + YearsAsTechnoHybrid + YearsAsThiefOrCriminal + YearsAsTrader + YearsAsTravellingTraderOrStoryteller
                + YearsAsTridentHybrid + YearsAsWatcher + YearsAsWorkerOrLongshoreman;
            return sum;
        }


        #region Tab1
        public int Age
        {
            get => 17 + SumOfYearsInProfessions();
        }
        public bool Fertility
        {
            get
            {
                if (Sexless)
                {
                    //Disable Fertility-Disadvantage
                    return false;
                }
                return fertility;
            }
            set
            {
                fertility = value;
                NotifyPropertyChanged(nameof(Fertility));
            }
        }
        public handedness Handedness
        {
            get => handedness;
            set
            {
                handedness = value;
                NotifyPropertyChanged(nameof(Handedness));
            }
        }
        public CharacterAttribute Strength { get; } = new CharacterAttribute(nameof(Strength));
        public CharacterAttribute Constitution { get; } = new CharacterAttribute(nameof(Constitution));
        public CharacterAttribute Coordination { get; } = new CharacterAttribute(nameof(Coordination));
        public CharacterAttribute Adaptation { get; } = new CharacterAttribute(nameof(Adaptation));
        public CharacterAttribute Perception { get; } = new CharacterAttribute(nameof(Perception));
        public CharacterAttribute Intelligence { get; } = new CharacterAttribute(nameof(Intelligence));
        public CharacterAttribute Willpower { get; } = new CharacterAttribute(nameof(Willpower));
        public CharacterAttribute Presence { get; } = new CharacterAttribute(nameof(Presence));
        public int Luck
        {
            get => 13;
            set {; }
        }
        public int StunThreshold
        {
            get
            {
                int specialAbilityBonus = 0;
                if (reinforcedSkeleton > 0)
                {
                    specialAbilityBonus = 3;
                    if (reinforcedSkeleton > 1)
                    {
                        specialAbilityBonus += reinforcedSkeleton - 1;
                    }
                }
                return (Strength.ActualAttributeValue + Constitution.ActualAttributeValue + Willpower.ActualAttributeValue) / 3 + specialAbilityBonus;
            }
        }
        public int KnockoutThreshold
        {
            get
            {
                return StunThreshold + 10;
            }
        }
        public int MeleeDamageModifier
        {
            get
            {
                switch (Strength.ActualAttributeValue)
                {
                    case int n when (n < 3):
                        return -6;
                    case int n when (n < 5):
                        return -4;
                    case int n when (n < 7):
                        return -2;
                    case int n when (n < 9):
                        return -1;
                    case int n when (n < 12):
                        return 0;
                    case int n when (n < 14):
                        return 1;
                    case int n when (n < 16):
                        return 2;
                    case int n when (n < 18):
                        return 3;
                    case int n when (n < 20):
                        return 4;
                    case int n when (n < 22):
                        return 5;
                    case int n when (n > 21):
                        return 5 + (Strength.ActualAttributeValue - 21) / 2;
                    default:
                        return 0;
                }
            }
        }
        public int Reaction
        {
            get
            {
                return (Adaptation.ActualAttributeValue + Perception.ActualAttributeValue) / 2;
            }
        }
        public int DamageResistance
        {
            get
            {
                int damageResistance = Strength.ActualAttributeValue + Constitution.ActualAttributeValue;
                int resistance;

                switch (damageResistance)
                {
                    case int n when (n < 6):
                        resistance = 6;
                        break;
                    case int n when (n < 10):
                        resistance = 4;
                        break;
                    case int n when (n < 14):
                        resistance = 2;
                        break;
                    case int n when (n < 18):
                        resistance = 1;
                        break;
                    case int n when (n < 22):
                        resistance = 0;
                        break;
                    case int n when (n < 26):
                        resistance = -1;
                        break;
                    case int n when (n < 30):
                        resistance = -2;
                        break;
                    case int n when (n < 34):
                        resistance = -3;
                        break;
                    case int n when (n < 38):
                        resistance = -4;
                        break;
                    case int n when (n < 42):
                        resistance = -5;
                        break;
                    case int n when (n > 41):
                        resistance = -5 - (damageResistance - 41) / 4;
                        break;
                    default:
                        resistance = 0;
                        break;
                }
                if (Parasites > 1)
                {
                    resistance++;
                    if (Parasites > 3)
                    {
                        resistance++;
                    }
                }
                if (reinforcedSkeleton > 0)
                {
                    resistance -= 2;
                    if (reinforcedSkeleton > 1)
                    {
                        resistance -= reinforcedSkeleton - 1;
                    }
                }
                return resistance;
            }
        }
        public int NaturalResistance_Poison
        {
            get => NaturalResistance() + ComputeNaturalResistenceBoni(naturalResistancePoison);
        }
        public int NaturalResistance_Disease
        {
            get => NaturalResistance() + ComputeNaturalResistenceBoni(naturalResistanceDiseases) + (Purulence ? 3 : 0);
        }
        public int NaturalResistance_Radiation
        {
            get => NaturalResistance() + ComputeNaturalResistenceBoni(naturalResistanceRadiation);
        }
        public int NaturalResistance_Fire
        {
            get => 0 + ComputeNaturalResistenceBoni(naturalResistanceFire);
        }
        public int NaturalResistance_Cold
        {
            get => 0 + ComputeNaturalResistenceBoni(naturalResistanceCold);
        }
        public int NaturalResistance_Drugs
        {
            get
            {
                int naturalDrugResistance = (Constitution.ActualAttributeValue + Willpower.ActualAttributeValue) / 2;
                int sum;

                switch (naturalDrugResistance)
                {
                    case int n when (n < 3):
                        sum = 6;
                        break;
                    case int n when (n < 5):
                        sum = 4;
                        break;
                    case int n when (n < 7):
                        sum = 2;
                        break;
                    case int n when (n < 9):
                        sum = 1;
                        break;
                    case int n when (n < 12):
                        sum = 0;
                        break;
                    case int n when (n < 14):
                        sum = -1;
                        break;
                    case int n when (n < 16):
                        sum = -2;
                        break;
                    case int n when (n < 18):
                        sum = -3;
                        break;
                    case int n when (n < 20):
                        sum = -4;
                        break;
                    case int n when (n < 22):
                        sum = -5;
                        break;
                    case int n when (n > 21):
                        sum = -5 - (naturalDrugResistance - 21) / 2;
                        break;
                    default:
                        sum = 0;
                        break;
                }
                return sum + ComputeNaturalResistenceBoni(naturalResistanceDrugs);
            }
        }
        public int Apnea
        {
            get
            {
                return (Constitution.ActualAttributeValue + Willpower.ActualAttributeValue) / 2;
            }
        }
        public int NaturalResistance()
        {
            switch (Constitution.ActualAttributeValue)
            {
                case int n when (n < 3):
                    return 6;
                case int n when (n < 5):
                    return 4;
                case int n when (n < 7):
                    return 2;
                case int n when (n < 9):
                    return 1;
                case int n when (n < 12):
                    return 0;
                case int n when (n < 14):
                    return -1;
                case int n when (n < 16):
                    return -2;
                case int n when (n < 18):
                    return -3;
                case int n when (n < 20):
                    return -4;
                case int n when (n < 22):
                    return -5;
                case int n when (n > 21):
                    return -5 - (Constitution.ActualAttributeValue - 21) / 2;
                default:
                    return 0;
            }
        }
        public int ComputeNaturalResistenceBoni(int param)
        {
            if (param < 2)
            {
                return param * -3;
            }
            return param * -1 - 2;
        }
        #endregion

        #region Tab2
        public bool NormalHuman
        {
            get => genotype == 0;
            set
            {
                genotype = 0;
                NotifyGeneticTypes();
            }
        }
        public bool NaturalHybrid
        {
            get => genotype == 1;
            set
            {
                genotype = 1;
                NotifyGeneticTypes();
            }
        }
        public bool GenoHybrid
        {
            get => genotype == 2;
            set
            {
                genotype = 2;
                NotifyGeneticTypes();
            }
        }
        public bool TechnoHybrid
        {
            get => genotype == 3;
            set
            {
                genotype = 3;
                NotifyGeneticTypes();
            }
        }
        public bool Deserter
        {
            get
            {
                if (genotype == 3)
                {
                    return deserter;
                }
                return false;
            }
            set
            {
                deserter = value;
                NotifyPropertyChanged(nameof(Deserter));
                NotifyPropertyChanged(nameof(ActualCP));
            }
        }
        public string GeneticType
        {
            get
            {
                if (NaturalHybrid)
                {
                    geneticType = "Natural Hybrid";
                    SetAttributeModifierToDefault();
                    Strength.AttributeModifier = 1;
                    Constitution.AttributeModifier = 2;
                    Coordination.AttributeModifier = 2;
                    Adaptation.AttributeModifier = 1;
                    Intelligence.AttributeModifier = -2;
                }
                else if (GenoHybrid)
                {
                    geneticType = "Geno-Hybrid";
                    SetAttributeModifierToDefault();
                    Strength.AttributeModifier = 1;
                    Constitution.AttributeModifier = 1;
                    Coordination.AttributeModifier = 2;
                    Presence.AttributeModifier = -2;
                }
                else if (TechnoHybrid)
                {
                    geneticType = "Techno-Hybrid";
                    SetAttributeModifierToDefault();
                    Strength.AttributeModifier = 2;
                    Constitution.AttributeModifier = 3;
                    Adaptation.AttributeModifier = -2;
                    Willpower.AttributeModifier = 3;
                    Presence.AttributeModifier = -6;
                }
                else
                {
                    geneticType = "Normal Human";
                    SetAttributeModifierToDefault();
                }
                return geneticType;
            }
            set
            {
                geneticType = value;
                NotifyPropertyChanged(nameof(GeneticType));
            }
        }
        public int AttributePointsToSpend
        {
            get
            {
                int attributePointsToSpend = 38
                    + CpToAttributepoints * 2
                    - Strength.SpentAttributePoints
                    - Constitution.SpentAttributePoints
                    - Coordination.SpentAttributePoints
                    - Adaptation.SpentAttributePoints
                    - Perception.SpentAttributePoints
                    - Intelligence.SpentAttributePoints
                    - Willpower.SpentAttributePoints
                    - Presence.SpentAttributePoints;
                if (attributePointsToSpend < 0)
                {
                    MessageBox.Show("You don't have enough Attributepoints to spend!\nPlease purchase more attributepoints with CP!");
                }

                return attributePointsToSpend;
            }
        }
        public int CpToAttributepoints
        {
            get => cpToAttributepoints;
            set
            {
                cpToAttributepoints = value;
                NotifyPropertyChanged(nameof(CpToAttributepoints));
                NotifyPropertyChanged(nameof(AttributePointsToSpend));
                NotifyPropertyChanged(nameof(ActualCP));
            }
        }
        public void NotifyGeneticTypes()
        {
            NotifyPropertyChanged(nameof(NormalHuman));
            NotifyPropertyChanged(nameof(NaturalHybrid));
            NotifyPropertyChanged(nameof(GenoHybrid));
            NotifyPropertyChanged(nameof(TechnoHybrid));
            NotifyPropertyChanged(nameof(Deserter));
            NotifyPropertyChanged(nameof(GeneticType));
            NotifyPropertyChanged(nameof(YearsAsTechnoHybrid));
            NotifyPropertyChanged(nameof(YearsAsTridentHybrid));
            NotifyPropertyChanged(nameof(YearsAsSoldierOrMilitiaman));
            NotifyPropertyChanged(nameof(Age));
            NotifyPropertyChanged(nameof(ActualCP));
        }
        private void SetAttributeModifierToDefault()
        {
            Strength.AttributeModifier = 0;
            Constitution.AttributeModifier = 0;
            Coordination.AttributeModifier = 0;
            Adaptation.AttributeModifier = 0;
            Perception.AttributeModifier = 0;
            Intelligence.AttributeModifier = 0;
            Willpower.AttributeModifier = 0;
            Presence.AttributeModifier = 0;
        }
        #endregion

        #region Tab3
        public int AdditionalEar { get; set; }
        public int AdditionalEye { get; set; }
        public int CorrosiveTouch { get; set; }
        public int NaturalResistanceBoni_Fire
        {
            get => naturalResistanceFire;
            set
            {
                naturalResistanceFire = value;
                NotifyPropertyChanged(nameof(NaturalResistanceBoni_Fire));
                NotifyPropertyChanged(nameof(NaturalResistance_Fire));
            }
        }
        public int NaturalResistanceBoni_Cold
        {
            get => naturalResistanceCold;
            set
            {
                naturalResistanceCold = value;
                NotifyPropertyChanged(nameof(NaturalResistanceBoni_Cold));
                NotifyPropertyChanged(nameof(NaturalResistance_Cold));
            }
        }
        public int NaturalResistanceBoni_Drugs
        {
            get => naturalResistanceDrugs;
            set
            {
                naturalResistanceDrugs = value;
                NotifyPropertyChanged(nameof(NaturalResistanceBoni_Drugs));
                NotifyPropertyChanged(nameof(NaturalResistance_Drugs));
            }
        }
        public int NaturalResistanceBoni_Diseases
        {
            get => naturalResistanceDiseases;
            set
            {
                naturalResistanceDiseases = value;
                NotifyPropertyChanged(nameof(NaturalResistanceBoni_Diseases));
                NotifyPropertyChanged(nameof(NaturalResistance_Disease));
            }
        }
        public int NaturalResistanceBoni_Poison
        {
            get => naturalResistancePoison;
            set
            {
                naturalResistancePoison = value;
                NotifyPropertyChanged(nameof(NaturalResistanceBoni_Poison));
                NotifyPropertyChanged(nameof(NaturalResistance_Poison));
            }
        }
        public int NaturalResistanceBoni_Radiation
        {
            get => naturalResistanceRadiation;
            set
            {
                naturalResistanceRadiation = value;
                NotifyPropertyChanged(nameof(NaturalResistanceBoni_Radiation));
                NotifyPropertyChanged(nameof(NaturalResistance_Radiation));
            }
        }
        public int Parasites
        {
            get => parasites;
            set
            {
                //random specialAbility_Checkbox = true oder random specialAbility_TextBox = 1
                parasites = value;
                NotifyPropertyChanged(nameof(Parasites));
                NotifyPropertyChanged(nameof(DamageResistance));
            }
        }
        public int Radiation { get; set; }
        public int Regeneration { get; set; }
        public int ReinforcedSkeleton
        {
            get => reinforcedSkeleton;
            set
            {
                reinforcedSkeleton = value;
                NotifyPropertyChanged(nameof(ReinforcedSkeleton));
                NotifyPropertyChanged(nameof(StunThreshold));
                NotifyPropertyChanged(nameof(KnockoutThreshold));
                NotifyPropertyChanged(nameof(DamageResistance));
            }
        }
        public int ReinforcedSkin { get; set; }
        public int RetractableTentacle { get; set; }
        public int Symbionts
        {
            get => symbionts;
            set
            {
                //jeweils entweder +2 in NaturalResistance_Any oder random SpecialAbility
                symbionts = value;
                NotifyPropertyChanged(nameof(Symbionts));
            }
        }
        public int Tail { get; set; }
        public int TailCost
        {
            get => tailCost;
            set
            {
                tailCost = value;
                NotifyPropertyChanged(nameof(TailCost));
            }
        }
        public int LightDeformity
        {
            get => lightDeformity;
            set
            {
                if (lightDeformity < value)
                {
                    lightDeformity = value;
                    Presence.SpecialModifier -= lightDeformity;
                }
                else
                {
                    if (value == 0)
                    {
                        Presence.SpecialModifier += lightDeformity;
                    }
                    lightDeformity = value;
                    Presence.SpecialModifier += lightDeformity;
                }
                NotifyPropertyChanged(nameof(LightDeformity));
            }
        }
        public int SevereDeformity
        {
            get => severeDeformity;
            set
            {
                if (severeDeformity < value)
                {
                    severeDeformity = value;
                    Presence.SpecialModifier -= severeDeformity * 2;
                }
                else
                {

                    if (value == 0)
                    {
                        Presence.SpecialModifier += severeDeformity * 2;
                    }
                    severeDeformity = value;
                    Presence.SpecialModifier += severeDeformity * 2;
                }
                NotifyPropertyChanged(nameof(SevereDeformity));
            }
        }
        public int MissingEar { get; set; }
        public int MissingEye { get; set; }
        public bool EnhancedTasteBuds { get; set; }
        public bool EnhancedSmell { get; set; }
        public bool EnhancedTouch { get; set; }
        public bool Amphibious { get; set; }
        public bool Androgynous { get; set; }
        public bool Claws { get; set; }
        public int ClawsCost
        {
            get => clawsCost;
            set
            {
                clawsCost = value;
                NotifyPropertyChanged(nameof(ClawsCost));
            }
        }
        public bool Contagion { get; set; }
        public bool Empathy { get; set; }
        public bool Fangs { get; set; }
        public int FangsCost
        {
            get => fangsCost;
            set
            {
                fangsCost = value;
                NotifyPropertyChanged(nameof(FangsCost));
            }
        }
        public bool Horn { get; set; }
        public bool FelineGeneticTraits
        {
            get
            {
                return felineGeneticTraits;
            }
            set
            {
                if (value)
                {
                    Coordination.SpecialModifier += 2;
                    ClawsCost -= 1;
                    NightVisionCost -= 1;
                }
                else
                {
                    Coordination.SpecialModifier -= 2;
                    ClawsCost += 1;
                    NightVisionCost += 1;
                }
                felineGeneticTraits = value;
                NotifyPropertyChanged(nameof(FelineGeneticTraits));
            }
        }
        public bool CanineGeneticTraits
        {
            get
            {
                return canineGeneticTraits;
            }
            set
            {
                if (value)
                {
                    Constitution.SpecialModifier += 1;
                    FangsCost -= 1;
                }
                else
                {
                    Constitution.SpecialModifier -= 1;
                    FangsCost += 1;
                }
                canineGeneticTraits = value;
                NotifyPropertyChanged(nameof(CanineGeneticTraits));
            }
        }
        public bool ReptilianGeneticTraits
        {
            get
            {
                return reptilianGeneticTraits;
            }
            set
            {
                if (value)
                {
                    Coordination.SpecialModifier += 1;
                }
                else
                {
                    Coordination.SpecialModifier -= 1;
                }
                reptilianGeneticTraits = value;
                NotifyPropertyChanged(nameof(ReptilianGeneticTraits));
            }
        }
        public bool SimianGeneticTraits
        {
            get
            {
                return simianGeneticTraits;
            }
            set
            {
                if (value)
                {
                    Strength.SpecialModifier += 1;
                    Constitution.SpecialModifier += 1;
                    TailCost -= 1;
                }
                else
                {
                    Strength.SpecialModifier -= 1;
                    Constitution.SpecialModifier -= 1;
                    TailCost += 1;
                }
                simianGeneticTraits = value;
                NotifyPropertyChanged(nameof(SimianGeneticTraits));
            }
        }
        public bool MolecularInstability { get; set; }
        public bool NightVision { get; set; }
        public int NightVisionCost
        {
            get => nightVisionCost;
            set
            {
                nightVisionCost = value;
                NotifyPropertyChanged(nameof(NightVisionCost));
            }
        }
        public bool OutdoorAdaptation { get; set; }
        public bool Purulence
        {
            get => purulence;
            set
            {
                if (purulence == true)
                {
                    purulence = value;
                    Presence.AttributeModifier += 2;
                }
                else
                {
                    purulence = value;
                    Presence.AttributeModifier -= 2;
                }
                NotifyPropertyChanged(nameof(Purulence));
                NotifyPropertyChanged(nameof(NaturalResistance_Disease));
            }
        }
        public bool RetractableBoneGrowth { get; set; }
        public bool SelfFertilization { get; set; }
        public bool Sexless { get; set; }
        public bool ShapeShifter { get; set; }
        public bool SixthSense { get; set; }
        public bool Sonar { get; set; }
        public bool PolarisEffectInitialPower
        {
            get => polarisEffectInitialPower;
            set
            {
                polarisEffectInitialPower = value;
                NotifyPropertyChanged(nameof(PolarisEffectInitialPower));
                NotifyPropertyChanged(nameof(PolarisEffectFirstPower));
                NotifyPropertyChanged(nameof(PolarisEffectSecondPower));
                NotifyPropertyChanged(nameof(PolarisEffectThridPower));
            }
        }
        public bool PolarisEffectFirstPower
        {
            get
            {
                if (!PolarisEffectInitialPower)
                {
                    return false;
                }
                return polarisEffectFirstPower;
            }
            set
            {
                polarisEffectFirstPower = value;
                NotifyPropertyChanged(nameof(PolarisEffectFirstPower));
                NotifyPropertyChanged(nameof(PolarisEffectSecondPower));
                NotifyPropertyChanged(nameof(PolarisEffectThridPower));
            }
        }
        public bool PolarisEffectSecondPower
        {
            get
            {
                if (!PolarisEffectFirstPower)
                {
                    return false;
                }
                return polarisEffectSecondPower;
            }
            set
            {
                polarisEffectSecondPower = value;
                NotifyPropertyChanged(nameof(PolarisEffectSecondPower));
                NotifyPropertyChanged(nameof(PolarisEffectThridPower));
            }
        }
        public bool PolarisEffectThridPower
        {
            get
            {
                if (!PolarisEffectSecondPower)
                {
                    return false;
                }
                return polarisEffectThridPower;
            }
            set
            {
                polarisEffectThridPower = value;
                NotifyPropertyChanged(nameof(PolarisEffectThridPower));
            }
        }
        public bool AtrophiedTasteBuds { get; set; }
        public bool AtrophiedNose { get; set; }
        public bool AtrophiedTouch { get; set; }
        #endregion

        #region Tab4
        public bool NomadicShip
        {
            get => geographicOrigin == 0;
            set
            {
                geographicOrigin = 0;
                NotifyGeographicOrigins();
            }
        }
        public bool SmallStation
        {
            get => geographicOrigin == 1;
            set
            {
                geographicOrigin = 1;
                NotifyGeographicOrigins();
            }
        }
        public bool MiddleSizedStation
        {
            get => geographicOrigin == 2;
            set
            {
                geographicOrigin = 2;
                NotifyGeographicOrigins();
            }
        }
        public bool MajorCity
        {
            get => geographicOrigin == 3;
            set
            {
                geographicOrigin = 3;
                NotifyGeographicOrigins();
            }
        }
        public GeographicOrigin GeographicOrigin
        {
            get
            {
                string name;

                switch (geographicOrigin)
                {
                    case 0:
                        name = "Nomadic Ship";
                        break;
                    case 1:
                        name = "Small Station";
                        break;
                    case 2:
                        name = "Middle-Sized Station";
                        break;
                    case 3:
                        name = "Major City";
                        break;
                    default:
                        name = "To choose!";
                        break;
                }
                return new GeographicOrigin(name);
            }
        }
        public bool Slums
        {
            get
            {
                if (!UpperClass_Or_MiddleClass_Or_Slums_isEnabled)
                {
                    return false;
                }
                return socialOrigin == 0;
            }
            set
            {
                socialOrigin = 0;
                NotifySocialOrigins();
            }
        }
        public bool WorkingClass
        {
            get => socialOrigin == 1;
            set
            {
                socialOrigin = 1;
                NotifySocialOrigins();
            }
        }
        public bool MiddleClass
        {
            get
            {
                if (!UpperClass_Or_MiddleClass_Or_Slums_isEnabled)
                {
                    return false;
                }
                return socialOrigin == 2;
            }
            set
            {
                socialOrigin = 2;
                NotifySocialOrigins();
            }
        }
        public bool UpperClass
        {
            get
            {
                if (!UpperClass_Or_MiddleClass_Or_Slums_isEnabled)
                {
                    return false;
                }
                return socialOrigin == 3;
            }
            set
            {
                socialOrigin = 3;
                NotifySocialOrigins();
            }
        }
        //public CCBA UpperClass = new CCBA(
        //            isEnabled = () => { return geographicOrigin == 2 || geographicOrigin == 3; }
        //,
        //            isChecked = () => { return socialOrigin == 3; }
        //,
        //            setChecked = (bool x) => { socialOrigin = x ? 3 : 4; NotifySocialOrigin(); }
        //);
        public bool WorkingClass_isEnabled
        {
            get => true;
        }
        public bool UpperClass_Or_MiddleClass_Or_Slums_isEnabled
        {
            get => geographicOrigin == 2 || geographicOrigin == 3;
        }
        public SocialOrigin SocialOrigin
        {
            get
            {
                string name;

                switch (socialOrigin)
                {
                    case 0:
                        name = "Slums";
                        break;
                    case 1:
                        name = "Working Class";
                        break;
                    case 2:
                        name = "Middle Class";
                        break;
                    case 3:
                        name = "Upper Class";
                        break;
                    default:
                        name = "To choose!";
                        break;
                }
                return new SocialOrigin(name);
            }
        }
        public bool DelinquencyOrCrimes
        {
            get
            {
                if (!SelfThaught_Or_DelinquencyOrCrimes_isEnabled)
                {
                    return false;
                }
                return initialTraining == 0;
            }
            set
            {
                initialTraining = 0;
                NotifyInitialTrainings();
            }
        }
        public bool TechnicalApprenticeship_Aquaculture
        {
            get
            {
                if (!TechnicalApprenticeship_isEnabled)
                {
                    return false;
                }
                return initialTraining == 1;
            }
            set
            {
                initialTraining = 1;
                NotifyInitialTrainings();
            }
        }
        public bool TechnicalApprenticeship_Mines
        {
            get
            {
                if (!TechnicalApprenticeship_isEnabled)
                {
                    return false;
                }
                return initialTraining == 2;
            }
            set
            {
                initialTraining = 2;
                NotifyInitialTrainings();
            }
        }
        public bool TechnicalApprenticeship_WorksOrWorkshop
        {
            get
            {
                if (!TechnicalApprenticeship_isEnabled)
                {
                    return false;
                }
                return initialTraining == 3;
            }
            set
            {
                initialTraining = 3;
                NotifyInitialTrainings();
            }
        }
        public bool Education
        {
            get
            {
                if (!Education_isEnabled)
                {
                    return false;
                }
                return initialTraining == 4;
            }
            set
            {
                initialTraining = 4;
                NotifyInitialTrainings();
            }
        }
        public bool SelfThaught
        {
            get
            {
                if (!SelfThaught_Or_DelinquencyOrCrimes_isEnabled)
                {
                    return false;
                }
                return initialTraining == 5;
            }
            set
            {
                initialTraining = 5;
                NotifyInitialTrainings();
            }
        }
        public bool SelfThaught_Or_DelinquencyOrCrimes_isEnabled
        {
            get => true;
        }
        public bool TechnicalApprenticeship_isEnabled
        {
            get => socialOrigin == 0 || socialOrigin == 1 || socialOrigin == 2;
        }
        public bool Education_isEnabled
        {
            get => UpperClass_Or_MiddleClass_Or_Slums_isEnabled && (socialOrigin == 2 || socialOrigin == 3);
        }
        public InitialTraining InitialTraining
        {
            get
            {
                string name;

                switch (initialTraining)
                {
                    case 0:
                        name = "Delinquency/Crimes";
                        break;
                    case 1:
                        name = "Technical Apprenticeship: Aquaculture";
                        break;
                    case 2:
                        name = "Technical Apprenticeship: Mines";
                        break;
                    case 3:
                        name = "Technical Apprenticeship: Works/Workshop";
                        break;
                    case 4:
                        name = "Education";
                        break;
                    case 5:
                        name = "Self-Thaught";
                        break;
                    default:
                        name = "To choose!";
                        break;
                }
                return new InitialTraining(name);
            }
        }
        public ObservableCollection<HigherEducation> HigherEducations
        {
            get => new ObservableCollection<HigherEducation>();
        }

        public void NotifyGeographicOrigins()
        {
            NotifyPropertyChanged(nameof(NomadicShip));
            NotifyPropertyChanged(nameof(SmallStation));
            NotifyPropertyChanged(nameof(MiddleSizedStation));
            NotifyPropertyChanged(nameof(MajorCity));
            NotifyPropertyChanged(nameof(GeographicOrigin));
            NotifySocialOrigins();
        }
        public void NotifySocialOrigins()
        {
            NotifyPropertyChanged(nameof(Slums));
            NotifyPropertyChanged(nameof(WorkingClass));
            NotifyPropertyChanged(nameof(MiddleClass));
            NotifyPropertyChanged(nameof(UpperClass));
            NotifyPropertyChanged(nameof(SocialOrigin));
            NotifyPropertyChanged(nameof(WorkingClass_isEnabled));
            NotifyPropertyChanged(nameof(UpperClass_Or_MiddleClass_Or_Slums_isEnabled));
            NotifyInitialTrainings();
        }
        public void NotifyInitialTrainings()
        {
            NotifyPropertyChanged(nameof(DelinquencyOrCrimes));
            NotifyPropertyChanged(nameof(TechnicalApprenticeship_Aquaculture));
            NotifyPropertyChanged(nameof(TechnicalApprenticeship_Mines));
            NotifyPropertyChanged(nameof(TechnicalApprenticeship_WorksOrWorkshop));
            NotifyPropertyChanged(nameof(Education));
            NotifyPropertyChanged(nameof(SelfThaught));
            NotifyPropertyChanged(nameof(SelfThaught_Or_DelinquencyOrCrimes_isEnabled));
            NotifyPropertyChanged(nameof(TechnicalApprenticeship_isEnabled));
            NotifyPropertyChanged(nameof(Education_isEnabled));
            NotifyPropertyChanged(nameof(InitialTraining));
        }
        #endregion

        #region Tab5
        #endregion

        #region Tab6
        public int YearsAsAssassin
        {
            get
            {
                if (!Assassin_isEnabled)
                {
                    return 0;
                }
                return yearsAsAssassin;
            }
            set
            {
                yearsAsAssassin = value;
                NotifyProfessions(nameof(YearsAsAssassin));
            }
        }
        public int YearsAsBartender
        {
            get
            {
                return yearsAsBartender;
            }
            set
            {
                yearsAsBartender = value;
                NotifyProfessions(nameof(YearsAsBartender));
            }
        }
        public int YearsAsBountyHunter
        {
            get
            {
                if (!BountyHunter_isEnabled)
                {
                    return 0;
                }
                return yearsAsBountyHunter;
            }
            set
            {
                yearsAsBountyHunter = value;
                NotifyProfessions(nameof(YearsAsBountyHunter));
            }
        }
        public int YearsAsCraftsmanOrArtist
        {
            get
            {
                return yearsAsCraftsmanOrArtist;
            }
            set
            {
                yearsAsCraftsmanOrArtist = value;
                NotifyProfessions(nameof(YearsAsCraftsmanOrArtist));
            }
        }
        public int YearsAsDiplomat
        {
            get
            {
                if (!Diplomat_isEnabled)
                {
                    return 0;
                }
                return yearsAsDiplomat;
            }
            set
            {
                yearsAsDiplomat = value;
                NotifyProfessions(nameof(YearsAsDiplomat));
            }
        }
        public int YearsAsDocterOrSurgeon
        {
            get
            {
                if (!DoctorOrSurgeon_isEnabled)
                {
                    return 0;
                }
                return yearsAsDocterOrSurgeon;
            }
            set
            {
                yearsAsDocterOrSurgeon = value;
                NotifyProfessions(nameof(YearsAsDocterOrSurgeon));
            }
        }
        public int YearsAsEliteSoldier
        {
            get
            {
                if (!EliteSoldier_isEnabled)
                {
                    return 0;
                }
                return yearsAsEliteSoldier;
            }
            set
            {
                yearsAsEliteSoldier = value;
                NotifyProfessions(nameof(YearsAsEliteSoldier));
            }
        }
        public int YearsAsFarmerOrLivestockFarmer
        {
            get
            {
                return yearsAsFarmerOrLivestockFarmer;
            }
            set
            {
                yearsAsFarmerOrLivestockFarmer = value;
                NotifyProfessions(nameof(YearsAsFarmerOrLivestockFarmer));
            }
        }
        public int YearsAsFighterPilot
        {
            get
            {
                if (!FigherPilot_isEnabled)
                {
                    return 0;
                }
                return yearsAsFighterPilot;
            }
            set
            {
                yearsAsFighterPilot = value;
                NotifyProfessions(nameof(YearsAsFighterPilot));
            }
        }
        public int YearsAsMercenary
        {
            get
            {
                return yearsAsMercenary;
            }
            set
            {
                yearsAsMercenary = value;
                NotifyProfessions(nameof(YearsAsMercenary));
            }
        }
        public int YearsAsMilitaryOfficer
        {
            get
            {
                if (!MilitaryOfficer_isEnabled)
                {
                    return 0;
                }
                return yearsAsMilitaryOfficer;
            }
            set
            {
                yearsAsMilitaryOfficer = value;
                NotifyProfessions(nameof(YearsAsMilitaryOfficer));
            }
        }
        public int YearsAsMiner
        {
            get
            {
                return yearsAsMiner;
            }
            set
            {
                yearsAsMiner = value;
                NotifyProfessions(nameof(YearsAsMiner));
            }
        }
        public int YearsAsNavalOfficerOrNavigator
        {
            get
            {
                if (!NavalOfficerOrNavigator_isEnabled)
                {
                    return 0;
                }
                return yearsAsNavalOfficerOrNavigator;
            }
            set
            {
                yearsAsNavalOfficerOrNavigator = value;
                NotifyProfessions(nameof(YearsAsNavalOfficerOrNavigator));
            }
        }
        public int YearsAsPirate
        {
            get
            {
                return yearsAsPirate;
            }
            set
            {
                yearsAsPirate = value;
                NotifyProfessions(nameof(YearsAsPirate));
            }
        }
        public int YearsAsPoliceOfficerOrInvestigator
        {
            get
            {
                return yearsAsPoliceOfficerOrInvestigator;
            }
            set
            {
                yearsAsPoliceOfficerOrInvestigator = value;
                NotifyProfessions(nameof(YearsAsPoliceOfficerOrInvestigator));
            }
        }
        public int YearsAsPriestOfTheTrident
        {
            get
            {
                return yearsAsPriestOfTheTrident;
            }
            set
            {
                yearsAsPriestOfTheTrident = value;
                NotifyProfessions(nameof(YearsAsPriestOfTheTrident));
            }
        }
        public int YearsAsScholarOrArchaelogist
        {
            get
            {
                if (!ScholarOrArchaelogist_isEnabled)
                {
                    return 0;
                }
                return yearsAsScholarOrArchaelogist;
            }
            set
            {
                yearsAsScholarOrArchaelogist = value;
                NotifyProfessions(nameof(YearsAsScholarOrArchaelogist));
            }
        }
        public int YearsAsScientistOrEngineer
        {
            get
            {
                if (!ScientistOrEngineer_isEnabled)
                {
                    return 0;
                }
                return yearsAsScientistOrEngineer;
            }
            set
            {
                yearsAsScientistOrEngineer = value;
                NotifyProfessions(nameof(YearsAsScientistOrEngineer));
            }
        }
        public int YearsAsSmuggler
        {
            get
            {
                return yearsAsSmuggler;
            }
            set
            {
                yearsAsSmuggler = value;
                NotifyProfessions(nameof(YearsAsSmuggler));
            }
        }
        public int YearsAsSoldierOrMilitiaman
        {
            get
            {
                if ((genotype != 3) || (genotype == 3 && yearsAsSoldierOrMilitiaman > 2))
                {
                    return yearsAsSoldierOrMilitiaman;
                }
                else
                {
                    return 2;
                }
            }
            set
            {
                if ((genotype != 3) || (genotype == 3 && value > 2))
                {
                    yearsAsSoldierOrMilitiaman = value;
                }
                else
                {
                    yearsAsSoldierOrMilitiaman = 2;
                }
                NotifyProfessions(nameof(YearsAsSoldierOrMilitiaman));
            }
        }
        public int YearsAsSpy
        {
            get
            {
                if (!Spy_isEnabled)
                {
                    return 0;
                }
                return yearsAsSpy;
            }
            set
            {
                yearsAsSpy = value;
                NotifyProfessions(nameof(YearsAsSpy));
            }
        }
        public int YearsAsSubmariner
        {
            get
            {
                return yearsAsSubmariner;
            }
            set
            {
                yearsAsSubmariner = value;
                NotifyProfessions(nameof(YearsAsSubmariner));
            }
        }
        public int YearsAsTechnicanOrMechanic
        {
            get
            {
                return yearsAsTechnicanOrMechanic;
            }
            set
            {
                yearsAsTechnicanOrMechanic = value;
                NotifyProfessions(nameof(YearsAsTechnicanOrMechanic));
            }
        }
        public int YearsAsTechnoHybrid
        {
            get
            {
                if (genotype != 3)
                {
                    return 0;
                }
                if (yearsAsTechnoHybrid > 1)
                {
                    return yearsAsTechnoHybrid;
                }
                else
                {
                    return 1;
                }
            }
            set
            {
                if (value > 1)
                {
                    yearsAsTechnoHybrid = value;
                }
                else
                {
                    yearsAsTechnoHybrid = 1;
                }
                NotifyProfessions(nameof(YearsAsTechnoHybrid));
            }
        }
        public int YearsAsThiefOrCriminal
        {
            get
            {
                return yearsAsThiefOrCriminal;
            }
            set
            {
                yearsAsThiefOrCriminal = value;
                NotifyProfessions(nameof(YearsAsThiefOrCriminal));
            }
        }
        public int YearsAsTrader
        {
            get
            {
                return yearsAsTrader;
            }
            set
            {
                yearsAsTrader = value;
                NotifyProfessions(nameof(YearsAsTrader));
            }
        }
        public int YearsAsTravellingTraderOrStoryteller
        {
            get
            {
                return yearsAsTravellingTraderOrStoryteller;
            }
            set
            {
                yearsAsTravellingTraderOrStoryteller = value;
                NotifyProfessions(nameof(YearsAsTravellingTraderOrStoryteller));
            }
        }
        public int YearsAsTridentHybrid
        {
            get
            {
                if (genotype != 2)
                {
                    return 0;
                }
                if (yearsAsTridentHybrid > 1)
                {
                    return yearsAsTridentHybrid;
                }
                else
                {
                    return 1;
                }
            }
            set
            {
                if (value > 1)
                {
                    yearsAsTridentHybrid = value;
                }
                else
                {
                    yearsAsTridentHybrid = 1;
                }
                NotifyProfessions(nameof(YearsAsTridentHybrid));
            }
        }
        public int YearsAsWatcher
        {
            get
            {
                return yearsAsWatcher;
            }
            set
            {
                yearsAsWatcher = value;
                NotifyProfessions(nameof(YearsAsWatcher));
            }
        }
        public int YearsAsWorkerOrLongshoreman
        {
            get
            {
                return yearsAsWorkerOrLongshoreman;
            }
            set
            {
                yearsAsWorkerOrLongshoreman = value;
                NotifyProfessions(nameof(YearsAsWorkerOrLongshoreman));
            }
        }

        public bool Assassin_isEnabled
        {
            get
            {
                if ((YearsAsBountyHunter + YearsAsMercenary + YearsAsThiefOrCriminal + YearsAsSoldierOrMilitiaman + YearsAsSpy) > 2)
                {
                    return true;
                }
                return false;
            }
        }
        public bool BountyHunter_isEnabled
        {
            get
            {
                if ((YearsAsMercenary + YearsAsPoliceOfficerOrInvestigator + YearsAsSoldierOrMilitiaman + YearsAsWatcher + YearsAsThiefOrCriminal) > 1)
                {
                    return true;
                }
                return false;
            }
        }
        public bool EliteSoldier_isEnabled
        {
            get
            {
                if ((YearsAsSoldierOrMilitiaman > 2) && (CountStatsOver14() > 1))
                {
                    return true;
                }
                return false;
            }
        }
        public bool MilitaryOfficer_isEnabled
        {
            get
            {
                //Or higher education == military school
                if ((YearsAsSoldierOrMilitiaman + YearsAsWatcher > 11) || (YearsAsEliteSoldier + YearsAsTechnoHybrid > 14))
                {
                    return true;
                }
                return false;
            }
        }
        public bool NavalOfficerOrNavigator_isEnabled
        {
            get
            {
                //Or higher education == naval school
                if (YearsAsSubmariner > 14)
                {
                    return true;
                }
                return false;
            }
        }
        public bool Spy_isEnabled
        {
            get
            {
                if (YearsAsDiplomat + YearsAsMercenary + YearsAsPoliceOfficerOrInvestigator + YearsAsSoldierOrMilitiaman + YearsAsWatcher > 2)
                {
                    return true;
                }
                return false;
            }
        }
        public bool Diplomat_isEnabled
        {
            get
            {
                //HigherEducation == Law || HigherEducation == PoliticalSciences
                if (true)
                {
                    return true;
                }
                return false;
            }
        }
        public bool DoctorOrSurgeon_isEnabled
        {
            get
            {
                //HigherEducation == Medicin
                if (true)
                {
                    return true;
                }
                return false;
            }
        }
        public bool FigherPilot_isEnabled
        {
            get
            {
                //HigherEducation == naval school
                if (true)
                {
                    return true;
                }
                return false;
            }
        }
        public bool ScholarOrArchaelogist_isEnabled
        {
            get
            {
                //HigherEducation == science/humanities
                if (true)
                {
                    return true;
                }
                return false;
            }
        }
        public bool ScientistOrEngineer_isEnabled
        {
            get
            {
                //HigherEducation == science/humanities || HigherEducation == engineering school
                if (true)
                {
                    return true;
                }
                return false;
            }
        }

        public void NotifyProfessions(string professionName)
        {
            NotifyPropertyChanged(nameof(professionName));

            NotifyPropertyChanged(nameof(YearsAsEliteSoldier));
            NotifyPropertyChanged(nameof(YearsAsMilitaryOfficer));
            NotifyPropertyChanged(nameof(YearsAsNavalOfficerOrNavigator));
            NotifyPropertyChanged(nameof(YearsAsSpy));
            NotifyPropertyChanged(nameof(YearsAsBountyHunter));
            NotifyPropertyChanged(nameof(YearsAsAssassin));

            NotifyPropertyChanged(nameof(Age));
            NotifyPropertyChanged(nameof(ActualCP));
        }
        private int CountStatsOver14()
        {
            int sum = 0;

            sum += (Strength.ActualAttributeValue > 14 ? 1 : 0) + (Constitution.ActualAttributeValue > 14 ? 1 : 0) + (Coordination.ActualAttributeValue > 14 ? 1 : 0) + (Adaptation.ActualAttributeValue > 14 ? 1 : 0)
                + (Willpower.ActualAttributeValue > 14 ? 1 : 0);

            return sum;
        }
        #endregion

        #region Tab7
        #endregion
    }

    public class BooleanToFertilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true)
                    return "fertile";
                else
                    return "sterile";
            }
            return "sterile";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch (value.ToString().ToLower())
            {
                case "fertile":
                    return true;
                case "sterile":
                    return false;
            }
            return false;
        }
    }
    public class IntToCostConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is int)
            {
                switch ((int)value)
                {
                    case 0:
                        return "(0 CP)";
                    case 1:
                        return "(1 CP)";
                    case 2:
                        return "(2 CP)";
                }
            }
            return "(2 CP)";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch (value.ToString())
            {
                case "(0 CP)":
                    return 0;
                case "(1 CP)":
                    return 1;
                case "(2 CP)":
                    return 2;
            }
            return 2;
        }
    }
    public enum handedness
    {
        righthanded,
        lefthanded,
        ambidextrous
    }
}
