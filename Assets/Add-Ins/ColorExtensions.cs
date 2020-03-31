using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReActionGames.Extensions
{
    public static class ColorExtensions
    {

        public static Color With(this Color original, float? r = null, float? g = null, float? b = null, float? a = null)
        {
            return new Color(r ?? original.r, g ?? original.g, b ?? original.b, a ?? original.a);
        }
    }
}