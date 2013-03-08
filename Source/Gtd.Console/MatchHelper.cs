namespace Gtd.Shell
{
    using System;

    public static class MatchHelper
    {
        public static string Shorten(this Guid target)
        {
            return target.ToString().ToLowerInvariant().Replace("-", "").Substring(0, 3);
        }

        public static bool Matches(this Guid target, string match)
        {
            return target.ToString().ToLowerInvariant().Replace("-", "").StartsWith(match);
        }
    }
}
