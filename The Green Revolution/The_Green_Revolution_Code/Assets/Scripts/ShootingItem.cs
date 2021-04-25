using UnityEngine;

public class ShootingItem : MonoBehaviour
{
    public float speed;
    int decayAmount = 25;

    private void Update()
    {
        transform.Translate(transform.right * transform.localScale.x * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            return;

        /*if (collision.tag == "FlyingEnemy")
        {
            FindObjectOfType<BlackCloud>().LoseHealth(decayAmount);
            //GameObject.SetActive = false;
        }*/

        //Trigger the custom action on the other object IF IT EXISTS
        if (collision.GetComponent<ShootingAction>())
            collision.GetComponent<ShootingAction>().Action();
        //Destroy
        Destroy(gameObject);

    }
}