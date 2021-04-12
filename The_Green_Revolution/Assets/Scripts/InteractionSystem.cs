using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionSystem : MonoBehaviour
{
    #region Fields
    [Header("Detection Fields")]
    //Detection Point
    public Transform detectionPoint;
    //Detection Radius
    private const float detectionRadius = 0.2f;
    //Detection Layer
    public LayerMask detectionLayer;
    //Catched Trigger Object
    public GameObject detectedObject;
    [Header("Examine Fields")]
    //Examine window object
    public GameObject examineWindow;
    public Image examineImage;
    public Text examineText;
    public bool isExamining;
    #endregion


    void Update()
    {
        #region Checking for detection
        if (DetectObject())
        {
            if (InteractInput())
            {
                detectedObject.GetComponent<Item>().Interact();
            }
        }
        #endregion
    }

    bool InteractInput()
    {
        #region Interact button
        return Input.GetKeyDown(KeyCode.Q);
        #endregion
    }

    bool DetectObject()
    {
        #region Detect Item
        Collider2D obj = Physics2D.OverlapCircle(detectionPoint.position, detectionRadius, detectionLayer);
        if (obj == null)
        {
            detectedObject = null;
            return false;
        }
        else
        {
            detectedObject = obj.gameObject;
            return true;
        }
        #endregion
    }

    private void OnDrawGizmosSelected()
    {
        #region Gizmos
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(detectionPoint.position, detectionRadius);
        #endregion
    }

    public void ExamineItem(Item item)
    {
        #region Examine item
        if (isExamining)
        {
            //Hide the Examine Window
            examineWindow.SetActive(false);
            //disable the boolean
            isExamining = false;
        }
        else
        {
            //Show the item's image in the middle
            examineImage.sprite = item.GetComponent<SpriteRenderer>().sprite;
            //Write description text underneath the image
            examineText.text = item.descriptionText;
            //Display the Examine Window
            examineWindow.SetActive(true);
            //enable the boolean
            isExamining = true;
        }
        #endregion
    }
}
