namespace SiteswapLib
{
    public static class ManipulationExtension
    {
        public static string ButtonText(this Manipulation manipulation)
        {
            return manipulation switch
            {
                Manipulation.AddPeriod => "+",
                Manipulation.SubtractPeriod => "-",
                Manipulation.Delete => "x",
                Manipulation.IncreaseAll => "+1",
                Manipulation.DecreaseAll => "-1",
                Manipulation.Insert => ">",
                _ => "",
            };
        }

        public static string DisplayName(this Manipulation manipulation)
        {
            return manipulation switch
            {
                Manipulation.AddPeriod => "Add period",
                Manipulation.SubtractPeriod => "Subtract period",
                Manipulation.InsertAfter => "Insert after",
                Manipulation.RemoveOrbit => "Remove orbit",
                Manipulation.RotateToStart => "Rotate to start",
                Manipulation.Delete => "Delete",
                Manipulation.IncreaseAll => "Increase all",
                Manipulation.DecreaseAll => "Decrease all",
                Manipulation.ShiftLeft => "Shift left",
                Manipulation.ShiftRight => "Shift right",
                Manipulation.Insert => "Insert",
                Manipulation.AddCycle => "Add cycle",
                Manipulation.RemoveCycle => "Remove cycle",
                Manipulation.SingleCycle => "Single cycle",
                Manipulation.TimeReverse => "Time reverse",
                Manipulation.Dual => "Dual",
                Manipulation.Showerify => "Showerify",
                Manipulation.DeShowerify => "De-showerify",
                Manipulation.MakeOneHanded => "Make one handed",
                Manipulation.MakeTwoHanded => "Make two handed",
                Manipulation.ExtendUp => "Extend up",
                Manipulation.ExtendDown => "Extend down",
                Manipulation.TruncateHighest => "Truncate highest",
                Manipulation.TruncateLowest => "Truncate lowest",
                Manipulation.Reset => "Reset",
                _ => manipulation.ToString(),
            };
        }

    }
}
