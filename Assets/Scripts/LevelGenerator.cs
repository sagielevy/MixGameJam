using UnityEngine;
using System;
namespace Assets.Scripts
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private int minItemCount = 1;
        [SerializeField] private int maxItemCount = 3;
        [SerializeField] private float worldRadius = 10;
        [SerializeField] private float minRotationSpeed = 50;
        [SerializeField] private float maxRotationSpeed = 200;
        [SerializeField] private float minScale = 1;
        [SerializeField] private float maxScale = 1;
        [SerializeField] private GameObject world;
        [SerializeField] private LevelConfigurationReference configurationReference;

        // TODO add more prefabs if more prototype items
        [SerializeField] private ItemAnimator spherePrefab;

        private ILevelGenerator algorithm;
        

        private void Awake()
        {
            algorithm = new LevelGeneratorAlgorithm();
        }

        public void GenerateLevel()
        {
            var output = algorithm.GenerateLevel(minItemCount, maxItemCount, worldRadius,
                minRotationSpeed, maxRotationSpeed, minScale, maxScale);
            configurationReference.SetLevelConfiguration(output);

            DestroyCurrentItems();
            CreateItems();
        }

        private void DestroyCurrentItems()
        {
            foreach (var item in world.GetComponentsInChildren<ItemAnimator>())
            {
                Destroy(item.gameObject);
            }
        }

        private void CreateItems()
        {
            
            foreach (var item in configurationReference.GetLevelConfiguration().items)
            {
                var newItemObject = Instantiate(spherePrefab, Vector3.zero, Quaternion.identity);

                newItemObject.transform.localScale = new Vector3(item.scale, item.scale, item.scale);
                newItemObject.SetItemData(item.id, item.rotationSpeed, item.rotateDirection);

                GameObject parent = Array.Find(world.GetComponentsInChildren<ItemAnimator>(),
                                                element => element.GetId() == item.parentId).gameObject;          
                newItemObject.transform.parent = parent.transform;
                newItemObject.transform.localPosition = item.startingPosition; ;
            }
        }
    }
}
