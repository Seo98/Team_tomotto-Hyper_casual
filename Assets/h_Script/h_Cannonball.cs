using UnityEngine;

public class h_Cannonball : MonoBehaviour, h_IItem
{
    public h_BoatController boatController;

    private void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Boat"))
        {
            Debug.Log("Æ÷Åº È¹µæ");
            Get();
        }
    }

    public void Get()
    {
        gameObject.SetActive(false);
        boatController.h_isCannonball = true;
    } 
}
