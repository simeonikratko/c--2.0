using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class Item : MonoBehaviour
{
    #region Fields
    public enum InteractionType { NONE,PickUp,Examine}
    public enum ItemType { Static, Consumables }
    [Header("Attributes")]
    public InteractionType interactType;
    public ItemType type;
    [Header("Examine")]
    public string descriptionText;
    [Header("Custom Events")]
    public UnityEvent customEvent;
    public UnityEvent consumeEvent;
    #endregion

    private void Reset()
    {
        #region Item:Collider & Layer
        GetComponent<Collider2D>().isTrigger = true;
        gameObject.layer = 9;
        #endregion
    }

    public void Interact()
    {
        #region Interaction
        switch (interactType)
        {
            case InteractionType.PickUp:
                //Add the object to the PickedUpItems list
                FindObjectOfType<InventorySystem>().PickUp(gameObject);
                //Disable the object
                gameObject.SetActive(false);
                break;
            case InteractionType.Examine:
                //Call the Examine item in the interaction system
                FindObjectOfType<InteractionSystem>().ExamineItem(this);
                Debug.Log("Examine");
                break;
            default:
                Debug.Log("Null");
                break;
        }

        //Invoke (call) the custom event(s)
        customEvent.Invoke();
        #endregion
    }
}
