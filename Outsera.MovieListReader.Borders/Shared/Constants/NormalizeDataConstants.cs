namespace Outsera.MovieListReader.Borders.Shared.Constants
{
    public static class NormalizeDataConstants
    {
        public static readonly string[] WinnerAsTrueValues = ["yes", "true", "1"];
        public static readonly string[] WinnerAsFalseValues = ["no", "false", "0"];

        public static readonly string[] WinnerValidValues = WinnerAsTrueValues.Concat(WinnerAsFalseValues).ToArray();
    }
}
