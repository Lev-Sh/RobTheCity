using UnityEngine;
using UnityEngine.SceneManagement;
public class Button : MonoBehaviour
{
    
    public FollowTarget Point;
    public void LoadLevel(string lvl)
    {
        SceneManager.LoadScene(lvl);
    }
    private void OnMouseDown()
    {
        Point.target = this.transform;
    }
}
