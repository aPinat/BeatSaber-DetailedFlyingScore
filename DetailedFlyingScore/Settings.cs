using UnityEngine;

namespace DetailedFlyingScore
{
    internal static class Settings
    {
        //http://digitalnativestudios.com/textmeshpro/docs/rich-text
        internal const string BeforeCutOk = "<line-height=75%><alpha=#FF> _";
        internal const string BeforeCutNg = "<line-height=75%><alpha=#00> _";
        internal const string AfterCutOk = "<alpha=#FF>_\\n<line-height=18%><alpha=#FF>";
        internal const string AfterCutNg = "<alpha=#00>_\\n<line-height=18%><alpha=#FF>";
        internal const string CutDistanceOk = "\\n<alpha=#FF> __";
        internal const string CutDistanceNg = "\\n<alpha=#00> __";

        internal static Color GetTextColor(int score)
        {
            if (score >= 115) return Color.cyan;
            if (score >= 101) return Color.green;
            if (score >= 80) return Color.yellow;
            return score >= 60 ? new Color(1f, 0.5f, 0.0f, 1f) : Color.red;
        }
    }
}