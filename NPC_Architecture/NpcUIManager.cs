using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NpcUIManager : MonoBehaviour
{
    #region Get Other Components
        [SerializeField] NPCEmotionManager npcEmotionManager;   
    #endregion

    #region UI Elements
        [Header("- UI Elements")]
        public GameObject UIPanel;
        public Image EmojiImage;
        public Sprite[] emotionSprites; // List of emotion sprites
        public Image orderIcon;
        public TMP_Text showTimeTxt; // Text to show the waiting time

    #endregion


    void Start()
    {
        UIPanel.SetActive(false);
        npcEmotionManager = GetComponent<NPCEmotionManager>();
    }

    // Start is called before the first frame update
    public void ShowOrderUI(OrderData orderData)
    {
        UIPanel.SetActive(true);
        orderIcon.sprite = orderData.icon;
    }

    // Update is called once per frame
    void Update()
    {
        UIPanel.transform.LookAt(Camera.main.transform);
        switch(npcEmotionManager.currentEmotionState) // switch Image for each emotion state
        {
            case NPCEmotionState.SlightlyImpatient:
                EmojiImage.sprite = emotionSprites[1]; 
            break;
            case NPCEmotionState.Impatient:
                EmojiImage.sprite = emotionSprites[2];
            break;
            case NPCEmotionState.Angry:
                EmojiImage.sprite = emotionSprites[3];
            break;
            case NPCEmotionState.Negative:
                EmojiImage.sprite = emotionSprites[4];
            break;
            default:
                EmojiImage.sprite = emotionSprites[0];
            break;
        }
    }
}
