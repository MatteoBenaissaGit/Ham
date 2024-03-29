﻿using Character;
using Common;
using Items.Props.ZipLine;
using UnityEngine;
using CharacterController = Character.CharacterController;

namespace Items.UseBehaviours
{
    public class ZiplineUseBehaviour : ItemUseBehaviour
    {
        private Transform _basePreviewParent;
        private Quaternion _desiredZipLineEndPreviewRotation;
        private bool _isPlacementValid;
        private MeshRenderer _previewEndMeshRenderer;
        private MeshRenderer _previewStartMeshRenderer;
        
        public ZiplineUseBehaviour(ItemController item) : base(item)
        {
            _basePreviewParent = item.PreviewMeshes[0].transform.parent;

            _previewStartMeshRenderer = Item.PreviewMeshes[0].GetComponentInChildren<MeshRenderer>();
            _previewEndMeshRenderer = Item.PreviewMeshes[1].GetComponentInChildren<MeshRenderer>();
        }

        public override void Initialize()
        {
        }

        public override void Update()
        {
            
        }

        public override void FixedUpdate()
        {
            
        }

        public override void Quit()
        {
        }

        public override void AimBehaviour(bool doAim)
        {
            MakeCameraAim(doAim);

            if (doAim == false)
            {
                SetPreviewActives(false);
            }

            SetPreviewParents(doAim ? null : _basePreviewParent);
            CharacterController.Instance.OnCharacterAction.Invoke(doAim ? CharacterGameplayAction.Aim : CharacterGameplayAction.StopAim);
        }

        public override void AimStayBehaviour()
        {
            SetZipLinePreview();
        }

        public override void ShootBehaviour(bool isShooting)
        {
            if (IsZipLinePlacementValid() == false)
            {
                return;
            }
            
            ZipLineController zipLine = ResourceManager.Instance.InstantiateResource(ResourceEnum.ZipLineController).GetComponent<ZipLineController>();
            zipLine.Initialize(Item.PreviewMeshes[0].transform, Item.PreviewMeshes[1].transform);

            Item.UseBehaviour?.AimBehaviour(false);
            
            Item.Destroy();
        }

        public override void ShootOnceBehaviour()
        {
            
        }

        private void SetPreviewActives(bool isActive)
        {
            if (Item.PreviewMeshes[0] == null || Item.PreviewMeshes[0].transform == null)
            {
                return;
            }
            Item.PreviewMeshes[0].SetActive(isActive);
            Item.PreviewMeshes[1].SetActive(isActive);
        }

        private void SetPreviewParents(Transform parent)
        {
            if (Item.PreviewMeshes[0] == null || Item.PreviewMeshes[0].transform == null)
            {
                return;
            }
            Item.PreviewMeshes[0].transform.parent = parent;
            Item.PreviewMeshes[1].transform.parent = parent;
        }
        
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

            RaycastHit? hit = GetClosestRaycastHitTowardCamera(() => SetPreviewActives(false));
            if (hit == null)
            {
                return;
            }

            SetPreviewActives(true);
            Item.PreviewMeshes[0].transform.position = hit.Value.point;

            Vector3 normal = hit.Value.normal.normalized;
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

        private const float DistanceToValidate = 25f;
        private const float DistanceMaxToValidateBetweenStartAndCharacter = 10f;
        private const float DotProductToValidate = 0.5f;
        private bool IsZipLinePlacementValid()
        {
            float distanceBetweenPreviews = Vector3.Distance(Item.PreviewMeshes[0].transform.position, Item.PreviewMeshes[1].transform.position);
            float dotProductBetweenPreviews = Vector3.Dot(Item.PreviewMeshes[0].transform.up, Item.PreviewMeshes[1].transform.up);

            float distanceBetweenStartAndCharacter = Vector3.Distance(Item.PreviewMeshes[1].transform.position, CharacterController.Instance.Rigidbody.transform.position);

            bool isValid = distanceBetweenPreviews > DistanceToValidate 
                           && Mathf.Abs(dotProductBetweenPreviews) > DotProductToValidate
                           && distanceBetweenStartAndCharacter < DistanceMaxToValidateBetweenStartAndCharacter;
            
            SetPreviewMaterials(isValid ? Item.PreviewMaterial : Item.PreviewMaterialError);
            return isValid;
        }
    }
}