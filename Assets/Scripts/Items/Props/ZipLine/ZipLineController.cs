using System.Reflection;
using UnityEngine;

namespace Items.Props.ZipLine
{
    public class ZipLineController : MonoBehaviour
    {
        [SerializeField] private Transform _zipLineStart;
        [SerializeField] private Transform _zipLineEnd;
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private float _lineHeight;
        [SerializeField] private int _lineResolution;

        public void Initialize(Transform start, Transform end)
        {
            _zipLineStart.position = start.position;
            _zipLineEnd.position = end.position;
            _zipLineStart.rotation = start.rotation;
            _zipLineEnd.rotation = end.rotation;

            Vector3[] positions = new[]
            {
                start.position + _zipLineStart.up * _lineHeight,
                end.position + _zipLineEnd.up * _lineHeight
            };
            _lineRenderer.SetPositions(positions);
        }
    }
}