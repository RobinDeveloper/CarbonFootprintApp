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
        public double Positive;
        public double Medium;
        public double Negative;
        public double Unkowm;

        public PMNUScore()
        {
        }
        
        public PMNUScore(double _positive, double _medium, double _negative, double _unkowm)
        {
            Positive = _positive;
            Medium = _medium;
            Negative = _negative;
            Unkowm = _unkowm;
        }
    }
    
    public class UserData
    {
        public string Name;
        public PMNUScore PMNUDayScore; //PMNU = positive medium negative unkown
        public PMNUScore PMNUWeekScore;
        public PMNUScore PMNUMonthScore;
        public PMNUScore PMNUYearScore;
        public PMNUScore ProdcutScore;
        public PMNUScore TransportScore;
        public PMNUScore EverydayScore;
        public PMNUScore FoodScore;
        public Car Car;
        public House House;
    }
}