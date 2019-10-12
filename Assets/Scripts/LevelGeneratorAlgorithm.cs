
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Assets.Scripts
{
    public class LevelGeneratorAlgorithm : ILevelGenerator
    {
        LevelConfiguration ILevelGenerator.GenerateLevel(int minItemCount, int maxItemCount,
            float worldRadius, float minRotationSpeed, float maxRotationSpeed, float minScale,
            float maxScale, float sphereRadius)
        {
            int objsAmount = Random.Range(minItemCount, maxItemCount);
            var levelObjects = new Dictionary<int, Item>();
            GenerateGameObjectsConfig(worldRadius, minRotationSpeed, maxRotationSpeed, 
                minScale, maxScale, objsAmount, levelObjects, sphereRadius);
            
            
            int solultionObjId = Random.Range(1, objsAmount + 1);
            var solutionPosition = generateRandVec3(worldRadius).normalized * sphereRadius;

            LevelConfiguration levelConfig = new LevelConfiguration();
            levelConfig.items = levelObjects.Values.ToArray();
            levelConfig.solutionItemId = solultionObjId;
            levelConfig.solutionStartPositionOnItem = solutionPosition;
            return levelConfig;
        }

        private void GenerateGameObjectsConfig(float worldRadius, float minRotationSpeed, float maxRotationSpeed, 
            float minScale, float maxScale, int objsAmount, Dictionary<int, Item> levelObjects, float sphereRadius)
        {
            for (int id = 1; id <= objsAmount; ++id)
            {
                Item currItem = new Item();
                currItem.id = id;
                int parentId = Random.Range(0, id);

                if (parentId == 0)
                {
                    currItem.parent = null;
                }
                else
                {
                    currItem.parent = levelObjects[parentId];
                }

                currItem.scale = Random.Range(minScale, maxScale);
                currItem.rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);

                if (currItem.parent == null)
                {
                    currItem.startingPosition = generateRandVec3(worldRadius);
                    currItem.aggregatedScale = currItem.scale;
                }
                else
                {
                    // Some big equation results in this
                    var normalizedDirection = generateRandVec3(worldRadius).normalized;
                    currItem.startingPosition = normalizedDirection * (currItem.scale + 1) * sphereRadius;
                    currItem.aggregatedScale = CalcAggregatedScale(currItem);
                }

                currItem.rotateDirection = generateRandVec3(worldRadius).normalized;
                levelObjects.Add(id, currItem);
            }
        }

        private float CalcAggregatedScale(Item item)
        {
            var scale = item.scale;
            var currParent = item.parent;

            while (currParent != null)
            {
                scale *= currParent.scale;
                currParent = currParent.parent;
            }

            return scale;
        }

        //private Item GenerateWorldConfig(float worldRadius, float minRotationSpeed, float maxRotationSpeed, Dictionary<int, Item> levelObjects)
        //{
        //    var worldConfig = new Item();
        //    worldConfig.id = 0;
        //    worldConfig.parent = worldConfig;
        //    worldConfig.rotateDirection = generateRandVec3(worldRadius).normalized;
        //    worldConfig.rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
        //    levelObjects.Add(worldConfig.id, worldConfig);
        //    return worldConfig;
        //}

        private Vector3 generateRandVec3(float worldRadius)
        {
            return new Vector3(Random.Range(0, worldRadius),
                Random.Range(0, worldRadius), Random.Range(0, worldRadius));
        }
    }
}