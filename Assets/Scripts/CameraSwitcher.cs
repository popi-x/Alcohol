using UnityEngine;
using Unity.Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    public bool isInvView = false;
    public CinemachineCamera normCam;
    public CinemachineCamera invCam;
    public static CameraSwitcher instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isInvView = !isInvView;
            SetCameraPriority(isInvView);
        }
    }

    private void SetCameraPriority(bool isInvView)
    {
        if (isInvView)
        {
            invCam.Priority = 10;
            normCam.Priority = 5;
        }
        else
        {
            invCam.Priority = 5;
            normCam.Priority = 10;
        }
    }
}
