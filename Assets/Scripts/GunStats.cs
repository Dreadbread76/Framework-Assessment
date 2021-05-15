using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace GunGame.Guns
{
    public class GunStats : MonoBehaviour
    {


        [Header("Gun")] 
        private GameObject gunModel;
        public int weight;
        public string gunName;
      
        
        [Header("Damage")]
        public int headshotMultiplier;
        public int burstSize;
        public int burstDelay;
        public float recoil;
        public float fireRate;
        public float spinTime;
        float currentSpinTime = 0;
        float rechamberTime = 0;
        public float burstTime = 0;
        int burstLeft;

        public List<FireMode> fireModes = new List<FireMode>();

        [Header("Ammo")]
        public GameObject gunBarrel;
        GameObject projectile;
        public float reloadTime;
        public int fullLoadSize;
        public int magSize;
        public int magLeft;
        public int carryAmmoMax;
        public int carryAmmo;

        [Header("Mechanism")]
        public int currentFireMode;

        bool reloading = false;

      
        private void Start()
        {
            
        }
       
       

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R) && !reloading && magLeft < fullLoadSize)
            {
                float reloadLeft = reloadTime;
                reloading = true;

                if (reloadLeft > 0)
                {
                    reloadLeft -= Time.deltaTime;
                }
                else
                {
                    reloading = false;
                    Reload();
                }
            }
        }
        public void Reload()
        {
            int shotsNeeded = magSize - magLeft;

            if(shotsNeeded >= carryAmmo)
            {
               
            }
            else
            {
                carryAmmo -= shotsNeeded;
                magLeft += shotsNeeded;
            }
            
            magLeft = magSize;

        }
      
        public void FireBullet()
        {
            

            while(rechamberTime > 0)
            {
                rechamberTime -= Time.deltaTime;
            }
            if(rechamberTime == 0)
            {
                Instantiate(projectile, gunBarrel.transform.position, gunBarrel.transform.rotation);
                rechamberTime = fireRate;
            }
           
        }
    }

    
}

