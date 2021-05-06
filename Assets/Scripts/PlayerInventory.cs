using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GunGame.Guns;

namespace GunGame.Inventory
{
    public class PlayerInventory : MonoBehaviour
    {
        public int maxCapacity;
        public List<Gun> weapons;
        public Gun currentGun;
        public Camera cam;


        private void Update()
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Gun currentGun = hit.transform.gameObject.GetComponent<Gun>();
                

            }

        }
    }
}

