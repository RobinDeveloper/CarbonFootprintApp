using System;

namespace CarbonFootprint.utilities
{
    public enum PositivityRating
    {
        Positive,
        Medium,
        Negative,
        Unkown
    }

    public class PMNUScore
    {
        public int Positive;
        public int Medium;
        public int Negative;
        public int Unkowm;
    }
    
    public class UserData
    {
        public string Name;
        public PMNUScore PMNUDayScore; //PMNU = positive medium negative unkown
        public PMNUScore PMNUWeekScore;
        public PMNUScore PMNUMonthScore;
        public PMNUScore PMNUYearScore;
        public double ProdcutScore;
        public double TransportScore;
        public double EverydayScore;
        public double FoodScore;
        public Car Car;
        public House House;
    }
}