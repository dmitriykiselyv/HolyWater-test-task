using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
   public class ResetButton : MonoBehaviour
   {
      public event Action ResetClicked;
   
      [SerializeField] private Button _button;

      private void Awake()
      {
         _button.onClick.AddListener(OnResetButtonClicked);
      }

      private void OnDestroy()
      {
         _button.onClick.RemoveListener(OnResetButtonClicked);
      }

      private void OnResetButtonClicked()
      {
         ResetClicked?.Invoke();
      }
   }
}
