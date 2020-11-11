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
        public double ScoreToday;
        public PMNUScore PMNUDayScore; //PMNU = positive medium negative unkown
        public double ScoreWeek;
        public PMNUScore PMNUWeekScore;
        public double ScoreMonth;
        public PMNUScore PMNUMonthScore;
        public double ScoreYear;
        public PMNUScore PMNUYearScore;
        public Car Car;
        public House House;
    }
}