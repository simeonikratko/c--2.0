using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    public GameObject shootingItem;
    public Transform shootingPoint;
    public bool canShoot = true;
    public Animator animator;
    float currentTime = 0f;
    float startingTime = 0.5f;

    void Start()
    {
        currentTime = startingTime;
    }

    private void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Return) && currentTime <= 0)
        {
            currentTime = startingTime;
            Shoot();
        }
    }

    void Shoot()
    {
        if (!canShoot)
            return;

        GameObject si = Instantiate(shootingItem, shootingPoint);
        si.transform.parent = null;
        animator.SetBool("BowAttack", true);
        Invoke("BowAttackFalse", 0.2f);
    }

    public void BowAttackFalse()
    {
        animator.SetBool("BowAttack", false);
    }
}
