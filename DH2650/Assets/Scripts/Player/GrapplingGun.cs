using UnityEngine;

public class GrapplingGun : MonoBehaviour {

    private LineRenderer lr;
    private Rigidbody rb;
    private Vector3 grapplePoint;

    public LayerMask whatIsGrappleable;
    public Transform gunTip, m_camera, player;

    //particles
    public ParticleSystem FireHookEffectPrefab;
    public ParticleSystem HarpoonHitEffectPrefab;
    public float pullSpeed = 1f;
    public float FastPullSpeed = 20f;
    public LayerMask IgnoredGrapelingLayer;
    public bool BreakIfObstructed = false;
    public float ObstructionProximityThreshold = 1f;
    public float ObstructionThreshold = 0;
    public Collider FootCollider;
    public KeyCode HookShootButton;
    public Transform playerContainer;

    //this is for the harpoon
    public Transform harpoon, harpoonStatic,harpoonPos;
    public float harpoonSpeed = 1;
    private Renderer HarpStaticRend, HarpRend;
    private Transform ropePoint;



    //this is for Grappeling
    private bool isGrappeling;
    private float distanceFromPoint;
    //this is a object that is used when grappeling to keep track of the point on moving objects
    private GameObject grappelingPointObject;
    private Rigidbody grappledObjectRB;


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


    public float maxDistance = 100f;
    private SpringJoint joint;
    private float ropeLength;
    private bool colliderOff = false;
    private bool startFastPull = false;
    private float currentObstructionIteration = 0;
    private Vector3 currentGrapplePosition;
    private AudioSource gunSound;
    private AudioSource harpoonSound;


    void Awake() {
        lr = GetComponent<LineRenderer>();
        rb = player.GetComponent<Rigidbody>();
        HarpRend = harpoon.GetComponent<Renderer>();
        HarpStaticRend = harpoonStatic.GetComponent<Renderer>();
        gunSound = transform.GetComponent<AudioSource>();
        harpoonSound = harpoon.GetComponent<AudioSource>();
        ropePoint = harpoon.GetChild(0);
        HarpRend.enabled = false;

    }

    void Update() {

        if (grappelingPointObject != null)
        {
            grapplePoint = grappelingPointObject.transform.position;
            if (joint != null && grappledObjectRB == null)
            {
                joint.connectedAnchor = grapplePoint;
            }
        }


        if (Input.GetMouseButtonDown(0) && !isGrabbing && !isGrappeling && !isActivating) {
            ActivateHookGun();
        }
        else if (!Input.GetMouseButton(0)) {
            if(IsGrapplingWithJoint())
                StopGrapple();

        }

        if (Input.GetMouseButton(1) && IsGrapplingWithJoint())
        {
            PullIn();   
        }
        else if(Input.GetKeyDown(HookShootButton) && IsGrapplingWithJoint())
        {
            StartPullInFast();
        }
        if (colliderOff && (!IsGrapplingWithJoint() || (grapplePoint - gunTip.position).magnitude < 10f))
        {
            colliderOff = false;
            rb.useGravity = true;
            FootCollider.enabled = true;
        }

       

        if (isGrabbing)
        {
            UpdateItemPull();

            if ((grapplePoint - gunTip.position).magnitude < 1.5f)
            {
                StopItemPull();
                itemTransform.GetComponent<Interactable>().Interact(offHand.GetComponent<OffHand>());
            }

        }

        if (isGrappeling)
        {
            UpdateGrapple();
        }

        if (isActivating)
        {
            UpdateActivationHook();
        }

        if (IsGrapplingWithJoint())
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
            Instantiate(FireHookEffectPrefab, gunTip.position, gunTip.rotation);
            gunSound.Play();    
            
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
        if ( CheckIfHit(currentGrapplePosition, activationTransform.position))
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
        itemTransform = hit.transform;
        itemTransform.gameObject.GetComponent<Item>().ReleaseFromContainer();

        isGrabbing = true;
        itemHitPoint = hit.point;
        harpoonItemOffset = Vector3.zero;
        grapplePoint = itemHitPoint;
        itemRb = hit.transform.GetComponent<Rigidbody>();
        itemRb.useGravity = false;
        itemCollider = hit.transform.GetComponent<Collider>();

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
            itemRb.AddForce((gunTip.transform.position - itemHitPoint).normalized * grabbingSpeed *Time.deltaTime * 15);
        }
        else if(CheckIfHit(currentGrapplePosition, itemRb.position))
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
        isGrappeling = true;
        //creat grappeling point object
        grappelingPointObject = new GameObject("GrappelingPoint");

