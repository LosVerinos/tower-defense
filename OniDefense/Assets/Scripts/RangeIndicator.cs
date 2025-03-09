using UnityEngine;

namespace Game
{
    public class RangeIndicator : MonoBehaviour
    {
        private GameObject maxRangeIndicator;
        private GameObject minRangeIndicator;

        public void ShowRange(float maxRange, float minRange, Transform parent)
        {
            if (maxRangeIndicator != null && minRangeIndicator != null)
            {
                Destroy(maxRangeIndicator);
                Destroy(minRangeIndicator);
            }

            maxRangeIndicator = CreateRangeCircle("MaxRangeIndicator", maxRange, Color.green, parent);
            minRangeIndicator = CreateRangeCircle("MinRangeIndicator", minRange, Color.red, parent);

            maxRangeIndicator.SetActive(true);
            minRangeIndicator.SetActive(true);
        }

        public void HideRange()
        {
            if (maxRangeIndicator != null) maxRangeIndicator.SetActive(false);
            if (minRangeIndicator != null) minRangeIndicator.SetActive(false);
        }

        private GameObject CreateRangeCircle(string name, float range, Color color, Transform parent)
        {
            GameObject rangeObj = new GameObject(name);
            rangeObj.transform.SetParent(parent);
            rangeObj.transform.localPosition = Vector3.zero;

            LineRenderer lineRenderer = rangeObj.AddComponent<LineRenderer>();
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.startColor = new Color(color.r, color.g, color.b, 0.5f);
            lineRenderer.endColor = new Color(color.r, color.g, color.b, 0.5f);
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
            lineRenderer.loop = true;
            lineRenderer.useWorldSpace = false;

            int segments = 50;
            lineRenderer.positionCount = segments + 1;
            float angleStep = 360f / segments;

            for (int i = 0; i <= segments; i++)
            {
                float angle = Mathf.Deg2Rad * angleStep * i;
                lineRenderer.SetPosition(i, new Vector3(Mathf.Cos(angle) * range, 0, Mathf.Sin(angle) * range));
            }

            return rangeObj;
        }
    }
}
