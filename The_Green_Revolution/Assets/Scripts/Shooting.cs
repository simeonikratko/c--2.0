using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    public GameObject shootingItem;
    public Transform shootingPoint;
    public bool canShoot = true;
    public Animator animator;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
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
