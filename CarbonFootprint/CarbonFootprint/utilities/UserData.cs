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
    public struct UserData
    {
        public string Name;
        public double ScoreToday;
        public Tuple<int, int, int, int> PMNUDayScore; //PMNU = positive medium negative unkown
        public double ScoreWeek;
        public Tuple<int, int, int, int> PMNUWeekScore;
        public double ScoreMonth;
        public Tuple<int, int, int, int> PMNUMonthScore;
        public double ScoreYear;
        public Tuple<int, int, int, int> PMNUYearScore;
        public Car Car;
        public House House;
    }
}