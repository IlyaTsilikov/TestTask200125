namespace TestTask200125.Models
{
    public class ObjectInfo
    {
        public const int ColumnsCount = 6;

        public const int NameIndex = 0;
        public const int DistanceIndex = 1;
        public const int AngleIndex = 2;
        public const int WidthIndex = 3;
        public const int HegthIndex = 4;
        public const int IsDefectIndex = 5;

        public const string TrueValueName = "yes";
        public const string FalseValueName = "no";

        public string Name { get; set; }
        public double? Distance { get; set; }
        public double? Angle { get; set; }
        public double? Width { get; set; }
        public double? Hegth { get; set; }
        public bool? IsDefect { get; set; }
    }
}