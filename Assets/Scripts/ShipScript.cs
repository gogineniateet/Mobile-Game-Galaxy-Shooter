using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour
{
    #region PUBLIC VARIABLES
    public float rotationSpeed = 10f;   // Rotation of ship in degrees per second.
    public float movementSpeed = 2f;    // Movement of ship by force unit per seconds.
    public Transform launcher;
    #endregion
    #region PRIVATE VARIABLES
    private bool isRotating = false;
    private string TURN_COROUTINE_FUNCTION = "TurnRotateOnTap";
    
    private GameManager gameManager;
    #endregion
    #region MONOBEHAVIOR METHODS
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnEnable()  // When game object is active, then we are subscribe.
    {
        MyMobileGalaxyShooter.UserInputHandler.OnTouchAction += TowardsTouch;
    }

    public void OnDisable() // When game object is inactive, then we are desubscribe
    {
        MyMobileGalaxyShooter.UserInputHandler.OnTouchAction -= TowardsTouch;
    }

    #region PUBLIC METHODS
    public void TowardsTouch(Touch touch)
    {
        Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(touch.position); // Converts pixel coordinates to world coordinates.
        StopCoroutine(TURN_COROUTINE_FUNCTION);
        StartCoroutine(TURN_COROUTINE_FUNCTION,worldTouchPosition);
    }
    /*
    IEnumerator Turn_RotateAndMoveTowardsTap(Vector3 tempPoint)
    {
        isRotating = true;
        //tempPoint = tempPoint - this.transform.position; // To find the difference between current ship positions and touch position
        //tempPoint.z = transform.position.z;  // Assgning the touch point of Z to ship position of Z. 
        Quaternion startRoation = this.transform.rotation;  // The rotation start point.
        Quaternion EndRotation = Quaternion.LookRotation(tempPoint,Vector3.up); // This rotation will at touch point in up drection.
        
        float time = Quaternion.Angle(startRoation,EndRotation)/rotationSpeed; // Angle between two rotations.
        for (float i = 0; i < time; i =i+Time.deltaTime)
        {
            transform.rotation = Quaternion.Slerp(startRoation, EndRotation, i);
        }
        transform.rotation = EndRotation;   // We need to put a shooting functionality here
        isRotating=false;
        yield return null;
    }
    */
    IEnumerator TurnRotateOnTap(Vector3 tempPoint)
    {
        isRotating = true;
        tempPoint = tempPoint - this.transform.position; // To find the difference between current ship positions and touch position
        tempPoint.z = transform.position.z;  // Assgning the touch point of Z to ship position of Z.
        Quaternion startRotation = this.transform.rotation; // took the value of ships rotation.
        Quaternion endRotation = Quaternion.LookRotation(tempPoint, Vector3.forward);
        for (float i = 0; i < 1f; i+=Time.deltaTime)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, i);
            yield return null;
        }
        
        transform.rotation = endRotation;

        Shoot();
        isRotating = false;
    }
    #endregion
    #region PUBLIC METHODS
    public void OnHit()
    {
        gameManager.LoseLife();
        StartCoroutine(StartInvincibilityTimer(2.5f));
    }
    #endregion
    // Shoot a bullet forward.
    private void Shoot()
    {
        BulletScript bullet = PoolManager.Instance.Spawn(Constants.BULLET_PREFAB_NAME).GetComponent<BulletScript>();
        bullet.SetPosition(launcher.position);
        bullet.SetTrajectory(bullet.transform.position + transform.forward);
    }
    #region PRIVATE METHODS
    #endregion
    // Make the ship invincible for a time.
    private IEnumerator StartInvincibilityTimer(float timeLimit)
    {
        //collider2D.enabled = false;

        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        float timer = 0;
        float blinkSpeed = 0.25f;

        while (timer < timeLimit)
        {
            yield return new WaitForSeconds(blinkSpeed);

            spriteRenderer.enabled = !spriteRenderer.enabled;
            timer += blinkSpeed;
        }

        spriteRenderer.enabled = true;
        //collider2D.enabled = true;
    }

}
