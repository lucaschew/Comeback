using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarImplementation : MonoBehaviour
{

    public Healthbar healthbar;
    public Text text;

    public int maxHealth;
    public int currentHealth;
    
    //Change dmg
    public int dmgTaken = 25;

    void Start() {
        //Change & set Max Hp
        maxHealth = 100;
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);

        //Create Numeric Healthbar
        text.text = currentHealth + "";
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {

            //Change amount of damage
            TakeDamage(dmgTaken);

            //In the case health goes below 0
            if (currentHealth > 0) {
                text.text = currentHealth + "";
            } else {
                currentHealth = 0;
                text.text = "" + currentHealth;
            }
        }
    }

    void TakeDamage(int damage) {
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);
    }
}
