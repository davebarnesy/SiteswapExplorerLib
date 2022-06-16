﻿namespace SiteswapLib
{
    public class ControlsViewModel
    {
        public readonly Siteswap Siteswap;

        public ControlsViewModel(Siteswap siteswap)
        {
            Siteswap = siteswap;
        }

        public Siteswap IncreaseAllResult => Siteswap.IncreaseAll;
        public Siteswap DecreaseAllResult => Siteswap.DecreaseAll;
        public Siteswap InsertResult => Siteswap.LastPossibleInsertion.Item2;

        public bool CanIncrease => IncreaseAllResult != null;
        public bool CanDecrease => DecreaseAllResult != null;
        public bool CanInsert => InsertResult != null;

    }
}
