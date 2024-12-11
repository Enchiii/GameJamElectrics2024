using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickupSystem : MonoBehaviour {
    public Transform handPosition;
    public TextMeshProUGUI pickupHintText;
    private GameObject heldItem = null;
    private GameObject highlightedItem = null;

    void Update() {
        HighlightItem();

        if (Input.GetKeyDown(KeyCode.E)) {
            if (highlightedItem != null) {
                if (heldItem == null) {
                    PickupItem(highlightedItem);
                    ClearHint();
                } else {
                    // DropItem();
                    // PickupItem(highlightedItem);
                    // ClearHint();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            UseItem();
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            DropItem();
        }
    }

    void HighlightItem() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 2f)) {
            if (hit.collider.CompareTag("Item")) {
                highlightedItem = hit.collider.gameObject;
                pickupHintText.text = "Press E to pick up " + highlightedItem.name; //dodac item.desciption
                pickupHintText.gameObject.SetActive(true);
                Vector3 screenPosition = Camera.main.WorldToScreenPoint(highlightedItem.transform.position);
                pickupHintText.transform.position = screenPosition + new Vector3(0, 50, 0);
            } else {
                ClearHint();
            }
        } else {
            ClearHint();
        }
    }

    void ClearHint() {
        highlightedItem = null;
        pickupHintText.gameObject.SetActive(false);
    }

    void PickupItem(GameObject item) {
        heldItem = item;
        heldItem.transform.SetParent(handPosition);
        heldItem.transform.localPosition = Vector3.zero;
        heldItem.transform.localRotation = Quaternion.identity;
        heldItem.GetComponent<Collider>().enabled = false;
        Debug.Log("Picked up: " + heldItem.name);
    }

    void UseItem() {
        if (heldItem != null) {
            Debug.Log("Using: " + heldItem.name);
            Destroy(heldItem);
            heldItem = null;
        } else {
            Debug.Log("You are not holding anything to use.");
        }
    }

    void DropItem() {
        if (heldItem != null) {
            Debug.Log("Dropped: " + heldItem.name);
            heldItem.GetComponent<Collider>().enabled = true;
            heldItem.transform.SetParent(null);
            heldItem = null;
        } else {
            Debug.Log("You have nothing to drop.");
        }
    }
}
