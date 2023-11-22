using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadManager : MonoBehaviour
{
  public void Retry()
  {
        SceneManager.LoadScene("MapaGero");
  }
}
