using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WowInspiredGame.scripts.helpers
{
    public static class LerpHelper
    {
        public static Vector3 LerpVector3(Vector3 valueToLerp, Vector3 valueToLerpTo, float weight) => new Vector3(
        Mathf.Lerp(valueToLerp.X, valueToLerpTo.X, weight),
        Mathf.Lerp(valueToLerp.Y, valueToLerpTo.Y, weight),
        Mathf.Lerp(valueToLerp.Z, valueToLerpTo.Z, weight)
        );
    }
}
