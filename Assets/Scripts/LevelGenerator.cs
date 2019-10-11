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
        public float animateTimeSeconds = 10;

        private LevelGeneratorAlgorithm algorithm;

        private void Start()
        {
            algorithm = new LevelGeneratorAlgorithm();
            GenerateLevel();
        }

        private void GenerateLevel()
        {
            var output = algorithm.ItemPositions(minItemCount, maxItemCount, worldRadius,
                minRotationSpeed, maxRotationSpeed, minScale, maxScale);
        }

        private void Update()
        {

        }
    }
}
