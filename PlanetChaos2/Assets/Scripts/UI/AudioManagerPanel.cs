using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 音频管理器面板
/// </summary>
public class AudioManagerPanel : BasePanel
{
    private Slider bgmSlider;

    private Slider soundSlider;

    private void Start()
    {
        bgmSlider = GetControl<Slider>("BGMSlider");
        soundSlider = GetControl<Slider>("SoundSlider");

        bgmSlider.value = MusicMgr.GetInstance().GetBKValue();
        soundSlider.value = MusicMgr.GetInstance().GetSoundValue();
    }

    protected override void OnClick(string btnName)
    {
        base.OnClick(btnName);
        switch (btnName)
        {
            case "CloseButton":
                UIManager.GetInstance().HidePanel("MainMenu/AudioManagerPanel");
                break;
        }
    }

    protected override void OnValueChanged(string objName, float value)
    {
        base.OnValueChanged(objName, value);
        switch (objName)
        {
            case "BGMSlider":
                MusicMgr.GetInstance().ChangeBKValue(value);
                break;

            case "SoundSlider":
                MusicMgr.GetInstance().ChangeSoundValue(value);
                break;
        }
    }
}
