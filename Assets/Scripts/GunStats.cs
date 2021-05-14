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
        float burstTime = 0;
        int burstLeft;

        public List<bool> fireModes = new List<bool>();

        [Header("Ammo")]
        public GameObject gunBarrel;
        GameObject projectile;
        public float reloadTime;
        public int fullLoadSize;
        public int magSize;
        int magLeft;
        public int carryAmmoMax;
        int carryAmmo;

        [Header("Mechanism")]
        public int currentFireMode;
        public bool fullAuto;
        public bool semiAuto;
        public bool burstFire;
        public bool spinFire;
        

        bool reloading = false;

      
        private void OnEnable()
        {
            
        }
        // Fully Automatic (Fires bullets so long as the button is held)
        public void FullAuto(bool active)
        {
            

            if (fullAuto)
            {
                fireModes.Add(fullAuto);
                
            }
            else
            {
                fireModes.Remove(fullAuto);
            }
            if (active)
            {
                if (Input.GetMouseButton(0))
                {
                    FireBullet();
                }
            }
        }
        // Semi Automatic (1 shot every trigger pull)
        public void SemiAuto(bool semiAuto, bool active)
        {
            if (semiAuto)
            {
                fireModes.Add(semiAuto);
            }
            else
            {
                fireModes.Remove(semiAuto);
            }
            if (active)
            {

                if (Input.GetMouseButtonDown(0))
                {

                    FireBullet();

                }

            }
        }
        // Fires a specified amount of bullets every time the trigger is pulled
        public void BurstFire(bool burstFire, bool active)
        {
            if (burstFire)
            {
                fireModes.Add(burstFire);
            }
            else
            {
                fireModes.Remove(burstFire);
            }
            if (active)
            {
                if(burstTime <= 0)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        burstLeft = burstSize;
                        burstTime = burstDelay;

                        if (burstLeft > 0)
                        {
                            FireBullet();
                            burstLeft--;
                        }
                        

                    }
                }
                
            }
        }
        // Has to spin up before firing 
        public void SpinFire(bool spinFire, bool active)
        {
            if (spinFire)
            {
                fireModes.Add(spinFire);
            }
            else
            {
                fireModes.Remove(spinFire);
            }
            if (active)
            {
                if (Input.GetMouseButton(0))
                {
                    if(currentSpinTime < spinTime)
                    {
                        currentSpinTime += Time.deltaTime;
                    }
                    else
                    {
                        FireBullet();
                    }
                    
                }
            }
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

