using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class GameLevel : MonoBehaviour
{
    [ReadOnly] public DataSaver saver;
    [SerializeField] int level;
    [SerializeField] int hearts;
    [SerializeField] List<Image> heartImages;
    [SerializeField] Sprite fullHeart;


    private void Awake()
    {
        saver = DataSaver.Instance;
    }

    private void Start()
    {
        hearts = saver.gameExpData.levelHearts[level];
        for(int i = 0; i < hearts; i++)
            heartImages[i].sprite = fullHeart;

    }

}
