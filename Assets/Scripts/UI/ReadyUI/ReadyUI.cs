using Conf;
using Managers;
using UnityEngine;

namespace UI
{
    public class ReadyUI : MonoBehaviour
    {
  

        private void ReturnObject()
        {
            UIManager.Instance.HideUIPanel("Ready");
        }
    }
}