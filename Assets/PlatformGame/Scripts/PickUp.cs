using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PlatformGame
{
    public class PickUp : MonoBehaviour
    {
        [SerializeField] private int _amount;

        // void OnTriggerEnter2D(Collider2D collision)
        // {
        //     if (collision.tag == "Player")
        //         Destroy(gameObject, 0.2f);
        // }

        public int GetPickUp()
        {
            Destroy(gameObject, 0.2f);

            return _amount;
        }
    }
}