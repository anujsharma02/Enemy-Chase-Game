using UnityEngine;
using UnityEngine.UI;

public class ProgressSliderController : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void Awake()
    {
        slider.minValue = 0;
        slider.maxValue = 1;
        slider.value = 0;
    }

    public void UpdateProgress(int current, int total)
    {
        slider.value = (float)current / total;
    }
}
