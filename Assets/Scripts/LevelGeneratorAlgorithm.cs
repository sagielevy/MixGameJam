
using System.Collections.Generic;
using System.Linq;
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
            var levelObjects = new Dictionary<int, Item>();
            GenerateGameObjectsConfig(worldRadius, minRotationSpeed, maxRotationSpeed, minScale, maxScale, objsAmount, levelObjects);
            
            int solultionObjId;
            Vector3 solutionPosition;
            GenerateSolutionConfig(objsAmount, levelObjects, out solultionObjId, out solutionPosition);

            LevelConfiguration levelConfig = new LevelConfiguration();
            levelConfig.items = levelObjects.Values.ToArray();
            levelConfig.solutionItemId = solultionObjId;
            levelConfig.solutionStartPosition = solutionPosition;
            return levelConfig;
        }

        private static void GenerateSolutionConfig(int objsAmount, Dictionary<int, Item> levelObjects, out int solultionObjId, out Vector3 solutionPosition)
        {
            solultionObjId = Random.Range(1, objsAmount + 1);
            float theta = Random.Range(0.0f, 180.0f);
            float phi = Random.Range(0.0f, 360.0f);
            float radius = levelObjects[solultionObjId].scale / 2;
            float xVal = radius * Mathf.Sin(theta) * Mathf.Cos(phi);
            float yVal = radius * Mathf.Sin(theta) * Mathf.Sin(phi);
            float zVal = radius * Mathf.Cos(theta);
            solutionPosition = new Vector3(xVal, yVal, zVal);
        }

        private void GenerateGameObjectsConfig(float worldRadius, float minRotationSpeed, float maxRotationSpeed, float minScale, float maxScale, int objsAmount, Dictionary<int, Item> levelObjects)
        {
            for (int id = 1; id <= objsAmount; ++id)
            {
                Item obj = new Item();
                obj.id = id;
                int parentId = Random.Range(0, id);
                if (parentId == 0)
                {
                    obj.parent = null;
                }
                else
                {
                    obj.parent = levelObjects[parentId];
                }
                obj.scale = Random.Range(minScale, maxScale);
                obj.rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
                if (obj.parent == null)
                {
                    obj.startingPosition = generateRandVec3(worldRadius);
                }
                else
                {
                    obj.startingPosition = new Vector3((obj.scale + obj.parent.scale) / 2.0f, 0, 0);
                }
                obj.rotateDirection = generateRandVec3(worldRadius);
                levelObjects.Add(id, obj);
            }
        }

        private Item GenerateWorldConfig(float worldRadius, float minRotationSpeed, float maxRotationSpeed, Dictionary<int, Item> levelObjects)
        {
            var worldConfig = new Item();
            worldConfig.id = 0;
            worldConfig.parent = worldConfig;
            worldConfig.rotateDirection = generateRandVec3(worldRadius);
            worldConfig.rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
            levelObjects.Add(worldConfig.id, worldConfig);
            return worldConfig;
        }

        private Vector3 generateRandVec3(float worldRadius)
        {
            return new Vector3(Random.Range(0, worldRadius),
                Random.Range(0, worldRadius), Random.Range(0, worldRadius));
        }
    }
}