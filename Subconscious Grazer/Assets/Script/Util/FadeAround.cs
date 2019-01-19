﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Graphic)), DisallowMultipleComponent]
public class FadeAround : MonoBehaviour {
    [SerializeField, Tooltip("Fade around these two values.")]
    private float minFadeValue, maxFadeValue;

    [SerializeField, Tooltip("How fast to fade this object.")]
    private float fadeSpeed;

    [SerializeField, Tooltip("Start fading at the start?")]
    private bool fadeOnStart = true;

    private bool goingMax;

    private float currProgress;

    public bool IsActive { get; set; }

    private void Awake() {
        IsActive = fadeOnStart;
        goingMax = false;
        currProgress = 0;
    }

    private void OnValidate() {
        // If minFadeValue is set to a higher value than maxFadeValue in the inspector.
        if (maxFadeValue < minFadeValue) {
            // Swap around the values.
            var temp = minFadeValue;
            minFadeValue = maxFadeValue;
            maxFadeValue = temp;
        }

        // If the fade speed is set to negative.
        if (fadeSpeed < 0) {
            // Set it back to positive.
            fadeSpeed = Mathf.Abs(fadeSpeed);
        }
    }

    // Update is called once per frame
    void Update() {

        if (!IsActive) { return; }
        // If the progress is not done yet.
        if (currProgress < 1) {
            // Determines if it should be fading in or fading out.
            var from = goingMax ? maxFadeValue : minFadeValue;
            var to = goingMax ? minFadeValue : maxFadeValue;

            // Get the opacity value and set to the graphic object.
            var temp = GetComponent<Graphic>().color;
            temp.a = Mathf.Lerp(from, to, currProgress);
            GetComponent<Graphic>().color = temp;

            // Update the current fade progress.
            currProgress += Time.deltaTime * fadeSpeed;
        } else {
            // Reset progress and fade the other direction.
            currProgress = 0;
            goingMax = !goingMax;
        }
    }

}
