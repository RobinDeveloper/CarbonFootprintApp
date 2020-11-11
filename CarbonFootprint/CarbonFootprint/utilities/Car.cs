namespace CarbonFootprint.utilities
{
    public struct Car
    {
        public enum CarType
        {
            Hybrid,
            LPG,
            Diesel,
            Electric,
            Default
        }

        public string Name;
        public int Age;
        public int BuildYear;
        public int GasMilage; //ToOne
        public CarType CarEnergyType;
    }
}