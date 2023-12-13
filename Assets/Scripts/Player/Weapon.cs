using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Enemy;

namespace Player
{
    public class Weapon : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.tag == "Enemy")
            {
                other.GetComponent<EStateMachine>().ChangeToDie();
            }
        }
    }
    
    
}