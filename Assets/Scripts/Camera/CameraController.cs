using UnityEngine;

namespace Camera
{
    public class CameraController : MonoBehaviour
    {
        [field:SerializeField] public UnityEngine.Camera Camera { get; private set; }
        [field: SerializeField] public Transform CameraTarget { get; private set; }
        [field: SerializeField] public CameraFollowTransform FollowTransform { get; private set; }
        [field: SerializeField] public CameraMoveWithInput MoveWithInput { get; private set; }
    }
}