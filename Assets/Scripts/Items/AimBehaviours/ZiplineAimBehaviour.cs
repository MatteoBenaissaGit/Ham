using UnityEngine;
using CharacterController = Character.CharacterController;

namespace Items.AimBehaviours
{
    public class ZiplineAimBehaviour : ItemAimBehaviour
    {
        private Transform _basePreviewParent;
        private Quaternion _desiredRotation;
        
        public ZiplineAimBehaviour(ItemController item) : base(item)
        {
            _basePreviewParent = item.PreviewMesh.transform.parent;
        }

        public override void Aim(bool doAim)
        {
            MakeCameraAim(doAim);

            if (doAim == false)
            {
                Item.PreviewMesh.SetActive(false);
            }

            Item.PreviewMesh.transform.parent = doAim ? null : _basePreviewParent;
        }

        private RaycastHit[] _hits = new RaycastHit[5];
        public override void AimStay()
        {
            Item.PreviewMesh.transform.rotation = Quaternion.Lerp(Item.PreviewMesh.transform.rotation, _desiredRotation, 0.1f);

            Vector3 origin = CharacterController.Instance.CameraController.Camera.transform.position;
            Vector3 direction = CharacterController.Instance.CameraController.Camera.transform.forward;
            int hitAmount = Physics.RaycastNonAlloc(origin, direction, _hits);

            RaycastHit closestHit = default;
            float closestHitDistance = float.MaxValue;
            bool didHit = false;
            
            for (int i = hitAmount-1; i >= 0; i--)
            {
                if (_hits[i].collider == null || _hits[i].collider.isTrigger)
                {
                    continue;
                }

                float distance = Vector3.Distance(CharacterController.Instance.CameraController.Camera.transform.position, _hits[i].point);
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
                Item.PreviewMesh.SetActive(false);
                return;
            }
            
            Item.PreviewMesh.SetActive(true);
            Item.PreviewMesh.transform.position = closestHit.point;

            Vector3 normal = closestHit.normal.normalized;
            Quaternion rotation = Quaternion.LookRotation(normal);
            Vector3 eulerAngles = rotation.eulerAngles;
            _desiredRotation = Quaternion.Euler(eulerAngles);
        }

        public override void Shoot()
        {
            
        }
    }
}