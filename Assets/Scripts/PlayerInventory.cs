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

        [Header("UI")]
        public Text capacityText;
        public Text pickupText;


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
                        pickupText.text = selectedGun.name + " is too heavy!\nWeight: " + selectedGun.weight;
                    }
                    else
                    {
                        pickupText.text = "Press F to pick up " + selectedGun.name + "\nWeight: " + selectedGun.weight;

                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            //Add the weapon to the inventory
                            weapons.Add(selectedGun);
                            currentCapacity += selectedGun.weight;
                            UpdateCapacity();
                            Destroy(selectedGun.gameObject);
                        }

                    }
                    


                   

                }
                else
                {
                    pickupText.gameObject.SetActive(false);
                }
                

            }

        }
        void UpdateCapacity()
        {
            capacityText.text = "Capacity: " + currentCapacity + "/" + maxCapacity;
           
        }

        
    }
}

