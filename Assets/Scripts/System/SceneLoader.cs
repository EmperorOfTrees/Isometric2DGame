using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.SceneManagement
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private Image loadingBar;
        [SerializeField] private float fillSpeed = 0.5f;
        [SerializeField] private Canvas loadingCanvas;
        [SerializeField] private Camera loadingCamera;
        [SerializeField] private SceneGroup[] sceneGroups;
        public int currentSceneIndex = 0;


        float targetProgress;

        bool isLoading;

        public readonly SceneGroupManager sceneGroupManager = new SceneGroupManager();


        private void Awake()
        {
            sceneGroupManager.OnSceneLoaded += sceneName => Debug.Log("Loaded: " + sceneName);
            sceneGroupManager.OnSceneUnloaded += sceneName => Debug.Log("Unloaded: " + sceneName);
            sceneGroupManager.OnSceneGroupLoaded += () => Debug.Log("Scene group loaded");
        }
        private async void Start()
        {
            await LoadSceneGroup(0);
        }

        private void Update()
        {
            if (!isLoading) return;


            float currentFillAmount = loadingBar.fillAmount;

            float progressDifference = Mathf.Abs(currentFillAmount - targetProgress);

            float dynamicFillSpeed = progressDifference + fillSpeed;

            loadingBar.fillAmount = Mathf.Lerp(currentFillAmount, targetProgress, Time.deltaTime*dynamicFillSpeed);
        }

        public async Task LoadSceneGroup(int index)
        {
            loadingBar.fillAmount = 0f;
            targetProgress = 1f;


            if(index < 0 || index >= sceneGroups.Length)
            {
                Debug.LogError("Invalid scene group Index:" + index);
                return;
            }

            LoadingProgress progress = new LoadingProgress();
            progress.Progressed += target => targetProgress = Mathf.Max(target, targetProgress);
            EnableLoadingCanvas();
            await sceneGroupManager.LoadScenes(sceneGroups[index], progress);
            EnableLoadingCanvas(false);
            currentSceneIndex = index;
        }

        void EnableLoadingCanvas(bool enable = true)
        {
            isLoading = enable;
            loadingCanvas.gameObject.SetActive(enable);
            loadingCamera.gameObject.SetActive(enable);
        }
    }

    public class LoadingProgress : IProgress<float>
    {
        public event Action<float> Progressed;

        const float ratio = 1f;

        public void Report(float value)
        {
            Progressed?.Invoke(value/ratio);
        }
    }
}