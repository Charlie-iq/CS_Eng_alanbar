using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class Player : MonoBehaviour
{  
    bool isPlayerALive = true;
    //Dash
    public float dashDOTweenPower=1f, dashDOTweenDuraction=0.3f;
    public float DashPower = 10.0f;
    public float TorquePower = 5.0f;
    public int DashCount = 0;
    //Refreaces
    public AudioSource DieSFX, dashSFX;
    public Rigidbody2D rb;
    public ParticleSystem  DieVFX,DashVFX;
    public GameObject PlayerVisual;
    //Base Values
    Vector3 baseScales;
    //Camera Shake 
    [Header("Camera Shake")]
    public float dashShakePower = 0.5f;
    [Range(0.05f,50f)]
    public float dashShakeDuration = 0.5f;
    //Gravity Related
    [Header("Gravity")]
    public float sideGravityPower = 1f;
    public float sideGravityAngelUnity = 0.07f;
    public Vector2 sideGravityMinMax=new Vector2(1f,2.5f);
    public GameObject RestartMenu;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        baseScales= transform.localScale;
    }

    private void Update()  
    {
        DashUpdate();
        GravityUpdate();
    }


    void GravityUpdate()
    {
        var _angel = CameraManager._inst.currentCameraAngel;
        var _power = Mathf.Abs(_angel * sideGravityAngelUnity);
        if (_power < sideGravityMinMax.x) _power = 1f;
        if (_power > sideGravityMinMax.y) _power = 2.5f;
        if (-_angel >8)
        {
            rb.velocity += new Vector2(1, 0) * Time.deltaTime * _power * sideGravityPower;
        }
        else if (_angel < -8)
        {
            rb.velocity += new Vector2(-1, 0) * Time.deltaTime * _power * sideGravityPower;
        }
    }
    void DashUpdate()
    {
        if (DashCount < 2 && Input.GetKeyDown(KeyCode.Space))
        {
            Dash();
        }
    }
    void Dash()
    { 
        if (!isPlayerALive) return;
      
        DashCount++;
        float diraction = Input.GetAxisRaw("Horizontal");
        float torque ;
        Instantiate(DashVFX, transform.position - new Vector3(0f,0.5f,0f),Quaternion.identity);
        DashVFX.Play();
       
        Vector2 dashVector = new Vector2();
        if (DashCount== 1)
        {
            dashVector = new Vector2(diraction, 1.2f);
            torque = TorquePower * diraction;
            rb.AddForce(dashVector*DashPower, ForceMode2D.Impulse);
            rb.AddTorque(torque * diraction, ForceMode2D.Impulse);
            transform.localScale = baseScales;
        }
        else
        {
            dashVector = new Vector2(diraction * 1.7f, 0.3f) ;
            rb.AddForce(dashVector * DashPower, ForceMode2D.Impulse);
            rb.AddTorque(TorquePower * diraction, ForceMode2D.Impulse);

        }

        //Feedback
        dashSFX.Play();
        transform.DOShakeScale(dashDOTweenDuraction, dashDOTweenPower).OnComplete(dashDOTweenFinsh);
        CameraShake._inst.Shake(dashShakeDuration, dashShakePower);
    }
    void dashDOTweenFinsh()
    {
        transform.localScale = baseScales;
    }
    private void ResetDashCount()
    {
        DashCount = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ResetDashCount();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // return; //For testing
        if (!isPlayerALive) return;
        PlayerVisual.SetActive(false);
        isPlayerALive = false;
        DieSFX.Play();
        DieVFX.Play();
     
       // rb.bodyType = RigidbodyType2D.Static;
       // Invoke("ParticalDie", 0.1f);
        RestartMenu.SetActive(true);
       // GameManger._inst.Restart();
        CameraShake._inst.Shake(dashShakeDuration * 5, dashShakePower * 5);
    }
}
    
