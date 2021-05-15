using System.Collections;
using System.Collections.Generic;
using GunGame.Guns;
using UnityEngine;

public class SemiAuto : GunStats
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
 // Semi Automatic (1 shot every trigger pull)
    public void FireMode(bool active)
    {
      
        if (active)
        {

            if (Input.GetMouseButtonDown(0))
            {

                FireBullet();

            }

        }
    }
}
