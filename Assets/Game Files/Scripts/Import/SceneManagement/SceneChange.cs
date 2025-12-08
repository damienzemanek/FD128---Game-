using Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;
using Extensions;
using static Extensions.FadeEX;
public class SceneChange : MonoBehaviour
{

    #region Privates
    [SerializeField] FadeSettings fade;
    #endregion

    public void ChangeSceneImmediate(int num)
    {
        SceneManager.LoadScene(num);
    }

    public void ChangeScreenFadeScreenToOpaque(int num)
    {
        StartCoroutine(C_FadeToOpaque(fade, () => ChangeSceneImmediate(num)));
    }


    #region Methods
        
    #endregion

}
