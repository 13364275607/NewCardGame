using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
    public class TestSliderEventThree : MonoBehaviour
    {
        public Text targetTextObject;
        public Slider targetSliderObject;
        void Update()
        {
            targetTextObject.text = "ÖµÎª£º" + targetSliderObject.value;
            MissionManager.Instance.SliderThreeValue = (int)targetSliderObject.value;
        }
        //public void SliderValue()
        //{
        //    MissionManager.Instance.SliderThreeValue = (int)targetSliderObject.value;
        //}
    }
