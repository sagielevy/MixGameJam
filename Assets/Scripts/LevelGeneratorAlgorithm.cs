
using UnityEngine;


namespace Assets.Scripts
{
    public class LevelGeneratorAlgorithm : ILevelGenerator
    {
        LevelConfiguration ILevelGenerator.GenerateLevel(int minItemCount, int maxItemCount,
            float worldRadius, float minRotationSpeed, float maxRotationSpeed, float minScale,
            float maxScale)
        {
            int objsAmount = Random.Range(minItemCount, maxItemCount + 1);
            Item[] levelObjects = new Item[objsAmount];

            for (int i = 0; i <= objsAmount; i++)
            {
                Item obj = new Item();
                obj.id = i;
                obj.parentId = Random.Range(0, i);
                obj.scale = Random.Range(minScale, maxScale);
                obj.rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
                obj.startingPosition = new Vector3(Random.Range(0, worldRadius),
                    Random.Range(0, worldRadius), Random.Range(0, worldRadius));
                obj.rotateDirection = new Vector3(Random.Range(0, worldRadius),
                    Random.Range(0, worldRadius), Random.Range(0, worldRadius));
                levelObjects[i] = obj;
            }

            Vector3 solutionPosition = new Vector3();
            LevelConfiguration levelConfig = new LevelConfiguration();
            levelConfig.items = levelObjects;
            levelConfig.solutionStartPosition = solutionPosition;
            return levelConfig;
        }
    }
}