using RandomRPG.Model.Enums;

namespace RandomRPG.Model
{
    public static class AttributeFactory
    {

        public static Attributes GetInstance(GladiatorTypes gladType)
        {
            switch (gladType.ToString())
            {
                case "Doctore":
                    return new Attributes()
                    {
                        Strength = 25,
                        Agility = 50,
                        CritChance = 25,
                        Vitality = 100,
                        Energy = 75,
                        HitPoints = 200
                    };
                default:
                    return new Attributes();
            }
        }
    }
}