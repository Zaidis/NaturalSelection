using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ShopController : MonoBehaviour
{
    [SerializeField] GameObject shop;
    public void ToggleMenu(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            shop.SetActive(!shop.activeInHierarchy);
        }
    }
}
