using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class LightningCooldownSlider : MonoBehaviour
{
    [SerializeField]
    private ALightningSpell lightning;
    [SerializeField]
    private List<GameObject> visibleChildren;

    private Slider slider;
    private bool activated;

    void Start()
    {
        this.slider = this.GetComponent<Slider>();

        this.slider.minValue = 0;
        this.slider.maxValue = this.lightning.GetMaxCooldown();

        RectTransform r = this.GetComponent<RectTransform>();
        //r.sizeDelta = new Vector2(r.rect.width, Screen.height * .9f);
        //r.anchoredPosition = new Vector2(r.anchoredPosition.x, Screen.height * .45f);

        //(float) Screen.height * .9f

        this.activated = this.lightning.GetCooldownRemaining() == 0;
    }

    void Update()
    {
        this.slider.value = this.lightning.GetCooldownRemaining();
        if (this.activated)
        {
            this.HideChildrenOnDeactivate();
        } else
        {
            this.ShowChildrenOnActivate();
        }
    }

    void HideChildrenOnDeactivate()
    {
        if (slider.value == 0)
        {
            foreach (GameObject g in this.visibleChildren)
            {
                g.SetActive(false);
            }
            this.activated = false;
        }
    }

    void ShowChildrenOnActivate()
    {
        if (slider.value > 0)
        {
            foreach (GameObject g in this.visibleChildren)
            {
                g.SetActive(true);
            }
            this.activated = true;
        }
    }
}
