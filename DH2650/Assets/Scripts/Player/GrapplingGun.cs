using UnityEngine;

public class GrapplingGun : MonoBehaviour {

    private LineRenderer lr;
    private Rigidbody rb;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public Transform gunTip, m_camera, player;
    public float pullSpeed = 1f;
    public float FastPullSpeed = 20f;
    public LayerMask IgnoredGrapelingLayer;
    public bool BreakIfObstructed = false;
    public float ObstructionThreshold = 0;
    public Collider FootCollider;
    public KeyCode HookShootButton;

    //this is for the harpoon
    public Transform harpoon, harpoonStatic,harpoonPos;
    private Renderer HarpStaticRend, HarpRend;



    //this is for Item grabbing
    private Vector3 itemHitPoint;
    public float grabbingSpeed = 200f;
    public Transform offHand;
    private bool isGrabbing = false;
    private Rigidbody itemRb;
    private Collider itemCollider;
    private Transform itemTransform;
    private Vector3 harpoonItemOffset;
    private bool hasHit = false;


    //this is for distance activating
    private bool isActivating = false;
    private Transform activationTransform;


    private float maxDistance = 100f;
    private SpringJoint joint;
    private float ropeLength;
    private bool colliderOff = false;
    private bool startFastPull = false;
    private float currentObstructionIteration = 0;
    private Vector3 harpoonRestingPos;


    void Awake() {
        lr = GetComponent<LineRenderer>();
        rb = player.GetComponent<Rigidbody>();
        harpoonRestingPos = harpoonStatic.position;
        HarpRend = harpoon.GetComponent<Renderer>();
        HarpStaticRend = harpoonStatic.GetComponent<Renderer>();
        HarpRend.enabled = false;

    }

    void Update() {
        if (Input.GetMouseButtonDown(0) && !isGrabbing) {
            StopGrapple();
            ActivateHookGun();
        }
        else if (Input.GetMouseButtonUp(0)) {
            if(IsGrappling())
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

        if ( isGrabbing && (grapplePoint - gunTip.position).magnitude < 1.5f)
        {
            StopItemPull();
            itemTransform.GetComponent<Interactable>().Interact(offHand.GetComponent<OffHand>());
        }

        if (isGrabbing)
        {
            UpdateItemPull();
        }

        if (isActivating)
        {
            UpdateActivationHook();
        }

        if (IsGrappling())
            BreakIfObstruction();
    }

    //Called after Update   
    void LateUpdate() {
        DrawRope();
     
    }



    //Does the raycasting and decides what hook script should be used
    void ActivateHookGun()
    {
        RaycastHit hit;
        if (Physics.Raycast(m_camera.position, m_camera.forward, out hit, maxDistance, whatIsGrappleable))
        {
            if(hit.transform.tag == "Item")
            {
                StartItemPull(hit);
            }
            else if (hit.transform.tag == "Interactable")
            {
                StartActivationHook(hit);
            }
            else
            {
                StartGrapple(hit);
            }
        }
    }



    void StartActivationHook(RaycastHit hit)
    {
        isActivating = true;
        grapplePoint = hit.point;
        activationTransform = hit.transform;

        //make prop harpoon invisible and animated harpoon visible
        HarpRend.transform.rotation = Quaternion.LookRotation(grapplePoint - gunTip.position) * Quaternion.Euler(0, 90, 0);
        HarpRend.enabled = true;
        HarpStaticRend.enabled = false;


        lr.positionCount = 2;
        currentGrapplePosition = gunTip.position;
    }

    void UpdateActivationHook()
    {
        if ((currentGrapplePosition - activationTransform.position).magnitude < 0.5f)
        {
            activationTransform.gameObject.GetComponent<Interactable>().Interact(offHand.GetComponent<OffHand>());
            StopActivationHook();
        }
        
    }

    void StopActivationHook()
    {
        lr.positionCount = 0;
        harpoon.GetComponent<Rigidbody>().velocity = Vector3.zero;
        harpoon.position = harpoonPos.position;


        HarpRend.enabled = false;
        HarpStaticRend.enabled = true;
        isActivating = false;
    }

    void StartItemPull(RaycastHit hit)
    {
        isGrabbing = true;
        itemHitPoint = hit.point;
        harpoonItemOffset = Vector3.zero;
        grapplePoint = itemHitPoint;
        itemRb = hit.transform.GetComponent<Rigidbody>();
        itemRb.useGravity = false;
        itemCollider = hit.transform.GetComponent<Collider>();
        itemTransform = hit.transform;
        itemCollider.enabled = false;
       

        //make prop harpoon invisible and animated harpoon visible
        HarpRend.transform.rotation = Quaternion.LookRotation(grapplePoint - gunTip.position) * Quaternion.Euler(0, 90, 0);
        HarpRend.enabled = true;
        HarpStaticRend.enabled = false;


        lr.positionCount = 2;
        currentGrapplePosition = gunTip.position;
    }

    void UpdateItemPull()
    {
        itemRb.velocity = Vector3.zero;
        itemHitPoint = itemRb.position + harpoonItemOffset;
        grapplePoint = itemHitPoint;


        if (hasHit)
        {
            itemRb.AddForce((gunTip.transform.position - itemHitPoint).normalized * grabbingSpeed);
        }
        else if((currentGrapplePosition - itemRb.position).magnitude < 0.5f)
        {
            harpoonItemOffset = currentGrapplePosition - itemRb.position;
            hasHit = true;
        }
           
    }


    void StopItemPull()
    {
        lr.positionCount = 0;
        harpoon.GetComponent<Rigidbody>().velocity = Vector3.zero;
        harpoon.position = harpoonPos.position;
       
        itemRb.useGravity = true;
        itemCollider.enabled = true;

        HarpRend.enabled = false;
        HarpStaticRend.enabled = true;
        isGrabbing = false;
        hasHit = false;

    }

    // Call at start of graple
    void StartGrapple(RaycastHit hit) {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);
            ropeLength = distanceFromPoint;
            harpoonStatic.GetComponent<Renderer>().enabled = false; 
            //The distance grapple will try to keep from grapple point. 
            joint.maxDistance = distanceFromPoint * 0.8f;


            //make prop harpoon invisible and animated harpoon visible
            HarpRend.transform.rotation = Quaternion.LookRotation(grapplePoint -gunTip.position)* Quaternion.Euler(0,90,0);
            HarpRend.enabled = true;
            HarpStaticRend.enabled = false;

            //Adjust these values
            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
            currentGrapplePosition = gunTip.position;
        
    }


    // Call to stop grapple
    void StopGrapple() {
        lr.positionCount = 0;
        currentObstructionIteration = 0;

        //reset the harpoon model
        harpoon.GetComponent<Rigidbody>().velocity = Vector3.zero;
        harpoon.position = harpoonPos.position;
        HarpRend.enabled = false;
        HarpStaticRend.enabled = true;
        if (joint)
            Destroy(joint);
    }

    // Call to pull the player towards the grapple point
    void PullIn()
    {
        ropeLength -= pullSpeed;

        joint.maxDistance = ropeLength * 0.8f;
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
        if (!joint && !isGrabbing && !isActivating) return;


        if (!hasHit)
        {
            currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 12f);
        }
        else
        {
            //if we have already hit something
            currentGrapplePosition = grapplePoint;
        }
        //moves the harpoon

     
        harpoon.position = currentGrapplePosition;

 
           
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
