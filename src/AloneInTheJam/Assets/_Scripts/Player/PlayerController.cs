using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform arm;
    public Animator armAnim, cameraAnim;
    public Transform cam;
    public float speed;
    public float sensitivity = 6;
    public Rigidbody rb;
    public int hitLife;
    float camZRot;
    public Vector3 armPos;
    public Quaternion armRot;

    public Phone phone;
    public bool canPlayFootStep;

    public static PlayerController instance;

    public AudioPlayer audioPlayer;
    void Start()
    {
        hitLife = 2;
        instance = this;

    }

    void Update()
    {
        if (GameController.gameController.iniciaPartida && !phone.recharging)
        {
            Move(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            CameraMovement(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            ArmPositioning();
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
            

        }
        else
        {
            canPlayFootStep = false;
        }

        if (GameController.gameController.pressEtoRechargeGO.activeSelf)
        {
            if (Input.GetKey(KeyCode.E))
            {
                phone.recharging = true;

            }
            else
                phone.recharging = false;
        }
    }
    //phone function
    void Shoot()
    {
        if (phone != null)
        {
            phone.ShootFlash(armAnim);
        }
    }
    //move the player based on where he is looking
    void Move(float x, float y)
    {
        float moving = Mathf.Clamp01(Mathf.Abs(x) + Mathf.Abs(y));
        if (moving > 0)
        {
            canPlayFootStep = true;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                cameraAnim.speed = 1.5f;
            }
            else
            {
                cameraAnim.speed = 1;
            }
        }
        else
        {
            canPlayFootStep = false;
            cameraAnim.speed = 1;
        }
        cameraAnim.SetFloat("Speed", Mathf.Lerp(cameraAnim.GetFloat("Speed"), moving, 5 * Time.deltaTime));
        Vector3 fixX = transform.right * x;
        Vector3 fixY = transform.forward * y;

        Vector3 direction = fixX + fixY;
        direction.Normalize();

        rb.position = rb.position + direction * speed * (cameraAnim.speed / 2) * Time.deltaTime;

    }
    //rotates player in Y axis and camera in X axis
    void CameraMovement(float x, float y)
    {
        transform.Rotate(0, x * sensitivity, 0);

        camZRot -= y * sensitivity;
        camZRot = Mathf.Clamp(camZRot, -70, 90);

        cam.localEulerAngles = new Vector3(camZRot, 0, 0);
    }
    //
    void ArmPositioning()
    {

        Vector3 _armPos = cam.position + cam.forward * armPos.z + cam.up * armPos.y + cam.right * (Input.GetAxis("Horizontal") / 125 + Input.GetAxis("Mouse X") / 125);
        arm.transform.position = Vector3.Lerp(arm.transform.position, _armPos, 15 * Time.deltaTime);

        arm.transform.localEulerAngles = cam.localEulerAngles;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == TagsUtil.ENEMY)
        {
            audioPlayer.AiAi();
            audioPlayer.GritoBruxa();
            GameController.gameController.GameOver();
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == TagsUtil.LOCAL_RECHARGE)
    //    {
    //        GameController.gameController.pressEtoRechargeGO.SetActive(true);
    //    }
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == TagsUtil.LOCAL_RECHARGE)
    //    {
    //        GameController.gameController.pressEtoRechargeGO.SetActive(false);
    //    }
    //}
}
