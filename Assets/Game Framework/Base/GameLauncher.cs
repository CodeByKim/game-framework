using System;
using UnityEngine;

namespace GameFramework
{
    public sealed class GameLauncher : GameFrameworkController
    {
        [SerializeField] private int frameRate = 60;
        [SerializeField] private float gameSpeed = 1f;
        [SerializeField] private bool runInBackground = true;
        [SerializeField] private bool neverSleep = true;
        [SerializeField] private string jsonHelperTypeName = "GameFramework.DefaultJsonHelper";
        private float gameSpeedBeforePause = 1f;

        public int FrameRate
        {
            get { return this.frameRate; }
            set { Application.targetFrameRate = this.frameRate = value; }
        }

        public float GameSpeed
        {
            get { return this.gameSpeed; }
            set { Time.timeScale = this.gameSpeed = value; }
        }

        public bool RunInBackground
        {
            get { return this.runInBackground; }
            set { Application.runInBackground = this.runInBackground = value; }
        }

        public bool NeverSleep
        {
            get { return this.neverSleep; }
            set
            {
                this.neverSleep = value;
                Screen.sleepTimeout = value ? SleepTimeout.NeverSleep : SleepTimeout.SystemSetting;
            }
        }

        public bool IsGamePaused => this.gameSpeed <= 0;

        public bool IsNormalGameSpeed => this.gameSpeed == 0;


        protected override void Awake()
        {
            base.Awake();

            InitializeJsonHelper();

            Application.targetFrameRate = this.frameRate;
            Time.timeScale = this.gameSpeed;
            Application.runInBackground = this.runInBackground;
            Screen.sleepTimeout = this.neverSleep ? SleepTimeout.NeverSleep : SleepTimeout.SystemSetting;
        }

        private void Update()
        {
            ModuleManager.Update(Time.deltaTime, Time.unscaledDeltaTime);
        }

        private void OnDestroy()
        {
            ModuleManager.Shutdown();
        }

        public void PauseGame()
        {
            if(IsGamePaused)
            {
                return;
            }

            this.gameSpeedBeforePause = GameSpeed;
            GameSpeed = 0f;
        }

        public void ResumeGame()
        {
            if(!IsGamePaused)
            {
                return;
            }

            GameSpeed = this.gameSpeedBeforePause;
        }

        public void ResetNormalGameSpeed()
        {
            if(IsNormalGameSpeed)
            {
                return;
            }

            GameSpeed = 1f;
        }

        public void Shutdown()
        {
            Destroy(gameObject);
        }

        private void InitializeJsonHelper()
        {
            if(string.IsNullOrEmpty(this.jsonHelperTypeName))
            {
                this.jsonHelperTypeName = "GameFramework.DefaultJsonHelper";
            }

            //Type jsonHelperType = Utility.Assembly.GetType(this.jsonHelperTypeName);
            Type jsonHelperType = Type.GetType(this.jsonHelperTypeName);
            Utility.Json.IJsonHelper jsonHelper = Activator.CreateInstance(jsonHelperType) as Utility.Json.IJsonHelper;
            Utility.Json.SetJsonHelper(jsonHelper);
        }
    }
}