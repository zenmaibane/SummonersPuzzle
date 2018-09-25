using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DamageEffectAnimation : MonoBehaviour
{
    bool up = true;
    float speed = 6f;
    int count = 2;

    void Update()
    {
        if (count >= 2)
        {
            return;
        }
        Color color = this.gameObject.GetComponent<Image>().color;
        if (!up)
        {
            this.gameObject.GetComponent<Image>().color = new Color(color.r, color.g, color.b, color.a - speed * Time.deltaTime);
            if (this.gameObject.GetComponent<Image>().color.a <= 0)
            {
                up = true;
                count++;
            }

        }
        else
        {
            this.gameObject.GetComponent<Image>().color = new Color(color.r, color.g, color.b, color.a + speed * Time.deltaTime);
            if (this.gameObject.GetComponent<Image>().color.a >= 1)
            {
                up = false;
                count++;
            }
        }
    }

    public void ShowDamageEffect()
    {
        Color color = this.gameObject.GetComponent<Image>().color;
        this.gameObject.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 0.8f);
        count = 0;
    }
}
