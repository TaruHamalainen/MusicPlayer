using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualizerLeft : MonoBehaviour
{
    [SerializeField] float minHeight = 15.0f;
    [SerializeField] float maxHeight = 420;
    [Range(64,8192)]
    [SerializeField] int visualizerSamples = 64;
    [SerializeField] VisualizerImages[] visualizerImages;
    [SerializeField] float updateSensitivity = 0.5f;
    [SerializeField] Color visualizerColor = Color.gray;
    float[] spectrumData;

    private void Start()
    {
        visualizerImages = GetComponentsInChildren<VisualizerImages>();
    }

    private void Update()
    {
        spectrumData = MusicPlayerController.instance.audioSource.GetSpectrumData(visualizerSamples,0,FFTWindow.Rectangular);

        for(int i = 0; i < visualizerImages.Length; i++)
        {
            Vector2 newSize = visualizerImages[i].GetComponent<RectTransform>().rect.size;
            newSize.y = Mathf.Lerp (newSize.y, minHeight +(spectrumData[i] * (maxHeight -minHeight)* 5.0f),updateSensitivity);
            visualizerImages[i].GetComponent<RectTransform>().sizeDelta = newSize;
            visualizerImages[i].GetComponent<Image>().color = visualizerColor;
        }
    }
}
