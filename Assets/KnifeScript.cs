
using System.Dynamic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeScript : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag != "Player")
        {
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
        }    

        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponentInParent<TakingDamageScript>().TriggerRagdoll(transform.forward * 20,other.contacts[0].point);
            GmScript.KillCount++;
        }
    }
}
