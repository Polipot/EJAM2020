using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Inventory : MonoBehaviour
{
    GameObject currentlyItem;
    GameObject temp;

    public Transform ParentForItem;
    public Text Dialogue_text;
    public Text Name_text;

    private void FixedUpdate()
    {
        if (currentlyItem != null)
            Name_text.text = currentlyItem.name;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && temp != null)
        {

            Item it = temp.GetComponent<Item>();
            if (it != null)
            {

                if (currentlyItem != null)
                {
                    drop(currentlyItem);
                }

                GetNewItem(temp.gameObject);

                Dialogue_text.text = "";

                temp.gameObject.tag = "Untagged";
                temp = null;
                it = null;
            }
        }
    }

    void GetItemGround(GameObject tp)
    {
        temp = tp;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            Dialogue_text.text = "Press E to take it";

            GetItemGround(other.gameObject);

            Tuto.Instance.HelpWeapon();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            Dialogue_text.text = "";
            GetItemGround(null);
        }
    }

    void drop(GameObject dropIt)
    {
        currentlyItem.tag = "Item";
        currentlyItem.transform.SetParent(null);
        currentlyItem.GetComponent<Item>().DropOrGet(false, null);
        currentlyItem.GetComponent<Item>().EnableCollider(true);
    }

    void GetNewItem(GameObject tt)
    {
        currentlyItem = tt;
        tt.GetComponent<Item>().DropOrGet(true, transform);
        currentlyItem.GetComponent<Item>().EnableCollider(false);
        tt.transform.SetParent(ParentForItem);
        tt.transform.position = ParentForItem.position;
        tt.transform.localEulerAngles = new Vector3(-90,0,0);
    }
}
