using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GunGame.Guns
{
    public class Gun : GunStats
    {
        [SerializeField]
        public string _gunName;


        [Header("Gun Parts")]
        public GameObject _barrel;
        public GameObject _projectile;

        [Header("Stats")]
        public int _damage;
        public float _fireRate;
        public int _magAmmo;
        public int _carryAmmo;

        
        
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
