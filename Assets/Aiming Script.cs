using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Aim : MonoBehaviour
{
   
    
    //public Collider2D player;
    //public Collider2D item;
    private Camera mainCam;
    private Vector3 mousePos;
    public GameObject bullet;
    public Transform bulletTransform;
    public bool canFire;
    private float timer;
    public float timeBetweenFiring;
    public int magazine;
    public float reloadTimer;
    private int magazineSize;
    public Text magCapasity;
    //public Transform aimTransform;


    private void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        magazineSize = magazine;
        
    }
    // Update is called once per frame
    void Update()
    {
        HandleAiming();
        HandleShooting();
        magCapasity.text = magazineSize.ToString(); //Вывод количества патрон в UI
    }
    // косячная реализация разворота руки на 90 и -90
    //private void HandleAiming()
    //{
    //    Vector3 mousePosition = GetMouseWorldPosition();

    //    Vector3 aimDirection = (mousePosition - aimTransform.position).normalized;
    //    float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
    //    aimTransform.eulerAngles = new Vector3(0, 0, angle);

    //    Vector3 aimlocalScale = Vector3.one;
    //    if (angle > 90 || angle < -90)
    //    {
    //        aimlocalScale.x = -1f;
    //    }
    //    else
    //    {
    //        aimlocalScale.x = +1f;
    //    }
    //    bulletTransform.localScale = aimlocalScale;
    //}
    private void HandleAiming()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotaion = mousePos - transform.position;
        float rotz = Mathf.Atan2(rotaion.y, rotaion.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotz);
        bulletTransform.eulerAngles = new Vector3(0, 0, rotz);

        Vector3 aimlocalScale = Vector3.one;//Не работает
        if (rotz > 90 || rotz < -90)
        {
            aimlocalScale.x = -1f;
        }
        else
        {
            aimlocalScale.x = +1f;
        }
        bulletTransform.localScale = aimlocalScale;
    }

    private void HandleShooting()
    {
        if (Input.GetButtonDown("Fire1") && canFire)
        {
            magazineSize--;
            canFire = false;
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
        }

        if (!canFire && magazineSize > 0)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }
        }

        if (magazineSize <= 0)
        {
            timer += Time.deltaTime;
            if (timer >= reloadTimer)
            {
                magazineSize = magazine;
                timer = 0;
            }
        }
    }

    //private void HandleShooting1()
    //{
    //    RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firepoint)
    //}

    //private static Vector3 GetMouseWorldPosition()
    //{
    //    Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    //    vec.z = 0f;
    //    return vec;
    //}

    //private static Vector3 GetMouseWorldPositionWithZ()
    //{
    //    return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    //}

    //private static Vector3 GetMouseWorldPositionWithZ (Camera worldCamera)
    //{
    //    return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    //}

    //private static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    //{
    //    Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
    //    return worldPosition;
    //}
}
