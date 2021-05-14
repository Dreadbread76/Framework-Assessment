using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GunGame.Guns;

namespace GunGame.Inventory
{
    public class PlayerInventory : MonoBehaviour
    {

        [Header("Inventory")]
        public int maxCapacity;
        int currentCapacity;
        public List<Gun> weapons;
        public Gun selectedGun;
        Gun currentGun;

        [Header("Utility")]
        public Camera cam;
        public float pickupDist;
        public Transform hands;

        [Header("UI")]
        public Text capacityText;
        public Text pickupText;
        public Text weaponNameText;
        public Text magAmmoText;
        public Text carryAmmoText;
        public Text fireModeText;


        private void Start()
        {
            UpdateCapacity();
        }


        private void Update()
        {








            RaycastHit hit;
            

            if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, pickupDist))
            {
                Gun selectedGun = hit.transform.gameObject.GetComponent<Gun>();
                
                if(selectedGun != null && pickupText != null)
                {
                    pickupText.gameObject.SetActive(true);

                    if(selectedGun.weight + currentCapacity > maxCapacity)
                    {
                        pickupText.text = selectedGun.gunName + " is too heavy!\nWeight: " + selectedGun.weight;
                    }
                    else
                    {
                        pickupText.text = "Press F to pick up " + selectedGun.gunName + "\nWeight: " + selectedGun.weight;

                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            //Add the weapon to the inventory
                            weapons.Add(selectedGun);
                            currentCapacity += selectedGun.weight;
                            UpdateCapacity();
                            SwitchWeapon(weapons[weapons.Count -1] );
                            Destroy(selectedGun.gameObject);
                            
                        }

                        if (Input.GetKeyDown(KeyCode.Backspace))
                        {
                            currentCapacity -= selectedGun.weight;
                            UpdateCapacity();
                            weapons.Remove(selectedGun);
                        }

                    }
                    


                   

                }
                else
                {
                    pickupText.gameObject.SetActive(false);
                }
                

            }

        }

        void SwitchWeapon(Gun weapon)
        {
            
            if (currentGun != null)
            {
                Destroy(hands.GetChild(0));
            }

            currentGun = weapon;
            
            GameObject switchedGun = Instantiate(weapon.gameObject, hands.position, hands.rotation);
            switchedGun.transform.SetParent(hands);
            Rigidbody rigi = switchedGun.GetComponent<Rigidbody>();
            rigi.constraints = RigidbodyConstraints.FreezeAll;
       
        }

        void DropWeapon()
        {
            
        }
        void UpdateCapacity()
        {
            capacityText.text = "Capacity: " + currentCapacity + "/" + maxCapacity;
           
        }

        void UpdateAmmo()
        {
            
        }

        
    }
}

