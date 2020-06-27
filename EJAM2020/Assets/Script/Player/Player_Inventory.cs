using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Inventory : MonoBehaviour
{
    GameObject currentlyItem;

    public Transform ParentForItem;

    private void Awake()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Item") && Input.GetAxis("Interaction") > 0)
        {
            Item it =other.GetComponent<Item>();
            if (it != null)
            {

                if (currentlyItem != null)
                {
                    drop(currentlyItem);
                }

                GetNewItem(other.gameObject);

                other.enabled = false;
            }
        }
    }

    void drop(GameObject dropIt)
    {
        currentlyItem.transform.SetParent(null);
        currentlyItem.GetComponent<Item>().DropOrGet(false);
        dropIt.GetComponent<Collider>().enabled = true;
    }

    void GetNewItem(GameObject tt)
    {
        currentlyItem = tt;
        tt.GetComponent<Item>().DropOrGet(true);
        tt.transform.SetParent(ParentForItem);
        tt.transform.position = ParentForItem.position;
    }
}

/*
 * 
 *         Weapons[0].SetActive(true);

 *     Dictionary<string, int> AllItems = new Dictionary<string, int>()
    {
        { "rien", 0 },
        { "Bulle", 1 },
        { "Tete", 2 },
    };
    public GameObject[] Weapons;
 *
 * 
 *         for (int i = 0; i < Weapons.Length; i++)
        {
            Weapons[i].SetActive(false);
        }
        Weapons[AllItems[type]].SetActive(true);
 * */
