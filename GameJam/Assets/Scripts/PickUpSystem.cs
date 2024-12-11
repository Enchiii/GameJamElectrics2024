<<<<<<< HEAD
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
=======
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickupSystem : MonoBehaviour {
    public Transform handPosition;
    public Text pickupHintText;
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
                    DropItem();
                    PickupItem(highlightedItem);
                    ClearHint();
                }
>>>>>>> d1803a29238b62958430380ababfadef8889cfe1
            }
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            UseItem();
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            DropItem();
        }
    }

<<<<<<< HEAD
    void TryPickupItem() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 2f)) {
            if (hit.collider.CompareTag("Item")) {
                PickupItem(hit.collider.gameObject);
            }
        }
    }

=======
    void HighlightItem() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 3f)) {
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

>>>>>>> d1803a29238b62958430380ababfadef8889cfe1
    void PickupItem(GameObject item) {
        heldItem = item;
        heldItem.transform.SetParent(handPosition);
        heldItem.transform.localPosition = Vector3.zero;
        heldItem.transform.localRotation = Quaternion.identity;
<<<<<<< HEAD
        // heldItem.GetComponent<Collider>().enabled = false;
        Debug.Log("You picked up: " + heldItem.name);
=======
        heldItem.GetComponent<Collider>().enabled = false;
        Debug.Log("Picked up: " + heldItem.name);
>>>>>>> d1803a29238b62958430380ababfadef8889cfe1
    }

    void UseItem() {
        if (heldItem != null) {
<<<<<<< HEAD
            Debug.Log("You are using: " + heldItem.name);
            Destroy(heldItem);
            heldItem = null;
        } else {
            Debug.Log("You dont have any items");
=======
            Debug.Log("Using: " + heldItem.name);
            Destroy(heldItem);
            heldItem = null;
        } else {
            Debug.Log("You are not holding anything to use.");
>>>>>>> d1803a29238b62958430380ababfadef8889cfe1
        }
    }

    void DropItem() {
        if (heldItem != null) {
<<<<<<< HEAD
            Debug.Log("You drop: " + heldItem.name);
            heldItem.GetComponent<Collider>().enabled = true;
            heldItem.transform.position = transform.position;
            heldItem.transform.SetParent(null);
            heldItem = null;
        } else {
            Debug.Log("You dont have any items");
=======
            Debug.Log("Dropped: " + heldItem.name);
            heldItem.GetComponent<Collider>().enabled = true;
            heldItem.transform.SetParent(null);
            heldItem = null;
        } else {
            Debug.Log("You have nothing to drop.");
>>>>>>> d1803a29238b62958430380ababfadef8889cfe1
        }
    }
}
