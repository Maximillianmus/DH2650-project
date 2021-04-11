using UnityEngine;

public class GrapplingGun : MonoBehaviour {

    private LineRenderer lr;
    private Rigidbody rb;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public Transform gunTip, camera, player;
    public float pullSpeed = 1f;
    public float FastPullSpeed = 20f;
    public LayerMask IgnoredGrapelingLayer;
    public bool BreakIfObstructed = false;
    public float ObstructionThreshold = 0;
    public Collider FootCollider;
    public KeyCode HookShootButton;
  

    private float maxDistance = 100f;
    private SpringJoint joint;
    private float ropeLength;
    private bool colliderOff = false;
    private bool startFastPull = false;
    private float currentObstructionIteration = 0;

    

    void Awake() {
        lr = GetComponent<LineRenderer>();
        rb = player.GetComponent<Rigidbody>();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            StopGrapple();
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(0)) {
            StopGrapple();
        }

        if (Input.GetMouseButton(1) && IsGrappling())
        {
            PullIn();   
        }
        else if(Input.GetKeyDown(HookShootButton) && IsGrappling())
        {
            StartPullInFast();
        }
        if (colliderOff && (!IsGrappling() || (grapplePoint - gunTip.position).magnitude < 10f))
        {
            colliderOff = false;
            rb.useGravity = true;
            FootCollider.enabled = true;
        }

        BreakIfObstruction();
    }

    //Called after Update
    void LateUpdate() {
        DrawRope();
     
    }


    // Call at start of graple
    void StartGrapple() {
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrappleable)) {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);
            ropeLength = distanceFromPoint;

            //The distance grapple will try to keep from grapple point. 
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            //Adjust these values
            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
            currentGrapplePosition = gunTip.position;
        }
    }


    // Call to stop grapple
    void StopGrapple() {
        lr.positionCount = 0;
        currentObstructionIteration = 0;
        Destroy(joint);
    }

    // Call to pull the player towards the grapple point
    void PullIn()
    {
        ropeLength -= pullSpeed;

        joint.maxDistance = ropeLength * 0.8f;
        joint.minDistance = ropeLength * 0.25f;
    }

    // Fast pull,
    void StartPullInFast()
    {
        Vector3 dir = (grapplePoint - gunTip.position).normalized;
        FootCollider.enabled = false;
        colliderOff = true;
        rb.velocity =  new Vector3(0f, 0f, 0f);
        rb.useGravity = false;
        player.position = player.position + (new Vector3(0, 1, 0));

        ropeLength = 0.05f * ropeLength;

        joint.maxDistance = ropeLength * 0.8f;
        joint.minDistance = ropeLength * 0.25f;
        startFastPull = true;

        Invoke("PullInFast", 0.05f);
    }
    //adds the force, has to be done a bit latter so the collision doesn't affect the pull
    void PullInFast()
    {
        if (startFastPull)
        {
            Vector3 dir = (grapplePoint - gunTip.position).normalized;
            Rigidbody rb = player.GetComponent<Rigidbody>();
            rb.AddForce(dir * FastPullSpeed);
            startFastPull = false;
        }
    }


    //call to check if the path between the player and the grapling point is obstructed
    void BreakIfObstruction()
    {
        //the 7 is the layer that the linecast should ignore, which in this case is layer 7,  ~ inverts the bitmask so 7 is 0, and all other layers are 1
        if (Physics.Linecast(gunTip.position, currentGrapplePosition, ~IgnoredGrapelingLayer) && BreakIfObstructed)
        {
            if (currentObstructionIteration >= ObstructionThreshold)
            {
                StopGrapple();
            }      
            else
                currentObstructionIteration++;
                
        }

    }

    private Vector3 currentGrapplePosition;
    
    void DrawRope() {
        //If not grappling, don't draw rope
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);
        
        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, currentGrapplePosition);
    }

    public bool IsGrappling() {
        return joint != null;
    }

    public Vector3 GetGrapplePoint() {
        return grapplePoint;
    }
}
