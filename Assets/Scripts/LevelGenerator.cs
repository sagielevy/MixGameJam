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
        [SerializeField] private Transform world;
        [SerializeField] private LevelConfigurationReference configurationReference;

        [SerializeField] private ArrowHintRotator arrowsPrefab;
        [SerializeField] private ItemAnimator spherePrefab;

        [SerializeField] private float colorGain;

        private ILevelGenerator algorithm;
        private List<ArrowHintRotator> rotators;
        private List<ItemAnimator> items;

        List<Color> colors;

        private void Awake()
        {
            algorithm = new LevelGeneratorAlgorithm();
            rotators = new List<ArrowHintRotator>();
            items = new List<ItemAnimator>();
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
            DestroyCurrentItems();
            CreateItems();
        }

        private void DestroyCurrentItems()
        {
            foreach (var item in items) 
            {
                Destroy(item.gameObject);
            }

            foreach (var arrowHintRotator in rotators)
            {
                Destroy(arrowHintRotator.gameObject);
            }

            rotators.Clear();
            items.Clear();
        }


        private void CreateItems()
        {
            int colorUsed = 0;

            foreach (var item in configurationReference.GetLevelConfiguration().items)
            {
                var newItemObject = Instantiate(spherePrefab, Vector3.zero, Quaternion.identity);
                items.Add(newItemObject);
                newItemObject.transform.localScale = new Vector3(item.scale, item.scale, item.scale);
                Transform parent = null;
                
                if (item.parent != null)
                {
                    parent = items.Find(element => element.GetId() == item.parent.id).transform; 
                }
                else
                {
                    parent = world;
                }

                newItemObject.transform.parent = parent;
                newItemObject.transform.localPosition = item.startingPosition;

                ArrowHintRotator arrowsHint = null;
                arrowsHint = Instantiate(arrowsPrefab, Vector3.zero, Quaternion.identity);
                arrowsHint.transform.position = newItemObject.transform.position;
                arrowsHint.transform.rotation = Quaternion.AngleAxis(90, item.rotateDirection);
                arrowsHint.transform.localScale *= item.scale;
                rotators.Add(arrowsHint);
                newItemObject.SetItemData(item.id, item.rotationSpeed, item.rotateDirection, arrowsHint);

                if (parent == world)
                {
                    newItemObject.GetComponent<MeshRenderer>().material.color = colors[colorUsed];
                    colorUsed++;
                }
                else
                {
                    newItemObject.GetComponent<MeshRenderer>().material.color =
                        parent.GetComponent<MeshRenderer>().material.color * colorGain;
                }
            }
        }
    }
}
