using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PauseFunction : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public void PauseGame(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
        }
    }
}
