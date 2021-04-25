using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerSubmarineHealthbar : MonoBehaviour
{
    [SerializeField] private Image _filler;

    public void SetFill(float value)
    {
        _filler.fillAmount = value;
    }
}
