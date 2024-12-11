using UnityEngine;

public class PickupSystem : MonoBehaviour
{
    public Transform handPosition;
    private GameObject heldItem = null;

    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (heldItem == null) {
                TryPickupItem();
            } else {
               DropItem();
               TryPickupItem();
            }
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            UseItem();
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            DropItem();
        }
    }

    void TryPickupItem() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 2f)) {
            if (hit.collider.CompareTag("Item")) {
                PickupItem(hit.collider.gameObject);
            }
        }
    }

    void PickupItem(GameObject item) {
        heldItem = item;
        heldItem.transform.SetParent(handPosition);
        heldItem.transform.localPosition = Vector3.zero;
        heldItem.transform.localRotation = Quaternion.identity;
        // heldItem.GetComponent<Collider>().enabled = false;
        Debug.Log("You picked up: " + heldItem.name);
    }

    void UseItem() {
        if (heldItem != null) {
            Debug.Log("You are using: " + heldItem.name);
            Destroy(heldItem);
            heldItem = null;
        } else {
            Debug.Log("You dont have any items");
        }
    }

    void DropItem() {
        if (heldItem != null) {
            Debug.Log("You drop: " + heldItem.name);
            heldItem.GetComponent<Collider>().enabled = true;
            heldItem.transform.position = transform.position;
            heldItem.transform.SetParent(null);
            heldItem = null;
        } else {
            Debug.Log("You dont have any items");
        }
    }
}
