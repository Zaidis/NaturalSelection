using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Graph : MonoBehaviour
{
    [SerializeField] Image dotPrefab;
    [SerializeField] Color dotColor;
    public int[] array = new int[60];

    [SerializeField] Image[] dots = new Image[60];

    void Start() {
        for (int i = 0; i < array.Length; i++) {
            dots[i] = Instantiate(dotPrefab);
            dots[i].transform.SetParent(this.transform);
            dots[i].color = dotColor;
        }
    }

    public void GraphValue(int newValue) {
        for (int i = 0; i < array.Length - 1; i++) {
            array[i] = array[i+1];
        }
        array[59] = newValue;

        for (int i = 0; i < array.Length; i++) {
            dots[i].transform.localPosition = new Vector3(i, array[i], 0);
            if (array[i] <= 0)
            {
                dots[i].gameObject.SetActive(false);
            }
            else {
                dots[i].gameObject.SetActive(true);
            }
        }
    }
}
