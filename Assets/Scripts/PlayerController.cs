using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Phase phase = Phase.NONE;
    private Vector2 movementVector;
    [SerializeField] private float movementSpeed;
    [SerializeField] private Rigidbody2D rb;
    private Camera mainCam;
    private Vector3 mousePos;
    public Transform aimTransform;
    public bool canFire;
    public float fireCooldown = .5f;
    public float fireTimer =0;
    public BulletBehaviour bullet;
    private void Awake()
    {
        mainCam = Camera.main;
        canFire = true;
        aimTransform.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        GameActions.onPhaseChanged += SetPhase;
    }
    private void OnDisable()
    {
        GameActions.onPhaseChanged -= SetPhase;
    }

    private void SetPhase(Phase currentPhase)
    {
        phase = currentPhase;
        if (currentPhase != Phase.Positioning)
        {
            movementVector = Vector2.zero;
            rb.velocity = movementVector;
        }

        if (currentPhase == Phase.War)
        {
            canFire = true;
            fireTimer = 0;
            aimTransform.gameObject.SetActive(true);
        }
        else
        {
            aimTransform.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if(phase == Phase.NONE) return;
        switch (phase)
        {
            case Phase.Positioning:
                CheckForMovementInput();
                break;
            case Phase.War:
                CheckForWarInput();
                break;
            case Phase.Build:
                break;
            default:
                return;
        }
    }

    private void CheckForWarInput()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rot = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg;
        aimTransform.rotation = Quaternion.Euler(0,0,rotZ);


        fireTimer += Time.deltaTime;
        if (fireTimer >= fireCooldown)
        {
            canFire = true;
        }
        
        if (Input.GetMouseButtonDown(0) && canFire)
        {
            Fire();
        }
    }

    private void Fire()
    {
        canFire = false;
        fireTimer = 0;
        var b =Instantiate(bullet, transform.position, Quaternion.identity);
        b.SetTarget(mousePos);
    }

    private void CheckForMovementInput()
    {
        movementVector.x = Input.GetAxisRaw("Horizontal") * MS() * Time.deltaTime;
        movementVector.y = Input.GetAxisRaw("Vertical") * MS() * Time.deltaTime;
        
        movementVector = movementVector.normalized;
    }

    private float MS()
    {
        return movementSpeed + PlayerUpgrades.I.speedAdded * .2f;
    }
    private void FixedUpdate()
    {
        switch (phase)
        {
            case Phase.Positioning:
                ConsumeMovementInput();
                break;
            case Phase.War:
                break;
            case Phase.Build:
                break;
            default:
                return;
        }
    }

    private void ConsumeMovementInput()
    {
        rb.velocity = movementVector;
    }
}
