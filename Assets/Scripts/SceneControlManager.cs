/**
    @file   SceneControlManager.cs
    @date   2018.12.20
    @author 황준성(hns17.tistory.com)
    @brief  Projcet Scene Control
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/**
    @class  SceneControlManager
    @date   2018.12.20
    @author 황준성(hns17.tistory.com)
    @brief  프로젝트의 씬 전환 작업(Fade, unLoad, Load)을 수행한다.
*/
public class SceneControlManager : MonoSingleton<SceneControlManager> {
    //Scene Fade Class
    [SerializeField] private Fade fade = null;

    private int currentSceneIndex = 1;
    private int sceneCount;
    

    private void Awake()
    {
        sceneCount = SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    /**
        @brief  다음 씬으로 전환
*/
    public void NextScene()
    {
        int idx = currentSceneIndex + 1;

        if (idx < sceneCount)
            LoadScene(idx);
    }

    /**
        @brief  이전 씬으로 전환
    */
    public void PrevScene()
    {
        int idx = currentSceneIndex - 1;

        if (idx >= 1)
            LoadScene(idx);
    }

    /**
        @brief  첫번째 씬으로 전환
    */
    public void FirstScene()
    {
        LoadScene(1);
    }

    /**
        @brief  지정된 씬으로 전환
    */
    public void LoadScene(int sceneIdx)
    {
        StartCoroutine(ChangeScene(sceneIdx));
    }

    /**
        @brief  씬 전환 이벤트
        @param  sceneName : 전환하려는 scene Nick Name
        @detail 현재 화면을 로딩 화면으로 Fade후 현재 씬 UnLoad
                UnLoad가 끝나면 Load, 완료 후 로드된 씬 화면으로 Fade
                UnLoad를 기다리지 않고 바로 로딩하거나 UnLoading을 마지막에 하는게 좋을지도...
    */
    public IEnumerator ChangeScene(int sceneIdx)
    {
        //화면 Fade
        yield return fade.FadeIn(2);
        
        //Scene Load & UnLoad
        yield return LoadingScene(sceneIdx);
        
        //화면 Fade
        yield return fade.FadeOut(2);
    }


    /**
        @brief  SceneLoading Event, 씬을 로딩하며 작업 완료를 체크
        @param  sceneName : Loading Scene Nick Name
    */
    IEnumerator LoadingScene(int sceneIdx)
    {
        AsyncOperation ao;
        ao = SceneManager.LoadSceneAsync(sceneIdx, LoadSceneMode.Single);
        ao.allowSceneActivation = false;

        while (!ao.isDone) {
            if (ao.progress == 0.9f)
                ao.allowSceneActivation = true;

            yield return null;
        }

        currentSceneIndex = sceneIdx;
    }

}
