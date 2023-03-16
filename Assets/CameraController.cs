using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float scrollSpeed = 5f;
    [SerializeField]
    private float smoothTime = 0.1f;
    
    [Space]
    [SerializeField]
    private new CinemachineVirtualCamera camera;
    [SerializeField]
    private Transform follow;
    
    private float orthographicSize;
    private float vel;
    
    
    private void Awake()
    {
        orthographicSize = camera.m_Lens.OrthographicSize;
    }
    
    private void Update()
    {
        var scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if (orthographicSize - scrollWheel * scrollSpeed > 0f)
        {
            orthographicSize -= scrollWheel * scrollSpeed;
        }
        
        if (camera.m_Lens.OrthographicSize != orthographicSize)
        {
            camera.m_Lens.OrthographicSize = Mathf.SmoothDamp(
                camera.m_Lens.OrthographicSize, orthographicSize, ref vel, smoothTime);
        }
    }
    
    public void OnFollowSwitch(bool value)
    {
        camera.Follow = value ? follow : null;
    }
}
