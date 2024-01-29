using Unity.Mathematics;
using UnityEngine;
using CharacterController = Character.CharacterController;

namespace Items.AimBehaviours
{
    public class ZiplineAimBehaviour : ItemAimBehaviour
    {
        private Transform _basePreviewParent;
        private Quaternion _desiredZipLineEndPreviewRotation;
        private bool _isPlacementValid;
        
        public ZiplineAimBehaviour(ItemController item) : base(item)
        {
            _basePreviewParent = item.PreviewMeshes[0].transform.parent;

            _previewStartMeshRenderer = Item.PreviewMeshes[0].GetComponentInChildren<MeshRenderer>();
            _previewEndMeshRenderer = Item.PreviewMeshes[1].GetComponentInChildren<MeshRenderer>();
        }

        public override void Aim(bool doAim)
        {
            MakeCameraAim(doAim);

            if (doAim == false)
            {
                SetPreviewActives(false);
            }

            SetPreviewParents(doAim ? null : _basePreviewParent);
        }

        private RaycastHit[] _hits = new RaycastHit[5];
        public override void AimStay()
        {
            SetZipLinePreview();
        }

        public override void Shoot()
        {
            
        }

        private void SetPreviewActives(bool isActive)
        {
            Item.PreviewMeshes[0].SetActive(isActive);
            Item.PreviewMeshes[1].SetActive(isActive);
        }

        private void SetPreviewParents(Transform parent)
        {
            Item.PreviewMeshes[0].transform.parent = parent;
            Item.PreviewMeshes[1].transform.parent = parent;
        }

        private MeshRenderer _previewEndMeshRenderer;
        private MeshRenderer _previewStartMeshRenderer;
        private void SetPreviewMaterials(Material material)
        {
            _previewStartMeshRenderer.material = material;
            _previewEndMeshRenderer.material = material;
        }
        
        /// <summary>
        /// Raycast and show the zipline placement previews
        /// </summary>
        private void SetZipLinePreview()
        {
            Item.PreviewMeshes[0].transform.rotation = Quaternion.Lerp(Item.PreviewMeshes[0].transform.rotation, _desiredZipLineEndPreviewRotation, 0.1f);

            Vector3 origin = CharacterController.Instance.CameraController.Camera.transform.position;
            Vector3 direction = CharacterController.Instance.CameraController.Camera.transform.forward;
            int hitAmount = Physics.RaycastNonAlloc(origin, direction, _hits);

            RaycastHit closestHit = default;
            float closestHitDistance = float.MaxValue;
            bool didHit = false;

            for (int i = hitAmount - 1; i >= 0; i--)
            {
                if (_hits[i].collider == null || _hits[i].collider.isTrigger)
                {
                    continue;
                }

                float distance = Vector3.Distance(CharacterController.Instance.CameraController.Camera.transform.position,
                    _hits[i].point);
                if (distance > closestHitDistance)
                {
                    continue;
                }

                closestHitDistance = distance;
                closestHit = _hits[i];
                didHit = true;
            }

            if (didHit == false)
            {
                SetPreviewActives(false);
                return;
            }

            SetPreviewActives(true);
            Item.PreviewMeshes[0].transform.position = closestHit.point;

            Vector3 normal = closestHit.normal.normalized;
            Quaternion rotation = Quaternion.LookRotation(normal);
            Vector3 eulerAngles = rotation.eulerAngles;
            _desiredZipLineEndPreviewRotation = Quaternion.Euler(eulerAngles);

            SetCloseZipLinePreview();

            _isPlacementValid = IsZipLinePlacementValid();
        }

        private void SetCloseZipLinePreview()
        {
            Transform character = CharacterController.Instance.Mesh.transform;
            
            float distance = 5f;
            Vector3 origin = character.position + character.forward * distance + character.up;
            if (Physics.Raycast(origin, -character.up, out RaycastHit hit) == false)
            {
                return;
            }

            Transform previewMesh = Item.PreviewMeshes[1].transform;
            previewMesh.position = hit.point;
            
            Vector3 normal = hit.normal.normalized;
            Quaternion rotation = Quaternion.LookRotation(normal);
            Vector3 eulerAngles = rotation.eulerAngles;
            previewMesh.rotation = Quaternion.Euler(eulerAngles);
        }

        private const float DistanceToValidate = 30f;
        private bool IsZipLinePlacementValid()
        {
            float distanceBetweenPreviews = Vector3.Distance(Item.PreviewMeshes[0].transform.position, Item.PreviewMeshes[1].transform.position);

            bool isValid = distanceBetweenPreviews < DistanceToValidate;
            SetPreviewMaterials(isValid ? Item.PreviewMaterialError : Item.PreviewMaterial);
            return isValid;
        }
    }
}