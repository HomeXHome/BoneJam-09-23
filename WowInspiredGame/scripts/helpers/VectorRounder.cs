using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WowInspiredGame.scripts.helpers
{
    public static class VectorRounder
    {
        public static Vector3 RoundVector(Vector3 vector, int numberOfZeroes) {
            return new Vector3(MathF.Round(vector.X, numberOfZeroes),
                               MathF.Round(vector.Y, numberOfZeroes), 
                               MathF.Round(vector.Z, numberOfZeroes));
        }
    }
}
