namespace SiteswapLib
{
    public static class IntExtenstion
    {
        public static bool IsOdd(this int value)
        {
            return value % 2 != 0;
        }

        public static bool IsEven(this int value)
        {
            return !value.IsOdd();
        }
    }
}
