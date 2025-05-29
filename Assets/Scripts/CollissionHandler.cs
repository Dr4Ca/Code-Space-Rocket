using UnityEngine;

public class CollissionHandler : MonoBehaviour
{

    void OnCollisionEnter(Collision collision)
    {
        
        switch (collision.gameObject.tag)
        {
            case "Respawn":
                Debug.Log("This is fine");
                break;
            case "Finish":
                Debug.Log("You've finished!");
                break;
            case "Testing":
                Debug.Log("Collision Tested");
                break;
            default:
                Debug.Log("You bumped!");
                break;
        }
    }
}
