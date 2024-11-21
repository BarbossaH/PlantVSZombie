using System;
using System.Collections;
using CameraController;
using Conf;
using UnityEngine;

namespace Managers
{
    public class LevelManager:SingletonMono<LevelManager>
    {
        //process: get camera controller, and start event of generating zombies, when zombies are cleaned up,start a new wave, repeating the process of control camera and 
        
        MainCamera cameraController;
        Camera mainCamera;
        private bool isActiveWave=false;
        private int waveCount = 0;
        protected override void Init()
        {
            base.Init();
            mainCamera=Camera.main;
        }

        private void Start()
        {
            cameraController = mainCamera.GetComponent<MainCamera>();
            // cameraController.MoveCamera(StartAWave);
            StartGenerateZombies();
        }

        private void StartAWave()
        {
           ZombieManager.Instance.StartInvasion();
        }
        
        private void  StartGenerateZombies()
        {
            StartCoroutine(GenerateZombieRoutine());
        }

        private IEnumerator GenerateZombieRoutine()
        {
            while (waveCount<=1)
            {
                //if there is no zombies, then generate zombies
                if (!isActiveWave)
                {
                    isActiveWave = true;
                    waveCount += 1;
                    //todo:I need to refresh the UI by using notification center.
                    // Debug.Log(UIManager.Instance);
                    UIManager.Instance.ShowUIPanel("Ready");
                    // NotificationCenter.Instance.NotifyObserver(EventTypeEnum.ZombieInvasion);
                    ZombieManager.Instance.GenerateZombies(1);
                    cameraController.MoveCamera(StartAWave);
                }
        
                //if zombies is invading,and they are cleaned up, then start a new invasion/wave
                while (isActiveWave)
                {
                    if (ZombieManager.Instance.IsZombieCleanUp())
                    {
                        isActiveWave = false;
                    }

                    yield return null;
                }
            }
            yield return new WaitForSeconds(1f);
        }
        
    }
}