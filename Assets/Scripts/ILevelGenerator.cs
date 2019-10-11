using UnityEngine;

namespace Assets.Scripts
{
    public interface ILevelGenerator
    {
        LevelConfiguration GenerateLevel(int minItemCount,
                                         int maxItemCount,
                                         float worldRadius,
                                         float minRotationSpeed,
                                         float maxRotationSpeed,
                                         float minScale,
                                         float maxScale);
    }
}
