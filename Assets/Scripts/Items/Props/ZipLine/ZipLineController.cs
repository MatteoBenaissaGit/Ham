using System.Reflection;
using DG.Tweening;
using UnityEngine;

namespace Items.Props.ZipLine
{
    public class ZipLineController : MonoBehaviour
    {
        [SerializeField] private Transform _zipLineStart;
        [SerializeField] private Transform _zipLineStartTip;
        [SerializeField] private Transform _zipLineEnd;
        [SerializeField] private Transform _zipLineEndTip;
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private int _lineResolution;

        private bool _canBeUsed;
        
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
            _zipLineStart.DOScale(baseScale, animationTime).SetEase(ease).OnComplete(CreateZipLine);
            
            _zipLineEnd.localScale = Vector3.zero;
            _zipLineEnd.DOKill();
            _zipLineEnd.DOScale(baseScale, animationTime).SetEase(ease);
        }

        private void CreateZipLine()
        {
            _lineRenderer.gameObject.SetActive(true);
            
            Vector3[] positions = new[]
            {
                _zipLineStartTip.position,
                _zipLineEndTip.position
            };
            _lineRenderer.SetPositions(positions);

            _canBeUsed = true;
        }
    }
}