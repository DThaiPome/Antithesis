using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FPObjectGrabbing : MonoBehaviour
{
    [SerializeField]
    private string grabInput;
    [SerializeField]
    private string activateInput;
    [SerializeField]
    private Transform grabberParent;
    [SerializeField]
    private float maxGrabRange = 3;

    [SerializeField]
    private string[] grabIgnoreLayers;
    [SerializeField]
    private string[] holdIgnoreLayers;

    private XRGrabInteractable grabbedObject;

    struct RigidbodySettings
    {
        public bool useGravity;
        public RigidbodyConstraints constraints;

        public RigidbodySettings(bool useGravity, RigidbodyConstraints constraints)
        {
            this.useGravity = useGravity;
            this.constraints = constraints;
        }
    }
    private RigidbodySettings baseRBSettings;
    private RigidbodySettings heldRBSettings;

    // Start is called before the first frame update
    void Awake()
    {
        this.heldRBSettings = new RigidbodySettings(false, RigidbodyConstraints.FreezeAll);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.IsGrabbing())
        {
            this.GrabbingUpdate();
        } else
        {
            this.NotGrabbingUpdate();
        }
    }

    private void NotGrabbingUpdate()
    {
        if (Input.GetButtonDown(this.grabInput))
        {
            XRGrabInteractable g = this.GetObjectToGrab();
            if (g != null)
            {
                this.StartGrabbing(g);
            }
        }
    }

    private XRGrabInteractable GetObjectToGrab()
    {
        Ray ray = new Ray(this.transform.position, this.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, this.maxGrabRange, ~LayerMask.GetMask(this.grabIgnoreLayers))) 
        {
            XRGrabInteractable g = hit.collider.gameObject.GetComponent<XRGrabInteractable>();
            if (g != null)
            {
                return g;
            }
        }
        return null;
    }

    private void StartGrabbing(XRGrabInteractable obj)
    {
        this.grabbedObject = obj;
        this.grabbedObject.transform.parent = this.grabberParent;
        Rigidbody rb = this.grabbedObject.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            this.baseRBSettings = new RigidbodySettings(rb.useGravity, rb.constraints);
            this.SetRBSettings(rb, this.heldRBSettings);
        }
    }

    private void GrabbingUpdate()
    {
        if (Input.GetButtonDown(this.grabInput))
        {
            this.StopGrabbing();
            return;
        }
        if (Input.GetButtonDown(this.activateInput))
        {
            this.ActivateObject();
        }

        Vector3 holdPoint = this.GetHoldPoint();
        this.grabberParent.position = holdPoint;

        this.StabalizeObject();
    }

    private void StabalizeObject()
    {
        this.grabbedObject.transform.localPosition = Vector3.zero;

        Rigidbody rb = this.grabbedObject.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Rigidbody nrb = new Rigidbody();
        }
    }

    private void StopGrabbing()
    {
        this.grabbedObject.transform.parent = null;
        Rigidbody rb = this.grabbedObject.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            this.SetRBSettings(rb, this.baseRBSettings);
            this.baseRBSettings = this.heldRBSettings;
        }
        this.grabbedObject = null;
    }

    private void ActivateObject()
    {
        this.grabbedObject.onActivate.Invoke(new NonVRInteractor(this.grabbedObject));
    }

    private Vector3 GetHoldPoint()
    {
        Ray ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit[] hits = Physics.RaycastAll(ray, this.maxGrabRange, ~LayerMask.GetMask(this.holdIgnoreLayers));
        foreach(RaycastHit hit in hits)
        {
            if (hit.collider.gameObject != this.grabbedObject.gameObject)
            {
                return hit.point;
            }
        }
        return this.transform.position + (this.transform.forward * this.maxGrabRange);
    }

    private bool IsGrabbing()
    {
        return this.grabbedObject != null;
    }

    private void SetRBSettings(Rigidbody rb, RigidbodySettings rbs)
    {
        rb.useGravity = rbs.useGravity;
        rb.constraints = rbs.constraints;
    }
}