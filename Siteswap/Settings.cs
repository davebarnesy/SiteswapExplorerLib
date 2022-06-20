namespace SiteswapLib
{

    // Need some IoC here so each project can handle its own settings
    public partial class Siteswap
    {
        public static bool AutoSort = false;
        public static bool BiggerAnimator = false;
        public static ColourMode ColourMode = ColourMode.orbit;
        public static bool Slomo = false;
        public static bool ChartMode = false;
    }
}
