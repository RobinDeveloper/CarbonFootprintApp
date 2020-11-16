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

        public PMNUScore()
        {
        }
        
        public PMNUScore(int _positive, int _medium, int _negative, int _unkowm)
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