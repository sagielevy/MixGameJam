using UnityEngine;
using System;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private int minItemCount = 2;
        [SerializeField] private int maxItemCount = 2;
        [SerializeField] private float worldRadius = 10;
        [SerializeField] private float minRotationSpeed = 50;
        [SerializeField] private float maxRotationSpeed = 200;
        [SerializeField] private float minScale = 1;
        [SerializeField] private float maxScale = 1;
        [SerializeField] private int worldId;
        [SerializeField] private ItemAnimator world;
        [SerializeField] private LevelConfigurationReference configurationReference;

        // TODO add more prefabs if more prototype items
        [SerializeField] private ItemAnimator spherePrefab;

        private ILevelGenerator algorithm;
        
        private void Awake()
        {
            algorithm = new LevelGeneratorAlgorithm();
        }

        public void GenerateNewLevel()
        {
            var output = algorithm.GenerateLevel(minItemCount, maxItemCount, worldRadius,
                minRotationSpeed, maxRotationSpeed, minScale, maxScale);
            configurationReference.SetLevelConfiguration(output);
        }

        public void LoadGeneratedLevel()
        {
            var worldItemData = Array.Find(configurationReference.GetLevelConfiguration().items,
                item => item.id == worldId);
            world.SetItemData(worldId, worldItemData.rotationSpeed, worldItemData.rotateDirection);
            DestroyCurrentItems();
            CreateItems();
        }

        private void DestroyCurrentItems()
        {
            foreach (var item in world.GetComponentsInChildren<ItemAnimator>())
            {
                if (item != world)
                {
                    Destroy(item.gameObject);
                }
            }
        }
        
        // filth
        private int colorUsed = 0;
        List<Color> colors = new List<Color> { Color.red, Color.green, Color.blue, Color.yellow, Color.white, Color.magenta };

        private void CreateItems()
        {
            foreach (var item in configurationReference.GetLevelConfiguration().items)
            {
                var newItemObject = Instantiate(spherePrefab, Vector3.zero, Quaternion.identity);

                newItemObject.transform.localScale = new Vector3(item.scale, item.scale, item.scale);
                newItemObject.SetItemData(item.id, item.rotationSpeed, item.rotateDirection);

                var parent = Array.Find(world.GetComponentsInChildren<ItemAnimator>(),
                                                element => element.GetId() == item.parentId).gameObject;          
                newItemObject.transform.parent = parent.transform;
                newItemObject.transform.localPosition = item.startingPosition;

                
                if (parent == world.gameObject) {
                    newItemObject.GetComponent<MeshRenderer>().material.color = colors[colorUsed];
                    colorUsed++;
                } else {
                    newItemObject.GetComponent<MeshRenderer>().material.SetColor(
                        "son_color",
                        new Color(parent.GetComponent<MeshRenderer>().material.color.r + 15,
                        parent.GetComponent<MeshRenderer>().material.color.g,
                        parent.GetComponent<MeshRenderer>().material.color.b
  ));

                }
            }
        }
    }
}
