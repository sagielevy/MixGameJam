using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public interface ITrail
    {
        IEnumerable<Vector3> GetSampledLocations();
    }
}
