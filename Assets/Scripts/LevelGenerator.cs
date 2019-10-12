using UnityEngine;
using System;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private int minItemCount;
        [SerializeField] private int maxItemCount;
        [SerializeField] private float worldRadius;
        [SerializeField] private float minRotationSpeed;
        [SerializeField] private float maxRotationSpeed;
        [SerializeField] private float minScale;
        [SerializeField] private float maxScale;
        [SerializeField] private int worldId;
        [SerializeField] private ItemAnimator world;
        [SerializeField] private LevelConfigurationReference configurationReference;

        private int colorUsed;
        List<Color> colors;

        [SerializeField] private ArrowHintRotator arrowsPrefab;
        [SerializeField] private ItemAnimator spherePrefab;

        private ILevelGenerator algorithm;
        private List<ArrowHintRotator> rotators;


        private void Awake()
        {
            algorithm = new LevelGeneratorAlgorithm();
            rotators = new List<ArrowHintRotator>();
            colors = new List<Color> { Color.red, Color.green, Color.blue, Color.yellow, Color.white, Color.magenta };
        }

        public void GenerateNewLevel()
        {
            var levelConfig = algorithm.GenerateLevel(minItemCount, maxItemCount, worldRadius,
                minRotationSpeed, maxRotationSpeed, minScale, maxScale);
            configurationReference.SetLevelConfiguration(levelConfig);
        }

        public void LoadGeneratedLevel()
        {
            var worldItemData = Array.Find(configurationReference.GetLevelConfiguration().items,
                                item => item.id == worldId);
            world.SetItemData(worldId, worldItemData.rotationSpeed, worldItemData.rotateDirection, null);
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

            foreach (var arrowHintRotator in rotators)
            {
                Destroy(arrowHintRotator.gameObject);
            }

            rotators.Clear();
        }
        
        private void CreateItems()
        {
            colorUsed = 0;
            foreach (var item in configurationReference.GetLevelConfiguration().items)
            {
                var newItemObject = Instantiate(spherePrefab, Vector3.zero, Quaternion.identity);
                newItemObject.transform.localScale = new Vector3(item.scale, item.scale, item.scale);

                var parent = Array.Find(world.GetComponentsInChildren<ItemAnimator>(),element => element.GetId() == item.parent.id).gameObject;          
                newItemObject.transform.parent = parent.transform;
                newItemObject.transform.localPosition = item.startingPosition;


                ArrowHintRotator arrowsHint = null;

                if (item.id != worldId)
                {
                    arrowsHint = Instantiate(arrowsPrefab, Vector3.zero, Quaternion.identity);
                    arrowsHint.transform.position = newItemObject.transform.position;
                    arrowsHint.transform.localRotation = newItemObject.transform.localRotation;
                    arrowsHint.transform.localScale *= item.scale;
                    arrowsHint.SetRotateDir(item.rotateDirection);
                    rotators.Add(arrowsHint);
                }

                newItemObject.SetItemData(item.id, item.rotationSpeed, item.rotateDirection, arrowsHint);

                if (parent == world.gameObject)
                {
                    newItemObject.GetComponent<MeshRenderer>().material.color = colors[colorUsed];
                    colorUsed++;
                }
                else
                {
                    newItemObject.GetComponent<MeshRenderer>().material.SetColor(
                        "son_color",
                        new Color(parent.GetComponent<MeshRenderer>().material.color.r + 15,
                        parent.GetComponent<MeshRenderer>().material.color.g,
                        parent.GetComponent<MeshRenderer>().material.color.b));

                }
            }
        }
    }
}
