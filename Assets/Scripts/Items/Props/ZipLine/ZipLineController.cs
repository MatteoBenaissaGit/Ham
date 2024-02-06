using System.Reflection;
using DG.Tweening;
using UnityEngine;

namespace Items.Props.ZipLine
{
    public class ZipLineController : MonoBehaviour
    {
        [field:SerializeField] public Transform ZipLineStartTip { get; private set; }
        [field:SerializeField] public Transform ZipLineEndTip { get; private set; }
        
        public bool CanBeUsed { get; private set; }

        [SerializeField] private Transform _zipLineStart;
        [SerializeField] private Transform _zipLineEnd;
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private int _lineResolution;

        public void Initialize(Transform start, Transform end)
        {
            _zipLineStart.position = start.position;
            _zipLineEnd.position = end.position;
            _zipLineStart.rotation = start.rotation;
            _zipLineEnd.rotation = end.rotation;

            _lineRenderer.gameObject.SetActive(false);

            float animationTime = 0.5f;
            Ease ease = Ease.OutBounce;
            
            Vector3 baseScale = _zipLineStart.localScale;
            
            _zipLineStart.localScale = Vector3.zero;
            _zipLineStart.DOKill();
            _zipLineStart.DOScale(baseScale, animationTime).SetEase(ease);
            
            _zipLineEnd.localScale = Vector3.zero;
            _zipLineEnd.DOKill();
            _zipLineEnd.DOScale(baseScale, animationTime).SetEase(ease).OnComplete(CreateZipLine);
        }

        private void CreateZipLine()
        {
            _lineRenderer.gameObject.SetActive(true);
            
            Vector3[] positions = new[]
            {
                ZipLineStartTip.position,
                ZipLineEndTip.position
            };
            _lineRenderer.SetPositions(positions);

            CanBeUsed = true;
        }
    }
}