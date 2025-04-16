using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorHandle : MonoBehaviour
{
    public void OpenDoor()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
