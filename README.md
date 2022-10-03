# SiteswapExplorerLib

This is the siteswap calculation code used by https://siteswapexplorer.com/

I wanted to share the code between the website and the mobile app versions which I'm also working on so I've split it out this way. 

I don't know right know the whats, whens and hows of sharing the other stuff.

Here's a bit of code showing how to access the most important things:

            // create a siteswap instance. Will return null if invalid
            var ss = Siteswap.Create("534");
            if (ss != null)
            {
                // get list of possible manipulations for the whole pattern
                // so that'll be things like showerify, increaseall, sequences, etc
                var patternManipulations = ss.PossibleManipulations();

                // get list of manipulations for the first throw
                // so that'll be things like add period, delete
                var throwManipulations = ss.PossibleManipulations(0);

                // get list of swaps for the second digit
                var swaps = ss.PossibleSwaps(1);

            }
