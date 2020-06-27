using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Inventory : MonoBehaviour
{
    GameObject currentlyItem;

    public Transform ParentForItem;
    public Text Dialogue_text;
    public Text Name_text;

    private void FixedUpdate()
    {
        if (currentlyItem != null)
            Name_text.text = currentlyItem.name;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            Dialogue_text.text = "Press E to take it";

            if (Input.GetKeyDown(KeyCode.E))
            {
                Item it = other.GetComponent<Item>();
                if (it != null)
                {

                    if (currentlyItem != null)
                    {
                        drop(currentlyItem);
                    }

                    GetNewItem(other.gameObject);

                    other.enabled = false;
                    Dialogue_text.text = "";
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            Dialogue_text.text = "";
        }
    }

    void drop(GameObject dropIt)
    {
        currentlyItem.transform.SetParent(null);
        currentlyItem.GetComponent<Item>().DropOrGet(false, null);
        dropIt.GetComponent<Collider>().enabled = true;
    }

    void GetNewItem(GameObject tt)
    {
        currentlyItem = tt;
        tt.GetComponent<Item>().DropOrGet(true, transform);
        tt.transform.SetParent(ParentForItem);
        tt.transform.position = ParentForItem.position;
        tt.transform.localEulerAngles = new Vector3(0,0,0);
    }
}
