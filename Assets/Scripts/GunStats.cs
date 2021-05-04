using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace GunGame.Guns
{
    public class GunStats : MonoBehaviour
    {
        [Header("UI")]
        public Text weaponNameText;
        public Text magAmmoText;
        public Text carryAmmoText;
        public Text fireModeText;
       
        [Header("Gun")]
        public int headshotMultiplier;
        public int currentFireMode;
        public int burstSize;
        public int burstDelay;
        public float recoil;
        public float weight;
        public float fireRate;
        public float spinTime;
        float currentSpinTime = 0;
        float rechamberTime = 0;
        float burstTime = 0;
        int burstLeft;

        public List<bool> fireModes = new List<bool>();

        [Header("Ammo")]
        GameObject gunBarrel;
        GameObject bullet;
        public float reloadTime;
        public int magSize;
        public int magLeft;
        public int carryAmmoMax;
        public int carryAmmo;
        

        bool reloading = false;
        

        public void FullAuto(bool fullAuto, bool active)
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
            if (Input.GetKeyDown(KeyCode.R) && !reloading && magLeft < magSize)
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
            carryAmmo -= magLeft;
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
                Instantiate(bullet, gunBarrel.transform.position, gunBarrel.transform.rotation);
                rechamberTime = fireRate;
            }
           
        }
    }
}

