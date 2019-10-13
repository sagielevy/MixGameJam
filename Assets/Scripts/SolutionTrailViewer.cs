using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(LineRenderer))]
    public class SolutionTrailViewer : MonoBehaviour
    {
        private LineRenderer lineRenderer;

        private void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        public void ClearTrail()
        {
            lineRenderer.SetPositions(new Vector3[] { });
        }

        public void GenerateSolution(Vector3[] originalSamples)
        {
            var samplesWithOffset = new Vector3[originalSamples.Length];
            originalSamples.CopyTo(samplesWithOffset, 0);

            for (int i = 0; i < samplesWithOffset.Length; i++)
            {
                samplesWithOffset[i] += transform.position;
            }

            lineRenderer.positionCount = originalSamples.Length;
            lineRenderer.SetPositions(originalSamples);
        }
    }
}
