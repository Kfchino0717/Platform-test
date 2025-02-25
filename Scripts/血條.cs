using UnityEngine;
using UnityEngine.UI;
public class 血條 : MonoBehaviour
{
    [SerializeField] Gradient 漸層;
    [SerializeField] Image 血條圖像;
    [SerializeField] Slider 血量計;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void 改變漸層色()
    {
        血條圖像.color = 漸層.Evaluate(血量計.normalizedValue);//獲取顏色後改變顏色
    }
}
