using UnityEngine;

namespace Assets.Scripts
{
    public class LevelGenerator : MonoBehaviour
    {
        public int minItemCount = 1;
        public int maxItemCount = 3;
        public float worldRadius = 10;
        public float minRotationSpeed = 50;
        public float maxRotationSpeed = 200;
        public float minScale = 1;
        public float maxScale = 1;

        private LevelGeneratorAlgorithm algorithm;
    }
}