        //the expression after the + sign is to make the grapple point not be in the wall
        grapplePoint = hit.point+((player.position-grapplePoint).normalized *0.1f);
        grappelingPointObject.transform.position = grapplePoint;
        grappelingPointObject.transform.SetParent(hit.transform);

        hit.transform.TryGetComponent<Rigidbody>(out grappledObjectRB);
 
        distanceFromPoint = Vector3.Distance(player.position, grapplePoint);
        ropeLength = distanceFromPoint;
        //harpoonStatic.GetComponent<Renderer>().enabled = false; 
        //The distance grapple will try to keep from grapple point. 
          


        //make prop harpoon invisible and animated harpoon visible
        HarpRend.transform.rotation = Quaternion.LookRotation(grapplePoint -gunTip.position)* Quaternion.Euler(0,90,0);
        HarpRend.enabled = true;
        HarpStaticRend.enabled = false;

           

        lr.positionCount = 2;
        currentGrapplePosition = gunTip.position;
        
    }

    //Call to check when we have hit something and should place the joint
    void UpdateGrapple()
    {
        
        if (!hasHit && CheckIfHit(currentGrapplePosition, grapplePoint))
        {
            hasHit = true;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;

            if(grappelingPointObject != null && grappledObjectRB != null)
            {
                joint.connectedBody = grappledObjectRB;
                //joint.connectedAnchor = grappelingPointObject.transform.localPosition;
                ropePoint.SetParent(grappledObjectRB.transform);
                joint.connectedAnchor = ropePoint.localPosition;
                ropePoint.SetParent(harpoon);
                harpoon.GetComponent<Rigidbody>().freezeRotation = false;
            }
            else
            {
                joint.connectedAnchor = grapplePoint;
            }


            harpoon.SetParent(grappelingPointObject.transform, true);
            

            joint.maxDistance = distanceFromPoint * 0.8f;

            //Adjust these values
           
            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;
            //joint.breakForce = 0;
            //joint.breakTorque
        }
       
    }
    // Call to stop grapple
    void StopGrapple() {
        hasHit = false;
        isGrappeling = false;
        Destroy(grappelingPointObject);

        lr.positionCount = 0;
        currentObstructionIteration = 0;

        //reset the harpoon model

        harpoon.SetParent(playerContainer);
        harpoon.GetComponent<Rigidbody>().freezeRotation = true;
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
        RaycastHit hit;
        if (Physics.Linecast(gunTip.position, currentGrapplePosition, out hit ,~IgnoredGrapelingLayer) && BreakIfObstructed)
        {
            if (Vector3.Distance(currentGrapplePosition, hit.point) >ObstructionProximityThreshold)
            {
                if (currentObstructionIteration >= ObstructionThreshold)
                {
                    StopGrapple();

                }
                else
                    currentObstructionIteration++;
            }
                
        }

    }


    bool CheckIfHit(Vector3 posA, Vector3 posB)
    {
        if ((posA - posB).magnitude < 0.5f)
        {
            Instantiate(HarpoonHitEffectPrefab, harpoon.position, gunTip.rotation);
            harpoonSound.Play();

            return true;
        }


        return false;
    }

 
    
    void DrawRope() {
        //If not grappling, don't draw rope
        if (!joint && !isGrabbing && !isActivating && !isGrappeling) return;


        if (!hasHit || isGrappeling)
        {
            //currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 12f);
            currentGrapplePosition =  Vector3.MoveTowards(currentGrapplePosition, grapplePoint, harpoonSpeed * Time.deltaTime);
        }
        else
        {
            //if we have already hit something
            currentGrapplePosition = grapplePoint;
        }

        //moves the harpoon
        if(  !hasHit || grappelingPointObject == null )
            harpoon.position = currentGrapplePosition;


 
           
        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, ropePoint.position);
    }

    public bool IsGrapplingWithJoint() {
        return joint != null;
    }

    public Vector3 GetGrapplePoint() {
        return grapplePoint;
    }

}
