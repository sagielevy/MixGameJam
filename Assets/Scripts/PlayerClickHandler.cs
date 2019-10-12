using Assets.Scripts.SharedData;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerClickHandler : MonoBehaviour
    {
        [SerializeField] private LevelConfigurationReference configurationReference;
        [SerializeField] private BooleanReference animate;
        [SerializeField] private BooleanReference levelPlayable;
        [SerializeField] private LevelHandler levelHandler;
        [SerializeField] private TrailSpawner prefab;

        private float clickStartTime;
        private TrailSpawner currSpawner;
        private bool wasAnimating;

        private void FixedUpdate()
        {
            if (wasAnimating && !animate.GetValue())
            {
                wasAnimating = false;

                Debug.Log($"Real sampled {currSpawner.GetSampledLocations().Count}, samples:");
                foreach (var sampledLocation in currSpawner.GetSampledLocations())
                {
                    Debug.Log(sampledLocation);
                }

                levelHandler.AttemptPlayerSolution(currSpawner);
            }

            if (Input.GetMouseButton(0))
            {
                HandlePlayerClick();
            }
        }

        private void HandlePlayerClick()
        {
            // Ignore clicks if not allowed
            if (animate.GetValue() || !levelPlayable.GetValue())
            {
                return;
            }

            var clickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(clickRay.origin, clickRay.direction, out var hit))
            {
                ClearOldTrail();
                StartTrail(hit);
            }
        }

        private void ClearOldTrail()
        {
            if (currSpawner == null) return;

            Destroy(currSpawner.gameObject);
            currSpawner = null;
        }

        private void StartTrail(RaycastHit hit)
        {
            currSpawner = Instantiate(prefab, hit.point, Quaternion.identity, hit.transform);
            clickStartTime = Time.fixedTime;
            animate.SetValue(true);
            wasAnimating = true;
        }
    }
}
