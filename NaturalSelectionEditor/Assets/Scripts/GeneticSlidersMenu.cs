using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GeneticSlidersMenu : MonoBehaviour
{

    [SerializeField] Slider fertilityMinSlider, fertilityMaxSlider;
    [SerializeField] Slider speedMinSlider, speedMaxSlider;
    [SerializeField] Slider intelligenceMinSlider, intelligenceMaxSlider;
    [SerializeField] Slider earSizeMinSlider, earSizeMaxSlider;

    [SerializeField] Slider fearMinSlider, fearMaxSlider;
    [SerializeField] Slider hungerMinSlider, hungerMaxSlider;
    [SerializeField] Slider hornyMinSlider, hornyMaxSlider;

    [SerializeField] TextMeshProUGUI fertilityMinText, fertilityMaxText;
    [SerializeField] TextMeshProUGUI speedMinText, speedMaxText;
    [SerializeField] TextMeshProUGUI intelligenceMinText, intelligenceMaxText;
    [SerializeField] TextMeshProUGUI earSizeMinText, earSizeMaxText;

    [SerializeField] TextMeshProUGUI fearMinText, fearMaxText;
    [SerializeField] TextMeshProUGUI hungerMinText, hungerMaxText;
    [SerializeField] TextMeshProUGUI hornyMinText, hornyMaxText;

    float fertilityMin, fertilityMax;
    float speedMin, speedMax;
    float intelligenceMin, intelligenceMax;
    float earSizeMin, earSizeMax;

    float fearMin, fearMax;
    float hungerMin, hungerMax;
    float hornyMin, hornyMax;

    void Start(){
        fertilityMin = PlayerPrefs.GetFloat("fertilityMin", 0.25f);
        fertilityMax = PlayerPrefs.GetFloat("fertilityMax", 0.75f);

        speedMin = PlayerPrefs.GetFloat("speedMin", 0f);
        speedMax = PlayerPrefs.GetFloat("speedMax", 1f);

        intelligenceMin = PlayerPrefs.GetFloat("intelligenceMin", 0f);
        intelligenceMax = PlayerPrefs.GetFloat("intelligenceMax", 1f);

        earSizeMin = PlayerPrefs.GetFloat("earSizeMin", 0f);
        earSizeMax = PlayerPrefs.GetFloat("earSizeMax", 1f);


        fearMin = PlayerPrefs.GetFloat("fearMin", 0f);
        fearMax = PlayerPrefs.GetFloat("fearMax", 1f);

        hungerMin = PlayerPrefs.GetFloat("hungerMin", 0f);
        hungerMax = PlayerPrefs.GetFloat("hungerMax", 1f);

        hornyMin = PlayerPrefs.GetFloat("hornyMin", 0f);
        hornyMax = PlayerPrefs.GetFloat("hornyMax", 1f);
        


        fertilityMinSlider.value = fertilityMin;
        fertilityMaxSlider.value = fertilityMax;

        speedMinSlider.value = speedMin;
        speedMaxSlider.value = speedMax;

        intelligenceMinSlider.value = intelligenceMin;
        intelligenceMaxSlider.value = intelligenceMax;

        earSizeMinSlider.value = earSizeMin;
        earSizeMaxSlider.value = earSizeMax;


        fearMinSlider.value = fearMin;
        fearMaxSlider.value = fearMax;

        hungerMinSlider.value = hungerMin;
        hungerMaxSlider.value = hungerMax;

        hornyMinSlider.value = hornyMin;
        hornyMaxSlider.value = hornyMax;


        UpdateText();
    }

    public void OnSLiderChange(){
        fertilityMin = fertilityMaxSlider.value;
        fertilityMax = fertilityMaxSlider.value;

        speedMin = speedMinSlider.value;
        speedMax = speedMaxSlider.value;

        intelligenceMin = intelligenceMinSlider.value;
        intelligenceMax = intelligenceMaxSlider.value;

        earSizeMin = earSizeMinSlider.value;
        earSizeMax = earSizeMaxSlider.value;

        fearMin = fearMinSlider.value;
        fearMax = fearMaxSlider.value;

        hungerMin = hungerMinSlider.value;
        hungerMax = hungerMaxSlider.value;

        hornyMin = hornyMinSlider.value;
        hornyMax = hornyMaxSlider.value;


        UpdateText();
    }

    void UpdateText() {
        fertilityMinText.text = Mathf.RoundToInt(fertilityMin * 100).ToString() + "%";
        fertilityMaxText.text = Mathf.RoundToInt(fertilityMax * 100).ToString() + "%";

        speedMinText.text = Mathf.RoundToInt(speedMin * 100).ToString() + "%";
        speedMaxText.text = Mathf.RoundToInt(speedMax * 100).ToString() + "%";

        intelligenceMinText.text = Mathf.RoundToInt(intelligenceMin * 100).ToString() + "%";
        intelligenceMaxText.text = Mathf.RoundToInt(intelligenceMax * 100).ToString() + "%";

        earSizeMinText.text = Mathf.RoundToInt(earSizeMin * 100).ToString() + "%";
        earSizeMaxText.text = Mathf.RoundToInt(earSizeMax * 100).ToString() + "%";


        fearMinText.text = Mathf.RoundToInt(fearMin * 100).ToString() + "%";
        fearMaxText.text = Mathf.RoundToInt(fearMax * 100).ToString() + "%";

        hungerMinText.text = Mathf.RoundToInt(hungerMin * 100).ToString() + "%";
        hungerMaxText.text = Mathf.RoundToInt(hungerMax * 100).ToString() + "%";

        hornyMinText.text = Mathf.RoundToInt(hornyMin * 100).ToString() + "%";
        hornyMaxText.text = Mathf.RoundToInt(hornyMax * 100).ToString() + "%";
    }

    public void SaveValues() {

        PlayerPrefs.SetFloat("fertilityMin", fertilityMin);
        PlayerPrefs.SetFloat("fertilityMax", fertilityMax);

        PlayerPrefs.SetFloat("speedMin", speedMin);
        PlayerPrefs.SetFloat("speedMax", speedMax);

        PlayerPrefs.SetFloat("intelligenceMin", intelligenceMin);
        PlayerPrefs.SetFloat("intelligenceMax", intelligenceMax);

        PlayerPrefs.SetFloat("earSizeMin", earSizeMin);
        PlayerPrefs.SetFloat("earSizeMax", earSizeMax);


        PlayerPrefs.SetFloat("fearMin", fearMin);
        PlayerPrefs.SetFloat("fearMax", fearMax);

        PlayerPrefs.SetFloat("hungerMin", hungerMin);
        PlayerPrefs.SetFloat("hungerMax", hungerMax);

        PlayerPrefs.SetFloat("hornyMin", hornyMin);
        PlayerPrefs.SetFloat("hornyMax", hornyMax);
    }
}
