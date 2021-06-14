using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using GunGame.Guns;
using GunGame.Inventory;


namespace GunGame.Guns
{
    public class GunStats : MonoBehaviour
    {
        #region Variables

        [Header("Gun")] 
        private GameObject gunModel;
        public int weight;
        public string gunName;
        public PlayerInventory inv;
        
        [Header("Damage")]
        public int headshotMultiplier;
        public int burstSize;
        public int burstDelay;
        public float recoil;
        public float fireRate;
        public float spinTime;
        float currentSpinTime = 0;
        public bool rechambering = false;
        public float burstTime = 0;
        int burstLeft;

        public List<FireMode> fireModes = new List<FireMode>();

        [Header("Ammo")]
        public GameObject gunBarrel;
        [SerializeField]
        private GameObject projectile;
        public float reloadTime;
        public int fullLoadSize;
        public int shotAmount;
        public int magSize;
        public int currentMag;
        public int carryAmmoMax;
        public int carryAmmo;

        [Header("Mechanism")]
        public int currentFireMode;

        public bool reloading = false;

        #endregion
        #region Update
        private void Update()
        {
            // Change Weapon Mode
            if (Input.GetKeyDown(KeyCode.M))
            {
                UpdateMode();
            }
            // Reload Weapon
            if (Input.GetKeyDown(KeyCode.R) && !reloading && currentMag < fullLoadSize)
            {
                StartCoroutine(Reloading());
            }
        }
        #endregion
        #region Reloading
        public void Reload()
        {
            // if there is enough ammo to fill the magazine
            if(carryAmmo >= magSize)
            {
                // Add bullets and count remainder
                currentMag += magSize;
                carryAmmo -= magSize;
                int remainder =  currentMag % fullLoadSize;
                Debug.Log(remainder);

                // If the remainder is the same as the mag size, simply add the mag size
                if(remainder == magSize)
                {
                    currentMag += magSize;
                    carryAmmo -= magSize;
                }

                // Take away the extra remainder of bullets from the mag and shift it to the reserve ammo
                currentMag -= remainder;
                carryAmmo += remainder;
            }
            //if there isn't enough ammo to fill the magazine
            else
            {
                // Add the remaining ammo
                magSize += carryAmmo;
                carryAmmo = 0;
            }
           

            // Update the Clip and Ammo to match the capacity
            inv.UpdateClip();
            inv.UpdateAmmo();
        }
        #endregion
        #region Fire Mode
        // Get the current fire mode
        public void GunStatsMain()
        {
            fireModes[currentFireMode].FireType(this);
        }
        // Change fire mode
        public void UpdateMode()
        {
            if (currentFireMode < fireModes.Count - 1)
            {
                currentFireMode++;
                inv.UpdateFireMode();
            }
            else
            {
                currentFireMode = 0;
                inv.UpdateFireMode();
            }
        }
        #endregion
        #region Fire Bullet
        //Shoot bullet
        public IEnumerator FireBullet()
        {
            
            if (currentMag > 0)
            {
                Debug.Log("bulletShot");
                rechambering = true;
                Instantiate(projectile, gunBarrel.transform.position, gunBarrel.transform.rotation);
                currentMag -= 1;
                inv.UpdateClip();
                yield return new WaitForSeconds(fireRate);
                rechambering = false;
                yield return null;
            }

        }
        #endregion
        #region ReloadTimer
        IEnumerator Reloading()
        {
            reloading = true;
            yield return new WaitForSeconds(reloadTime);
            reloading = false;
            Reload();
            yield return null;
        }
        #endregion
    }


}

