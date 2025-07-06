using UnityEngine;

public class h_BoatController : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private float h_boatSpeed = 3f;

    public bool h_isCannonball = false;
    private float h, v;

    private void Update()
    {
        Move();
        UseItem();
    }

    void Move()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
               
        int scaleX = h > 0 ? 1 : -1;
        
        transform.localScale = new Vector3(scaleX, 1, 1);

        var dirX = new Vector3(h, v, 0).normalized;
        transform.position += dirX * h_boatSpeed * Time.deltaTime;
    }

    public void UseItem()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("Æ÷Åº ¹ß»ç");
            h_isCannonball = false;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<IItem>() != null)
        {
            IItem item = other.gameObject.GetComponent<IItem>();
            item.Get();
        }
    }
}
