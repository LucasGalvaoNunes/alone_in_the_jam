using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour
{
    public AudioSource cameraShot;
    public static Phone phone;
    public SpawEnemy spawEnemy;
    int freirasDied;
    public int contFreirasTotal;

    public Transform cam;
    public float charge = 0;
    public TextMesh chargePercentage;
    public bool isShooting;
    public bool recharging;

    public Renderer screen;
    public Material[] screenState;
    public Texture[] rechargeScreen;
    public Light screenLight;
    public GameObject botaocamera;
    public GameObject crosshairCamera;
    public Light flashLight;
    public GameObject flare;

    public AudioPlayer audioPlayer;
    public ScreenState state;

    public enum ScreenState
    {
        InCamera,
        PowerOff,
        PowerOn
    }

    public CapsuleCollider FlashDamageRangeCollider;

    // Use this for initialization
    void Start()
    {
        state = ScreenState.InCamera;
        phone = this;
        charge = Random.Range(5, 9);
        isShooting = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.gameController.iniciaPartida)
        {
            if (IsCharged())
            {
                if (!recharging)
                {
                    charge -= 0.05f * Time.deltaTime;
                }
                string txtPercentage = charge.ToString("F0");
                chargePercentage.text = txtPercentage + "%";

                if (txtPercentage == "0")
                {
                    chargePercentage.text = "!";
                }
            }

            if (!IsCharged() && state == ScreenState.InCamera)
            {
                PowerOffPhone();
            }

            if (recharging)
            {
                PowerOnPhone();
                if (charge < 100)
                    charge += 2 * Time.deltaTime;
            }
        }
    }
    public void PowerOnPhone()
    {
        if (state == ScreenState.PowerOff)
        {
            state = ScreenState.PowerOn;
            StartCoroutine(PowerOnPhoneProcess());
            screen.material = screenState[3];
        }
    }
    IEnumerator PowerOnPhoneProcess()
    {

        for (int i = 0; i < 6; i++)
        {
            screenLight.intensity = 1;
            screen.material.mainTexture = rechargeScreen[2];
            yield return new WaitForSeconds(1f);
            chargePercentage.text = "";
            screenLight.intensity = 0;
            screen.material.mainTexture = rechargeScreen[0];
            yield return new WaitForSeconds(.5f);
        }
        screenLight.intensity = 1;
        screen.material = screenState[0];
        botaocamera.SetActive(true);
        crosshairCamera.SetActive(true);
        state = ScreenState.InCamera;
    }
    public void PowerOffPhone()
    {
        state = ScreenState.PowerOff;
        botaocamera.SetActive(false);
        crosshairCamera.SetActive(false);
        audioPlayer.DeuPau();
        StartCoroutine(PowerOffPhoneProcess());
    }
    IEnumerator PowerOffPhoneProcess()
    {
        screen.material = screenState[2];
        chargePercentage.text = "";
        for (int i = 0; i < 3; i++)
        {
            screenLight.intensity = 1;
            screen.material.mainTexture = rechargeScreen[1];
            yield return new WaitForSeconds(.5f);
            screenLight.intensity = 0;
            screen.material.mainTexture = rechargeScreen[0];
            yield return new WaitForSeconds(.5f);
        }

    }



    public void ShootFlash(Animator anim)
    {
        if (IsCharged())
        {
            if (!isShooting && !recharging)
            {
                StartCoroutine(ShootProcess(anim));
            }
        }
    }
    IEnumerator ShootProcess(Animator a)
    {
        if (!cameraShot.isPlaying)
        {
            cameraShot.Play();
        }
        isShooting = true;
        charge -= 0.5f;
        yield return new WaitForSeconds(0.05f);
        flashLight.intensity = 3;
        //screen.material = screenState[1];
        flare.SetActive(true);
        a.SetTrigger("Take");
        RaycastHit hit;
        Physics.Raycast(cam.position, cam.forward, out hit, 5);

        if (hit.point != Vector3.zero)
        {
            FlashDamageRangeCollider.height = Vector3.Distance(cam.position, hit.point) + 0.5f;
            FlashDamageRangeCollider.center = new Vector3(0, 0, FlashDamageRangeCollider.height / 2);
        }

        yield return new WaitForSeconds(0.2f);
        flashLight.intensity = 0;
        yield return new WaitForSeconds(0.1f);
        flashLight.intensity = 1.5F;
        yield return new WaitForSeconds(0.15f);
        flashLight.intensity = 0;
        screen.material = screenState[0];
        flare.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        isShooting = false;
    }
    public bool IsCharged()
    {
        return (charge > 0) ? true : false;
    }
    void OnTriggerStay(Collider col)
    {
        if (col.tag == TagsUtil.ENEMY && isShooting)
        {
            EnemyAIController enemy = col.GetComponent<EnemyAIController>();
            spawEnemy.freirasActived.Remove(col.gameObject);
            spawEnemy.freirasDesactived.Add(col.gameObject);
            enemy.Died();
            freirasDied += 1;
            contFreirasTotal += 1;
            if (freirasDied >= spawEnemy.maxEemyWave)
            {
                GameController.gameController.ShowWarning();
                StartCoroutine(NewWave());
                freirasDied = 0;
            }
        }
    }
    IEnumerator NewWave()
    {
        yield return new WaitForSeconds(5);
        spawEnemy.waveCompleted = true;
    }
}
