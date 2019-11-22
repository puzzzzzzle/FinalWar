using System.Collections;
using UnityEngine;

namespace artest.AIThirdPerson
{
    public class TestScale : MonoBehaviour
    {
        //实时大小  
        Vector3 RealScale = new Vector3(1f, 1f, 1f);
        //原始大小  
        //float InitialScale = 0;
        //缩放速度  
        //public float ScaleSpeed = 0.1f;
        ////缩放比例  
        //public float MaxScale = 2.5f;
        //public float MinScale = 0.5f;
        public float newScale;

        void Start()
        {
            //获取物体最原始大小  
            //InitialScale = this.transform.localScale.x;
            if (GetComponentInChildren<ParticlesSize>() == null)
            {
                gameObject.AddComponent<ParticlesSize>();
            }
            this.transform.localScale = this.transform.localScale * newScale;
            ParticlesSize.ScaleParticleSystem(newScale);
            Debug.Log("change");
        }

        void Update()
        {
            //if (Input.GetKeyDown(KeyCode.A))
            //{
            //    if (RealScale.x < InitialScale * MaxScale)
            //    {
            //        this.transform.localScale += this.transform.localScale * ScaleSpeed;
            //        ParticlesSize.ScaleParticleSystem(this.transform.localScale.x / InitialScale);
            //    }
            //}

            //if (true)
            //{
            //    if (RealScale.x > InitialScale * MinScale)
            //    {
            //        this.transform.localScale -= this.transform.localScale * ScaleSpeed;
            //        ParticlesSize.ScaleParticleSystem(this.transform.localScale.x / InitialScale);
            //    }
            //}

        }
    }
}
