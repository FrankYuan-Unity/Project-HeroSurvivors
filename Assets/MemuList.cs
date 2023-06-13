using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MemuList : MonoBehaviour
{
    public GameObject menuList;

    [SerializeField] private bool menuKeys = true;

    // Update is called once per frame
    void Update()
    {
        if (menuKeys)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Pause();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Return();
        }

    }

    public void Pause()
    { //游戏暂停
        menuList.SetActive(true);
        menuKeys = false;
        // Time.timeScale = 0;
        Debug.Log("暂停游戏");
    }

    public void Return() //返回游戏
    {
        //如果角色死亡，则复活，否则返回游戏
        PlayerControl player = GameObject.Find("player").GetComponent<PlayerControl>();
        Debug.Log("blood -- " + player.blood);
        if (player.blood > 0)
        {  //仍然存活
            menuList.SetActive(false);
            menuKeys = true;
            Time.timeScale = 1;
        }
        else
        {
            Debug.Log("Reward: watch reward video and revive" ) ;
            if (IronSource.Agent.isRewardedVideoAvailable())
            {
                IronSource.Agent.showRewardedVideo();
            }
        }

    }

    public void Revive()//复活
    {
        PlayerControl player = GameObject.Find("player").GetComponent<PlayerControl>();
        player.Revive();
        menuList.SetActive(false);
        menuKeys = true;
        Time.timeScale = 1;
    }

    public void Restart() //重新开始
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void Exit() //退出游戏
    {
        Application.Quit();
    }
}
