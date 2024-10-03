using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private List<Character> _fallenCharacters;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Character target))
        {
            target.GetDamage(target.Max_hp);

        }
    }
}
