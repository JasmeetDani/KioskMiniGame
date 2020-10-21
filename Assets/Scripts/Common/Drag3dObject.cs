using UnityEngine;

public class Drag3dObject : MonoBehaviour
{
    [SerializeField]
    private Stage2ContentViewModel topLevelView;

    public int thresholdX = 150;
    public int thresholdY = 150;
    private Vector3 offset;

    private bool dragging = false;

    private Vector3 initialPos;
    public Transform parent = null;
    public int hitBounRectThreshold = 50;
    public Vector3 dropPos;

    public int index;


    private void Start()
    {
        initialPos = gameObject.transform.localPosition;
    }

    private void OnMouseDown()
    {
        offset = gameObject.transform.position - GetMouseWorldPos();
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.x = Mathf.Clamp(mousePoint.x, thresholdX, Screen.width - thresholdX);
        mousePoint.y = Mathf.Clamp(mousePoint.y, thresholdY, Screen.height - thresholdY);

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        dragging = true;
        transform.position = GetMouseWorldPos() + offset;
    }

    private void OnMouseUp()
    {
        if(dragging)
        {
            if(parent != null)
            {
                Vector3 targetRectCenter = Camera.main.WorldToScreenPoint(parent.transform.position);
                Vector3 mousePoint = Input.mousePosition;

                bool bSecond = false;
                if(topLevelView.FlapDropPossible(ref bSecond) && ((Mathf.Abs(mousePoint.x - targetRectCenter.x) < hitBounRectThreshold) ||
                    (Mathf.Abs(mousePoint.y - targetRectCenter.y) < hitBounRectThreshold)))
                {
                    transform.SetParent(parent);
                    transform.localPosition = dropPos;
                    Destroy(gameObject.GetComponent<Drag3dObject>());


                    if (bSecond)
                    {
                        transform.localRotation = Quaternion.Euler(-180, 0, -90);
                    }

                    if(!bSecond)
                    {
                        topLevelView.NotifyFirstFlapPlaced();
                    }
                    topLevelView.ShellReadyToWrap(index);
                }
                else
                {
                    transform.localPosition = initialPos;
                }
            }
            else
            {
                transform.localPosition = initialPos;
            }

            dragging = false;
        }
    }
}