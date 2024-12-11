using UnityEngine;
using UnityEngine.UI;

public class PickupSystem : MonoBehaviour
{
    public Transform handPosition;
    private GameObject heldItem = null;
    public Text pickupHintText;
    private GameObject highlightedItem = null;

    void Update() {
        HighlightItem();
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
    if (Input.touchCount > 0) {
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began) {
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            if (Physics.Raycast(ray, out RaycastHit hit, 3f)) {
                if (hit.collider.CompareTag("Item")) {
                    PickupItem(hit.collider.gameObject);
                }
            }
        }
    }
}

void HighlightItem() {
    if (Input.touchCount > 0) {
        Touch touch = Input.GetTouch(0);
        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        if (Physics.Raycast(ray, out RaycastHit hit, 3f)) {
            if (hit.collider.CompareTag("Item")) {
                highlightedItem = hit.collider.gameObject;
                pickupHintText.text = "Press E to pick up " + highlightedItem.name; // Add item.description if necessary
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
            Debug.Log("You drop: " + heldItem.name);
            heldItem.GetComponent<Collider>().enabled = true;
            heldItem.transform.position = transform.position + new Vector3(0, 1f, 0);
            heldItem.transform.SetParent(null);
            heldItem = null;
        } else {
            Debug.Log("You dont have any items");
        }
    }
}
