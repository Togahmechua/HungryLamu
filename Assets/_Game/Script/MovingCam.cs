using DG.Tweening;
using UnityEngine;

public class MovingCam : MonoBehaviour
{
    [Header("------Player Details------")]
    [SerializeField] private LamuCtrl lamu;
    [SerializeField] private Transform bananaDog;
    [SerializeField] private float speed;

    private Vector3 offset;
    private Camera cam;

    private enum CameraState
    {
        Lamu,
        BananaDog,
        FruitFriends
    }

    private CameraState currentState;

    private void Start()
    {
        offset = transform.position - lamu.transform.position;
        cam = GetComponent<Camera>();
        currentState = CameraState.Lamu;
    }

    private void FixedUpdate()
    {
        switch (currentState)
        {
            case CameraState.Lamu:
                MoveCamera(lamu.transform.position, 6f);
                lamu.isAbleToMove = true;
                break;

            case CameraState.BananaDog:
                MoveCamera(bananaDog.position, 2.5f);
                lamu.isAbleToMove = false;
                break;

            case CameraState.FruitFriends:
                MoveCamera(new Vector3(35.1f, -2.29f, -10f), 6f);
                lamu.isAbleToMove = false;
                break;
        }
    }

    private void OnDestroy()
    {
        cam?.DOComplete(); // Kết thúc tất cả tween đang hoạt động
        cam?.DOKill();     // Hủy toàn bộ tween liên quan đến camera
    }

    private void MoveCamera(Vector3 target, float targetSize)
    {
        if (cam == null) return; // Ngừng thực hiện nếu camera đã bị hủy

        Vector3 targetPos = target + offset;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * 8 * Time.fixedDeltaTime);

        cam.DOOrthoSize(targetSize, 0.8f).SetUpdate(true); // Thêm SetUpdate để tween không bị ảnh hưởng bởi TimeScale
    }

    public void FocusOnDog()
    {
        currentState = CameraState.BananaDog;
    }

    public void FocusOnLamu()
    {
        currentState = CameraState.Lamu;
    }

    public void FocusOnFruitFriends()
    {
        currentState = CameraState.FruitFriends;
    }
}
