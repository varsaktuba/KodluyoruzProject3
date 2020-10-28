//#define LAP
using System.Collections.Generic;
using UnityEngine;



    public class CheckpointManager : MonoBehaviour
    {
    public GameObject youWinText;
    public float resetDelay = 10f;
#if LAP
    private const int TOTAL_LAP = 3;
    private int _lap = 0;
#endif



        [SerializeField] private List<Checkpoint> checkPoints = new List<Checkpoint>();
        private int _lastPassedCheckPoint;

        private void Start()
        {
            for (int i = 0; i < checkPoints.Count; i++)
            {
                checkPoints[i].checkPointManager = this;
                if (i == 0) checkPoints[i].isMyTurn = true;
            }
        }

        public void SetLastPassedCheckPoint(int id)
        {
            _lastPassedCheckPoint = id;

            if (checkPoints.Count - 1 > id)
            {
                checkPoints[id + 1].isMyTurn = true;
            }
            else
            {
#if LAP
            if(_lap < TOTAL_LAP)
            {
                ResetLap();

            }
            else
            {
                EndGame();
            }

           
#else
                EndGame();
#endif
            }
        }




#if LAP
    private void ResetLap()
    {
        _lap++;

        for(int i = 0; i < checkPoints.Count; i++)
        {
            checkPoints[i].ResetCheckPoint();

            if (i == 0)
            {
                checkPoints[i].isMyTurn = true;
            }
            else
            {
                checkPoints[i].isMyTurn = false;
            }
        }
    }

#endif

        private void EndGame()
        {
        // Debug.Log("Level Complete");
         EventManager.TriggerPlayAds(AdsPlacementType.rewardedVideo);
        //var adsManager = GameObject.Find("Managers").GetComponent<Ads.AdsSettings>();
        youWinText.SetActive(true);
        Time.timeScale = .5f;
        Invoke("Reset", resetDelay);
        }
    public void Reset()
    {
        Time.timeScale = 1.0f;
        Application.LoadLevel (Application.loadedLevel);
    }
}
