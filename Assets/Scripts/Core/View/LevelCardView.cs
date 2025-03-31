using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCardView : MonoBehaviour
{
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private TMP_Text _TitleText;
    [SerializeField] private TMP_Text _TagsText;
    [SerializeField] private TMP_Text _DurationText;
    [SerializeField] private Image _mainImage;
    [SerializeField] private Button _playSongButton;
    [SerializeField] private Button _loadLevelButton;

    public TMP_Text LevelText { get => _levelText; }
    public TMP_Text TitleText { get => _TitleText; }
    public TMP_Text TagsText { get => _TagsText; }
    public TMP_Text DurationText { get => _DurationText; }
    public Image MainImage { get => _mainImage; }
    public Button PlaySongButton { get => _playSongButton; }
    public Button LoadLevelButton { get => _loadLevelButton; }
}
