using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GunGame.Guns;

namespace GunGame.Inventory
{
    public class PlayerInventory : MonoBehaviour
    {
        #region Variables
        [Header("Inventory")]
        public int maxCapacity;
        int currentCapacity;
        public List<Gun> weapons;
        public Gun selectedGun;
        public Gun currentGun;
        public int gunIndex = 0;

        [Header("Utility")]
        public Camera cam;
        public float pickupDist;
        public Transform hands;
        [SerializeField]
        public float gunThrowVelocity;

        [Header("UI")]
        public Text capacityText;
        public Text pickupText;
        public Text weaponNameText;
        public Text magAmmoText;
        public Text carryAmmoText;
        public Text fireModeText;

        #endregion
        #region Start
        private void Start()
        {
            UpdateCapacity();
            SetWeaponText();
        }
        #endregion
        #region Update
        private void Update()
        {
            // If there is a gun in hand, get the current fire mode
            if (currentGun != null)
            {
                currentGun.GunStatsMain();
            }
            // Drop Weapon
            if (Input.GetKeyDown(KeyCode.Backspace) && currentGun != null)
            {
                DropWeapon(currentGun);

            }
            // Switch weapon if the player has a weapon
            if(weapons != null)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    SwitchWeapon(false);
                }
            }
           


            RaycastHit hit;
            
            //Shoot out ray
            if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, pickupDist))
            {

                // Get gun script of highlighted object
                Gun selectedGun = hit.transform.gameObject.GetComponent<Gun>();
                
                if(selectedGun != null && pickupText != null)
                {
                    pickupText.gameObject.SetActive(true);


                    // if the weight of the highlighted gun + your current weight exceeds the capacity, show the player that
                    if(selectedGun.weight + currentCapacity > maxCapacity)
                    {
                        pickupText.text = selectedGun.gunName + " is too heavy!\nWeight: " + selectedGun.weight;
                    }
                    else
                    {

                        // Show propmt to pick up the weapon and the weight of the highlighted gun
                        pickupText.text = "Press F to pick up " + selectedGun.gunName + "\nWeight: " + selectedGun.weight;

                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            //Add the weapon to the inventory
                            PickupWeapon(selectedGun);

                        }
                    }
                }
                // if there is no gun, no prompt
                else
                {
                    pickupText.gameObject.SetActive(false);
                }

            }

        }
        #endregion
        #region Switch Weapons
        void SwitchWeapon(bool removingGun)
        {
            // If the gun index is about to exceed the count
            if(gunIndex >= weapons.Count -1)
            {
                if (!removingGun)
                {
                    //Hide gun 1 
                    currentGun.gameObject.SetActive(false);
                }
                DetachCam();

                //Set index to 0
                gunIndex = 0;
                currentGun = weapons[gunIndex];

                //Show gun 2
                currentGun.gameObject.SetActive(true);
                AttachCam();
            }
            // If it isn't
            else
            {
                if (!removingGun)
                {
                    //Hide gun 1 
                    currentGun.gameObject.SetActive(false);
                }
                //Next gun in index
                DetachCam();

                gunIndex++;
                currentGun = weapons[gunIndex];

                //Show gun 2
                currentGun.gameObject.SetActive(true);
                AttachCam();
            }

        }
        #endregion
        #region Pickup Weapon
        void PickupWeapon(Gun weapon)
        {
            // If the player has a gun in hand, put it away
            if (currentGun)
            {
                DetachCam();
                currentGun.gameObject.SetActive(false);
            }
            // Add picked up weapon to inventory
            weapons.Add(weapon);
            

            // Add weight from new weapon
            currentCapacity += weapon.weight;
            UpdateCapacity();

            // Set the current weapon to the new weapon
            currentGun = weapon;
            AttachCam();
            gunIndex = weapons.Count -1;
            currentGun.inv = this;
            SetWeaponText();
            
            // Attach the weapon model to the player's hands
            weapon.transform.position = hands.position;
            weapon.transform.rotation = hands.rotation;
            weapon.transform.SetParent(hands);
            Rigidbody rigi = weapon.GetComponent<Rigidbody>();
            rigi.constraints = RigidbodyConstraints.FreezeAll;
            
            
        }
        #endregion
        #region Drop Weapon
        void DropWeapon(Gun weapon)
        {

            // Reduce players current weight
            currentCapacity -= currentGun.weight;
            UpdateCapacity();

            // Physically drop the weapon
            weapon.transform.SetParent(null);
            Rigidbody rigi = weapon.GetComponent<Rigidbody>();
            rigi.constraints = RigidbodyConstraints.None;

            //Chuck gun forward
            rigi.AddRelativeForce(0,0,currentGun.weight / gunThrowVelocity);
            DetachCam();

            //Change Weapons
            weapons.Remove(currentGun);
            
            if (currentCapacity == 0)
            {
                currentGun = null;
            }
            else
            {
                SwitchWeapon(true);
                currentGun = weapons[gunIndex];
                AttachCam();
            }
            SetWeaponText();
        }
        #endregion
        #region UI
        void SetWeaponText()
        {

            // If there is a weapon in hand, show it's stats
            if (currentGun != null)
            {
                weaponNameText.text = currentGun.gunName;
                magAmmoText.text = currentGun.currentMag.ToString();
                carryAmmoText.text = currentGun.carryAmmo.ToString();
                fireModeText.text = currentGun.fireModes[currentGun.currentFireMode].modeName;
            }
            // If not, hide the appropriate hud elements
            else
            {
                weaponNameText.text = "";
                magAmmoText.text = "";
                carryAmmoText.text = "";
                fireModeText.text = "";
            }
        }

        void UpdateCapacity()
        {
            // Refresh the Weight on the UI
            capacityText.text = "Capacity: " + currentCapacity + "/" + maxCapacity;
           
        }
        
        public void UpdateClip()
        {
            // Refresh the Mag Capacity on the UI
            magAmmoText.text = currentGun.currentMag.ToString();
        }
        public void UpdateAmmo()
        {
            // Refresh the Reserve Ammo Capacity on the UI
            carryAmmoText.text = currentGun.carryAmmo.ToString();
        }
        public void UpdateFireMode()
        {
            // Refresh the Fire Mode on the UI
            fireModeText.text = currentGun.fireModes[currentGun.currentFireMode].modeName;

        }
        #endregion
        public void AttachCam()
        {
            currentGun.cam = cam;
        }
        public void DetachCam()
        {
            currentGun.cam = null;
        }
    }

}

