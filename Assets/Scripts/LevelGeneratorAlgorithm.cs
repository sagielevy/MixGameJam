
using UnityEngine;


namespace Assets.Scripts
{
    public class LevelGeneratorAlgorithm : ILevelGenerator
    {
        LevelConfiguration ILevelGenerator.GenerateLevel(int minItemCount, int maxItemCount,
            float worldRadius, float minRotationSpeed, float maxRotationSpeed, float minScale,
            float maxScale)
        {
            int objsAmount = Random.Range(minItemCount, maxItemCount);
            
            Item[] levelObjects = new Item[objsAmount];

            for (int i = 0; i < objsAmount; i++)
            {
                Item obj = new Item();
                obj.id = i;
                obj.parentId = Random.Range(0, i);
                obj.scale = Random.Range(minScale, maxScale);
                obj.rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);

                if (obj.parentId == 0)
                {
                    obj.startingPosition = new Vector3(Random.Range(0, worldRadius),
                        Random.Range(0, worldRadius), Random.Range(0, worldRadius));
                } else{
                    obj.startingPosition = new Vector3((obj.scale + levelObjects[obj.parentId].scale) / 2, 0, 0);
                }
                obj.rotateDirection = new Vector3(Random.Range(0, worldRadius),
                    Random.Range(0, worldRadius), Random.Range(0, worldRadius));
                levelObjects[i] = obj;
            }

            int solultionObj = Random.Range(0, objsAmount);
            float theta = Random.Range(0.0f, 180.0f);
            float phi = Random.Range(0.0f, 360.0f);
            float radius = levelObjects[solultionObj].scale / 2;
            float xVal = radius * Mathf.Sin(theta) * Mathf.Cos(phi);
            float yVal = radius * Mathf.Sin(theta) * Mathf.Sin(phi);
            float zVal = radius * Mathf.Cos(theta);
            Vector3 solutionPosition = new Vector3(xVal,yVal,zVal);
            
            LevelConfiguration levelConfig = new LevelConfiguration();
            levelConfig.items = levelObjects;
            levelConfig.solutionItemId = solultionObj;
            levelConfig.solutionStartPosition = solutionPosition;
            return levelConfig;
        }
    }
}