using System;
using UnityEngine;
using UnityEngine.UI;

namespace Settings
{
    public class UrlOpener : MonoBehaviour
    {
        public event Action NoInternetConnection;
    
        [SerializeField] private Button _githubBttn;
        [SerializeField] private Button _linkedinBttn;
        

        private void Awake()
        {
            _githubBttn.onClick.AddListener(OpenGithub);
            _linkedinBttn.onClick.AddListener(OpenLinkedin);
        }

        private void OnDestroy()
        {
            _githubBttn.onClick.RemoveListener(OpenGithub);
            _linkedinBttn.onClick.RemoveListener(OpenLinkedin);
        }

        private void OpenGithub()
        {
            TryOpenURL(GameConstants.GITHUB_URL);
        }

        private void OpenLinkedin()
        {
            TryOpenURL(GameConstants.LINKEDIN_URL);
        }
    
        private void TryOpenURL(string url)
        {
            var connectionIsUnavailable = Application.internetReachability == NetworkReachability.NotReachable;
            if (connectionIsUnavailable)
            {
                NoInternetConnection?.Invoke();
                return;
            }
            Application.OpenURL(url);
        }
    }
}
