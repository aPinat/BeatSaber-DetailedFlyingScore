using System.Collections.Generic;
using HarmonyLib;
using TMPro;
using UnityEngine;

namespace DetailedFlyingScore;

[HarmonyPatch(typeof(FlyingScoreEffect))]
internal class FlyingScoreEffectPatch
{
    [HarmonyPatch("InitAndPresent")]
    [HarmonyPostfix]
    private static void InitAndPresentPostfix(ISaberSwingRatingCounter ____saberSwingRatingCounter, float ____cutDistanceToCenter, TextMeshPro ____text,
        SpriteRenderer ____maxCutDistanceScoreIndicator, ref Color ____color)
    {
        ____text.richText = true;
        ____maxCutDistanceScoreIndicator.enabled = false;
        SetFlyingScore(____saberSwingRatingCounter, ____cutDistanceToCenter, ____text, ref ____color);
    }

    [HarmonyPatch("HandleSaberSwingRatingCounterDidChange")]
    [HarmonyPostfix]
    private static void HandleSaberSwingRatingCounterDidChangeEventPostfix(ISaberSwingRatingCounter ____saberSwingRatingCounter, float ____cutDistanceToCenter, TextMeshPro ____text,
        ref Color ____color)
    {
        SetFlyingScore(____saberSwingRatingCounter, ____cutDistanceToCenter, ____text, ref ____color);
    }

    private static void SetFlyingScore(ISaberSwingRatingCounter ____saberSwingRatingCounter, float ____cutDistanceToCenter, TextMeshPro ____text, ref Color ____color)
    {
        ScoreModel.RawScoreWithoutMultiplier(____saberSwingRatingCounter, ____cutDistanceToCenter, out var beforeCutRawScore, out var afterCutRawScore, out var cutDistanceRawScore);

        var beforeCut = beforeCutRawScore == 70 ? Settings.BeforeCutOk : Settings.BeforeCutNg;
        var afterCut = afterCutRawScore == 30 ? Settings.AfterCutOk : Settings.AfterCutNg;
        var score = beforeCutRawScore + afterCutRawScore + cutDistanceRawScore;
        var cutDistance = cutDistanceRawScore == 15 ? Settings.CutDistanceOk : Settings.CutDistanceNg;

        var charList = new List<char>();
        charList.AddRange(beforeCut);
        charList.AddRange(afterCut);
        charList.AddRange(score.ToString());
        charList.AddRange(cutDistance);

        ____text.SetCharArray(charList.ToArray());
        ____color = Settings.GetTextColor(score);
    }
}