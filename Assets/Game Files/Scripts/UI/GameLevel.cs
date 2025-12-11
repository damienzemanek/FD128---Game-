using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Extensions;
using static Extensions.EnumerateEX;

public class GameLevel : MonoBehaviour
{
    [ReadOnly] public DataSaver saver;
    [SerializeField] int level;
    [SerializeField] int hearts;
    [SerializeField] List<GameObject> heartImages;
    [SerializeField] Sprite fullHeart;


    private void Awake()
    {
        saver = DataSaver.Instance;
    }

    private void Start()
    {
        heartImages.SetAllActive(false);

        hearts = saver.gameExpData.levelHearts[level];
        for (int i = 0; i < hearts; i++)
            heartImages[i].gameObject.SetActive(true);

    }

}
