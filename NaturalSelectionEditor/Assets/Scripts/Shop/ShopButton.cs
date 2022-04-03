using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopButton : MonoBehaviour
{
    public int m_cost;
    public int id;
    public Sprite m_Image;
    public string m_title;
    [TextArea()]
    public string m_Description;
    public bool purchased;

}
