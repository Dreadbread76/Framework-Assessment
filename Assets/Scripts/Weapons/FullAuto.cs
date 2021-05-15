using System.Collections;
using System.Collections.Generic;
using GunGame.Guns;
using UnityEngine;

namespace GunGame.Guns
{
    [CreateAssetMenu(menuName = "Guns/Fire Mode/FullAuto")]
    public class FullAuto : FireMode
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Fully Automatic (Fires bullets so long as the button is held)
        public override void FireType(GunStats gunStats, int burstSize, float fireRate )
        {
            if (Input.GetMouseButton(0))
            {
                gunStats.FireBullet();
            }

        }


    }
}

