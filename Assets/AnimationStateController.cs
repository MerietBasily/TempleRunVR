using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    float blendZ = 0.0f;
    float blendX = 0.0f;
    public float acceleration = 2.0f;
    public float deceleration =2.0f;
    public float maximumWalkVelocity = 0.5f;
    public float maximumRunVelocity = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool forwardPress = Input.GetKey("w");
        bool leftPress = Input.GetKey("a");
        bool rightPress = Input.GetKey("d");
        bool runPress = Input.GetKey("left shift");

        float currentMaxVelocity = runPress ? maximumRunVelocity : maximumWalkVelocity;
        if(forwardPress && (blendZ < currentMaxVelocity) ){
            blendZ += Time.deltaTime * acceleration;
        }
        if(leftPress && (blendX > -currentMaxVelocity) ){
            blendX -= Time.deltaTime * acceleration;
        }
        if(rightPress && (blendX < currentMaxVelocity) ){
            blendX += Time.deltaTime * acceleration;
        }

        //Deceleration forward
        if(!forwardPress && (blendZ > 0.0f)){
            blendZ -= Time.deltaTime * deceleration;
        }
        if(!forwardPress && (blendZ <0.0f)){
          blendZ = 0.0f;  
        }

        //Deceleration left/right
        if(!leftPress && (blendX < 0.0f)){
            blendX += Time.deltaTime * deceleration;
        }
        if(!rightPress && (blendX > 0.0f)){
            blendX -= Time.deltaTime * deceleration;
        }

        // Set velocity on the x axis to zero
        if(!leftPress && !rightPress && blendX != 0.0f && (blendX > -0.05f && blendX < 0.05f)){
            blendX = 0.0f;
        }

        // lock forward, else decelerate to the maximum walk velocity
        if(forwardPress && runPress && blendZ > currentMaxVelocity){
            blendZ = currentMaxVelocity;
        }else if(forwardPress && blendZ > currentMaxVelocity){
            blendZ -= Time.deltaTime * deceleration;
            if(blendZ > currentMaxVelocity && blendZ < (currentMaxVelocity - 0.05f)){
                blendZ = currentMaxVelocity;
            }
        }else if(forwardPress && blendZ < currentMaxVelocity && blendZ > (currentMaxVelocity - 0.05f)){
            blendZ = currentMaxVelocity;
        }

        // lock left, else decelerate to the maximum walk velocity
        if(leftPress && runPress && blendZ < -currentMaxVelocity){
            blendZ = -currentMaxVelocity;
        }else if(leftPress && blendZ < -currentMaxVelocity){
            blendZ -= Time.deltaTime * deceleration;
            if(blendZ < -currentMaxVelocity && blendZ > (-currentMaxVelocity - 0.05f)){
                blendZ = -currentMaxVelocity;
            }
        }else if(leftPress && blendZ > currentMaxVelocity && blendZ < (-currentMaxVelocity + 0.05f)){
            blendZ = -currentMaxVelocity;
        }

        // lock right, else decelerate to the maximum walk velocity
        if(rightPress && runPress && blendZ > currentMaxVelocity){
            blendZ = currentMaxVelocity;
        }else if(rightPress && blendZ > currentMaxVelocity){
            blendZ -= Time.deltaTime * deceleration;
            if(blendZ > currentMaxVelocity && blendZ < (currentMaxVelocity - 0.05f)){
                blendZ = currentMaxVelocity;
            }
        }else if(rightPress && blendZ < currentMaxVelocity && blendZ > (-currentMaxVelocity - 0.05f)){
            blendZ = currentMaxVelocity;
        }

        animator.SetFloat("BlendZ",blendZ);
        animator.SetFloat("BlendX",blendX);
        
    }
}
