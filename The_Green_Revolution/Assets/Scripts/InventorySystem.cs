using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    #region Fields
    [Header("General Fields")]
    //List of items picked up
    public List<GameObject> items = new List<GameObject>();
    //flag indicates if the inventory is open or not
    public bool isOpen;
    [Header("UI Items Section")]
    //Inventory System Window
    public GameObject ui_Window;
    public Image[] items_images;
    [Header("UI Item Description")]
    public GameObject ui_description_Window;
    public Image description_Image;
    public Text description_Title;
    public Text description_Text;
    #endregion

    private void Update()
    {
        #region Inventory button
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleInventory();
        }
        #endregion
    }

    void ToggleInventory()
    {
        #region Display inventory
        isOpen = !isOpen;
        ui_Window.SetActive(isOpen);
        Update_UI();
        #endregion
    }

    public void PickUp(GameObject item)
    {
        #region Add the item to the items list
        items.Add(item);
        Update_UI();
        #endregion
    }

    void Update_UI()
    {
        #region Refresh the UI elements in the inventory window
        HideAll();
        //For each item in the "items" list show it in the respective slot in the "items_images"
        for (int i = 0; i < items.Count; i++)
        {
            items_images[i].sprite = items[i].GetComponent<SpriteRenderer>().sprite;
            items_images[i].gameObject.SetActive(true);
        }
        #endregion
    }

    void HideAll()
    {
        #region Hide all the items ui images
        foreach (var i in items_images) 
        { 
            i.gameObject.SetActive(false); 
        }
        HideDescription();
        #endregion
    }

    public void ShowDescription(int id)
    {
        #region Show Description window
        //Set the Image
        description_Image.sprite = items_images[id].sprite;
        //Set the Title
        description_Title.text = items[id].name;
        //Set the Description
        description_Text.text = items[id].GetComponent<Item>().descriptionText;
        //Show the elements
        description_Image.gameObject.SetActive(true);
        description_Title.gameObject.SetActive(true);
        description_Text.gameObject.SetActive(true);
        #endregion
    }

    public void HideDescription()
    {
        #region Hide Description window
        description_Image.gameObject.SetActive(false);
        description_Title.gameObject.SetActive(false);
        description_Text.gameObject.SetActive(false);
        #endregion
    }

    public void Consume(int id)
    {
        #region Consume item
        if (items[id].GetComponent<Item>().type == Item.ItemType.Consumables)
        {
            Debug.Log($"CONSUMED {items[id].name}");
            //Invoke the consume custom event
            items[id].GetComponent<Item>().consumeEvent.Invoke();
            //Destroy the item in very tiny time
            Destroy(items[id], 0.1f);
            //Clear the item from the list
            items.RemoveAt(id);
            //Update UI
            Update_UI();
        }
        #endregion
    }
}
