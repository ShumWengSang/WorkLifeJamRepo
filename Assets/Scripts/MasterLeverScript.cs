using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterLeverScript : MonoBehaviour
{
    //put script on master slider

    //fill in list with sub levers
    public List<Slider> subLevers = new List<Slider>();
    
    
    //call on slider update value
    public void OnMasterUpdate(float value)
    {
        foreach(Slider slider in subLevers)
        {
            slider.value = value;
        }
    }
}
