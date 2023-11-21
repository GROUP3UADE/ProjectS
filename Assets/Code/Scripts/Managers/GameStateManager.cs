using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    // Hay cosas que no se pausan:
    // player aim tracker
    // player rotation with mouse
    // player attack vfx
    // Parecen ser todos los eventos de mouse que si se siguen actualizando
    
    
    public void InactiveState()
    {
        Time.timeScale = 0f;
    }

    public void ActiveState()
    {
        Time.timeScale = 1f;
    }
}