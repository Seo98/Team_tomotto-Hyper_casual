using UnityEngine;

public class h_Cannonball : MonoBehaviour, h_IItem
{
    public h_BoatController boatController;

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Boat"))
        {
            Debug.Log("��ź ȹ��");
            Get();
        }
    }

    public void Get()
    {
        gameObject.SetActive(false);
        boatController.h_isCannonball = true;
    } 
}
