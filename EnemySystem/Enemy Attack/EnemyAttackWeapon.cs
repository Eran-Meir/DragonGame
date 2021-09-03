using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackWeapon : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        /*
        if (gameObject.transform.root.GetComponent<Enemy>() != null)
        {
            //Enemy enemy = gameObject.transform.root.GetComponent<Enemy>();
            //MalbersAnimations.MDamageable player = other.transform.parent.transform.root.GetComponent<MalbersAnimations.MDamageable>();
            //MalbersAnimations.StatModifier healthModifier = other.transform.parent.transform.root.GetComponent<Stats>();
            if (player != null)
            {
                Debug.Log("Player got hit!!!");
                // player.ReceiveDamage(gameObject.transform.root.transform.LookAt, gameObject.transform.root.parent, 1, false, true, true);
            }
        }*/
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
