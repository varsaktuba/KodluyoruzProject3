using System.Collections;
using System.Collections.Generic;
using UnityEngine;

  public class Checkpoint : MonoBehaviour
    {
        public CheckpointManager checkPointManager;
        [SerializeField] private CheckPointData _checkPointData;
        public bool isMyTurn;
        [SerializeField] private Material[] _checkPointMaterials;

        public HealthManager theHealthMan;

        //public Renderer theRend;

       // public Material cpOff;
        //public Material cpOn;

        void Start()
        {
            theHealthMan = FindObjectOfType<HealthManager>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                theHealthMan.SetSpawnPoint(transform.position);
                if (isMyTurn)
                {
                    isMyTurn = false;
                    _checkPointData.isPassed = true;
                    ChangeColor();

                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                if (_checkPointData.isPassed)
                {
                    checkPointManager.SetLastPassedCheckPoint(_checkPointData.checkPointID);
                }
            }
        }
        public void ResetCheckPoint()
        {
            _checkPointData.isPassed = false;
            ChangeColor();
        }

        private void ChangeColor()
        {
            _checkPointData.checkPointRenderer.material = _checkPointMaterials[(_checkPointData.isPassed ? 1 : 0)];
        }
    }
    /* public void CheckpointOn()
     {
         Checkpoint[] checkpoints = FindObjectsOfType<Checkpoint>();
         foreach (Checkpoint cp in checkpoints)
         {
             cp.CheckpointOff();
         }
         theRend.material = cpOn;
     }

     public void CheckpointOff()
     {
         theRend.material = cpOff;
     }

     private void OnTriggerEnter(Collider other)
     {
         if (other.tag.Equals("Player"))
         {
             theHealthMan.SetSpawnPoint(transform.position);
             CheckpointOn();
         }
     }
     */
    [System.Serializable]
    public class CheckPointData
    {
        public int checkPointID;
        public bool isPassed;
        public Renderer checkPointRenderer;
    }


