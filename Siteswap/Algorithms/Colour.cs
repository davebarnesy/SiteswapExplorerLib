using System;
using System.Linq;

namespace SiteswapLib
{
    public partial class Siteswap
    {
        public static Colour OrbitColour(int orbit)
        {
            if (orbit < 0)
            {
                return new Colour() { name = "white", value = "ffffff" };
            }
            return Colours[orbit % Colours.Count()];
        }

        public static Colour[] Colours
        {
            get
            {
                return new Colour[] {
                    new Colour() { name = "red", value = "ff0900" },
                    new Colour() { name = "blue", value = "4343ff" },
                    new Colour() { name = "yellow", value = "e3c710" },
                    new Colour() { name = "green", value = "3cb44b" },
                    new Colour() { name = "orange", value = "f58231" },
                    new Colour() { name = "purple", value = "911eb4" },
                    new Colour() { name = "cyan", value = "42d4f4" },
                    new Colour() { name = "magenta", value = "f032e6" },
                    new Colour() { name = "lime", value = "bfef45" },
                    new Colour() { name = "teal", value = "469990" },
                    new Colour() { name = "mint", value = "aaffc3" },
                    new Colour() { name = "olive", value = "808000" },
                };
            }
        }
    }
}
