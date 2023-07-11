using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilitySystem : MonoBehaviour
{
    [SerializeField] private float maxMana = 100f;

    // Just here to test and keep track of mana
    [SerializeField] private float currentMana;

    [SerializeField] private float manaRegenRate = 2f;

    [SerializeField] private float manaRegenDelay = 1f;

    [SerializeField] private float manaRegenDelayTimer = 0f;

    [SerializeField] private float manaRegenDelayTimerMax = 1f;

    [SerializeField] private float manaRegenDelayTimerReset = 1f;

    [SerializeField] private float cooldown = 1f;

    [SerializeField] private bool cooldownTimer;

    [SerializeField] private Transform castPoint;
}
