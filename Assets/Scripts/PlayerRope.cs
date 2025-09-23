using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(DistanceJoint2D), typeof(LineRenderer))]
public class PlayerRope : MonoBehaviour
{
    public Transform armTip; // Tip of the stick (fire point)
    public LayerMask grappleLayer;

    public float maxGrappleDistance = 10f;
    public float reelSpeed = 2f;

    private DistanceJoint2D joint;
    private LineRenderer line;
    private Rigidbody2D body;
    private bool isGrappling = false;

    void Start()
    {
        joint = GetComponent<DistanceJoint2D>();
        line = GetComponent<LineRenderer>();
        body = GetComponent<Rigidbody2D>();

        joint.enabled = false;
        line.positionCount = 0;
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            TryGrapple();
        }
        else if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            Detach();
        }

        if (isGrappling && Mouse.current.middleButton.isPressed)
        {
            joint.distance -= reelSpeed * Time.deltaTime;
        }

        UpdateRopeLine();
    }

    void TryGrapple()
    {
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 direction = (mouseWorld - armTip.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(armTip.position, direction, maxGrappleDistance, grappleLayer);
        if (hit.collider != null)
        {
            joint.enabled = true;
            joint.connectedAnchor = hit.point;
            joint.autoConfigureDistance = false;
            joint.distance = Vector2.Distance(transform.position, hit.point);
            joint.enableCollision = true;

            isGrappling = true;
            line.positionCount = 2;
        }
    }

    void Detach()
    {
        joint.enabled = false;
        isGrappling = false;
        line.positionCount = 0;
    }

    void UpdateRopeLine()
    {
        if (isGrappling)
        {
            line.SetPosition(0, armTip.position);
            line.SetPosition(1, joint.connectedAnchor);
        }
    }
}
