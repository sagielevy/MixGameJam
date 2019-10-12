
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
            
            // Create World Object
            var worldConfig = new Item();
            worldConfig.id = 0;
            worldConfig.parent = worldConfig;
            worldConfig.rotateDirection = generateRandVec3(worldRadius);
            worldConfig.rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
            levelObjects.Add(worldConfig.id, worldConfig);

            int id = 1;
            while (id <= objsAmount)
            {
                Item obj = new Item();
                obj.id = id;
                obj.parent = levelObjects[Random.Range(0, id)];
                obj.scale = Random.Range(minScale, maxScale);
                obj.rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
                if (obj.parent == worldConfig)
                {
                    obj.startingPosition = generateRandVec3(worldRadius);
                }
                else
                {
                    obj.startingPosition = new Vector3((obj.scale + obj.parent.scale) / 2, 0, 0);
                }
                obj.rotateDirection = generateRandVec3(worldRadius);
                levelObjects.Add(id++, obj);
            }

            int solultionObjId = Random.Range(1, objsAmount+1);
            float theta = Random.Range(0.0f, 180.0f);
            float phi = Random.Range(0.0f, 360.0f);
            float radius = levelObjects[solultionObjId].scale / 2;
            float xVal = radius * Mathf.Sin(theta) * Mathf.Cos(phi);
            float yVal = radius * Mathf.Sin(theta) * Mathf.Sin(phi);
            float zVal = radius * Mathf.Cos(theta);
            Vector3 solutionPosition = new Vector3(xVal,yVal,zVal);
            
            LevelConfiguration levelConfig = new LevelConfiguration();
            levelConfig.items = levelObjects.Values.ToArray();
            levelConfig.solutionItemId = solultionObjId;
            levelConfig.solutionStartPosition = solutionPosition;
            return levelConfig;
        }
        private Vector3 generateRandVec3(float worldRadius)
        {
            return new Vector3(Random.Range(0, worldRadius),
                Random.Range(0, worldRadius), Random.Range(0, worldRadius));
        }
    }
}